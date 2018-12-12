using System;
using System.Collections.Generic;
using UnityEngine;

public  class HumanoidAttackPlannerCommunications : MonoBehaviour{
    private  List<HumanoidAttackPlanner> planners;
    private  List<MarkerForAttack> markersForAttack;
    private  List<TargetForAttack> targetsForAttack;

    private static HumanoidAttackPlannerCommunications instance;

    static HumanoidAttackPlannerCommunications(){

    }

    private void Awake(){
        planners = new List<HumanoidAttackPlanner>();
        markersForAttack = new List<MarkerForAttack>();
        targetsForAttack = new List<TargetForAttack>();
        instance = this;
    }

    public static void AddPlanner(
        HumanoidAttackPlanner planner
    ){
        instance.planners.Add(planner);
    }

    public static bool CanFlankLeftMarker(
        EnemyMarker marker,
        int limit
    ){
        foreach(MarkerForAttack attackable in instance.markersForAttack){
            if(attackable.GetEnemyMarker() == marker){
                return attackable.CanFlankLeft(limit);
            }
        }
        Debug.Log("WARNING: Enemy marker not found");
        return false;
    }

    public static bool CanFlankRightMarker(
        EnemyMarker marker,
        int limit
    ){
        foreach (MarkerForAttack attackable in instance.markersForAttack){
            if (attackable.GetEnemyMarker() == marker){
                return attackable.CanFlankRight(limit);
            }
        }
        Debug.Log("WARNING: Enemy marker not found");
        return false;
    }

    public static bool CanSuppressMarker(
        EnemyMarker marker,
        int limit
    ){
        foreach (MarkerForAttack attackable in instance.markersForAttack){
            if (attackable.GetEnemyMarker() == marker){
                return attackable.CanCoveringFire(limit);
            }
        }
        Debug.Log("WARNING: Enemy marker not found");
        return false;
    }

    public List<HumanoidAttackPlanner> GetRightRelocators(){
        return planners.FindAll(p =>{
            return p.IsRelocatingRight();
        });
    }

    public List<HumanoidAttackPlanner> GetLeftRelocators(){
        return planners.FindAll(p => {
            return p.IsRelocatingLeft();
        });
    }


    private class MarkerForAttack{
        private EnemyMarker underlyingTarget;

        private int coveringFireAttackers;
        private int leftFlankers;
        private int rightFlankers;

        public MarkerForAttack(
            EnemyMarker marker
        ){
            underlyingTarget = marker;
        }

        public bool CanFlankLeft(int limit){
            return leftFlankers < limit;
        }

        public bool CanFlankRight(int limit){
            return rightFlankers < limit;
        }

        public bool CanCoveringFire(int limit){
            return coveringFireAttackers < limit;
        }

        public EnemyMarker GetEnemyMarker(){
            return underlyingTarget;
        }
    }

    private class TargetForAttack{
        private EnemyTarget underlyingTarget;

        private int coveringFireAttackers;
        private int leftFlankers;
        private int rightFlankers;

        public TargetForAttack(
            EnemyTarget marker
        ){
            underlyingTarget = marker;
        }

        public bool CanFlankLeft(int limit){
            return leftFlankers < limit;
        }

        public bool CanFlankRight(int limit){
            return rightFlankers < limit;
        }

        public bool CanCoveringFire(int limit){
            return coveringFireAttackers < limit;
        }

        public EnemyTarget GetEnemyTarget(){
            return underlyingTarget;
        }
    }
}

