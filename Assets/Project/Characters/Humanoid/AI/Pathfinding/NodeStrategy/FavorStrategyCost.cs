﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FavorStrategyCost : IComparer<PathfinderNode>{
    public int Compare(PathfinderNode x, PathfinderNode y){
        bool xIsCover = x.IsCover();
        bool yIsCover = y.IsCover();
        int compare = 0;

        compare = x.GetStrategyCost().CompareTo(y.GetStrategyCost());
        if (compare == 0){
            if ((xIsCover && yIsCover) || (!xIsCover && !yIsCover)){
                compare = (x.GetGCost()).CompareTo(y.GetGCost());
            }
            else if (xIsCover){
                compare = -1;
            }
            else if (yIsCover){
                compare = 1;
            }
        }
        return -compare;
    }
}
