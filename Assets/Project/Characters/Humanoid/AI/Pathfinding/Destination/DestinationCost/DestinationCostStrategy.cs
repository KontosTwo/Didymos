using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public abstract class DestinationCostStrategy : MonoBehaviour, IDestinationCostCalculator{
    public abstract CostResult GetAdditionalCostAt(Vector3 location);
}
