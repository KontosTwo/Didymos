using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;
using System.Reflection;
using Environment;
using System;
using System.Linq;



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

    private static readonly int MAX_NODE_CACHE_SIZE = 50000;

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
        /*
        * 
        * 
        * 
        * 
        * 
        * CONVERT TO BATCH ADD NODES
        * WONT WORK UNTIL BATCHADDNOES IS IMPLEMENTED
        * 
        * 
        * 
        *         
        */
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
    }

    /*
     * Efficiently calculates and fetches multiple
     * mapnodes.     Make sure to filter existing ones first
     */
    public static List<MapNode> GetBatchMapNodeAt(List<Vector2> locations){
        if(locations.Count > MAX_NODE_CACHE_SIZE){
            UnityEngine.Debug.Log("WARNING: number of mapnodes requested exceeds cache size");
        }
        List<Point> points = 
            locations.Select(
                l => instance.WorldCoordToNode(l.To3D())
            ).ToList();
        List<Point> cacheHits =
            points.FindAll(p => instance.nodes[p] != null);
        List<Point> cacheMisses =
            points.Except(cacheHits).ToList();
        List<MapNode> hitMapNodes = 
            cacheHits.Select(ch => instance.nodes[ch]).ToList();
        BatchNodesMissResolve(cacheMisses);
        List<Point> cacheMissesNeighbors = new List<Point>();
        cacheMisses.ForEach(p =>{
            cacheMissesNeighbors.AddRange(instance.GetNeighbors(p));
        });
        List<Point> cacheMissesNeighborsUninitialized =
            cacheMissesNeighbors.FindAll(p => !instance.nodes[p].AdjacencyDataSet()).ToList();
        BatchAddNodes(cacheMissesNeighborsUninitialized);

        List<MapNode> allRelevantNodes =
            points.Select(p => instance.nodes[p]).ToList();

        allRelevantNodes.ForEach(node =>{
            node.CalculateAdjancencyData(
                    instance.GetNeighbors(
                        instance.WorldCoordToNode(
                            node.GetLocation()
                        )
                    ).Select(p => instance.nodes[p]).ToList()
            );
        });
        return allRelevantNodes;
    }

    private static void BatchNodesMissResolve(List<Point> locations){
        BatchAddNodes(locations);
    }

    private static void BatchAdjacencyMissResolve(List<Point> locations){

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



    public List<Point> GetNeighbors(Point point){
        List<Point> neighbors = new List<Point>();
        Point p1 = new Point(point.x - 1, point.y - 1);
        if (InBound(p1)){
            neighbors.Add(p1);
        }
        Point p2 = new Point(point.x - 1, point.y);
        if (InBound(p2)){
            neighbors.Add(p2);
        }
        Point p3 = new Point(point.x - 1, point.y + 1);
        if (InBound(p3)){
            neighbors.Add(p3);
        }
        Point p4 = new Point(point.x, point.y + 1);
        if (InBound(p4)){
            neighbors.Add(p4);
        }
        Point p5 = new Point(point.x + 1, point.y + 1);
        if (InBound(p5)){
            neighbors.Add(p5);
        }
        Point p6 = new Point(point.x + 1, point.y);
        if (InBound(p6)){
            neighbors.Add(p6);
        }
        Point p7 = new Point(point.x + 1, point.y - 1);
        if (InBound(p7)){
            neighbors.Add(p7);
        }
        Point p8 = new Point(point.x, point.y - 1);
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
    }

    private static void BatchAddNodes(List<Point> locations){
        List<MapNode> newNodes =
            EnvironmentPhysics.CreateMapNodesAt(
                locations.Select(l => instance.NodeTo2DWorldCoord(l)).ToList()
            );
        newNodes.ForEach(
            node =>{
                instance.nodes[instance.WorldCoordToNode(node.GetLocation())] =
                    node;
            }
        );
    }

    void OnDrawGizmos(){
        if (Application.isPlaying){
            foreach (Tuple<MapNode, Point> pair in nodes){
                MapNode node = pair.Item1;
               // Gizmos.color = (node.TerrainIsWalkable()) ? Color.white : Color.red;
               // Gizmos.DrawCube(NodeToWorldCoord(pair.Item2), Vector3.one * (nodeSize-.1f));
            }
        }

    }
}


