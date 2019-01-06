using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Environment;
using System.Threading.Tasks;
using UnityEngine.AI;
using Unity.Jobs;
using System;
using Unity.Collections;
//using UnityEngine.Profiling;

/*
 * To do: shorten projectil length. Even though a projectile has a start
 * and an end, the projectile can still extend beyond its endpoint. I must
 * extend it all the way to the mapbounds, then find an appropriate compromise
 * that shortens the extended vector as much as possible to reduce object calculations.
 * One good limiting factor is the camera current position; only let the vector extend
 * a bit beyond the camera bounds
 */
//using System.Linq;
using UnityEngine.Profiling;
public class EnvironmentPhysics : MonoBehaviour {

    private static EnvironmentPhysics instance;

    [SerializeField]
    private LayerMask collisionMask;
    private Vector3 bottomLeftCorner;
    private Vector3 maxDimensions;
    [SerializeField]
    private LayerMask boundsMask;
    [SerializeField]
    private MapBound mapbounds;

    [SerializeField]
    private Grid grid;
    private static readonly int TERRAINDISPARITYCONFIDENCEINTERVAL = 4;

    private static readonly float CACHE_VECTOR_CLOSE_ENOUGH_DISTANCE = .2f;

    private static readonly int PROJECTILE_CAN_PASS_THROUGH_CACHE_MAX_SIZE = 20000;
    private LinkedDictionary<Tuple<Projectile, Vector3, Vector3>, bool> projectileCanPassThroughCache;

    private static readonly int CALCULATE_TERRAIN_DISPARITY_BETWEEN_CACHE_MAX_SIZE = 20000;
    private LinkedDictionary<Tuple<Projectile, Projectile, Vector3, Vector3>, TerrainDisparity> calculateTerrainDisparityBetweenCache;

    private static readonly int CALCULATE_VISIBLE_PORTION_CACHE_MAX_SiZE = 20000;
    private LinkedDictionary<Tuple<Projectile, Vector3, Vector3>, VisiblePortionResult> calculateVisiblePortionCache;

    void Awake() {
        instance = this;
        bottomLeftCorner = mapbounds.GetBottomLeftCorner();
        maxDimensions = mapbounds.GetDimensions();
        projectileCanPassThroughCache =
            new LinkedDictionary<Tuple<Projectile, Vector3, Vector3>, bool>(
                new ProjectileCanPassThroughCacheComparer()
            );
        calculateTerrainDisparityBetweenCache =
            new LinkedDictionary<Tuple<Projectile, Projectile, Vector3, Vector3>, TerrainDisparity>(
                new CalculateTerrainDisparityBetweenComparer()
            );
        calculateVisiblePortionCache =
            new LinkedDictionary<Tuple<Projectile, Vector3, Vector3>, VisiblePortionResult>();
    }

    void OnDrawGizmos() {

    }

    public static void NotifyOfNewObstacle(Obstacle obstacle) {
        /*
         * 
         * 
         * 
         * 
         * Implement, sp that notifying of an obstacle
         * that interesects a cached raycast will invadliate
         * that cached data
         * 
         * 
         * 
         * 
         * 
         */
    }

    public struct IntersectionResult {
        private Vector3 position;
        private Obstacle obstacle;
        public IntersectionResult(RaycastHit info) {
            position = info.point;
            obstacle = info.collider.transform.GetComponent<Obstacle>();
        }
        public IntersectionResult(
            Vector3 location,
            Obstacle obstacle
        ) {
            this.position = location;
            this.obstacle = obstacle;
        }

        public Vector3 GetPosition() {
            return position;
        }
        public Obstacle GetObstacle() {
            return obstacle;
        }
    }

    /*****
     * 
     * CACHE THIS AS WELL
     * 
     */
    private static List<Obstacle> FindNearbyObstacles(Vector3 location, float radius) {
        Collider[] collided = Physics.OverlapSphere(location, radius, instance.collisionMask);
        List<Obstacle> obstacles = new List<Obstacle>();
        for (int i = 0; i < collided.Length; i++) {
            Collider collider = collided[i];

            Obstacle newObstacle = collider.GetComponent<Obstacle>();
            if (newObstacle != null) {
                obstacles.Add(newObstacle);
            }
        }
        return obstacles;
    }



    public static bool LineOfSightToVantagePointExists(int visionSharpness, Vector3 start, Vector3 target) {
        Profiler.BeginSample("Fast");
        bool uninterrupted = true;
        int clarityLeft = visionSharpness;
        ProcessIntersectionFast onIntersect = (result => {
            for (int i = 0; i < result.Count; i++) {
                IntersectionResult r = result[i];
                clarityLeft -= r.GetObstacle().GetTransparency();
            }
            if (clarityLeft <= 0) {
                uninterrupted = false;
            }
        });
        ShouldContinueRayCastFast continueCondition = (result => {
            return clarityLeft > 0;
        });
        IncrementalRaycastFast(start, target, onIntersect, continueCondition);
        Profiler.EndSample();

        Profiler.BeginSample("Regular");

        bool uninterrupted2 = true;
        int clarityLeft2 = visionSharpness;
        ProcessIntersection onIntersect2 = (result => {
            clarityLeft -= result.GetObstacle().GetTransparency();
            if (clarityLeft <= 0){
                uninterrupted = false;
            }
        });
        ShouldContinueRayCast continueCondition2 = (result => {
            return clarityLeft > 0;
        });

        IncrementalRaycast(start, target, onIntersect2, continueCondition2);
        Profiler.EndSample();
        return uninterrupted;
    }

    public static bool LineOfSightToGroundExists(int visionSharpness, Vector3 start, Vector3 target) {
        /* To avoid floating point errors and hitting the terrain,
			raise the height of the target a bit above ground level	
			*/
        return LineOfSightToVantagePointExists(visionSharpness, start, FindGroundedLocation(target.x, target.z) + new Vector3(0, 0.1f, 0));
    }

    public static void SendProjectile(Projectile projectile, Vector3 start, Vector3 target) {
        /*
         * First extend the target point to the bounds
         */
        Vector3 boundsDimensions = instance.mapbounds.GetDimensions();
        float largestDimension = Mathf.Max(
            boundsDimensions.x,
            boundsDimensions.y,
            boundsDimensions.z
        );
        Vector3 direction = target - start;
        Ray ray = new Ray(start, direction);
        // reverse the ray
        ray.origin = ray.GetPoint(largestDimension);
        ray.direction = -ray.direction;

        RaycastHit info;
        Physics.Raycast(ray, out info, instance.boundsMask);

        target = info.point;
        //Debug.DrawLine(start, target, color: Color.white, duration: .2f);

        Vector3 lastImpact = target;
        ProcessIntersection onIntersect = (result => {
            result.GetObstacle().HitBy(projectile);

            Vector3 location = result.GetPosition();
            float power = projectile.GetStrength();
            float suppressiveRadius = projectile.GetSuppressiveRadius();
            lastImpact = location;
            EnvironmentInteractions.ProjectileInstantImpactEffect(location, suppressiveRadius, power);


            projectile.SlowedBy(result.GetObstacle());
        });
        ShouldContinueRayCast continueCondition = (result => {
            return projectile.IsStillActive();
        });
        IncrementalRaycast(start, target, onIntersect, continueCondition);
        projectile.ResetStrength();
        Debug.DrawLine(start, lastImpact, color: Color.white, duration: .2f);
    }

    public static TerrainDisparity CalculateTerrainDisparityBetween(
        Projectile heuristicOfObserver,
        Projectile heuristicOfTarget,
        Vector3 observerVantage,
        Vector3 targetVantage
    ) {
        Tuple<Projectile, Projectile, Vector3, Vector3> data =
            new Tuple<Projectile, Projectile, Vector3, Vector3>(
                heuristicOfObserver,
                heuristicOfTarget,
                observerVantage,
                targetVantage
            );

        if (instance.calculateTerrainDisparityBetweenCache.ContainsKey(data)) {
            return instance.calculateTerrainDisparityBetweenCache[data];
        }

        TerrainDisparity result = new TerrainDisparity();
        VisiblePortionResult observerResult =
            CalculateVisiblePortion(heuristicOfObserver, observerVantage, targetVantage);
        VisiblePortionResult targetResult =
            CalculateVisiblePortion(heuristicOfTarget, targetVantage, observerVantage);

        result.visibleToObserver = observerResult.visible;
        result.targetHeight = observerResult.heightOfTarget;
        result.visibleToTarget = targetResult.visible;
        result.observerHeight = targetResult.heightOfTarget;

        instance.calculateTerrainDisparityBetweenCache[data] =
            result;
        if (instance.calculateTerrainDisparityBetweenCache.Count
            > CALCULATE_TERRAIN_DISPARITY_BETWEEN_CACHE_MAX_SIZE) {
            instance.calculateTerrainDisparityBetweenCache.PopFirst();
        }
        return result;
    }

    public struct VisiblePortionResult {
        public float visible;
        public float heightOfTarget;
    }

    private static VisiblePortionResult CalculateVisiblePortion(Projectile heuristic,
                                                  Vector3 observerVantage,
                                                  Vector3 targetVantage) {

        var data = new Tuple<Projectile, Vector3, Vector3>(
            heuristic,
            observerVantage,
            targetVantage
        );
        if (instance.calculateVisiblePortionCache.ContainsKey(data)){
            return instance.calculateVisiblePortionCache[data];
        }

        float targetHeightAboveGround = targetVantage.y - FindHeightAt(targetVantage.x, targetVantage.z);
        float heightAdjustment = targetHeightAboveGround / 2;
        Vector3 rayTarget = targetVantage - new Vector3(0, heightAdjustment, 0);
        bool canPassThrough = false;
        for (int i = 0; i < TERRAINDISPARITYCONFIDENCEINTERVAL; i++) {
            heightAdjustment /= 2;
            heuristic.ResetStrength();
            canPassThrough = ProjectileCanPassThrough(heuristic, observerVantage, rayTarget);
            if (canPassThrough) {
                rayTarget.y -= heightAdjustment;
            } else {
                rayTarget.y += heightAdjustment;
            }
        }
        //Debug.DrawLine(observerVantage, rayTarget);
        VisiblePortionResult result = new VisiblePortionResult();
        result.visible = targetVantage.y - rayTarget.y;
        result.heightOfTarget = targetHeightAboveGround;

        instance.calculateVisiblePortionCache[data] = result;
        if(instance.calculateVisiblePortionCache.Count > CALCULATE_VISIBLE_PORTION_CACHE_MAX_SiZE){
            instance.calculateVisiblePortionCache.PopFirst();
        }
        return result;
    }


    public static bool ProjectileCanPassThrough(Projectile projectile, Vector3 start, Vector3 target) {
        var data = new Tuple<Projectile, Vector3, Vector3>(projectile, start, target);
        if (instance.projectileCanPassThroughCache.ContainsKey(data)) {
            return instance.projectileCanPassThroughCache[data];
        }

        ProcessIntersection onIntersect = (result => {
            projectile.SlowedBy(result.GetObstacle());
        });
        ShouldContinueRayCast continueCondition = (result => {
            return projectile.IsStillActive();
        });
        IncrementalRaycast(start, target, onIntersect, continueCondition);
        bool passedThrough = projectile.IsStillActive();
        projectile.ResetStrength();

        instance.projectileCanPassThroughCache[data] = passedThrough;
        if (instance.projectileCanPassThroughCache.Count > PROJECTILE_CAN_PASS_THROUGH_CACHE_MAX_SIZE) {
            instance.projectileCanPassThroughCache.PopFirst();
        }

        return passedThrough;
        // ProcessIntersectionFast onIntersect = (result => {
        //    for(int i = 0; i <  result.Count; i++) {
        //         var r = result[i];
        //         projectile.SlowedBy(r.GetObstacle());
        //    }
        //});
        //ShouldContinueRayCastFast continueCondition = (result => {
        //    return projectile.IsStillActive();
        //});
        //IncrementalRaycastFast (start, target, onIntersect,continueCondition);
        //bool passedThrough = projectile.IsStillActive ();
        //projectile.ResetStrength ();
        //return passedThrough;

    }

    public static float FindHeightAt(float x, float z) {
        float height = 0;
        ProcessIntersection onIntersect = (result => {
            height = result.GetPosition().y;
        });
        ShouldContinueRayCast continueCondition = (result => {
            return result.GetObstacle().CanPhaseThrough();
        });
        IncrementalRaycast(
            new Vector3(x, instance.bottomLeftCorner.y + instance.maxDimensions.y, z),
            new Vector3(x, instance.bottomLeftCorner.y, z),
            onIntersect,
            continueCondition
        );
        return height;
    }


    public static float FindWalkableHeightAt(float x, float z) {
        return FindHeightAt(x, z) + 0.01f;
    }

    public static bool WalkableAt(float x, float z, float radius) {
        float height = FindHeightAt(x, z);
        List<Obstacle> obstacles = FindNearbyObstacles(new Vector3(x, height, z), radius);
        for (int i = 0; i < obstacles.Count; i++) {
            if (!obstacles[i].IsWalkable()) {
                return false;
            }
        }
        return true;
    }

    public static Vector3 FindGroundedLocation(float x, float z) {
        return new Vector3(x, FindHeightAt(x, z), z);
    }

    public static Vector3 ScreenToEnvironmentPoint(GameplayCamera cam, Vector3 screenPoint) {
        Vector3 environmentPoint = new Vector3();
        Camera camera = cam.GetCamera();
        Ray rayFromMouse = camera.ScreenPointToRay(screenPoint);
        Vector3 origin = rayFromMouse.origin;
        Vector3 direction = rayFromMouse.direction;
        Vector3 end = origin + (direction * cam.GetMaxDistanceFromFocus() * 2);

        environmentPoint = origin;
        ProcessIntersection onIntersect = (result) => {
            environmentPoint = result.GetPosition();
        };
        ShouldContinueRayCast shouldContinue = (result) => {
            return false;
        };
        IncrementalRaycast(origin, end, onIntersect, shouldContinue);
        return environmentPoint;
    }

    public static MapNode CreateMapNodeAt(Vector2 location) {
        float x = location.x;
        float z = location.y;
        float height = 0;
        bool heightSet = false;
        bool walkable = true;
        float speedModifier = 1;
        List<IntersectionResult> layers =
            new List<IntersectionResult>();
        ProcessIntersection onIntersect = (result => {
            if (!heightSet && !result.GetObstacle().CanPhaseThrough()) {
                height = result.GetPosition().y;
                heightSet = true;
            }
            if (!result.GetObstacle().IsWalkable()) {
                walkable = false;
            }
            layers.Add(new IntersectionResult(
                result.GetPosition(),
                result.GetObstacle()
            ));
            speedModifier *= result.GetObstacle().GetSpeedModifier();
        });
        ShouldContinueRayCast continueCondition = (result => {
            return true;
        });
        IncrementalRaycast(
            new Vector3(x, instance.bottomLeftCorner.y + instance.maxDimensions.y, z),
            new Vector3(x, instance.bottomLeftCorner.y, z),
            onIntersect,
            continueCondition
        );

        MapNode newMapNode = Pools.MapNode;
        newMapNode.Set(
            new Vector3(location.x, height, location.y),
            height,
            layers,
            walkable
        );

        return newMapNode;
    }

    public static List<MapNode> CreateMapNodesAt(List<Vector2> locations) {

        List<Tuple<Vector2, ParallelIncrementalRaycastResult>> results =
            new List<Tuple<Vector2, ParallelIncrementalRaycastResult>>();
        for (int i = 0; i < locations.Count; i++) {
            results.Add(
                new Tuple<Vector2, ParallelIncrementalRaycastResult>(
                    locations[i],
                    new ParallelIncrementalRaycastResult()
                )
            );
        }
        List<ParallelIncrementalRaycastData> raycastData =
            new List<ParallelIncrementalRaycastData>();

        for (int i = 0; i < results.Count; i++) {
            var tuple = results[i];

            ParallelIncrementalRaycastResult result = tuple.Item2;
            Vector2 location = tuple.Item1;
            float x = location.x;
            float z = location.y;
            result.walkable = true;
            result.heightSet = false;

            ProcessIntersection onIntersect = (r => {
                if (!result.heightSet && !r.GetObstacle().CanPhaseThrough()) {
                    result.height = r.GetPosition().y;
                    result.heightSet = true;
                }
                if (!r.GetObstacle().IsWalkable()) {
                    result.walkable = false;
                }
                result.layers.Add(
                    new IntersectionResult(
                        r.GetPosition(),
                        r.GetObstacle()
                    )
                );

                result.speedModifier *= r.GetObstacle().GetSpeedModifier();
            });
            ShouldContinueRayCast continueCondition = (r => {
                return true;
            });

            raycastData.Add(
                new ParallelIncrementalRaycastData(
                    new Vector3(x, instance.bottomLeftCorner.y + instance.maxDimensions.y, z),
                    new Vector3(x, instance.bottomLeftCorner.y, z),
                    onIntersect,
                    continueCondition
                )
            );
        }

        ParallelIncrementalRaycast(raycastData);

        List<MapNode> mapNodes = new List<MapNode>();
        for (int i = 0; i < results.Count; i++) {
            var tuple = results[i];
            ParallelIncrementalRaycastResult result =
                tuple.Item2;
            Vector2 location = tuple.Item1;
            MapNode newMapNode = Pools.MapNode;
            newMapNode.Set(
                new Vector3(location.x, result.height, location.y),
                result.height,
                result.layers,
                result.walkable
            );

            mapNodes.Add(
                newMapNode
            );
        }

        return mapNodes;
    }

    private delegate void ProcessIntersection(IntersectionResult result);
    private delegate bool ShouldContinueRayCast(IntersectionResult result);

    /*
     * Might cause memory leaks if interrupted before NativeArrays are disposed   
    */
    private static void ParallelIncrementalRaycast(
        List<ParallelIncrementalRaycastData> paths
    ) {
        List<ParallelIncrementalRaycastData> remainingPaths =
            new List<ParallelIncrementalRaycastData>(paths);

        NativeArray<RaycastHit> results = default(NativeArray<RaycastHit>);
        NativeArray<RaycastCommand> commands = default(NativeArray<RaycastCommand>);
        do {

            results = new NativeArray<RaycastHit>(remainingPaths.Count, Allocator.TempJob);
            commands = new NativeArray<RaycastCommand>(remainingPaths.Count, Allocator.TempJob);

            for (int i = 0; i < remainingPaths.Count; i++) {
                commands[i] = new RaycastCommand(
                    remainingPaths[i].start,
                    remainingPaths[i].dx,
                    remainingPaths[i].remaining.magnitude,
                    instance.collisionMask
                );
            }

            JobHandle handle = RaycastCommand.ScheduleBatch(commands, results, 1, default(JobHandle));
            handle.Complete();

            for (int i = 0; i < remainingPaths.Count; i++) {
                ParallelIncrementalRaycastData data = remainingPaths[i];
                RaycastHit batchedHit = results[i];
                data.hit = batchedHit;
                if (batchedHit.collider == null) {
                    continue;
                }


                data.remaining -= (batchedHit.point - data.start);
                data.start = (batchedHit.point + data.dx);


                if (batchedHit.collider.transform.GetComponent<Obstacle>() != null) {
                    IntersectionResult result = new IntersectionResult(batchedHit);
                    data.onIntersect(result);
                }
            }

            remainingPaths.RemoveAll(
                path => !path.continueCondition(path.result) || path.hit.collider == null
            );
            results.Dispose();
            commands.Dispose();
        } while (remainingPaths.Count > 0);

    }


    private class ParallelIncrementalRaycastData {
        public Vector3 start;
        public readonly Vector3 end;
        public readonly ProcessIntersection onIntersect;
        public readonly ShouldContinueRayCast continueCondition;
        public readonly Vector3 ray;
        public readonly Vector3 dx;
        public Vector3 remaining;
        public IntersectionResult result;
        public RaycastHit hit;

        public ParallelIncrementalRaycastData(
            Vector3 start,
            Vector3 end,
            ProcessIntersection onIntersect,
            ShouldContinueRayCast continueCondition
        ) {
            this.start = start;
            this.end = end;
            this.onIntersect = onIntersect;
            this.continueCondition = continueCondition;
            ray = end - start;
            remaining = end - start;
            dx = ray.normalized;
        }
    }

    private class ParallelIncrementalRaycastResult {
        public float height;
        public bool walkable;
        public float speedModifier;
        public List<IntersectionResult> layers;

        public RaycastHit hit;

        public bool heightSet;

        public ParallelIncrementalRaycastResult() {
            heightSet = false;
            layers = new List<IntersectionResult>();
        }
    }

    private static void test()
    {
        String name = nameof(LineOfSightToVantagePointExists);
    }

    private static void IncrementalRaycast(
        Vector3 start,
        Vector3 end,
        ProcessIntersection onIntersect,
        ShouldContinueRayCast shouldContinue
    ) {
        RaycastHit hitInfo;
        Vector3 ray = end - start;
        Vector3 dx = ray.normalized;
        Vector3 remaining = end - start;
        dx.Scale(new Vector3(.01f, .01f, .01f));
        while (Physics.Raycast(start, dx, out hitInfo, remaining.magnitude, instance.collisionMask)) {

            remaining -= (hitInfo.point - start);
            start = (hitInfo.point + dx);

            //hit object must have an obstacle script
            if (hitInfo.collider.transform.GetComponent<Obstacle>() != null) {
                IntersectionResult result = new IntersectionResult(hitInfo);
                onIntersect(result);
                if (!shouldContinue(result)) {
                    break;
                }
            }
        }
    }

    private delegate void ProcessIntersectionFast(List<IntersectionResult> result);
    private delegate bool ShouldContinueRayCastFast(List<IntersectionResult> result);
    /*
     *  Cannot be used for vertical raycasts!
     * 
     * Also attempted to parallelize a portion of the
     * code,which WILL ACTUALLY RESULT IN SLOWDOWN
     * in the case that the enemy cannot see the humanoid
     */
    private static void IncrementalRaycastFast(
        Vector3 start,
        Vector3 end,
        ProcessIntersectionFast onIntersect,
        ShouldContinueRayCastFast shouldContinue
    ) {

        Vector2 start2D = start.To2D();
        Vector2 end2D = end.To2D();

        float endpointDistance2D =
            Vector2.Distance(start2D, end2D);

        float endpointHeight2D =
            Math.Abs(end.y - start.y);

        Vector3 higher = new Vector3();
        Vector2 higher2D = higher.To2D();
        Vector3 lower = new Vector3();
        if (start.y > end.y) {
            higher = start;
            lower = end;
        }
        else {
            higher = end;
            lower = start;
        }

        List<MapNode> nodesInTheWay =
            instance.grid.GetMapNodesBetween(start2D, end2D);
        /*
        nodesInTheWay.Sort(
            (x, y) => {
                float xDistance = Vector2.Distance(
                    x.GetLocation().To2D(),
                    start2D
                );
                float yDistance = Vector2.Distance(
                    y.GetLocation().To2D(),
                    start2D
                );
                return xDistance.CompareTo(yDistance);
            }
        );
        */
        List<FindTallEnoughDepthCubeJob> jobs = Pools.ListFindTallEnoughDepthCubeJob;
        for(int i = 0; i < nodesInTheWay.Count; i++){
            MapNode node = nodesInTheWay[i];
            var layers = node.GetLayers();

            FindTallEnoughDepthCubeJob job = Pools.FindTallEnoughDepthCubeJob;
            job.endpointDistance2D = endpointDistance2D;
            job.endpointHeight2D = endpointHeight2D;
            job.position = node.GetLocation();
            job.higher = higher;
            job.layers = new NativeArray<float>(layers.Count, Allocator.TempJob);
            job.tallEnough = new NativeArray<int>(layers.Count, Allocator.TempJob);
            for(int j = 0; j < layers.Count; j++){
                job.layers[j] = layers[j].GetPosition().y;
                job.tallEnough[j] = 0;
            }
            jobs.Add(job);
        }

        List<JobHandle> jobHandles = new List<JobHandle>();
        for(int i = 0; i < jobs.Count; i++){
            jobHandles.Add(jobs[i].Schedule());
        }
        for (int i = 0; i < jobs.Count; i++){
            jobHandles[i].Complete();
        }

        List<IntersectionResult> results = Pools.ListIntersectionResults;
        for (int i = 0; i < jobs.Count; i++) {
            FindTallEnoughDepthCubeJob job = jobs[i];
            NativeArray<int> tallEnough = job.tallEnough;
            MapNode node = nodesInTheWay[i];
            var layers = node.GetLayers();

            for (int j = 0; j < tallEnough.Length; j++)
            {
                if (tallEnough[j] == 1)
                {
                    results.Add(layers[j]);
                }
            }
            onIntersect(results);
            if (!shouldContinue(results))
            {
                break;
            }
            results.Clear();
        }
        for (int i = 0; i < jobs.Count; i++){
            Pools.FindTallEnoughDepthCubeJob = jobs[i];
        }
        Pools.ListIntersectionResults = results;
        Pools.ListMapNodes = nodesInTheWay;
        Pools.ListFindTallEnoughDepthCubeJob = jobs;
        Pools.ListJobHandles = jobHandles;
    }

    private static float FindHeightAtLineSegment(
        Vector2 lower,
        Vector2 higher,
        float distanceFromHigher
    ) {
        float y2 = (higher - lower).y;
        float x2 = Math.Abs((higher - lower).x);
        float x = x2 - distanceFromHigher;
        return ((x * y2) / x2) + lower.y;
    }

    private class ProjectileCanPassThroughCacheComparer : IEqualityComparer<Tuple<Projectile, Vector3, Vector3>> {
        public bool Equals(
            Tuple<Projectile, Vector3, Vector3> x,
            Tuple<Projectile, Vector3, Vector3> y
        ) {
            return
                Vector3.Distance(x.Item2, y.Item2) < CACHE_VECTOR_CLOSE_ENOUGH_DISTANCE &&
                Vector3.Distance(x.Item3, y.Item3) < CACHE_VECTOR_CLOSE_ENOUGH_DISTANCE &&
                x.Item1.Equals(y.Item1);
        }

        public int GetHashCode(
            Tuple<Projectile, Vector3, Vector3> obj
        ) {
            int hash = 17;
            // Suitable nullity checks etc, of course :)
            hash = hash * 23 + obj.Item1.GetHashCode();
            hash = hash * 23 + obj.Item2.GetHashCode();
            hash = hash * 23 + obj.Item3.GetHashCode();
            return hash;
        }

    }

    private class CalculateTerrainDisparityBetweenComparer : IEqualityComparer<Tuple<Projectile, Projectile, Vector3, Vector3>>
    {

        public bool Equals(Tuple<Projectile, Projectile, Vector3, Vector3> x, Tuple<Projectile, Projectile, Vector3, Vector3> y)
        {
            return
                Vector3.Distance(x.Item4, y.Item4) < CACHE_VECTOR_CLOSE_ENOUGH_DISTANCE &&
                Vector3.Distance(x.Item3, y.Item3) < CACHE_VECTOR_CLOSE_ENOUGH_DISTANCE &&
                x.Item1.Equals(y.Item1) &&
                x.Item2.Equals(y.Item2);
        }

        public int GetHashCode(Tuple<Projectile, Projectile, Vector3, Vector3> obj)
        {
            int hash = 17;
            // Suitable nullity checks etc, of course :)
            hash = hash * 23 + obj.Item1.GetHashCode();
            hash = hash * 23 + obj.Item2.GetHashCode();
            hash = hash * 23 + obj.Item3.GetHashCode();
            hash = hash * 23 + obj.Item4.GetHashCode();
            return hash;
        }
    }

    public struct FindTallEnoughDepthCubeJob : IJob{


        public NativeArray<float> layers;
        public NativeArray<int> tallEnough;
        public Vector3 position;

        public Vector3 higher;
        public float endpointDistance2D;
        public float endpointHeight2D;

        public void Execute(){
            for (int i = 0; i < layers.Length; i++){
                float height = layers[i];

                float distanceFromHigher =
                        Vector2.Distance(
                            position.To2D(), higher.To2D()
                        );
                float heightAtLS = FindHeightAtLineSegment(
                    new Vector2(0, 0),
                    new Vector2(
                        endpointDistance2D,
                        endpointHeight2D
                    ),
                    distanceFromHigher
                );
                if (position.y > heightAtLS){
                    tallEnough[i] = 1;
                }
            }
        }
    }
}

