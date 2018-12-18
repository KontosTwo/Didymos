using System;
using System.Collections.Generic;
using UnityEngine;
public class FindCandidatesForFlank : CandidateStrategy{
    [Header("Dependencies")]
    [SerializeField]
    private HumanoidAttackPlanner planner;

    [Header("Fields")]
    private float flankingRadius;

    public FindCandidatesForFlank(){

    }



    /*
     * planner needs to have calculated its next action first
     */
    public override List<MapNode> FindDestinationCandidates(
        Vector3 start,
        Grid grid
    ){
        Vector2 closestEnemyLocation = 
            planner.GetClosestEnemyLocation();
        Vector2 plannerLocation = planner.GetLocation();
        Vector2 plannerToClosestEnemy = 
            closestEnemyLocation - plannerLocation;
        Vector2 enemyToDirection = new Vector2();

        if(planner.IsFlankingLeft()){
            enemyToDirection = plannerToClosestEnemy.Rotate(90);
        }else if(planner.IsFlankingRight()){
            enemyToDirection = plannerToClosestEnemy.Rotate(-90);
        }
        else
        {
            Debug.LogError("planner isn't even flanking");
        }
        return FindCandidatesInSquare(
            plannerLocation,
            plannerToClosestEnemy, 
            enemyToDirection
        );
    }

    private static List<MapNode> FindCandidatesInSquare(
        Vector2 planner,
        Vector2 plannerToClosestEnemy,
        Vector2 closestEnemyToDirection
    ){
        Vector2 closestEnemyToDirectionNormalized = 
            closestEnemyToDirection.normalized;

        Vector2 closestEnemyToDirectionCorrectLength =
            closestEnemyToDirectionNormalized * plannerToClosestEnemy.magnitude;

        List<Vector2> points = new List<Vector2>();
        points.Add(planner);
        points.Add(planner + plannerToClosestEnemy);
        points.Add(planner + plannerToClosestEnemy + 
                   closestEnemyToDirectionCorrectLength);
        points.Add(planner + closestEnemyToDirectionCorrectLength);

    }
}

