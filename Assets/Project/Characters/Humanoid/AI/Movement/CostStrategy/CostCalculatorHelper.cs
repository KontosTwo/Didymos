using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class CostCalculatorHelper {
    /*
     * Flush the first dictionary KV pair 
     */
   

    public static float CalculateHeightPenalty(float heightDiff,
                                             float goUpPenalty,
                                             float climbUpPenalty, 
                                             float climbUpThreshold,
                                             float goDownPenalty,
                                             float climbDownPenalty,
                                             float climbDownThreshold){
        float heightPenalty = 0;
        if (heightDiff > 0){
            if (heightDiff > climbUpThreshold){
                heightPenalty = heightDiff * climbUpPenalty;
            }
            else{
                heightPenalty = heightDiff * goUpPenalty;
            }
        }
        else{
            if (heightDiff < climbDownThreshold){
                heightPenalty = -heightDiff * climbDownPenalty;
            }
            else{
                heightPenalty = -heightDiff * goDownPenalty;
            }
        }
        return heightPenalty;
    }
    /*
     * Optimized to reduce raycasting
     * Actually, do not calculate the recommended height to
     * move at. The locations of enemy markers and
     * visible enemies may have changed by the time
     * the strategizer is moving along the calculated path.
     * Also visible enemies should be in the enemies list. 
     * Calculate the height at runtime
     */
    public static TerrainDisparity CalculateByMostVisibleToTarget(
        HumanoidVantage strategizer,
        List<HumanoidVantage> enemyVantages,
        Vector3 location
    ){
        List<TerrainDisparity> allDisparities =
            CalculateAllCoverDisparities(
                strategizer,
                enemyVantages,
                location
            );

        return allDisparities.MaxBy(td => td.visibleToTarget);
    }

    private static List<TerrainDisparity> CalculateAllCoverDisparities(
        HumanoidVantage strategizer,
        List<HumanoidVantage> enemyVantages,
        Vector3 location
    ){
        List<TerrainDisparity> totalDisparity = new List<TerrainDisparity>();

        Projectile stratWeaponThreat = strategizer.GetWeaponThreat();
        Vector3 standingAtEnd = location.AddY(strategizer.GetStandingHeight());
        Vector3 kneelingAtEnd = location.AddY(strategizer.GetKneelingHeight());
        Vector3 layingAtEnd = location.AddY(strategizer.GetLayingHeight());

        foreach (HumanoidVantage enemyVantage in enemyVantages){
            Projectile enemyWeaponThreat = enemyVantage.GetWeaponThreat();
            Vector3 enemyStanding = enemyVantage.GetStandingVantage();
            Vector3 enemyKneeling = enemyVantage.GetKneelingVantage();
            Vector3 enemyLaying = enemyVantage.GetLayingVantage();

            TerrainDisparity topToTopDisp =
                EnvironmentPhysics.CalculateTerrainDisparityBetween(
                    stratWeaponThreat,
                    enemyWeaponThreat,
                    standingAtEnd,
                    enemyStanding
                );

            totalDisparity.Add(topToTopDisp);

        }

        return totalDisparity;
    }

    private class ListHumanoidVantageComparer : IEqualityComparer<List<HumanoidVantage>>
    {
        public bool Equals(List<HumanoidVantage> x, List<HumanoidVantage> y)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode(List<HumanoidVantage> obj)
        {
            throw new NotImplementedException();
        }
    }
}
