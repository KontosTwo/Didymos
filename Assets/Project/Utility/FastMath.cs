using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FastMath : MonoBehaviour
{
    /*
     * This is to replace Mathf if things 
     *  slow down too much
     */
    private static double[] cos;
    private static double[] sin;

    private static readonly float UNCERTAINTY = 0.1f;

    static FastMath(){
        cos = new double[360];
        sin = new double[360];

        for (int i = 0; i < cos.Length; i ++){
            cos[i] = Math.Cos((i * Math.PI) / 180);
        }
        for (int i = 0; i < sin.Length; i++)
        {
            cos[i] = Math.Sin((i * Math.PI) / 180);
        }
    }

    public static float Hyp(float x1,float y1,float x2,float y2 ){
        return Mathf.Sqrt((float)(Math.Pow(y2 - y1, 2) + Math.Pow(x2 - x1, 2)));
    }

    public static Vector3 CreateVectorCube(float length){
        return new Vector3(length, length, length);
    }

    public static bool FloatCloseToZero(
        float number
    ){
        return Math.Abs(number) < UNCERTAINTY;
    }
    /*
     * Source:
     * https://github.com/setchi/Unity-LineSegmentsIntersection/blob/master/Assets/LineSegmentIntersection/Scripts/Math2d.cs    
     */
    public static bool LineSegmentsIntersection(
        Vector2 p1, 
        Vector2 p2, 
        Vector2 p3, 
        Vector3 p4, 
        out Vector2 intersection
    ){
        intersection = Vector2.zero;

        var d = (p2.x - p1.x) * (p4.y - p3.y) - (p2.y - p1.y) * (p4.x - p3.x);

        if (d.CloseToZero(.01f)){
            return false;
        }

        var u = ((p3.x - p1.x) * (p4.y - p3.y) - (p3.y - p1.y) * (p4.x - p3.x)) / d;
        var v = ((p3.x - p1.x) * (p2.y - p1.y) - (p3.y - p1.y) * (p2.x - p1.x)) / d;

        if (u < 0.0f || u > 1.0f || v < 0.0f || v > 1.0f){
            return false;
        }

        intersection.x = p1.x + u * (p2.x - p1.x);
        intersection.y = p1.y + u * (p2.y - p1.y);

        return true;
    }

    public static Vector2 LineIntersection(Vector2 s1, Vector2 e1, Vector2 s2, Vector2 e2)
    {
        float a1 = e1.y - s1.y;
        float b1 = s1.x - e1.x;
        float c1 = a1 * s1.x + b1 * s1.y;

        float a2 = e2.y - s2.y;
        float b2 = s2.x - e2.x;
        float c2 = a2 * s2.x + b2 * s2.y;

        float delta = a1 * b2 - a2 * b1;
        //If lines are parallel, the result will be (NaN, NaN).
        return delta == 0 ? new Vector2(float.NaN, float.NaN)
            : new Vector2((b2 * c1 - b1 * c2) / delta, (a1 * c2 - a2 * c1) / delta);
    }
}
