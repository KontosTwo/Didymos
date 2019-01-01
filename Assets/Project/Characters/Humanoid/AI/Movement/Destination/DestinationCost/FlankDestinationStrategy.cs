using UnityEngine;
using System.Collections;
using System;

public class FlankDestinationStrategy : DestinationCostStrategy{
    [SerializeField]
    private HumanoidModel strategizer;
    [SerializeField]
    private HumanoidTargeter targeter;

    [SerializeField]
    private CoverDisparityPenaltyAttributes coverDisparityData;

    public override CostResult GetAdditionalCostAt(Vector3 location){
        var enemyVantages = targeter.GetAllKnownVantages();

        TerrainDisparity totalCoverDisparityPenalty = 
            CostCalculatorHelper.CalculateByMostVisibleToTarget(
                strategizer.InfoGetVantageData(),
                enemyVantages,
                location
            );

        CostResult result = new CostResult(
            new Tuple<float,float,TerrainDisparity>(
                coverDisparityData.coverDisparityPenalty,
                coverDisparityData.exposedPenalty,
                totalCoverDisparityPenalty
            ),
            0,
            new Tuple<bool, float>(
                Grid.GetMapNodeAt(location).IsCoverNode(),
                coverDisparityData.notCoverPenalty
            )

        );

        return result;
    }
}
