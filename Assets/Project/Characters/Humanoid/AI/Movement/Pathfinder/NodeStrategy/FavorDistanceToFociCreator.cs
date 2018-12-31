using System;

public class FavorDistanceToFociCreator : PathfinderNodeCreator{
    private Point foci;

    public FavorDistanceToFociCreator(){

    }

    public void SetFoci(Point foci){
        this.foci = foci;
    }

    public PathfinderNode CreateNode(Point location, MapNode data){
        return new PathfinderNode(
                    location,
                    data,
                    new FavorClosenessToLocation(foci),
                    new RestrictByGCost(),
                    new ExtractNothing()
                );
    }
}

