using System;
using UnityEngine;
using UnityEditor;
using System.Collections;

public class HumanoidActionCoordinator : MonoBehaviour
{


    [SerializeField]
    private Animator animator;

    private AnimationClipSwitcher clipSwitcher;

    private IEnumerator currentWait;
    private bool actionInProgress;


    void Awake()
    {
        actionInProgress = false;
        clipSwitcher = new AnimationClipSwitcher(this.animator);
        currentWait = ExecuteActionCoroutine(0);
    }

	void Update()
	{
        clipSwitcher.PrintCurrentState();
    }


    public bool ActionInProgress()
    {
        return actionInProgress;
    }

    public void SwitchWeapon(Weapon oldWeapon,Weapon newWeapon){
        if(actionInProgress){
            return;
        }
        clipSwitcher.SwitchClipForState("Stash",
                                        oldWeapon.GetStashBehaviour()
        );
        clipSwitcher.SwitchClipForState("Takeout",
                                        newWeapon.GetTakeoutBehaviour()
        );
        clipSwitcher.SwitchClipForState("StandAttack",
                                        newWeapon.GetStandAttackBehaviour()
        );
        clipSwitcher.SwitchClipForState("KneelAttack",
                                        newWeapon.GetKneelAttackBehaviour()
        );
        clipSwitcher.SwitchClipForState("LayAttack",
                                        newWeapon.GetLayAttackBehaviour()
        );
        clipSwitcher.SwitchClipForState("Reload",
                                        newWeapon.GetReloadBehaviour()
        );
        ExecuteActionIfAvailable(oldWeapon.GetTimeForStash() + 
                                 newWeapon.GetTimeForTakeOut(),"switch");
    }

    public void InitializeWeapon(Weapon starter){
        clipSwitcher.SwitchClipForState("Stash",
                                        starter.GetStashBehaviour()
        );
        clipSwitcher.SwitchClipForState("Takeout",
                                        starter.GetTakeoutBehaviour()
        );
        clipSwitcher.SwitchClipForState("StandAttack",
                                        starter.GetStandAttackBehaviour()
        );
        clipSwitcher.SwitchClipForState("KneelAttack",
                                        starter.GetKneelAttackBehaviour()
        );
        clipSwitcher.SwitchClipForState("LayAttack",
                                        starter.GetLayAttackBehaviour()
        );
        clipSwitcher.SwitchClipForState("Reload",
                                        starter.GetReloadBehaviour()
        );
    }

	public void SetMoving(bool state)
    {
        SetStateIfAvailable("isMoving", state);
    }
    public void SetStand()
    {
        Pulse("stand");
    }
    public void SetKneel()
    {
        Pulse("kneel");
    }
    public void SetLay()
    {
        Pulse("lay");
    }
    public void Reload(Weapon weapon){
        ExecuteActionIfAvailable(weapon.GetTimeForReload(), "reload");
    }
    public void SetAttack(Weapon weapon, bool active)
    {
        SetStateIfAvailable("attack", active);
    }
   
    public void Flinch(float time){
        ExecuteActionNoMatterWhat(time, "flinch");
    }

    /*
     * Same as SetState, but the animator state transitioned to
     * should repeat until transitioned out
     */
    public void SetChannelingGroundAction1(bool active)
    {
        SetStateIfAvailable("channe", active);
    }
    /*
     * Set every boolean to false 
     */
    public void Interrupt(){
        SetMoving(false);

        actionInProgress = false;
    }
    private void ExecuteActionIfAvailable(float time, String actionName)
    {
        if (!actionInProgress)
        {
            ExecuteActionTimer(time);
            animator.SetTrigger(actionName);
        }
    }
    private void ExecuteActionNoMatterWhat(float time, String actionName)

    {
        ExecuteActionTimer(time);
        animator.SetTrigger(actionName);
    }

    private void SetStateIfAvailable(String stateName,bool transition){
        if (!actionInProgress && transition){
            animator.SetBool(stateName, transition);
            actionInProgress = true;
        }else if(transition == false){
            animator.SetBool(stateName, transition);
            actionInProgress = false;
        }
    }
   

    private void ExecuteActionTimer(float seconds)
    {
        StopCoroutine(currentWait);
        currentWait = ExecuteActionCoroutine(seconds);
        StartCoroutine(currentWait);
    }

    private IEnumerator ExecuteActionCoroutine(float seconds)
    {
        actionInProgress = true;
        yield return new WaitForSeconds(seconds);
        actionInProgress = false;
    }

    private void Pulse(String state){
        if (!actionInProgress)
        {
            animator.SetTrigger(state);
        }    
    }

	private void OnDrawGizmos()
	{
        if(!Application.isPlaying){
            return;
        }
        Handles.Label(
            transform.position,
            gameObject.name + ": " + animator.GetCurrentAnimatorClipInfo(0)[0].clip.name
        );
	}
}

