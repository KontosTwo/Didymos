using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PathfinderHelper;

public class FlankingImplementation : ImplementationStrategy{

    private NodeCreator currentNodeCreator;
    private CostStrategy currentCostStrategy;

    private NodeCreator nonCoverNodeCreator;
    private CostStrategy nonCoverCostStrategy;
    private NodeCreator coverNodeCreator;
    private CostStrategy coverCostStrategy;



    public FlankingImplementation(NodeCreator nonCoverNodeCreator,
                                  CostStrategy nonCoverCostStrategy,
                                  NodeCreator coverNodeCreator,
                                  CostStrategy coverCostStrategy){
        this.nonCoverNodeCreator = nonCoverNodeCreator;
        this.nonCoverCostStrategy = nonCoverCostStrategy;
        this.coverNodeCreator = coverNodeCreator;
        this.coverCostStrategy = coverCostStrategy;

        currentNodeCreator = nonCoverNodeCreator;
        currentCostStrategy = nonCoverCostStrategy;
    }

    public override void ProcessNode(
        PathfinderNode currentNode,
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
                neighbour.SetStrategyCost(currentCostStrategy.GetAdditionalCostAt(currentLocation, neighbourLocation));
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
