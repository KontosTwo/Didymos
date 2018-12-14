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


        Vector2 plannerToClosestEnemy;
        if(planner.IsFlankingLeft()){

        }else if(planner.IsFlankingRight()){

        }else{
            Debug.LogError("WARNING: planner isn't even flanking");
        }
    }

    private static List<MapNode> FindCandidatesInSquare(
        Vector2 plannerToClosestEnemy,
        Vector2 closestEnemyToDirection
    ){

    }
}

