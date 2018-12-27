using System;
using UnityEngine;



public struct PathRequest
{
    public Vector3 pathStart;
    public Vector3 pathEnd;
    public float maxLength;
    public PathfinderImplementationStrategy aStarImpl;

    public PathRequest(Vector3 _start,
                       Vector3 _end,
                       float _maxLength,
                       PathfinderImplementationStrategy aStarImpl)
    {
        pathStart = _start;
        pathEnd = _end;
        maxLength = _maxLength;
        this.aStarImpl = aStarImpl;
    }

}
