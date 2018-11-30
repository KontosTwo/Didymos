using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public abstract class PathfinderStrategy : MonoBehaviour, PathfinderStrategy {
    public abstract int GetAdditionalCostAt(Vector3 start, Vector3 end);
}
