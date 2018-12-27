using UnityEngine;
using System.Collections;

public abstract class SorterStrategy : MonoBehaviour , IDestinationSorter
{
    public abstract int Compare(CostResult x, CostResult y);
}
