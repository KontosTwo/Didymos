using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTarget {
    private Vector3 environmentTarget;

    public MouseTarget(){
        environmentTarget = new Vector3();
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ScreenTarget(GameplayCamera cam, Vector3 screenLocation){
        environmentTarget = EnvironmentPhysics.ScreenToEnvironmentPoint(cam,screenLocation);
    }

    public Vector3 GetEnvironmentTarget(){
        return environmentTarget;
    }
}
