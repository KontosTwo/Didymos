using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class HumanoidPathfindingStrategies {
    [SerializeField]
    private PathfinderCostStrategy flankStrategy;

    public PathfinderCostStrategy GetFlankStrategy(){
        return flankStrategy;
    }
}
