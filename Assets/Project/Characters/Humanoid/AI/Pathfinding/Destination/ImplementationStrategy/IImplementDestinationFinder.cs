using System;
using System.Collections.Generic;
using UnityEngine;
public interface IImplementDestinationFinder
{
    SortedDictionary<int, MapNode> FindDestinationCandidates(
        Grid grid,
        IDestinationFilterer filterer,
        IDestinationCostCalculator costCalculator
    );
}

