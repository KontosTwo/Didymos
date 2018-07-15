using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : MonoBehaviour {

	// Use this for initialization
	[SerializeField]
	private Controller controller;
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private Transform camFocus;
    [SerializeField]
    private GameplayCamera camera;


    [SerializeField]
    private AmyScriptHandle amyScriptHandle;

	public bool turnOnAi;

	void Awake(){

	}
	void Start () {
		controller.setControllable (player );
        camera.FocusOn(camFocus);
        if(turnOnAi){
            amyScriptHandle.StartAI();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
