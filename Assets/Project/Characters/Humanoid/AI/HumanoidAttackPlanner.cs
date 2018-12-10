using UnityEngine;
using System.Collections;

public class HumanoidAttackPlanner : MonoBehaviour{
    [SerializeField]
    private HumanoidModel planner;
    [SerializeField]
    private HumanoidTargeter targeter;


    private FlankDirection flankDirection;
    private EnemyMarker currentTarget;
    

    private void Awake(){
        HumanoidAttackPlannerCommunications.AddPlanner(
            this
        );
        flankDirection = FlankDirection.CANNOT;
    }

    public bool IsFlanking(){
        return flankDirection != FlankDirection.CANNOT;
    }

    public bool FlankingRight(){
        return flankDirection == FlankDirection.RIGHT;
    }

    public bool FlankingLeft(){
        return flankDirection == FlankDirection.LEFT;
    }

    public void FindFlankingDirection(){

    }

    private enum FlankDirection
    {
        RIGHT,
        LEFT,
        CANNOT
    }
}
