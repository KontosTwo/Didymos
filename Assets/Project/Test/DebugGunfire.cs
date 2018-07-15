using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGunfire : MonoBehaviour {
    private static DebugGunfire instance;

    private void Awake()
    {
        instance = this;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void CreateGunfireLine(Vector3 start, Vector3 end){
        
    }

}
