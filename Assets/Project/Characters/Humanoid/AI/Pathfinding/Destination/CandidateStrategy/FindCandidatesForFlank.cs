using System;
using System.Collections.Generic;
using UnityEngine;
public class FindCandidatesForFlank : CandidateStrategy{
    [SerializeField]
    private HumanoidTargeter targeter;
    [SerializeField]
    private HumanoidModel strategizer;

    public FindCandidatesForFlank(){

    }




    public override List<MapNode> FindDestinationCandidates(
        Vector3 start,
        Grid grid
    ){
        List<MapNode> candidates = new List<MapNode>();

        Vector2 stratLocation = strategizer.InfoGetCenterBottom().To2D();
        Vector2[] enemiesLocations = targeter.GetEnemyBounds().points;

        if(enemiesLocations.Length == 0){
            Debug.LogError("No enemies found!");
            return new List<MapNode>();
        }

        float closestDistance = int.MaxValue;
        Vector2 closestEnemy = new Vector2();
        foreach(Vector2 enemyLocation in enemiesLocations){
            float distance = Vector2.Distance(
                enemyLocation,
                stratLocation
            );

            if(distance < closestDistance){
                closestDistance = distance;
                closestEnemy = enemyLocation;
            }
        }

        Vector2 stratToEnemy = closestEnemy - stratLocation;
    }
}

