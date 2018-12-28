using System;

public class CostResult{

    private TerrainDisparity terrainDisparity;
    private float coverDisparityMultiplier;
    private int heightPenalty;

    public CostResult(
        Tuple<float,TerrainDisparity> disparityPenalty,
        int terrainPenalty
    ){
        terrainDisparity = disparityPenalty.Item2;
        coverDisparityMultiplier = disparityPenalty.Item1;
        heightPenalty = terrainPenalty;
    }

    public bool CompletelyHidden(){
        return terrainDisparity.ObserverHidden();
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
}

