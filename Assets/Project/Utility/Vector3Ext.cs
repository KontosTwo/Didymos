using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class Vector3Ext
{
    

    public static void Set(this Vector3 target, Vector3 newVector){
        Debug.Log("before" + target);

        target.Set(newVector.x, newVector.y, newVector.z);
        Debug.Log("after" + target);
    }

    public static void SetX(this Transform transform,float x){
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
    public static void SetY(this Transform transform, float y)
    {
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
    public static void SetZ(this Transform transform, float z)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
    }

    public static Vector3 SetX(this Vector3 position, float x)
    {
        return new Vector3(x, position.y, position.z);
    }
    public static Vector3 SetY(this Vector3 position, float y)
    {
        return new Vector3(position.x, y, position.z);
    }
    public static Vector3 SetZ(this Vector3 position, float z)
    {
        return new Vector3(position.x, position.y, z);
    }
    public static Vector2 To2D(this Vector3 vector){
        return new Vector2(vector.x, vector.z);
    }
    public static Vector3 AddX(this Vector3 position, float x)
    {
        return new Vector3(position.x + x, position.y, position.z);
    }
    public static Vector3 AddY(this Vector3 position, float y)
    {
        return new Vector3(position.x, position.y + y, position.z);
    }
    public static Vector3 AddZ(this Vector3 position, float z)
    {
        return new Vector3(position.x, position.y, position.z + z);
    }
}

