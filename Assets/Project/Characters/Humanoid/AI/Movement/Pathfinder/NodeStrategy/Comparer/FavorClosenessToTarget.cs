using System;

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FavorClosenessToTarget : IComparer<PathfinderNode>
{
    public int Compare(PathfinderNode x, PathfinderNode y)
    {
        int compare = (x.GetHCost()).CompareTo(y.GetHCost());
        return -compare;
    }

}


