using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Pathfinder : MonoBehaviour
{
	[SerializeField]
	private Grid grid;

	private const int MAXPATHHEAPSIZE = 1000;

	void Awake()
	{
		
	}


		
	public void FindPath(PathRequest request, Action<PathResult> callback)
	{
		Vector3[] waypoints = new Vector3[0];
		bool pathSuccess = false;
        float maxLength = request.maxLength;
        /* multiple by 10 to account for the factor of 10 in GetDistance()*/
        int maxPathLength = grid.DistanceToNodeDistance(maxLength) * 10;
        Point startPoint = grid.WorldCoordToNode (request.pathStart);
		Point endPoint = grid.WorldCoordToNode (request.pathEnd);

		PathfinderNode startNode = new PathfinderNode (startPoint,grid.GetNodeAt(startPoint));
		PathfinderNode targetNode = new PathfinderNode (endPoint,grid.GetNodeAt(endPoint));

        PathfinderStrategy strategy = request.strategy;

		if (startNode.isWalkable() && targetNode.isWalkable()){
			PathfindingHeap<PathfinderNode> openSet = new PathfindingHeap<PathfinderNode>(MAXPATHHEAPSIZE);
			HashSet<PathfinderNode> closedSet = new HashSet<PathfinderNode>();
			Dictionary<Point,PathfinderNode> activeNodes = new Dictionary<Point,PathfinderNode> ();
			openSet.Add(startNode);
			activeNodes.Add (startNode.GetGridCoord(),startNode);
			activeNodes.Add (targetNode.GetGridCoord(),targetNode);
			while (openSet.Count > 0){
				PathfinderNode currentNode = openSet.RemoveFirst();
                Vector3 currentNodeLocation = grid.NodeToWorldCoord(currentNode.GetGridCoord());

				closedSet.Add(currentNode);

				if (currentNode == targetNode){
					pathSuccess = true;
					break;
				}

				List<PathfinderNode> neighbors = GetNeighbors (currentNode, activeNodes);
                for (int i = 0; i < neighbors.Count; i++){
                    PathfinderNode neighbour = neighbors[i];
                    Vector3 neighbourLocation = grid.NodeToWorldCoord(neighbour.GetGridCoord());
                    if (!neighbour.isWalkable()
                        || closedSet.Contains(neighbour)){
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                    if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)){
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.strategyCost = strategy.GetAdditionalCostAt(currentNodeLocation, neighbourLocation);
                        neighbour.SetParent(currentNode);

                        if (!openSet.Contains(neighbour)
                            && neighbour.WithInRangeOfStart(maxPathLength)){
                            openSet.Add(neighbour);
                            DrawGizmo.AddGizmo(Color.blue, "" + neighbour.gCost, neighbourLocation);
                        }
                        else{
                            openSet.UpdateItem(neighbour);
                        }
                    }
                }
            }
		}
		if (pathSuccess)
		{
			waypoints = RetracePath(startNode, targetNode);
			pathSuccess = waypoints.Length > 0;
		}
		callback(new PathResult(waypoints, pathSuccess, request.callback));
	}

	private List<PathfinderNode> GetNeighbors(PathfinderNode node,Dictionary<Point,PathfinderNode> activeNodes){
		List<PathfinderNode> neighbors = new List<PathfinderNode> ();
		List<Point> neighborPoints = grid.GetNeighbors (node.GetGridCoord());
		for(int i = 0; i < neighborPoints.Count; i ++){
			Point currentPoint = neighborPoints [i];
			PathfinderNode currentNode = null;
			activeNodes.TryGetValue (currentPoint, out currentNode);
			if(currentNode != null){
				neighbors.Add (currentNode);
			}else{
                currentNode = new PathfinderNode(
                    currentPoint,grid.GetNodeAt(currentPoint)
                );
				activeNodes.Add (currentPoint, currentNode);
				neighbors.Add (currentNode);
			}
		}
		return neighbors;
	}

	private Vector3[] RetracePath(PathfinderNode startNode, PathfinderNode endNode)
	{
        List<PathfinderNode> path = endNode.TraceParents(startNode);

		Vector3[] waypoints = SimplifyPath(path);
		Array.Reverse(waypoints);
		return waypoints;

	}

	private Vector3[] SimplifyPath(List<PathfinderNode> path)
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
			Vector2 directionNew = new Vector2(currentPoint.x - previousPoint.x,currentPoint.y - previousPoint.y);
			//if (directionNew != directionOld) {
				waypoints.Add(grid.NodeToWorldCoord(currentPoint));
			//}
			directionOld = directionNew;
		}
		return waypoints.ToArray();
	}

	private int GetDistance(PathfinderNode nodeA, PathfinderNode nodeB)
	{
		Point a = nodeA.GetGridCoord ();
		Point b = nodeB.GetGridCoord ();

		int dstX = Mathf.Abs(a.x - b.x);
		int dstY = Mathf.Abs(a.y - b.y);

		if (dstX > dstY)
			return (int)(14 * dstY + 10 * (dstX - dstY));
		return (int)(14 * dstX + 10 * (dstY - dstX));
	}

}