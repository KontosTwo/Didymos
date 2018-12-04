using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FavorClosenessToTarget : IComparer<PathfinderNode>{
    public int Compare(PathfinderNode x, PathfinderNode y){
        int compare = x.FCost.CompareTo(y.FCost);
        if (compare == 0){
            compare = (x.GetGCost()).CompareTo(y.GetGCost());
        }
        return -compare;
    }

}
