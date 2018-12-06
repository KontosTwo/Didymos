using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPathfinderCostCalculator  {
    int GetAdditionalCostAt(Vector3 start, Vector3 end);
}
