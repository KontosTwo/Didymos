using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/*
 *  Source
 *  https://github.com/SebLague/Pathfinding
 */
public class PathfinderHelper :MonoBehaviour{
    [SerializeField]
    private Grid grid;

    private static PathfinderHelper instance;
    private static readonly int MAXPATHHEAPSIZE = 1000;

    private void Awake(){
        instance = this;
    }

    public delegate PathfinderNode NodeCreator(
        Point point,
        MapNode node
    );

    public delegate PathfinderNode FociNodeCreator(
        Point point,
        Point foci,
        MapNode node
    );


    public static PathResult FindPath(
        PathRequest request
    ){
        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;
        float maxLength = request.maxLength;
        PathfinderImplementationStrategy implementationStrategy = request.aStarImpl;
        /* multiple by 10 to account for the factor of 10 in GetDistance()*/
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

        if (startNode.IsWalkable() && targetNode.IsWalkable()){
            PathfindingHeap<PathfinderNode> openSet = 
                new PathfindingHeap<PathfinderNode>(MAXPATHHEAPSIZE);
            HashSet<PathfinderNode> closedSet = 
                new HashSet<PathfinderNode>();
            Dictionary<Point, PathfinderNode> activeNodes = 
                new Dictionary<Point, PathfinderNode>();
            openSet.Add(startNode);
            activeNodes.Add(startNode.GetGridCoord(), startNode);
            activeNodes.Add(targetNode.GetGridCoord(), targetNode);
            while (openSet.Count > 0){
                PathfinderNode currentNode = openSet.RemoveFirst();

                closedSet.Add(currentNode);

                /*if (currentNode == targetNode){
                    pathSuccess = true;
                    break;
                }*/
                if (openSet.Contains(targetNode))
                {
                    pathSuccess = true;
                    break;
                }

                implementationStrategy.ProcessNode(
                    currentNode,
                    startNode,
                    targetNode,
                    openSet,
                    closedSet,
                    activeNodes,
                    instance.grid,
                    maxPathLength
                );

            }
        }
        if (pathSuccess){
            waypoints = RetracePath(startNode, targetNode);
            pathSuccess = waypoints.Length > 0;
        }
        PathfinderVisualizer.Visualize();

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
        Dictionary<Point, 
        PathfinderNode> activeNodes,
        PathfinderNodeCreator nodeCreator
    ){
        List<PathfinderNode> neighbors = new List<PathfinderNode>();
        List<Point> neighborPoints = instance.grid.GetNeighbors(node.GetGridCoord());
        List<Point> unActiveNeighbors = new List<Point>();
        for (int i = 0; i < neighborPoints.Count; i++){
            Point currentPoint = neighborPoints[i];
            PathfinderNode currentNode = null;
            activeNodes.TryGetValue(currentPoint, out currentNode);
            if (currentNode != null){
                neighbors.Add(currentNode);
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
