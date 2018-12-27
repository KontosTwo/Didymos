using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;
using UnityEditor;


public class HumanoidAILinker : MonoBehaviour
{
    [SerializeField]
    private HumanoidModel model;

    private HumanoidTaskExecuter taskExecuter;

    private bool active;

    public void Awake(){
        taskExecuter = new HumanoidTaskExecuter(model);
        active = false;
    }

    public void Activate()
    {
        active = true;
    }
    public void Deactivate()
    {
        active = false;
    }

    [Task]
    public bool IsActive()
    {
        return active;
    }
    [Task]
    public bool IsFlinch(){
        return model.InfoIsFlinch();
    }




    protected void ExecuteAction(Action action)
    {
        taskExecuter.ExecuteAction(action);
    }
    protected void ExecuteChannelingAction(Action onBegin, Action onEnd, EndCondition condition)
    {
        taskExecuter.ExecuteChannelingAction(onBegin, onEnd, condition);
    }
}

