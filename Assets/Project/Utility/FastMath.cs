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
}
