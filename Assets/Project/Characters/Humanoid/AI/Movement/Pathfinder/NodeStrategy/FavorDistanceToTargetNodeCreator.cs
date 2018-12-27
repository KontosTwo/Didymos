using System;

public class FavorDistanceToTargetNodeCreator : PathfinderNodeCreator{
   

    public PathfinderNode CreateNode(Point location, MapNode data){
        return new PathfinderNode(
                location,
                data,
                new FavorClosenessToTarget(),
                new RestrictByGCost()
        );
    }
}

