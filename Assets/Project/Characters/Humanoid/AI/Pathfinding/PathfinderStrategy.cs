using System;

public interface PathfinderStrategy
{
    int GetCostAt(Point start, Point end);
}

