using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPathfinderCostCalculator  {
    CostResult GetAdditionalCostAt(Vector3 start, Vector3 end);
}
