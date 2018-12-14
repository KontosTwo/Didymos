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

        ConvexPolygon enemyBounds = attackPlanner.GetEnemyBounds();
        bool tooClose = false;

        if(enemyBounds.GetCount() == 0){
            Debug.Log("WARNING: No enemies for filterforunoccupiedcover");
            return free && node.IsCoverNode();
        }else{
            return free && node.IsCoverNode() && !enemyBounds.WithinRange(
                attackPlanner.GetLocation(),
                enemyAvoidanceRadius
            );
        }
    }
}

