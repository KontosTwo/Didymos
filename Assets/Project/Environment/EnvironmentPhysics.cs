using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Environment;
using System.Collections;

using UnityEngine.AI;
/*
 * To do: shorten projectil length. Even though a projectile has a start
 * and an end, the projectile can still extend beyond its endpoint. I must
 * extend it all the way to the mapbounds, then find an appropriate compromise
 * that shortens the extended vector as much as possible to reduce object calculations.
 * One good limiting factor is the camera current position; only let the vector extend
 * a bit beyond the camera bounds
 */
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

	private static readonly int TERRAINDISPARITYCONFIDENCEINTERVAL = 8	;

	void Awake(){
		instance = this;
		bottomLeftCorner = mapbounds.GetBottomLeftCorner();
		maxDimensions = mapbounds.GetDimensions ();
	}

	void Start(){

	}

	void Update(){
        
	}

	void OnDrawGizmos(){
		
	}

	private struct IntersectionResult{
		private Vector3 position;
		private Obstacle obstacle;
		public IntersectionResult(RaycastHit info){
			position = info.point;
			obstacle = info.collider.transform.GetComponent<Obstacle>();
		}

		public Vector3 GetPosition(){
			return position;
		}
		public Obstacle GetObstacle(){
			return obstacle;
		}
	}

	private static List<Obstacle> FindNearbyObstacles(Vector3 location, float radius){
		Collider[] collided = Physics.OverlapSphere (location, radius, instance.collisionMask);
		List<Obstacle> obstacles = new List<Obstacle> ();
		foreach(Collider collider in collided){
			Obstacle newObstacle = collider.GetComponent<Obstacle> ();
			if(newObstacle != null){
				obstacles.Add (newObstacle);
			}
		}
		return obstacles;
	}


	private delegate void ProcessIntersection(IntersectionResult result);
	private delegate bool ShouldContinueRayCast(IntersectionResult result);

	private static void IncrementalRaycast(Vector3 start,Vector3 end,ProcessIntersection onIntersect,ShouldContinueRayCast shouldContinue){
		RaycastHit hitInfo;
		Vector3 ray = end - start;
		Vector3 dx = ray.normalized;
		Vector3 remaining = end - start;
		dx.Scale(new Vector3(.01f,.01f,.01f));
		while (Physics.Raycast (start, dx, out hitInfo, remaining.magnitude, instance.collisionMask)) {
			
			remaining -= (hitInfo.point - start);	
			start = (hitInfo.point + dx);

			//hit object must have an obstacle script
			if(hitInfo.collider.transform.GetComponent<Obstacle>() != null){
				IntersectionResult result = new IntersectionResult (hitInfo);
				onIntersect (result);
				if (!shouldContinue (result)) {
					break;
				}
			}
		}
	}

	public static bool LineOfSightToVantagePointExists(int visionSharpness,Vector3 start,Vector3 target){
		bool uninterrupted = true;
		int clarityLeft = visionSharpness;
		ProcessIntersection onIntersect = (result => {
			clarityLeft -= result.GetObstacle().GetTransparency();
			if(clarityLeft <= 0){
				uninterrupted = false;
			}
		});
		ShouldContinueRayCast continueCondition = (result => {
			return clarityLeft > 0;
		});
		IncrementalRaycast (start, target, onIntersect,continueCondition);
		return uninterrupted;
	}

	public static bool LineOfSightToGroundExists(int visionSharpness,Vector3 start,Vector3 target){
		/* To avoid floating point errors and hitting the terrain,
			raise the height of the target a bit above ground level	
			*/
		return LineOfSightToVantagePointExists(visionSharpness,start,FindGroundedLocation(target.x,target.z) + new Vector3(0,0.1f,0));
	}

	public static void SendProjectile(Projectile projectile,Vector3 start,Vector3 target){
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
		IncrementalRaycast (start, target, onIntersect,continueCondition);
		projectile.ResetStrength ();
        Debug.DrawLine(start, lastImpact, color: Color.white, duration: .2f);
	}

	public struct TerrainDisparity{
		public float visibleToObserver;
		public float visibleToTarget;
	}

	public static TerrainDisparity CalculateTerrainDisparityBetween(Projectile heuristicOfObserver,Projectile heuristicOfTarget,Vector3 observerVantage,Vector3 targetVantage){
		TerrainDisparity result = new TerrainDisparity ();
		result.visibleToObserver = CalculateVisiblePortion (heuristicOfObserver,observerVantage,targetVantage);
		result.visibleToTarget = CalculateVisiblePortion (heuristicOfTarget,targetVantage,observerVantage);
		return result;
	}

	private static float CalculateVisiblePortion (Projectile heuristic, Vector3 observerVantage, Vector3 targetVantage){
		float targetHeightAboveGround = targetVantage.y - FindHeightAt (targetVantage.x,targetVantage.z);
		float heightAdjustment = targetHeightAboveGround / 2;
		Vector3 rayTarget = targetVantage - new Vector3(0,heightAdjustment,0);
		bool canPassThrough = false;
		for(int i = 0; i < TERRAINDISPARITYCONFIDENCEINTERVAL; i ++){
			heightAdjustment /= 2;
			heuristic.ResetStrength ();
			canPassThrough = ProjectileCanPassThrough (heuristic, observerVantage, rayTarget);
			if (canPassThrough) {
				rayTarget.y -= heightAdjustment;
			} else {
				rayTarget.y += heightAdjustment;
			}
		}
        //Debug.DrawLine(observerVantage, rayTarget);
		return targetVantage.y - rayTarget.y;
	}

	public static bool ProjectileCanPassThrough(Projectile projectile,Vector3 start, Vector3 target){
		ProcessIntersection onIntersect = (result => {
			projectile.SlowedBy(result.GetObstacle());


		});
		ShouldContinueRayCast continueCondition = (result => {
			return projectile.IsStillActive();
		});
		IncrementalRaycast (start, target, onIntersect,continueCondition);
		bool passedThrough = projectile.IsStillActive ();
		projectile.ResetStrength ();
		return passedThrough;
	}
		
	public static float FindHeightAt(float x,float z){	
		float height = 0;
		ProcessIntersection onIntersect = (result => {
			height = result.GetPosition().y;
		});
		ShouldContinueRayCast continueCondition = (result => {
			return result.GetObstacle().CanPhaseThrough();
		});
		IncrementalRaycast (
			new Vector3(x,instance.bottomLeftCorner.y + instance.maxDimensions.y,z),
			new Vector3(x,instance.bottomLeftCorner.y,z),
			onIntersect,
			continueCondition
		);
		return height;
	}

    public static MapNode CreateMapNoteAt(float x,float z){
        float height = 0;
        bool heightSet = false;
        bool walkable = true;
        float speedModifier = 1;
        ProcessIntersection onIntersect = (result => {
            if(!heightSet && !result.GetObstacle().CanPhaseThrough()){
                height = result.GetPosition().y;
                heightSet = true;
            }
            if(!result.GetObstacle().IsWalkable()){
                walkable = false;
            }
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
        /*MapNode mapNode = MapNode.GetMapNode();
        mapNode.Reinitialize(height, speedModifier, walkable);
        return mapNode;*/
        return new MapNode(height, walkable);
    }

    public static float FindWalkableHeightAt(float x, float z){
        return FindHeightAt(x, z) + 0.01f;
    }

	public static bool WalkableAt(float x,float z,float radius){
		float height = FindHeightAt (x, z);
		List<Obstacle> obstacles = FindNearbyObstacles (new Vector3 (x, height, z), radius);
		for(int i = 0; i < obstacles.Count; i ++){
			if(!obstacles[i].IsWalkable()){
				return false;
			}
		}
		return true;
	}

	public static Vector3 FindGroundedLocation(float x,float z){
		return new Vector3 (x, FindHeightAt (x,z), z);
	}

    public static Vector3 ScreenToEnvironmentPoint(GameplayCamera cam, Vector3 screenPoint){
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
}

