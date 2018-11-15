using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class DebugCubes : MonoBehaviour
{
    [SerializeField]
    private Transform debugCube1;

    private static DebugCubes instance;
    public DebugCubes()
    {
    }

    void Awake(){
        instance = this;
    }

    public static Transform GetDebugCube1(){
        return instance.debugCube1;
    }
}

