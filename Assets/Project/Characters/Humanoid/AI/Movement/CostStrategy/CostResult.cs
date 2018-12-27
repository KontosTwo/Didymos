using System;

public class CostResult{
    private float coverDisparityPenalty;
    private int terrainPenalty;

    private float visibleToObserver;
    private float visibleToEnemy;

    private bool isPartialCover;
    private bool completelyHidden;

    public CostResult(
        Tuple<float,TerrainDisparity> disparityPenalty,
        int terrainPenalty
    ){
        float disparityMultiplier = disparityPenalty.Item1;
        TerrainDisparity terrainDisparity = disparityPenalty.Item2;
        this.visibleToObserver =
            terrainDisparity.visibleToObserver;
        this.visibleToEnemy =
            terrainDisparity.visibleToTarget;
        this.coverDisparityPenalty = 
            (int)(terrainDisparity.TargetDisparity() 
            * disparityMultiplier);
        this.terrainPenalty = terrainPenalty;
        this.isPartialCover =
            terrainDisparity.ObserverPartiallyExposed();
        this.completelyHidden = terrainDisparity.ObserverHidden();
    }

    public bool CompletelyHidden(){
        return completelyHidden;
    }

    public int GetTerrainPenalty(){
        return terrainPenalty;
    }

    public bool IsPartialCover(){
        return isPartialCover;
    }

    public float GetVisibleToObserver(){
        return visibleToObserver;
    }
    public float GetVisibleToEnemy(){
        return visibleToEnemy;
    }
    public int GetCoverDisparityPenalty(){
        return (int)((visibleToEnemy - visibleToObserver) * coverDisparityPenalty);
    }
}

