using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStressManager : StressManager {
    [SerializeField]
    private int cringeThreshold;
    [SerializeField]
    private int retreatThreshold;
    [SerializeField]
    private int fleeThreshold;

    private int stress;


    // Use this for initialization
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
	}

    public bool CringeThreshold(){
        return GetStress() > cringeThreshold;
    }
    public bool RetreatThreshold()
    {
        return GetStress() > retreatThreshold;
    }
    public bool FleeThreshold()
    {
        return GetStress() > fleeThreshold;
    }
}
