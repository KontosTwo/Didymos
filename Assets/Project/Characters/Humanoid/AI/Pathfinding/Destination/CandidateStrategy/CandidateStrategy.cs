using System;
using UnityEngine;

using System.Collections.Generic;

public abstract class CandidateStrategy : IFindCandidates{
    public abstract List<MapNode> FindDestinationCandidates(
        Vector3 start,
        Grid grid
    );
}

