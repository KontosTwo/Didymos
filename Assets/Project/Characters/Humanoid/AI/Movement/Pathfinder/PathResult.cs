using System;
using UnityEngine;
public struct PathResult
{
    public Vector3[] path;
    public bool success;

    public PathResult(Vector3[] path, bool success)
    {
        this.path = path;
        this.success = success;
    }

}