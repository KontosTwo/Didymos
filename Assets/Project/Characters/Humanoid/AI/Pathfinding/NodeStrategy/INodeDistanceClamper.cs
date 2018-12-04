using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INodeDistanceClamper{
    bool WithinRangeOfStart(PathfinderNode node,
                            int nodeDistance);
}
