using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using static PathfinderHelper;

public static class Pathfinder{
    /*
     * Cannot implement coroutined pathfinding
     * unless pinning poolable objects is 
     * implemented, due to excess mapnodes being 
     * recycled every lateUpdate();    
     *     
     */
	public static PathResult FindPath(PathRequest request)
	{
        return
            PathfinderHelper.FindPath(
                request
            );
	}
}