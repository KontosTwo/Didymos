using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PathfinderHelper;

public class BaseImplementation : PathfinderImplementationStrategy{

    private PathfinderNodeCreator currentNodeCreator;
    private PathfinderCostStrategy currentCostStrategy;

 

    public BaseImplementation(
        PathfinderCostStrategy costStrategy
    ){
        currentCostStrategy = costStrategy;
        currentNodeCreator = new FavorDistanceToStartNodeCreator();
    }

    public override void ProcessNode(
        PathfinderNode currentNode,
        PathfinderNode startNode,
        PathfinderNode targetNode,
        PathfindingHeap<PathfinderNode> openSet, 
        HashSet<PathfinderNode> closedSet, 
        Dictionary<Point, PathfinderNode> activeNodes,
        Grid grid,
        int maxPathLength
    ){



        Vector3 currentLocation = grid.NodeToWorldCoord(currentNode.GetGridCoord());

        List<PathfinderNode> neighbors = PathfinderHelper.GetNeighbors(
            currentNode,
            activeNodes,
            currentNodeCreator
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
                neighbour.SetStrategyCost(
                    currentCostStrategy.GetAdditionalCostAt(
                        currentLocation, 
                        neighbourLocation
                    ).GetCoverDisparityPenalty()
                );
                neighbour.SetParent(currentNode);
                if (!openSet.Contains(neighbour)
                    && neighbour.WithInRangeOfStart(maxPathLength)
                ){



                    openSet.Add(neighbour);

                }
                else{

                    openSet.UpdateItem(neighbour);
                }
            }
        }
    }

}
