using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FlankPathfinderStrategy : PathfinderCostStrategy
{
    [SerializeField]
    private HumanoidModel strategizer;
    [SerializeField]
    private HumanoidTargeter targeter;

    [SerializeField]
    private HeightPenaltyAttributes heightData;
    [SerializeField]
    private CoverDisparityPenaltyAttributes coverDisparityData;


    private void Awake()
    {

    }

    private void Start()
    {

    }

    /*
     * Need to cache the result of this
     */
    public override CostResult GetAdditionalCostAt(Vector3 start, Vector3 end)
    {
        var enemyVantages = targeter.GetAllKnownVantages();

        float heightDifference = end.y - start.y;
        float heightPenalty = CostCalculatorHelper.CalculateHeightPenalty(
            heightDifference,
            heightData.goingUpPenalty,
            heightData.climbUpThreshold,
            heightData.climbUpThreshold,
            heightData.goingDownPenalty,
            heightData.climbDownPenalty,
            heightData.climbDownThreshold
        );

        TerrainDisparity totalCoverDisparityPenalty = CostCalculatorHelper.CalculateByMostVisibleToTarget(
            strategizer.InfoGetVantageData(),
            enemyVantages,
            end/*,
            coverDisparityData.exposedPenalty,
            coverDisparityData.coverDisparityPenalty*/
        );

        CostResult result = new CostResult(
            new Tuple<float,float, TerrainDisparity> (
                coverDisparityData.coverDisparityPenalty,
                coverDisparityData.exposedPenalty,
                totalCoverDisparityPenalty
            )
            ,(int)heightPenalty
            , new Tuple<bool, float>(
                Grid.GetMapNodeAt(end).IsCoverNode(),
                coverDisparityData.notCoverPenalty
            )
        );

        //DrawGizmo.AddGizmo(Color.grey, "" + result.CompletelyHidden(), end);

        return result;

    }
}