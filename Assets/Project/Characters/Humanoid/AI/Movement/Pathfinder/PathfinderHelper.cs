using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/*
 *  Source
 *  https://github.com/SebLague/Pathfinding
 * 
 *  NOT THREAD OR COROUTINE SAFE
 */
using UnityEngine.Profiling;
public class PathfinderHelper :MonoBehaviour{
    [SerializeField]
    private Grid grid;

    private static PathfinderHelper instance;
    private static readonly int MAXPATHHEAPSIZE = 10000;

    private PathfindingHeap<PathfinderNode> openSet;
    private Dictionary<Point, PathfinderNode> activeNodes;
    private HashSet<PathfinderNode> closedSet;

    private void Awake(){
        openSet = new PathfindingHeap<PathfinderNode>(MAXPATHHEAPSIZE);
        activeNodes = new Dictionary<Point, PathfinderNode>(1000);
        closedSet = new HashSet<PathfinderNode>();
        instance = this;
    }

    public delegate PathfinderNode NodeCreator(
        Point point,
        MapNode node
    );


    public static PathResult FindPath(
        PathRequest request
    ){
        instance.openSet.Clear();
        instance.activeNodes.Clear();
        instance.closedSet.Clear();
        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;
        float maxLength = request.maxLength;
        PathfinderImplementationStrategy implementationStrategy = request.aStarImpl;
        int maxPathLength = instance.grid.DistanceToNodeDistance(maxLength);
        Point startPoint = instance.grid.WorldCoordToNode(request.pathStart);
        Point endPoint = instance.grid.WorldCoordToNode(request.pathEnd);

        PathfinderNode startNode =
            implementationStrategy.CreateStarterNodes(
                startPoint,
                instance.grid.GetMapNodeAt(startPoint)
            );
        PathfinderNode targetNode =
            implementationStrategy.CreateStarterNodes(
                endPoint,
                instance.grid.GetMapNodeAt(endPoint)
            );
        /*
         * If startnode and targetnode are the same
         * it causes a heap error        
         */
        if (startNode.IsWalkable() && targetNode.IsWalkable()){


            instance.openSet.Add(startNode);

            instance.activeNodes.Add(startNode.GetGridCoord(), startNode);
            instance.activeNodes.Add(targetNode.GetGridCoord(), targetNode);
            while (instance.openSet.Count > 0){
                Profiler.BeginSample("Remove first from open set");
                PathfinderNode currentNode = instance.openSet.RemoveFirst();
                Profiler.EndSample();

                Profiler.BeginSample("Add current to closed set");
                instance.closedSet.Add(currentNode);
                Profiler.EndSample();


                if (currentNode == targetNode){
                    pathSuccess = true;
                    break;
                }
                if (instance.openSet.Contains(targetNode))
                {
                    pathSuccess = true;
                    break;
                }
                Profiler.BeginSample("Process current node");
                implementationStrategy.ProcessNode(
                    currentNode,
                    startNode,
                    targetNode,
                    instance.openSet,
                    instance.closedSet,
                    instance.activeNodes,
                    instance.grid,
                    maxPathLength
                );
                Profiler.EndSample();

            }
        }
        if (pathSuccess){
            waypoints = RetracePath(startNode, targetNode);
            pathSuccess = waypoints.Length > 0;
        }

        List<Point> recycledPoints = Pools.ListPoints;
        //recycledPoints.Add(startPoint);
        //recycledPoints.Add(endPoint);
        Dictionary<Point,PathfinderNode>.Enumerator enumerator = 
            instance.activeNodes.GetEnumerator();
        while (enumerator.MoveNext()){
            var current = enumerator.Current;
            Point p = current.Key;
            recycledPoints.Add(p);
        }
        for(int i = 0; i < recycledPoints.Count; i++){
            Point current = recycledPoints[i];
            Pools.Point = current;


        }

        Pools.ListPoints = recycledPoints;
        PathfinderVisualizer.Visualize();
        //Debug.Log("path end");
        return new PathResult(
            waypoints,
            pathSuccess
        );
    }

    public delegate List<PathfinderNode> GetNeighborWithNodeStrategy(
        PathfinderNode node,
        Dictionary<Point,
        PathfinderNode> activeNodes,
        NodeCreator nodeCreator
    );

    public static List<PathfinderNode> GetNeighbors(
        PathfinderNode node, 
        Dictionary<Point, PathfinderNode> activeNodes,
        PathfinderNodeCreator nodeCreator
    ){
        List<PathfinderNode> neighbors = new List<PathfinderNode>();
        List<Point> neighborPoints = instance.grid.GetNeighbors(node.GetGridCoord());
        List<Point> unActiveNeighbors = Pools.ListPoints;
        for (int i = 0; i < neighborPoints.Count; i++){
            Point currentPoint = neighborPoints[i];


            PathfinderNode currentNode = null;
            activeNodes.TryGetValue(currentPoint, out currentNode);
            // current node is already active
            if (currentNode != null){
                neighbors.Add(currentNode);
               
                Pools.Point = currentPoint;

                
                // current node is not active
            }
            else{
                unActiveNeighbors.Add(currentPoint);
                currentNode = nodeCreator.CreateNode(
                    currentPoint, instance.grid.GetMapNodeAt(currentPoint)
                );
                activeNodes.Add(currentPoint, currentNode);
                neighbors.Add(currentNode);
            }
        }

        Pools.ListPoints = neighborPoints;
        Pools.ListPoints = unActiveNeighbors;
        
        return neighbors;
    }

    private static Vector3[] RetracePath(PathfinderNode startNode, PathfinderNode endNode)
    {
        List<PathfinderNode> path = endNode.TraceParents(startNode);

        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;

    }

    private static Vector3[] SimplifyPath(List<PathfinderNode> path)
    {
        /*List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;
        
        for (int i = 1; i < path.Count; i ++) {
            Vector2 directionNew = new Vector2(path[i-1].gridX - path[i].gridX,path[i-1].gridY - path[i].gridY);
            if (directionNew != directionOld) {
                waypoints.Add(path[i-1].worldPosition);
            }
            directionOld = directionNew;
        }
        return waypoints.ToArray();*/


        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for (int i = 1; i < path.Count; i++)
        {
            Point currentPoint = path[i].GetGridCoord();
            Point previousPoint = path[i - 1].GetGridCoord();
            Vector2 directionNew = new Vector2(currentPoint.x - previousPoint.x, currentPoint.y - previousPoint.y);
            //if (directionNew != directionOld) {
            waypoints.Add(instance.grid.NodeToWorldCoord(currentPoint));
            //}
            directionOld = directionNew;
        }
        return waypoints.ToArray();
    }

    public static int GetDistance(PathfinderNode nodeA, PathfinderNode nodeB){
        Point a = nodeA.GetGridCoord();
        Point b = nodeB.GetGridCoord();

        int dstX = Mathf.Abs(a.x - b.x);
        int dstY = Mathf.Abs(a.y - b.y);

        if (dstX > dstY)
            return (int)(14 * dstY + 10 * (dstX - dstY));
        return (int)(14 * dstX + 10 * (dstY - dstX));
    }
    public static int GetDistance(Point a, Point b){

        int dstX = Mathf.Abs(a.x - b.x);
        int dstY = Mathf.Abs(a.y - b.y);

        if (dstX > dstY)
            return (int)(14 * dstY + 10 * (dstX - dstY));
        return (int)(14 * dstX + 10 * (dstY - dstX));
    }
}
