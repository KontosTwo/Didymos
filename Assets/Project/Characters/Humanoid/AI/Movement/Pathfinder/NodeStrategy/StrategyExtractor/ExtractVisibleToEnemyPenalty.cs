using System;

public class ExtractVisibleToEnemyPenalty : IExtractCostFromCostResult
{
    public int Extract(CostResult result){
        return result.GetVisiblePenalty();
    }
}

