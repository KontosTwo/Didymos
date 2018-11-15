using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Test : MonoBehaviour {
	public UnityEvent on1;
	public UnityEvent on2;
	public UnityEvent on3;
	public UnityEvent on4;
	public UnityEvent on5;
	public UnityEvent on6;
	public UnityEvent on7;
	public UnityEvent on8;
	public UnityEvent on9;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			on1.Invoke ();
		}
		if(Input.GetKeyDown(KeyCode.Alpha2)){
			on2.Invoke ();
		}
		if(Input.GetKeyDown(KeyCode.Alpha3)){
			on3.Invoke ();
		}
		if(Input.GetKeyDown(KeyCode.Alpha4)){
			on4.Invoke ();
		}
		if(Input.GetKeyDown(KeyCode.Alpha5)){
			on5.Invoke ();
		}
		if(Input.GetKeyDown(KeyCode.Alpha6)){
			on6.Invoke ();
		}
		if(Input.GetKeyDown(KeyCode.Alpha7)){
			on7.Invoke ();
		}
		if(Input.GetKeyDown(KeyCode.Alpha8)){
			on8.Invoke ();
		}
		if(Input.GetKeyDown(KeyCode.Alpha9)){
			on9.Invoke ();
		}
	}
}
