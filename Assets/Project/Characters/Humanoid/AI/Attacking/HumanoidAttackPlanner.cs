using UnityEngine;
using System.Collections;

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
    }

    private Vector3 GetPlannerLocation(){
        return planner.InfoGetCenterBottom();
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
}
