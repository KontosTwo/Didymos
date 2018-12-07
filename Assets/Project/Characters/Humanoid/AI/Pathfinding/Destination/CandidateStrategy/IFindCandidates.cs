using System;
using System.Collections.Generic;
using UnityEngine;
public interface IFindCandidates
{
    List<MapNode> FindDestinationCandidates(
        Vector3 start,
        Grid grid
    );
}

