using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class HumanoidAttackPlanner : MonoBehaviour{
    [SerializeField]
    private HumanoidModel planner;
    [SerializeField]
    private HumanoidTargeter targeter;

    [SerializeField]
    private int flankParticipationLimit;
    [SerializeField]
    private int suppressiveFireParticipationLimit;


    private AttackAction attackAction;
    private EnemyMarker currentTarget;
    private bool hasTarget;
    

    private void Awake(){
        HumanoidAttackPlannerCommunications.AddPlanner(
            this
        );
        attackAction = AttackAction.NONE;
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
        /*
         * Get closest marker or target
         * Maintain a 1 : 1 : 1 ratio of covering, right, and left flankers
         * satisfy that ratio first, then check limit
         * if not relocate left or right based on 1 : 1 ratio
         * of left and right
         * 
         * Make sure to constantly check if communicatble marker
         * or target is still valid!
         */

        var boundingEnemies = targeter.GetBoundingEnemies();
        var boundingHiddenEnemies = boundingEnemies.Item1;
        var boundingVisibleEnemies = boundingEnemies.Item2;

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

        if(rightRelocators < leftRelocators){
            attackAction = AttackAction.RELOCATE_RIGHT;
        }else{
            attackAction = AttackAction.RELOCATE_LEFT;
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

        if (rightRelocators < leftRelocators)
        {
            attackAction = AttackAction.RELOCATE_RIGHT;
        }
        else
        {
            attackAction = AttackAction.RELOCATE_LEFT;
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
