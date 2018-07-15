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

    public static void SetX(this Vector3 position, float x)
    {
        position.Set(x, position.y, position.z);
    }
    public static void SetY(this Vector3 position, float y)
    {
        position.Set(position.x, y, position.z);
    }
    public static void SetZ(this Vector3 position, float z)
    {
        position.Set(position.x, position.y, z);
    }
    public static Vector2 To2D(this Vector3 vector){
        return new Vector2(vector.x, vector.z);
    }
}

