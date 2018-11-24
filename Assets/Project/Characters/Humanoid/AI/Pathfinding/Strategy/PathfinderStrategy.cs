using System;

public interface PathfinderStrategy
{
    int GetAdditionalCostAt(Point start, Point end);
}

