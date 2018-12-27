using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class FindCandidatesForFlank : CandidateStrategy{
    [Header("Dependencies")]
    [SerializeField]
    private HumanoidAttackPlanner planner;


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
            grid,
            plannerLocation,
            plannerToClosestEnemy, 
            enemyToDirection
        );
    }

    private static List<MapNode> FindCandidatesInSquare(
        Grid grid,
        Vector2 plannerLocation,
        Vector2 plannerToClosestEnemyCorrectLength,
        Vector2 closestEnemyToDirection
    ){
        Vector2 closestEnemyToDirectionNormalized = 
            closestEnemyToDirection.normalized;

        Vector2 closestEnemyToDirectionCorrectLength =
            closestEnemyToDirectionNormalized * plannerToClosestEnemyCorrectLength.magnitude;

        List<Vector2> candidateBoundsPoints = new List<Vector2>();

        Vector2 plannerAtDirection = 
            plannerLocation + closestEnemyToDirectionCorrectLength;
        Vector2 enemyLocation =
            plannerLocation + plannerToClosestEnemyCorrectLength;

        Vector2 enemyAtDirection =
            enemyLocation + closestEnemyToDirectionCorrectLength;

        candidateBoundsPoints.Add(plannerLocation);
        candidateBoundsPoints.Add(plannerAtDirection);
        candidateBoundsPoints.Add(enemyLocation);
        candidateBoundsPoints.Add(enemyAtDirection);

        ConvexPolygon candidateBounds =
            new ConvexPolygon(candidateBoundsPoints);

        float minXOrientedSquare =
            candidateBoundsPoints.Min(v => v.x);
        float minYOrientedSquare =
            candidateBoundsPoints.Min(v => v.y);
        float maxXOrientedSquare =
            candidateBoundsPoints.Max(v => v.x);
        float maxYOrientedSquare =
            candidateBoundsPoints.Max(v => v.y);

        Point startPoint = grid.WorldCoordToNode(
            new Vector2(minXOrientedSquare,minYOrientedSquare)
        );
        Point endPoint = grid.WorldCoordToNode(
            new Vector2(maxXOrientedSquare, maxYOrientedSquare)
        );

        List<MapNode> candidates = new List<MapNode>();
        for(int x = startPoint.x; x < endPoint.x; x++){
            for(int y = startPoint.y; y < endPoint.y; y++){
                Point currentPoint = new Point(x, y);
                Vector3 candidateLocation = 
                    grid.NodeToWorldCoord(
                        currentPoint
                    );
                if (candidateBounds.Contains(
                    candidateLocation.To2D()
                )){
                    candidates.Add(
                        grid.GetNodeAt(
                            currentPoint
                        )
                    );
                }
            }
        }
        return candidates;
    }
}

