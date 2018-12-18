using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
/*
 *  Call find next action to update this humanoid's
 *  attack strategy
 */
public class HumanoidAttackPlanner : MonoBehaviour{
    [Header("Dependencies")]
    [SerializeField]
    private HumanoidModel planner;
    [SerializeField]
    private HumanoidTargeter targeter;

    [Header("Fields")]
    [SerializeField]
    private int flankParticipationLimit;
    [SerializeField]
    private int relocateParticipationLimit;
    [SerializeField]
    private int suppressiveFireParticipationLimit;


    private AttackAction attackAction;
    private List<EnemyTarget> boundingTargets;
    private List<EnemyMarker> boundingMarkers;
    private ConvexPolygon enemyBounds;
    private bool hasTarget;
    

    private void Awake(){
        HumanoidAttackPlannerCommunications.AddPlanner(
            this
        );
        attackAction = AttackAction.NONE;
        hasTarget = false;
    }

    public bool TooCloseToEnemyBounds(float limit){

        return enemyBounds.WithinRange(
            planner.InfoGetCenterBottom().To2D(),
            limit
        );
    }

    public Vector2 GetClosestEnemyLocation(){
        float closestEnemyDistance = int.MaxValue;
        Vector2 closestLocation = new Vector2();
        Vector2 plannerLocation = planner.InfoGetCenterBottom().To2D();
        foreach(Vector2 enemyLocation in enemyBounds.GetVertices()){
            float currentDistance =
                Vector2.Distance(plannerLocation, enemyLocation);
            if(currentDistance < closestEnemyDistance){
                closestEnemyDistance = currentDistance;
                closestLocation = enemyLocation;
            }
        }
        return closestLocation;
    }

    public Vector2 GetLocation(){
        return planner.InfoGetCenterBottom();
    }

    public bool IsAttacking(){
        return attackAction != AttackAction.NONE;
    }

    public bool IsFlankingRight(){
        return attackAction == AttackAction.FLANK_RIGHT;
    }

    public bool IsFlankingLeft(){
        return attackAction == AttackAction.FLANK_LEFT;
    }

    public bool IsRelocatingRight(){
        return attackAction == AttackAction.RELOCATE_RIGHT;
    }

    public bool IsRelocatingLeft(){
        return attackAction == AttackAction.RELOCATE_LEFT;
    }

    public bool IsSuppressingFire(){
        return attackAction == AttackAction.SUPPRESS_FIRE;
    }

    public void FindNextAction(){

        var boundingEnemies = targeter.GetBoundingEnemies();
        var boundingHiddenEnemies = boundingEnemies.Item1;
        var boundingVisibleEnemies = boundingEnemies.Item2;

        boundingMarkers = boundingHiddenEnemies.ToList();
        boundingTargets = boundingVisibleEnemies.ToList();

        var currentMarkersLocations = boundingMarkers.Select(m =>{
            return m.GetLocation().To2D();
        });
        var currentTargetsLocations = boundingTargets.Select(m => {
            return m.GetLocation().To2D();
        });

        enemyBounds =
            new ConvexPolygon(
                currentMarkersLocations.Concat(
                    currentTargetsLocations
                ).ToList()
            );

        float closestHiddenEnemyDistance = float.MaxValue;
        EnemyMarker closestHiddenEnemy = null;
        foreach(var hiddenEnemy in boundingHiddenEnemies){
            float currentDistanceFromPlanner =
                Vector2.Distance(
                    hiddenEnemy.GetLocation().To2D(),
                    planner.InfoGetCenterBottom().To2D()
                );
            if (currentDistanceFromPlanner < closestHiddenEnemyDistance){
                closestHiddenEnemy = hiddenEnemy;
                closestHiddenEnemyDistance = currentDistanceFromPlanner;
            }
        }

        float closestVisibleEnemyDistance = float.MaxValue;
        EnemyTarget closestVisibleEnemy = null;
        foreach (var visibleEnemy in boundingVisibleEnemies){
            float currentDistanceFromPlanner =
                Vector2.Distance(
                    visibleEnemy.GetLocation().To2D(),
                    planner.InfoGetCenterBottom().To2D()
                );
            if (currentDistanceFromPlanner < closestVisibleEnemyDistance){
                closestVisibleEnemy = visibleEnemy;
                closestVisibleEnemyDistance = currentDistanceFromPlanner;
            }
        }

        if(closestVisibleEnemy != null){
            ProcessVisibleEnemy(closestVisibleEnemy);
        }
        else if(closestHiddenEnemy != null){
            ProcessHiddenEnemy(closestHiddenEnemy);
        }else{
            attackAction = AttackAction.NONE;
        }
    }

    private void ProcessVisibleEnemy(
        EnemyTarget enemy
    ){
        int leftFlankers =
            HumanoidAttackPlannerCommunications.GetLeftFlankersOnVisibleEnemy(
                enemy
            );
        int rightFlankers =
            HumanoidAttackPlannerCommunications.GetRightFlankersOnVisibleEnemy(
                enemy
            );
        int suppressors =
            HumanoidAttackPlannerCommunications.GetSuppressorsOnVisibleEnemy(
                enemy
            );

        bool canFlankLeft = leftFlankers < flankParticipationLimit;
        bool canFlankRight = rightFlankers < flankParticipationLimit;
        bool canSuppress = suppressors < suppressiveFireParticipationLimit;

        CanDoAction flankLeftResult = new CanDoAction(
            canFlankLeft,
            leftFlankers,
            AttackAction.FLANK_LEFT
        );
        CanDoAction flankRightResult = new CanDoAction(
            canFlankRight,
            rightFlankers,
            AttackAction.FLANK_RIGHT
        );
        CanDoAction suppressorResult = new CanDoAction(
            canSuppress,
            suppressors,
            AttackAction.SUPPRESS_FIRE
        );
        List<CanDoAction> potentialActions = new List<CanDoAction>();

        potentialActions.Add(flankLeftResult);
        potentialActions.Add(flankRightResult);
        potentialActions.Add(suppressorResult);

        potentialActions.OrderBy(pa =>{
            return pa.number;
        });

        foreach(CanDoAction action in potentialActions){
            if(action.canDo){
                attackAction = action.action;
                switch (action.action)
                {
                    case AttackAction.FLANK_LEFT:
                        HumanoidAttackPlannerCommunications.FlankLeftVisibleEnemy(
                            enemy
                        );
                        break;
                    case AttackAction.FLANK_RIGHT:
                        HumanoidAttackPlannerCommunications.FlankRightVisibleEnemy(
                            enemy
                        );
                        break;
                    case AttackAction.SUPPRESS_FIRE:
                        HumanoidAttackPlannerCommunications.SuppressVisibleEnemy(
                            enemy
                        );
                        break;
                }
                return;
            }
        }

        int rightRelocators =
            HumanoidAttackPlannerCommunications.GetRightRelocators().Count;

        int leftRelocators =
            HumanoidAttackPlannerCommunications.GetLeftRelocators().Count;

        if (rightRelocators < leftRelocators){
            if (rightRelocators < relocateParticipationLimit){
                attackAction = AttackAction.RELOCATE_RIGHT;
            }
        }
        else{
            if (leftRelocators < relocateParticipationLimit){
                attackAction = AttackAction.RELOCATE_LEFT;
            }
        }
    }

    private void ProcessHiddenEnemy(
        EnemyMarker enemy
    ){
        int leftFlankers =
            HumanoidAttackPlannerCommunications.GetLeftFlankersOnHiddenEnemy(
                enemy
            );
        int rightFlankers =
            HumanoidAttackPlannerCommunications.GetRightFlankersOnHiddenEnemy(
                enemy
            );
        int suppressors =
            HumanoidAttackPlannerCommunications.GetSuppressorsOnHiddenEnemy(
                enemy
            );

        bool canFlankLeft = leftFlankers < flankParticipationLimit;
        bool canFlankRight = rightFlankers < flankParticipationLimit;
        bool canSuppress = suppressors < suppressiveFireParticipationLimit;

        CanDoAction flankLeftResult = new CanDoAction(
            canFlankLeft,
            leftFlankers,
            AttackAction.FLANK_LEFT
        );
        CanDoAction flankRightResult = new CanDoAction(
            canFlankRight,
            rightFlankers,
            AttackAction.FLANK_RIGHT
        );
        CanDoAction suppressorResult = new CanDoAction(
            canSuppress,
            suppressors,
            AttackAction.SUPPRESS_FIRE
        );
        List<CanDoAction> potentialActions = new List<CanDoAction>();

        potentialActions.Add(flankLeftResult);
        potentialActions.Add(flankRightResult);
        potentialActions.Add(suppressorResult);

        potentialActions.OrderBy(pa => {
            return pa.number;
        });

        foreach (CanDoAction action in potentialActions)
        {
            if (action.canDo)
            {
                attackAction = action.action;
                switch(action.action){
                    case AttackAction.FLANK_LEFT:
                        HumanoidAttackPlannerCommunications.FlankLeftHiddenEnemy(
                            enemy
                        );
                        break;
                    case AttackAction.FLANK_RIGHT:
                        HumanoidAttackPlannerCommunications.FlankRightHiddenEnemy(
                            enemy
                        );
                        break;
                    case AttackAction.SUPPRESS_FIRE:
                        HumanoidAttackPlannerCommunications.SuppressHiddenEnemy(
                            enemy
                        );
                        break;
                }
                return;
            }
        }

        int rightRelocators =
            HumanoidAttackPlannerCommunications.GetRightRelocators().Count;

        int leftRelocators =
            HumanoidAttackPlannerCommunications.GetLeftRelocators().Count;

        if (rightRelocators < leftRelocators){
            if(rightRelocators < relocateParticipationLimit){
                attackAction = AttackAction.RELOCATE_RIGHT;
            }
        }
        else{
            if(leftRelocators < relocateParticipationLimit){
                attackAction = AttackAction.RELOCATE_LEFT;
            }
        }
    }
    private enum AttackAction
    {
        FLANK_RIGHT,
        FLANK_LEFT,
        RELOCATE_RIGHT,
        RELOCATE_LEFT,
        SUPPRESS_FIRE,
        NONE


    }

    private class CanDoAction{
        public bool canDo;
        public int number;
        public AttackAction action;

        public CanDoAction(
            bool canDo,
            int number,
            AttackAction action
        ){
            this.canDo = canDo;
            this.number = number;
            this.action = action;
        }
    }
}
