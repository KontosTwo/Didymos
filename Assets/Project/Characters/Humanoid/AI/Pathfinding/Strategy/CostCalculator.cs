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
    /*
     * Optimized to reduce raycasting
     * Actually, do not calculate the recommended height to
     * move at. The locations of enemy markers and
     * visible enemies may have changed by the time
     * the strategizer is moving along the calculated path.
     * Also visible enemies should be in the enemies list. 
     * Calculate the height at runtime
     */
    public static float CalculateTotalCoverDisparity(HumanoidVantage strategizer,
                                                     List<HumanoidVantage> enemyVantages,
                                                     Vector3 location){
        float totalDisparity = 0;

        Projectile stratWeaponThreat = strategizer.GetWeaponThreat();
        Vector3 standingAtEnd = location.AddY(strategizer.GetStandingHeight());
        Vector3 kneelingAtEnd = location.AddY(strategizer.GetKneelingHeight());
        Vector3 layingAtEnd = location.AddY(strategizer.GetLayingHeight());

        foreach (HumanoidVantage enemyVantage in enemyVantages)
        {
            Projectile enemyWeaponThreat = enemyVantage.GetWeaponThreat();
            Vector3 enemyStanding = enemyVantage.GetStandingVantage();
            Vector3 enemyKneeling = enemyVantage.GetKneelingVantage();
            Vector3 enemyLaying = enemyVantage.GetLayingVantage();

            Projectile higherWeapon;
            Projectile lowerWeapon;

            Vector3 higherStanding;
            Vector3 higherKneeling;
            Vector3 higherLaying;

            Vector3 lowerStanding;
            Vector3 lowerKneeling;
            Vector3 lowerLaying;

            if(strategizer.HigherThan(enemyVantage)){
                higherStanding = standingAtEnd;
                higherKneeling = kneelingAtEnd;
                higherLaying = layingAtEnd;
                higherWeapon = stratWeaponThreat;

                lowerStanding = enemyStanding;
                lowerKneeling = enemyKneeling;
                lowerLaying = enemyLaying;
                lowerWeapon = enemyWeaponThreat;
            }else{
                higherStanding = enemyStanding;
                higherKneeling = enemyKneeling;
                higherLaying = enemyLaying;
                higherWeapon = enemyWeaponThreat;

                lowerStanding = standingAtEnd;
                lowerKneeling = kneelingAtEnd;
                lowerLaying = layingAtEnd;
                lowerWeapon = stratWeaponThreat;
            }

            TerrainDisparity topToTop = 
                EnvironmentPhysics.CalculateTerrainDisparityBetween(
                    higherWeapon,
                    lowerWeapon,
                    higherStanding,
                    lowerStanding
                );

            if(!topToTop.IsNegligible()){
                /* Use multithreading here */
            }
        }
    }
}
