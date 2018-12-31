using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PathfinderImplementationStrategy : IImplementPathfinder {
    public abstract PathfinderNode CreateStarterNodes(
        Point point,
        MapNode data
    );
    public abstract void ProcessNode(
        PathfinderNode currentNode,
        PathfinderNode startNode,
        PathfinderNode targetNode,
        PathfindingHeap<PathfinderNode> openSet,
        HashSet<PathfinderNode> closedSet,
        Dictionary<Point, PathfinderNode> activeNodes,
        Grid grid,
        int maxPathLength
    );

}
