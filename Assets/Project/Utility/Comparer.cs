using System;
using System.Collections.Generic;
using UnityEngine;
public class Comparer{
    public static int NestedCompare<T>(List<Func<T,int>> waysToCompare,T a,T b) {
        if(waysToCompare.Count == 0) {
            Debug.LogError("Nested compare has no ways to compare");
            return 0;
        }else {
            int compare = 0;
            foreach(Func<T,int> wayToCompare in waysToCompare) {
                int aValue = wayToCompare(a);
                int bValue = wayToCompare(b);
                compare = aValue.CompareTo(bValue);
                if(compare != 0) {
                    return compare;
                }
            }
            return 0;
        }
    }
}

