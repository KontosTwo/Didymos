using System;

public class CostResult{
    private int coverDisparityPenalty;
    private int terrainPenalty;

    private bool partialCover;

    public CostResult(
        int coverDisparityPenalty,
        bool partialCover,
        int terrainPenalty
    ){
        this.coverDisparityPenalty = coverDisparityPenalty;
        this.terrainPenalty = terrainPenalty;
        this.partialCover = partialCover;
    }

    public bool CompletelyHidden(){
        return coverDisparityPenalty == 0;
    }

    public int GetCoverDisparityPenalty(){
        return coverDisparityPenalty;
    }

    public int GetTerrainPenalty(){
        return terrainPenalty;
    }

    public bool IsPartialCover(){
        return partialCover;
    }
}

