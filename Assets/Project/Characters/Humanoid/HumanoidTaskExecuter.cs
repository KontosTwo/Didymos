using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;
using System;

public class HumanoidTaskExecuter
{
    
    private HumanoidModel model;

    public HumanoidTaskExecuter(HumanoidModel model){
        this.model = model;
    }
    public void ExecuteAction(Action action)
    {
        Task task = Task.current;
        if (task.isStarting)
        {
            action();
        }
        if (!model.InfoIsExecutingAction())
        {
            task.Succeed();
        }
        if (model.InfoIsFlinch())
        {
            task.Fail();
        }
    }
    public void ExecuteChannelingAction(Action onBegin
        , Action onEnd
        , EndCondition condition)
    {
        Task task = Task.current;
        if (task.isStarting)
        {
            onBegin();
        }
        if (condition())
        {
            onEnd();
            task.Succeed();
        }
        if (model.InfoIsFlinch())
        {
            onEnd();
            task.Fail();
        }
    }


}

public delegate bool EndCondition();
public delegate void Action();

