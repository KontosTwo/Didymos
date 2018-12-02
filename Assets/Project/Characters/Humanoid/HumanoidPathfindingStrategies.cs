using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class HumanoidPathfindingStrategies {
    [SerializeField]
    private PathfinderStrategy flankStrategy;

    public PathfinderStrategy GetFlankStrategy(){
        return flankStrategy;
    }
}
