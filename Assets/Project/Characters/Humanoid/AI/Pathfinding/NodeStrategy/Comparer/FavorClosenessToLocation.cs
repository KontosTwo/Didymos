using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FavorClosenessToLocation : IComparer<PathfinderNode>{

    private Point location;

    public FavorClosenessToLocation(Point location){
        this.location = location;
    }

    public int Compare(PathfinderNode x, PathfinderNode y){
        return -(PathfinderHelper.GetDistance(x.GetGridCoord(), location) -
                PathfinderHelper.GetDistance(y.GetGridCoord(), location));
    }
}

