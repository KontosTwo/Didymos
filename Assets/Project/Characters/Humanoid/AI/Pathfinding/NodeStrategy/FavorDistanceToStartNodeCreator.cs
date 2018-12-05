using System;

public class FavorDistanceToStartNodeCreator : PathfinderNodeCreator{
    public FavorDistanceToStartNodeCreator(){
    }

    public PathfinderNode CreateNode(Point location, MapNode data){
        return new PathfinderNode(
            location,
            data,
            new FavorClosenessToOrigin(),
            new RestrictByGCost()
        );
    }
}

