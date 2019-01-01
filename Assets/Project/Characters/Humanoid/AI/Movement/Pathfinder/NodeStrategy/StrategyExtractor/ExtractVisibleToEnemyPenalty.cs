using System;

public class ExtractVisibleToEnemyPenalty : IExtractCostFromCostResult
{
    public int Extract(CostResult result){
        int penalty = 0;
        if (!result.IsCoverNode())
        {
            penalty += 500;
        }
        return penalty + result.GetVisiblePenalty();
    }
}

