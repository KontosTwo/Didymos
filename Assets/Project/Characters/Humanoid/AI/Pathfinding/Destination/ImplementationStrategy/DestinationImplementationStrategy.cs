using System;
using System.Collections.Generic;

public abstract class DestinationImplementationStrategy : IImplementDestinationFinder{
    public abstract SortedDictionary<int, MapNode> FindDestinationCandidates(
        Grid grid,
        IDestinationFilterer filterer,
        IDestinationCostCalculator costCalculator
    );
}

