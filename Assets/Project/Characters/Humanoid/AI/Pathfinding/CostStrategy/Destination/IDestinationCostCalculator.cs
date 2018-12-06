using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestinationCostCalculator  {
    int GetAdditionalCostAt(Vector3 location);
}
