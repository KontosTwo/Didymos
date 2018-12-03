
using System;
using UnityEngine;

public struct TerrainDisparity
{
    private static readonly float NEGLIGIBLE_THRESHOLD = 0.5f;

    public float visibleToObserver;
    public float visibleToTarget;
    public float observerHeight;
    public float targetHeight;

    public bool BothHidden(){
        return TargetNegligible()
            && ObserverNegligible();
    }

    public bool BothCompletelyExposed(){
        return Math.Abs(visibleToObserver - targetHeight) < NEGLIGIBLE_THRESHOLD
                   && Math.Abs(visibleToTarget - observerHeight) < NEGLIGIBLE_THRESHOLD;
    }

    public bool BothExposed(){
        return !TargetNegligible()
            && !ObserverNegligible();
    }

    public bool EquallyExposed(){
        return Math.Abs(visibleToTarget - visibleToObserver) < NEGLIGIBLE_THRESHOLD;
    }

    public float ObserverDisparity(){
        return visibleToObserver - visibleToTarget;
    }

    public float TargetDisparity()
    {
        return visibleToTarget - visibleToObserver;
    }

    public void Print(){
        Debug.Log("Visible to observer: " + visibleToObserver);
        Debug.Log("Visible to target: " + visibleToTarget);

    }

    private bool ObserverNegligible(){
        return Math.Abs(visibleToObserver) < 0.1f;
    }

    private bool TargetNegligible(){
        return Math.Abs(visibleToTarget) < 0.1f;
    }
}


