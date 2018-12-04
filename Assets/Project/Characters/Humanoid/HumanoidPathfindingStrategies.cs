using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class HumanoidPathfindingStrategies {
    [SerializeField]
    private CostStrategy flankStrategy;

    public CostStrategy GetFlankStrategy(){
        return flankStrategy;
    }
}
