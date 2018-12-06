using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public abstract class DestinationCostStrategy : MonoBehaviour, IDestinationCostCalculator{
    public abstract int GetAdditionalCostAt(Vector3 location);
}
