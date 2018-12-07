using System;
using UnityEngine;
using System.Collections.Generic;
public class FilterForUnoccupiedCover : IDestinationFilterer{
    [SerializeField]
    private HumanoidTargeter targeter;

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

        List<Vector2> enemyBounds = targeter.GetEnemyBounds();
        Vector2[] enemyLocations = enemyBounds.points;
        bool tooClose = false;

        if(enemyBounds.Count == 0){
            return free && node.IsCoverNode();
        }else if(enemyBounds.Count == 1)



        foreach(Vector2 enemyLocation in enemyLocations){
            if(Vector2.Distance(nodeLocation2D,enemyLocation) 
               < enemyAvoidanceRadius){
                tooClose = true;
                break;
            }
        }
        return !tooClose && free && node.IsCoverNode();
    }
}

