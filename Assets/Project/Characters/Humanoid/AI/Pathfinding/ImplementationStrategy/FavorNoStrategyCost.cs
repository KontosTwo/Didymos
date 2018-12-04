using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PathfinderHelper;

public class FavorNoStrategyCost : ImplementationStrategy{

    private NodeCreator nodeCreator;

    public FavorNoStrategyCost(NodeCreator nodeCreator){
        this.nodeCreator = nodeCreator;
    }

    public override void ProcessNode(
        PathfinderNode currentNode,
        PathfindingHeap<PathfinderNode> openSet, 
        HashSet<PathfinderNode> closedSet, 
        Dictionary<Point, PathfinderNode> activeNodes,
        CostStrategy costStrategy,
        Grid grid,
        int maxPathLength,
        PathfinderNode targetNode
    ){
        Vector3 currentLocation = grid.NodeToWorldCoord(currentNode.GetGridCoord());

        List<PathfinderNode> neighbors = PathfinderHelper.GetNeighbors(
            currentNode,
            activeNodes,
            nodeCreator
        );

        foreach (PathfinderNode neighbour in neighbors){
            Vector3 neighbourLocation = grid.NodeToWorldCoord(neighbour.GetGridCoord());
            if (!neighbour.IsWalkable()
                || closedSet.Contains(neighbour)){
                continue;
            }

            int newMovementCostToNeighbour = 
                currentNode.GetGCost() + PathfinderHelper.GetDistance(currentNode, neighbour);
            if (newMovementCostToNeighbour < neighbour.GetGCost() || !openSet.Contains(neighbour)){
                neighbour.SetGCost(newMovementCostToNeighbour);
                neighbour.SetHCost(GetDistance(neighbour, targetNode));
                neighbour.SetStrategyCost(costStrategy.GetAdditionalCostAt(currentLocation, neighbourLocation));
                neighbour.SetParent(currentNode);

                if (!openSet.Contains(neighbour)
                    && neighbour.WithInRangeOfStart(maxPathLength)
                ){
                    openSet.Add(neighbour);
                    DrawGizmo.AddGizmo(Color.blue, "" + neighbour.GetGCost(), neighbourLocation);
                }
                else{
                    openSet.UpdateItem(neighbour);
                }
            }
        }
    }
}
