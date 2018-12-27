using System;

public interface PathfinderNodeCreator{
    PathfinderNode CreateNode(Point location,
                              MapNode data);
}

