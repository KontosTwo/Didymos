using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PathfinderHelper;

public class FlankingImplementation : PathfinderImplementationStrategy{

    private PathfinderNodeCreator currentNodeCreator;
    private PathfinderCostStrategy currentCostStrategy;

    private FavorDistanceToFociCreator nonCoverNodeCreator;
    private PathfinderCostStrategy nonCoverCostStrategy;
    private FavorCoverAndStrategyCostCreator coverNodeCreator;
    private PathfinderCostStrategy coverCostStrategy;

    private bool inCover;
    private bool notInCover;

    private bool nonCoverCostStrategyInitialized;

    public FlankingImplementation(
        PathfinderCostStrategy nonCoverCostStrategy,
        PathfinderCostStrategy coverCostStrategy
    ){
        this.nonCoverNodeCreator = new FavorDistanceToFociCreator();
        this.nonCoverCostStrategy = nonCoverCostStrategy;
        this.coverNodeCreator = new FavorCoverAndStrategyCostCreator();
        this.coverCostStrategy = coverCostStrategy;

        nonCoverCostStrategyInitialized = false;

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
        if(!nonCoverCostStrategyInitialized){
            nonCoverCostStrategyInitialized = true;
            SwitchToNonCover(currentNode);
        }



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
                    if(CanSwitchToCover(currentNode,neighbour)){
                        SwitchToCover();

                        openSet.Clear();
                        activeNodes.Clear();

                       // openSet.Add(startNode);
                       // activeNodes.Add(startNode.GetGridCoord(), startNode);
                        activeNodes.Add(targetNode.GetGridCoord(), targetNode);
                        DrawGizmo.AddGizmo(Color.blue, "Switching to cover: " + PathRequestManager.counter, neighbourLocation + new Vector3(0, 10f, 0));
                    }
                    else if(CanSwitchToNonCover(currentNode,neighbour)){
                        SwitchToNonCover(currentNode);

                        openSet.Clear();
                        activeNodes.Clear();

                        //openSet.Add(startNode);
                        //activeNodes.Add(startNode.GetGridCoord(), startNode);
                        activeNodes.Add(targetNode.GetGridCoord(), targetNode);

                        DrawGizmo.AddGizmo(Color.red, "switching out of cover: " + PathRequestManager.counter, neighbourLocation + new Vector3(0,10f, 0));

                        DrawGizmo.AddGizmo(Color.green, "Foci: " + PathRequestManager.counter, currentLocation + new Vector3(0, 15f, 0));
                    }


                    openSet.Add(neighbour);
                    DrawGizmo.AddGizmo(Color.gray, "" + neighbour.GetStrategyCost(), neighbourLocation);
                }
                else{

                    openSet.UpdateItem(neighbour);
                }
            }
        }
    }

    private bool CanSwitchToCover(PathfinderNode currentNode,
                                  PathfinderNode neighbor){
        if(notInCover){
            return neighbor.GetStrategyCost() < currentNode.GetStrategyCost() &&
                           neighbor.IsCover();
        }
        return false;
    }

    private bool CanSwitchToNonCover(PathfinderNode currentNode,
                                  PathfinderNode neighbor){
        if (inCover){
            return neighbor.GetStrategyCost() > currentNode.GetStrategyCost() &&
                           !neighbor.IsCover();
        }
        return false;
    }

    private void SwitchToCover(){
        currentNodeCreator = coverNodeCreator;
        currentCostStrategy = coverCostStrategy;
        inCover = true;
        notInCover = false;

    }

    private void SwitchToNonCover(PathfinderNode currentNode){
        nonCoverNodeCreator.SetFoci(currentNode.GetGridCoord());
        currentNodeCreator = nonCoverNodeCreator;
        currentCostStrategy = nonCoverCostStrategy;
        inCover = false;
        notInCover = true;

    }
}
