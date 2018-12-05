using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ImplementationStrategy : IImplementPathfinder {
    public abstract void ProcessNode(
        PathfinderNode currentNode,
        PathfinderNode targetNode,
        PathfindingHeap<PathfinderNode> openSet,
        HashSet<PathfinderNode> closedSet,
        Dictionary<Point, PathfinderNode> activeNodes,
        Grid grid,
        int maxPathLength
    );

}
