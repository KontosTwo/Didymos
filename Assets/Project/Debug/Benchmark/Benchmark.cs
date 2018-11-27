using System;
using System.Diagnostics;
using Unity;
using UnityEngine;

public class Benchmark
{
    public delegate void Action();

    public static void Test(Action action){
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        action.Invoke();
        stopwatch.Stop();
        UnityEngine.Debug.Log(stopwatch.ElapsedMilliseconds);
    }
}

