using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class RotateAround : MonoBehaviour
{
    public RotateAround()
    {
    }

	public void Update()
	{
        /*
         * 
         * 
         * 
         * This IS working. The position vector is being rotated
         * around its origin with axis (0,1,0).
         * 
         * 
         */
        transform.position = Quaternion.AngleAxis(10, Vector3.up) * transform.position;

	}
}

