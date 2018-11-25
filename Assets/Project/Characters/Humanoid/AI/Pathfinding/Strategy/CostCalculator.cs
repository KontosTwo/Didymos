using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostCalculator {

    public static float CalculateHeightPenalty(float heightDiff,
                                             float goUpPenalty,
                                             float climbUpPenalty, 
                                             float climbUpThreshold,
                                             float goDownPenalty,
                                             float climbDownPenalty,
                                             float climbDownThreshold)
    {
        float heightPenalty = 0;
        if (heightDiff > 0)
        {
            if (heightDiff > climbUpThreshold)
            {
                heightPenalty = heightDiff * climbUpPenalty;
            }
            else
            {
                heightPenalty = heightDiff * goUpPenalty;
            }
        }
        else
        {
            if (heightDiff < climbDownThreshold)
            {
                heightPenalty = heightDiff * climbDownPenalty;
            }
            else
            {
                heightPenalty = heightDiff * goDownPenalty;
            }
        }
        return heightPenalty;
    }

    public static float CalculateTotalCoverDisparity(HumanoidVantage strategizer,
                                                     List<HumanoidVantage> enemyVantages,
                                                     Vector3 location,
                                                     float coverDisparityPenalty,
                                                    float negligibleCoverThreshold){
        Vector3 standingAtEnd = location.AddY(strategizer.GetStandingHeight());
        Vector3 kneelingAtEnd = location.AddY(strategizer.GetKneelingHeight());
        Vector3 layingAtEnd = location.AddY(strategizer.GetLayingHeight());
        foreach (HumanoidVantage enemyVantage in enemyVantages)
        {
            //float standingDisparity = EnvironmentPhysics.CalculateTerrainDisparityBetween   
        }
    }
}
