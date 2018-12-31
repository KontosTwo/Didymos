using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrictByGCost : INodeDistanceClamper {

    public bool WithinRangeOfStart(PathfinderNode node,
                                   int nodeDistance)
    {
        return node.GetPhysicalGCost() < nodeDistance;
    }
}
