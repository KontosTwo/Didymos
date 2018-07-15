using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AmyScriptHandle : MonoBehaviour
{
    [SerializeField]
    private AmyAILinker ai;
    public AmyScriptHandle()
    {
        
    }

    public void StartAI(){
        ai.Activate();
    }

    public void StopAI(){
        ai.Deactivate();
    }
}

