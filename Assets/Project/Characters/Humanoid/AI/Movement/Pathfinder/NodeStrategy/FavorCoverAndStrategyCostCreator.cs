using System;

public class FavorCoverAndStrategyCostCreator : PathfinderNodeCreator{
    public FavorCoverAndStrategyCostCreator()
    {
    }

    public PathfinderNode CreateNode(Point location, MapNode data){
        return new PathfinderNode(
                location,
                data,
                new FavorCoverAndStrategyCost(),
                new RestrictByGCost(),
                new ExtractVisibleToEnemyPenalty()
        );
    }
}

