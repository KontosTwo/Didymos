using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IImplementPathfinder {
    void ProcessNode(
        PathfinderNode currentNode,
        PathfinderNode targetNode,
        PathfindingHeap<PathfinderNode> openSet,
        HashSet<PathfinderNode> closedSet,
        Dictionary<Point, PathfinderNode> activeNodes,
        Grid grid,
        int maxPathLength
    );

}
