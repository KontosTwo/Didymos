using System;
using System.Collections.Generic;

public class SortByCost : SorterStrategy
{
    public override int Compare(CostResult x, CostResult y)
    {
        return -(x.GetCoverDisparityPenalty() - y.GetCoverDisparityPenalty());
    }
}

