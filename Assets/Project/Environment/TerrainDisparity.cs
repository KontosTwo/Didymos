
using System;

public struct TerrainDisparity
{
    private static readonly float NEGLIGIBLE_THRESHOLD = 0.1f;

    public float visibleToObserver;
    public float visibleToTarget;

    public bool IsNegligible(){
        return Math.Abs(visibleToTarget) < 0.1f
                   && Math.Abs(visibleToObserver) < 0.1f;
    }
}


