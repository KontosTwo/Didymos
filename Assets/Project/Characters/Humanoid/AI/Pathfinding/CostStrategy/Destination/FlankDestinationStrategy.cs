using UnityEngine;
using System.Collections;

public class FlankDestinationStrategy : DestinationCostStrategy{
    [SerializeField]
    private HumanoidModel strategizer;
    [SerializeField]
    private HumanoidTargeter targeter;

    [SerializeField]
    private CoverDisparityPenaltyAttributes coverDisparityData;

    public override int GetAdditionalCostAt(Vector3 location){
        var enemyVantages = targeter.GetAllKnownVantages();

        float totalCoverDisparityPenalty = CostCalculatorHelper.CalculateTotalCoverDisparity(
            strategizer.InfoGetVantageData(),
            enemyVantages,
            location,
            coverDisparityData.exposedPenalty,
            coverDisparityData.coverDisparityPenalty
        );



        return (int)(totalCoverDisparityPenalty);
    }
}
