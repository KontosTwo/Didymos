
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
        return TargetHidden()
            && ObserverHidden();
    }

    public bool TargetHidden(){
        return Math.Abs(visibleToObserver) < 0.5f;
    }

    public bool ObserverHidden(){
        return Math.Abs(visibleToTarget) < 0.5f;
    }

    public bool ObserverPartiallyExposed(){
        return !ObserverHidden() 
            && !ObserverCompletelyExposed();
    }

    public bool TargetPartiallyExposed(){
        return !TargetHidden()
            && !TargetCompletelyExposed();
    }


    public bool BothCompletelyExposed(){
        return ObserverCompletelyExposed()
            && TargetCompletelyExposed();
    }

    public bool ObserverCompletelyExposed(){
        return Math.Abs(visibleToTarget - observerHeight) 
            < NEGLIGIBLE_THRESHOLD;
    }

    public bool TargetCompletelyExposed(){
        return Math.Abs(visibleToObserver - targetHeight) 
            < NEGLIGIBLE_THRESHOLD;
    }



    public bool BothExposed(){
        return ObserverCompletelyExposed()
            && TargetCompletelyExposed();
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

}


