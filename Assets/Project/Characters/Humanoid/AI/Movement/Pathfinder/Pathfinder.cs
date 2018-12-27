using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using static PathfinderHelper;

public static class Pathfinder{

	public static PathResult FindPath(PathRequest request)
	{
        return
            PathfinderHelper.FindPath(
                request
            );
	}
}