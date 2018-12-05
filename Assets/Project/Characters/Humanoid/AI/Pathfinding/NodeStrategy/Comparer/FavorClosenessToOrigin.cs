﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FavorClosenessToOrigin : IComparer<PathfinderNode>{
    public int Compare(PathfinderNode x, PathfinderNode y){
        int compare = (x.GetGCost()).CompareTo(y.GetGCost());
        return -compare;
    }

}