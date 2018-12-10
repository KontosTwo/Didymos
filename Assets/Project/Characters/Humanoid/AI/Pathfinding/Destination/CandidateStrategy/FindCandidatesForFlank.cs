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
        ConvexPolygon enemiesLocations = targeter.GetEnemyBounds();

        if(enemiesLocations.GetCount() == 0){
            Debug.LogError("No enemies found!");
            return new List<MapNode>();
        }

        /*
         * Get closest point on enemyPolygon, then try to outflank that
         * 
         */

        float closestDistance = int.MaxValue;
        Vector2 closestEnemy = new Vector2();
        foreach(Vector2 enemyLocation in enemiesLocations.GetVertices()){
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

