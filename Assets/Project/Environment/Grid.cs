using System.Collections.Generic;
using UnityEngine;
using Environment;
using System;
using System.Linq;
using UnityEngine.Profiling;
using System.Collections;
using UnityEditor.SceneManagement;

/*
 * This class is not Coroutine-safe!
 */
public class Grid : MonoBehaviour{

    [SerializeField]
    private MapBound mapbounds;
    [SerializeField]
    private float nodeSize;

    private LinkedDictionary<Point,MapNode> nodes;
    private Vector2 bottomLeftCorner;
    private Vector2 dimensions;

    private int length;
    private int height;

    private static Grid instance;

    private static readonly int MAX_NODE_CACHE_SIZE = 10;

    void Awake(){
        Vector3 bottomLeftCorner3d = mapbounds.GetBottomLeftCorner();
        Vector3 dimensions3d = mapbounds.GetDimensions();
        bottomLeftCorner = new Vector2(bottomLeftCorner3d.x, bottomLeftCorner3d.z);
        dimensions = new Vector2(dimensions3d.x, dimensions3d.z);
        nodes = new LinkedDictionary<Point, MapNode>();
        length = (int)(dimensions.x / nodeSize);
        height = (int)(dimensions.y / nodeSize);

        instance = this;


    }

    void Start(){
    }

    private void Update()
    {
    }

    private void LateUpdate()
    {
        for(int i = 0; i < instance.nodes.Count - MAX_NODE_CACHE_SIZE; i++){
            RemoveNode();
        }
    }

    public static MapNode GetMapNodeAt(Vector3 location){
        Point point = instance.WorldCoordToNode(location);
        return instance.GetMapNodeAt(point);
    }

    public MapNode GetMapNodeAt(Point point){
        MapNode potentialNode = instance.nodes[point];
        if (potentialNode == null){
            NodesMissResolve(point);
        }
        potentialNode = instance.nodes[point];
        if (!potentialNode.AdjacencyDataSet()){
            AdjacencyMissResolve(point);
        }
        return instance.nodes[point];
    }

    private static void NodesMissResolve(Point location){
        AddNode(location);
    }

    private static void AdjacencyMissResolve(Point location){
        List<Point> neighbors = instance.GetNeighbors(location);
             
        List<Point> unInitializedNeighbours =
            neighbors.FindAll(
                p => instance.nodes[p] == null
            ).ToList();
        BatchAddNodes(unInitializedNeighbours);
        MapNode node = instance.nodes[location];
        neighbors = instance.GetNeighbors(location);
        node.CalculateAdjancencyData(
            neighbors.Select(
                n => {
                    return instance.nodes[n];
                }
            ).ToList()
        );
        Pools.ListPoints = neighbors;
    }

    /*
     * Efficiently calculates and fetches multiple
     * mapnodes.     Make sure to filter existing ones first
     */
     public static List<MapNode> GetBatchMapNodesAt(
        List<Point> points
     ){
        Profiler.BeginSample("GetBatchMapNodesAt");

        List<Point> cacheHits = Pools.ListPoints;
        for(int i = 0; i < points.Count; i++){
            Point currentPoint = points[i];
            if (instance.nodes[currentPoint] != null){
                cacheHits.Add(currentPoint);
            }
        }

        List<Point> cacheMisses = Pools.ListPoints;
        for (int i = 0; i < points.Count; i++){
            Point currentPoint = points[i];
            if (instance.nodes[currentPoint] == null){
                cacheMisses.Add(currentPoint);
            }
        }

        HashSet<Point> allNeededPoints = Pools.HashSetPoints;
        for (int i = 0; i < cacheMisses.Count; i++){
            Point cm = cacheMisses[i];
            allNeededPoints.Add(cm);
            List<Point> neighbours =
                instance.GetNeighbors(
                    cm
                );

            for (int j = 0; j < neighbours.Count; j++){
                allNeededPoints.Add(neighbours[j]);
            }
        }

        List<Point> allNeededPointsMisses = Pools.ListPoints;
        IEnumerator<Point> allNeededPointsEnum =
            allNeededPoints.GetEnumerator();
        while (allNeededPointsEnum.MoveNext()){
            Point current = allNeededPointsEnum.Current;
            if(instance.nodes[current] == null){
                allNeededPointsMisses.Add(current);
            }
        }

        BatchNodesMissResolve(allNeededPointsMisses);
        BatchAdjacencyMissResolve(cacheMisses);
        Profiler.EndSample();

        List<MapNode> mapnodes = Pools.ListMapNodes;
        for(int i = 0; i < points.Count; i++){
            mapnodes.Add(instance.nodes[points[i]]);
        }

        Pools.HashSetPoints = allNeededPoints;
        Pools.ListPoints = cacheHits;
        Pools.ListPoints = cacheMisses;
        Pools.ListPoints = allNeededPointsMisses;

        return mapnodes;
    }
    public static List<MapNode> GetBatchMapNodeAt(List<Vector2> locations){
        if(locations.Count > MAX_NODE_CACHE_SIZE){
            UnityEngine.Debug.Log("WARNING: number of mapnodes requested exceeds cache size");
        }
        List<Point> points = Pools.ListPoints;
        for(int i = 0; i < locations.Count; i++){
            Vector2 current = locations[i];
            points.Add(instance.WorldCoordToNode(current.To3D()));
        }
        List<MapNode> mapnodes = GetBatchMapNodesAt(points);
        Pools.ListPoints = points;
        return mapnodes;
    }

    private static void BatchNodesMissResolve(List<Point> locations){
        if(locations.Count == 0){
            return;
        }
        BatchAddNodes(locations);
    }

    private static void BatchAdjacencyMissResolve(List<Point> locations){
        for(int i = 0; i < locations.Count; i++){
            Point current = locations[i];
            MapNode currentNode = instance.nodes[current];
            List<Point> neighbours = instance.GetNeighbors(current);
            List<MapNode> neighbourNodes = Pools.ListMapNodes;
            for(int j = 0; j < neighbours.Count; j++){
                Point neighbourPoint = neighbours[j];
                MapNode neighbourNode = instance.nodes[neighbourPoint];
                neighbourNodes.Add(neighbourNode);
            }
            currentNode.CalculateAdjancencyData(neighbourNodes);

            Pools.ListMapNodes = neighbourNodes;
            Pools.ListPoints = neighbours;
        }
    }

    public int DistanceToNodeDistance(float distance){
        return (int)(distance / nodeSize);
    }

    public Point WorldCoordToNode(Vector3 worldCoord){
        return new Point((int)(worldCoord.x / nodeSize - bottomLeftCorner.x), (int)(worldCoord.z / nodeSize - bottomLeftCorner.y));
    }

    public Vector3 NodeToWorldCoord(Point point){
        return new Vector3(
            (point.x * nodeSize + nodeSize / 2) + bottomLeftCorner.x
            , nodes[point].GetHeight()
            , (point.y * nodeSize + nodeSize / 2) + bottomLeftCorner.y);
    }

    public List<MapNode> GetMapNodesBetween(
        Vector2 start,
        Vector2 end
    ) {
        Vector2 relativeStart = start - bottomLeftCorner;
        Vector2 relativeEnd = end - bottomLeftCorner;

        List<Point> intersectingPoints = Bresenham.FindTiles(relativeStart, relativeEnd, nodeSize);

        List<MapNode> between = GetBatchMapNodesAt(intersectingPoints);
        Pools.ListPoints = intersectingPoints;
        return between;
    }



    public List<Point> GetNeighbors(Point point){
        List<Point> neighbors = Pools.ListPoints;
        Point p1 = Pools.Point;
        p1.Set(point.x - 1, point.y - 1);
        if (InBound(p1)){
            neighbors.Add(p1);
        }
        Point p2 = Pools.Point;
        p2.Set(point.x - 1, point.y);
        if (InBound(p2)){
            neighbors.Add(p2);
        }
        Point p3 = Pools.Point;
        p3.Set(point.x, point.y - 1);
        if (InBound(p3)){
            neighbors.Add(p3);
        }
        Point p4 = Pools.Point;
        p4.Set(point.x + 1, point.y - 1);
        if (InBound(p4)){
            neighbors.Add(p4);
        }
        Point p5 = Pools.Point;
        p5.Set(point.x - 1, point.y + 1);
        if (InBound(p5)){
            neighbors.Add(p5);
        }
        Point p6 = Pools.Point;
        p6.Set(point.x + 1, point.y + 1);
        if (InBound(p6)){
            neighbors.Add(p6);
        }
        Point p7 = Pools.Point;
        p7.Set(point.x + 1, point.y );
        if (InBound(p7)){
            neighbors.Add(p7);
        }
        Point p8 = Pools.Point;
        p8.Set(point.x, point.y + 1);
        if (InBound(p8)){
            neighbors.Add(p8);
        }
        return neighbors;
    }
    
    private bool InBound(Point p){
        return p.x >= 0 && p.x < length - 1 && p.y >= 0 && p.y < height;
    }

    private Vector2 NodeTo2DWorldCoord(Point point){
        return new Vector2(
            (point.x * nodeSize + nodeSize / 2) + bottomLeftCorner.x
            , (point.y * nodeSize + nodeSize / 2) + bottomLeftCorner.y);
    }

    private static void AddNode(Point location){

        MapNode newNode =
            EnvironmentPhysics.CreateMapNodeAt(
                instance.NodeTo2DWorldCoord(location)
            );
        instance.nodes[location] = newNode;
        //if(instance.nodes.Count > MAX_NODE_CACHE_SIZE){
        //    RemoveNode();
        //}
    }

    private static void BatchAddNodes(List<Point> locations){

        List<Vector2> worldLocations = new List<Vector2>();
        for(int i = 0; i < locations.Count; i++){
            worldLocations.Add(instance.NodeTo2DWorldCoord(locations[i]));
        }
        List<MapNode> newNodes =
            EnvironmentPhysics.CreateMapNodesAt(
                worldLocations
            );

        for(int i = 0; i < newNodes.Count; i++){
            MapNode current = newNodes[i];
            instance.nodes[instance.WorldCoordToNode(current.GetLocation())] =
                    current;
        }
        Pools.ListMapNodes = newNodes;
        //for(int i = 0; i < instance.nodes.Count - MAX_NODE_CACHE_SIZE; i++){
        //    RemoveNode();
        //}
    }

    /*
     * This can cause serious issues
     *  if Unity one day becomes multithreaded
     */    
    private static void RemoveNode(){
        MapNode removed = instance.nodes.PopFirst();
        Pools.MapNode = removed;
        //if (AllPool.MapNodes.IsPinned(removed))
        //{
        //    Debug.Log("WARNING: Mapnode that just got removed is pinned");
        //}
    }

    void OnDrawGizmos(){
        if (Application.isPlaying){
            foreach (var pair in nodes){
                MapNode node = pair.Value.Item1;
                Gizmos.color = (node.TerrainIsWalkable()) ? Color.white : Color.red;
                Gizmos.DrawCube(NodeToWorldCoord(pair.Value.Item2), Vector3.one * (nodeSize-.1f));
            }
        }

    }
}


