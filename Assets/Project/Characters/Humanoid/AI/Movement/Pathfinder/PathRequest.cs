using System;
using UnityEngine;



public struct PathRequest
{
    public Vector3 pathStart;
    public Vector3 pathEnd;
    public Action<Vector3[], bool> callback;
    public float maxLength;
    public PathfinderImplementationStrategy aStarImpl;

    public PathRequest(Vector3 _start,
                       Vector3 _end,
                       Action<Vector3[], bool> _callback,
                       float _maxLength,
                       PathfinderImplementationStrategy aStarImpl)
    {
        pathStart = _start;
        pathEnd = _end;
        callback = _callback;
        maxLength = _maxLength;
        this.aStarImpl = aStarImpl;
    }

}
