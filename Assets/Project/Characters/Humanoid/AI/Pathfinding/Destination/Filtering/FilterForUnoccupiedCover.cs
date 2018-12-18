using System;
using UnityEngine;
using System.Collections.Generic;
public class FilterForUnoccupiedCover : IDestinationFilterer{
    [SerializeField]
    private HumanoidAttackPlanner attackPlanner;

    [SerializeField]
    private float occupancyRadius;
    [SerializeField]
    private float enemyAvoidanceRadius;

    public FilterForUnoccupiedCover(){

    }

    public bool KeepDestination(MapNode node){

        List<AIHumanoidModel> allies = HumanoidStore.GetEnemiesModels();
        bool free = true;
        Vector3 nodeLocation = node.GetLocation();
        Vector2 nodeLocation2D = nodeLocation.To2D();
        foreach(AIHumanoidModel ally in allies){
            if(Vector3.Distance(ally.InfoGetCenterBottom(),nodeLocation)
               < occupancyRadius){
                free = false;
                break;
            }
        }

        return free && node.IsCoverNode() && attackPlanner.TooCloseToEnemyBounds(
            enemyAvoidanceRadius
        );

    }
}

