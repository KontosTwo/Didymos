using System;
using UnityEngine;

public class CostResult{

    private TerrainDisparity terrainDisparity;
    private float coverDisparityMultiplier;
    private float exposedPenalty;
    private int heightPenalty;

    public CostResult(
        Tuple<float,float,TerrainDisparity> disparityPenalty,
        int terrainPenalty
    ){
        terrainDisparity = disparityPenalty.Item3;
        coverDisparityMultiplier = disparityPenalty.Item1;
        exposedPenalty = disparityPenalty.Item2;
        heightPenalty = terrainPenalty;
    }

    public bool CompletelyHidden(){
        return terrainDisparity.ObserverHidden();
    }

    public bool CompletelyExposed(){
        return terrainDisparity.ObserverCompletelyExposed();
    }

    public int GetTerrainPenalty(){
        return heightPenalty;
    }

    public bool IsPartialCover(){
        return terrainDisparity.ObserverPartiallyExposed();
    }

    public float GetVisibleToObserver(){
        return terrainDisparity.visibleToObserver;
    }
    public float GetVisibleToEnemy(){
        return terrainDisparity.visibleToTarget;
    }
    public int GetCoverDisparityPenalty(){
        return (int)((GetVisibleToEnemy() 
                - GetVisibleToObserver()) 
                * coverDisparityMultiplier);
    }

    public int GetVisiblePenalty(){
        float penalty = GetVisibleToEnemy();
        penalty += !CompletelyHidden() ? exposedPenalty : 0;
        return (int)(penalty * coverDisparityMultiplier);
    }
}

