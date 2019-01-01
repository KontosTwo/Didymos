using System;
using UnityEngine;
public class RestrictByDistanceFromStart : INodeDistanceClamper{
    private Vector2 origin;
    public RestrictByDistanceFromStart(Vector3 start){
        origin = start.To2D();
    }

    public bool WithinRangeOfStart(PathfinderNode node, int nodeDistance){
        return Vector2.Distance(node.GetLocation().To2D(), origin) < nodeDistance;
    }
}

