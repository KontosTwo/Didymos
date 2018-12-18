using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector2Ext
{
    public static Vector3 To3D(this Vector2 original, float height)
    {
        return new Vector3(
            original.x,
            height,
            original.y
        );
    }

    /*
     * Source
     * https://stackoverflow.com/questions/3120357/get-closest-point-to-a-line
     */

    public static Vector2 GetClosestPointOnLineSegment(this Vector2 P, Vector2 A, Vector2 B)
    {
        Vector2 AP = P - A;       //Vector from A to P   
        Vector2 AB = B - A;       //Vector from A to B  

        float magnitudeAB = AB.magnitude;     //Magnitude of AB vector (it's length squared)     
        float ABAPproduct = Vector2.Dot(AP, AB);    //The DOT product of a_to_p and a_to_b     
        float distance = ABAPproduct / magnitudeAB; //The normalized "distance" from a to your closest point  

        if (distance < 0)     //Check if P projection is over vectorAB     
        {
            return A;

        }
        else if (distance > 1)
        {
            return B;
        }
        else
        {
            return A + AB * distance;
        }
    }

    public static float GetDistanceFromLineSegment(Vector2 P, Vector2 A, Vector2 B)
    {
        Vector2 closest = P.GetClosestPointOnLineSegment(A, B);
        return Vector2.Distance(closest, P);
    }

    public static Vector2 Rotate(this Vector2 v, float degrees){
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;

        v.x = (tx * cos) - (ty * sin);
        v.y = (sin * tx) - (cos * ty);
        return v;

    }
}
                                           
