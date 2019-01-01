using System;
using UnityEngine;

public class CostResult{

    private TerrainDisparity terrainDisparity;
    private float coverDisparityMultiplier;
    private float exposedPenalty;
    private float notCoverPenalty;

    private int heightPenalty;
    private bool isCover;

    public CostResult(
        Tuple<float,float,TerrainDisparity> disparityPenalty,
        int terrainPenalty,
        Tuple<bool,float> coverData
    ){
        terrainDisparity = disparityPenalty.Item3;
        coverDisparityMultiplier = disparityPenalty.Item1;
        exposedPenalty = disparityPenalty.Item2;
        heightPenalty = terrainPenalty;
        isCover = coverData.Item1;
        notCoverPenalty = coverData.Item2;
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

    public int GetFlankingPenalty(){
        float penalty = GetVisibleToEnemy();
        penalty += !(IsCoverNode() && !CompletelyExposed())
            ? notCoverPenalty
            : 0;

        penalty += !CompletelyHidden() ? exposedPenalty : 0;
        return (int)(penalty * coverDisparityMultiplier);
    }

    public bool IsCoverNode(){
        return isCover;
    }
}

