using System;
using UnityEngine;
public interface PathfinderStrategy
{
    int GetAdditionalCostAt(Vector3 start, Vector3 end);
}

