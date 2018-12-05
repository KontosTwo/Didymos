using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using static PathfinderHelper;

public static class Pathfinder{

	public static void FindPath(PathRequest request, Action<PathResult> callback)
	{
        callback(
            PathfinderHelper.FindPath(
                request,
                PathfinderNode.CreateEndpointNode,
                request.aStarImpl
            )
        );
	}
}