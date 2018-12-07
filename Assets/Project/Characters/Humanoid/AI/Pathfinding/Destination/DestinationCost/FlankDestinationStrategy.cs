using UnityEngine;
using System.Collections;

public class FlankDestinationStrategy : DestinationCostStrategy{
    [SerializeField]
    private HumanoidModel strategizer;
    [SerializeField]
    private HumanoidTargeter targeter;

    [SerializeField]
    private CoverDisparityPenaltyAttributes coverDisparityData;

    public override CostResult GetAdditionalCostAt(Vector3 location){
        var enemyVantages = targeter.GetAllKnownVantages();

        float totalCoverDisparityPenalty = CostCalculatorHelper.CalculateTotalCoverDisparity(
            strategizer.InfoGetVantageData(),
            enemyVantages,
            location,
            coverDisparityData.exposedPenalty,
            coverDisparityData.coverDisparityPenalty
        );

        CostResult result = new CostResult(
            (int)totalCoverDisparityPenalty,
            totalCoverDisparityPenalty > 0 &&
            totalCoverDisparityPenalty < coverDisparityData.exposedPenalty,
            0
        );

        return result;
    }
}
