using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FavorCoverAndStrategyCost : IComparer<PathfinderNode>{
    public int Compare(PathfinderNode x, PathfinderNode y){
        int compare = Comparer.NestedCompare(
            new List<Func<PathfinderNode, int>>(){
            

                pn => pn.GetFCost() ,

                //pn => pn.GetStrategyGCost(),
            },
            x,
            y
        );

        return -compare;
    }
}
