using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PathfinderHelper;

public class BaseImplementation : PathfinderImplementationStrategy{

    private PathfinderNodeCreator currentNodeCreator;
    private PathfinderCostStrategy currentCostStrategy;



    public BaseImplementation(
        PathfinderCostStrategy costStrategy,
        PathfinderNodeCreator nodeCreator
    ){
        currentCostStrategy = costStrategy;
        currentNodeCreator = nodeCreator;
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

            CostResult newStrategyCost = 
                currentCostStrategy.GetAdditionalCostAt(
                    currentLocation,
                    neighbourLocation
                );

            neighbour.UpdateAccumulatedStrategyCost(newStrategyCost);

            int newPhysicalGCost =
                currentNode.GetPhysicalGCost()
                + PathfinderHelper.GetDistance(currentNode, neighbour);


            int newMovementCostToNeighbour =
                currentNode.GetPhysicalGCost()
                + PathfinderHelper.GetDistance(currentNode, neighbour)
                + neighbour.GetExtractor().Extract(newStrategyCost);

            //Debug.Log(neighbour.GetGCost());
            if (newMovementCostToNeighbour < neighbour.GetGCost() || !openSet.Contains(neighbour)){
                //Debug.Log(neighbour.GetGCost());
                //DrawGizmo.AddGizmo(Color.green, ""  + neighbour.GetExtractor().Extract(newStrategyCost), neighbour.GetLocation());

                neighbour.SetStrategyCost(
                    newStrategyCost
                );
                neighbour.SetPhysicalGCost(newPhysicalGCost);
                neighbour.SetHCost(GetDistance(neighbour, targetNode));

                neighbour.SetParent(currentNode);
                if (!openSet.Contains(neighbour)
                    && neighbour.WithInRangeOfStart(maxPathLength)
                ){
                    openSet.Add(neighbour);
                    PathfinderVisualizer.Visit(neighbour);

                    /*DrawGizmo.AddGizmo(Color.grey, "", grid.NodeToWorldCoord(
                        neighbour.GetGridCoord())
                    );*/
                }
                else{
                    openSet.UpdateItem(neighbour);
                }
            }
            else{

            }
        }
    }

}
