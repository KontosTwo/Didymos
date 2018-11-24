using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlankStrategy : PathfinderStrategy
{
    private Grid grid;
    private HumanoidVantage strategizer;
    private List<HumanoidVantage> enemies;

    private static readonly float GOING_UP_PENALTY = 1f;
    private static readonly float GOING_DOWN_PENALTY = 1f;
    private static readonly float CLIMB_UP_PENALTY = 1f;
    private static readonly float CLIMB_DOWN_PENALTY = 1f;
    private static readonly float CLIMB_UP_THRESHHOLD = 1f;
    private static readonly float CLIMB_DOWN_THRESHHOLD = 1f;

    private static readonly float COVER_DISPARITY_PENALTY = 1f;
    private static readonly float NEGLIGIBLE_COVER_DISPARITY = 0.1f;

    public FlankStrategy(Grid grid,
                         HumanoidVantage s,
                         List<HumanoidVantage> e)
    {
        this.grid = grid;
        this.strategizer = s;
        this.enemies = e;
    }
    /*
     * Need to cache the result of this
     * Also wrap chunks of the logic in static functions
     */
    public int GetAdditionalCostAt(Point start, Point end)
    {
        Vector3 startVector = grid.NodeToWorldCoord(start);
        Vector3 endVector = grid.NodeToWorldCoord(end);

        float heightDifference = endVector.y - startVector.y;
        float heightPenalty = 0;
        if(heightDifference > 0){
            if(heightDifference > CLIMB_UP_THRESHHOLD){
                heightPenalty = heightDifference * CLIMB_UP_PENALTY;
            }else{
                heightPenalty = heightDifference * GOING_UP_PENALTY;
            }
        }else{
            if(heightDifference < CLIMB_DOWN_THRESHHOLD){
                heightPenalty = heightDifference * CLIMB_DOWN_PENALTY;
            }else{
                heightPenalty = heightDifference * GOING_DOWN_PENALTY;
            }
        }

        float totalCoverDisparity = 0;
        Vector3 standingAtEnd = endVector.AddY(strategizer.GetStandingHeight());
        Vector3 kneelingAtEnd = endVector.AddY(strategizer.GetKneelingHeight());
        Vector3 layingAtEnd = endVector.AddY(strategizer.GetLayingHeight());
        foreach(HumanoidVantage enemyVantage in enemies){
            //float standingDisparity = EnvironmentPhysics.CalculateTerrainDisparityBetween   
        }
        return 0;
    }
}