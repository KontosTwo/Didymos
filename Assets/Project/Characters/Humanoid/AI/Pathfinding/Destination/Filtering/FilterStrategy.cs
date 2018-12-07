using UnityEngine;
using System.Collections;

public abstract class FilterStrategy : MonoBehaviour, IDestinationFilterer{
    public abstract bool KeepDestination(MapNode node);
}
