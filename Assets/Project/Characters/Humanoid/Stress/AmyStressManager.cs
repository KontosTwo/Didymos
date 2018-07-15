using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmyStressManager : StressManager {
    [SerializeField]
    private int cringeThreshold;
    [SerializeField]
    private int hallucinateThreshold;

	// Use this for initialization
	public override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	public override void Update () {
        base.Update();
        //sDebug.Log("Amy Stress: " + GetStress());
	}

    public bool IsCringe(){
        return GetStress() > cringeThreshold;
    }

    public bool isHallucinate(){
        return GetStress() > hallucinateThreshold;
    }
}
