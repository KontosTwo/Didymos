
using System;

public struct TerrainDisparity
{
    private static readonly float NEGLIGIBLE_THRESHOLD = 0.1f;

    public float visibleToObserver;
    public float visibleToTarget;
    private bool observerCompletelyExposed;
    private bool enemyCompletelyExposed;

    public bool BothHidden(){
        return TargetNegligible()
            && ObserverNegligible();
    }

    public bool BothExposed(){
        return !BothHidden();
    }

    public float ObserverDisparity(){
        return visibleToObserver - visibleToTarget;
    }

    public float TargetDisparity()
    {
        return visibleToTarget - visibleToObserver;
    }

    private bool ObserverNegligible(){
        return Math.Abs(visibleToObserver) < 0.1f;
    }

    private bool TargetNegligible(){
        return Math.Abs(visibleToTarget) < 0.1f;
    }
}


