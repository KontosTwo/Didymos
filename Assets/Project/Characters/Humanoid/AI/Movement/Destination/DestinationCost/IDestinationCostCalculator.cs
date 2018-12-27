using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestinationCostCalculator  {
    CostResult GetAdditionalCostAt(Vector3 location);
}
