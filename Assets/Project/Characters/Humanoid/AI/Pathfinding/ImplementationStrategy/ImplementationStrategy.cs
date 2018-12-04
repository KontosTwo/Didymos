using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ImplementationStrategy : IImplementPathfinder {
    public abstract void ProcessNode(
        PathfinderNode currentNode,
        PathfindingHeap<PathfinderNode> openSet,
        HashSet<PathfinderNode> closedSet,
        Dictionary<Point, PathfinderNode> activeNodes,
        CostStrategy costStrategy,
        Grid grid,
        int maxPathLength,
        PathfinderNode targetNode
    );

}
