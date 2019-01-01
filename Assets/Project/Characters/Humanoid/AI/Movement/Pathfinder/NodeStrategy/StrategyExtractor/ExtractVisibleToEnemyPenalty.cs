using System;

public class ExtractVisibleToEnemyPenalty : IExtractCostFromCostResult
{
    public int Extract(CostResult result){
        int penalty = 0;
        return penalty + result.GetFlankingPenalty();
    }
}

