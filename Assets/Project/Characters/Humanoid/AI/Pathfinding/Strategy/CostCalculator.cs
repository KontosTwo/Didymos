using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
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

            float worstDisparity = int.MaxValue;

            TerrainDisparity topToTopDisp =
                EnvironmentPhysics.CalculateTerrainDisparityBetween(
                    stratWeaponThreat,
                    enemyWeaponThreat,
                    standingAtEnd,
                    enemyStanding
                );

            if(!topToTopDisp.IsNegligible()){
                worstDisparity = topToTopDisp.ObserverDisparity();

                TerrainDisparity topToMidDisp =
                    EnvironmentPhysics.CalculateTerrainDisparityBetween(
                        stratWeaponThreat,
                        enemyWeaponThreat,
                        standingAtEnd,
                        enemyKneeling
                    );

                if (!topToMidDisp.IsNegligible()
                        && topToMidDisp.ObserverDisparity() < worstDisparity){
                    worstDisparity = topToMidDisp.ObserverDisparity();

                    TerrainDisparity topToBotDisp =
                        EnvironmentPhysics.CalculateTerrainDisparityBetween(
                            stratWeaponThreat,
                            enemyWeaponThreat,
                            standingAtEnd,
                            enemyLaying
                        );

                    if (!topToBotDisp.IsNegligible()
                        && topToBotDisp.ObserverDisparity() < worstDisparity){
                        worstDisparity = topToBotDisp.ObserverDisparity();
                    }
                }
            }
            else{
                continue;
            }

            TerrainDisparity midToTopDisp =
                EnvironmentPhysics.CalculateTerrainDisparityBetween(
                    stratWeaponThreat,
                    enemyWeaponThreat,
                    kneelingAtEnd,
                    enemyStanding
                );

            if (!midToTopDisp.IsNegligible() && 
                midToTopDisp.ObserverDisparity() < worstDisparity){
                worstDisparity = midToTopDisp.ObserverDisparity();

                TerrainDisparity midToMidDisp =
                    EnvironmentPhysics.CalculateTerrainDisparityBetween(
                        stratWeaponThreat,
                        enemyWeaponThreat,
                        kneelingAtEnd,
                        enemyKneeling
                    );

                if (!midToMidDisp.IsNegligible()
                        && midToMidDisp.ObserverDisparity() < worstDisparity){
                    worstDisparity = midToMidDisp.ObserverDisparity();

                    TerrainDisparity midToBotDisp =
                        EnvironmentPhysics.CalculateTerrainDisparityBetween(
                            stratWeaponThreat,
                            enemyWeaponThreat,
                            kneelingAtEnd,
                            enemyLaying
                        );

                    if (!midToBotDisp.IsNegligible()
                        && midToBotDisp.ObserverDisparity() < worstDisparity){
                        worstDisparity = midToBotDisp.ObserverDisparity();
                    }
                }
            }
            else{
                continue;
            }

            TerrainDisparity botToTopDisp =
                EnvironmentPhysics.CalculateTerrainDisparityBetween(
                    stratWeaponThreat,
                    enemyWeaponThreat,
                    layingAtEnd,
                    enemyStanding
                );

            if (!botToTopDisp.IsNegligible() &&
                botToTopDisp.ObserverDisparity() < worstDisparity){
                worstDisparity = botToTopDisp.ObserverDisparity();

                TerrainDisparity botToMidDisp =
                    EnvironmentPhysics.CalculateTerrainDisparityBetween(
                        stratWeaponThreat,
                        enemyWeaponThreat,
                        layingAtEnd,
                        enemyKneeling
                    );

                if (!botToMidDisp.IsNegligible()
                        && botToMidDisp.ObserverDisparity() < worstDisparity){
                    worstDisparity = botToMidDisp.ObserverDisparity();

                    TerrainDisparity botToBotDisp =
                        EnvironmentPhysics.CalculateTerrainDisparityBetween(
                            stratWeaponThreat,
                            enemyWeaponThreat,
                            layingAtEnd,
                            enemyLaying
                        );

                    if (!botToBotDisp.IsNegligible()
                        && botToBotDisp.ObserverDisparity() < worstDisparity){
                        worstDisparity = botToBotDisp.ObserverDisparity();
                    }
                }
            }
        }
        // the lower the disparity, the higher the penalty
        return -totalDisparity;
    }
}
