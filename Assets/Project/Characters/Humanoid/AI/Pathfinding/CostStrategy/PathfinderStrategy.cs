using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public abstract class PathfinderStrategy : MonoBehaviour, ICostCalculator{
    public abstract int GetAdditionalCostAt(Vector3 start, Vector3 end);
}
