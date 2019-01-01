using System;
using UnityEngine;

public class FavorCoverAndStrategyCostCreator : PathfinderNodeCreator{
    private Vector3 start;
    public FavorCoverAndStrategyCostCreator(Vector3 start){
        this.start = start;
    }

    public PathfinderNode CreateNode(Point location, MapNode data){
        return new PathfinderNode(
                location,
                data,
                new FavorCoverAndStrategyCost(),
                new RestrictByDistanceFromStart(start),
                new ExtractVisibleToEnemyPenalty()
        );
    }
}

