using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class PlayerController : MonoBehaviour ,Controller.Controllable {
    [SerializeField]
    private GameplayCamera cam;
    [SerializeField]
    private AmyModel amy;

    private MouseTarget mouseTarget;
    private Vector3 target;

	private bool canMove;

	

	private bool inControl;


	void Awake(){
		inControl = true;
        		canMove = true;
        mouseTarget = new MouseTarget();
        target = new Vector3();
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //target = mouseTarget.GetEnvironmentTarget();
	}

    public void onI()
    {
        cam.PlayerRotateUp();
    }

    public void onK()
    {
        cam.PlayerRotateDown();
    }

    public void onJ()
    {
        cam.PlayerRotateLeft();
    }

    public void onL()
    {
        cam.PlayerRotateRight();
    }

    public void onU()
    {
        cam.ZoomIn();
    }

    public void onO()
    {
        cam.ZoomOut();
    }

	public void onWUp (){
		if (inControl) {

            amy.pendUp(false);
		}
	}
	public void onSUp (){
		if (inControl) {
			
            amy.pendDown(false);
		}
	}
	public void onAUp (){
		if (inControl) {
			
            amy.pendLeft(false);
		}
	}
	public void onDUp (){
		if (inControl) {
			
            amy.pendRight(false);
		}
	}

	public void onWDown (){
		if (inControl) {
			
            amy.pendUp(true);
		}
	}
	public void onSDown (){
		if (inControl) {
			
            amy.pendDown(true);
		}
	}
	public void onADown (){
		if (inControl) {
			
            amy.pendLeft(true);
		}
	}
	public void onDDown (){
		if (inControl) {
			
            amy.pendRight(true);
		}
	}
    public void onRClickDown(Vector3 pos){
		

	}
    public void onRClickUp(Vector3 pos)
    {


    }
    public void onLClickDown (Vector3 pos){
        if(inControl){
            amy.ActionStartAttack();
        }
	}
    public void onLClickUp(Vector3 pos)
    {
        if (inControl)
        {
            amy.ActionEndAttack();
        }
    }
    public void onMouseMove (Vector3 pos){
        cam.UpdateMousePosition(pos);
        mouseTarget.ScreenTarget(cam,pos);
        target = mouseTarget.GetEnvironmentTarget();
        amy.EffectSetTarget(target);
	}





	public  void EnableMove(){
		canMove = true;
	}

	public  void DisableMove(){
		canMove = false;
	}


	public void EnableControl(){
		inControl = true;
	}
	public void DisableControl(){
		inControl = false;
	}

    public void onSpace()
    {
    }

    public void onShift()
    {
    }

    public void onF()
    {
    }

    public void onSpaceUp()
    {
    }

    public void onShiftUp()
    {
    }

    public void onFUp()
    {
    }

    public void onSpaceDown()
    {
    }

    public void onShiftDown()
    {
        amy.ActionReload();
    }


    public void onRDown()
    {
        amy.EffectStand();
    }
    public void onFDown()
    {
        amy.EffectKneel();
    }

    public void onVDown()
    {
        amy.EffectLay();
    }
    public void onEDown()
    {
        amy.EffectSwitchWeaponRight();
    }

    public void onQDown()
    {
        amy.EffectSwitchWeaponLeft();
    }

    public void on1()
    {
    }

    public void on2()
    {
    }

    public void on3()
    {
    }

    public void on4()
    {
    }

    public void on5()
    {
    }

    public void on6()
    {
    }

    public void on7()
    {
    }

    public void on8()
    {
    }

    public void on9()
    {
    }

    public void on0()
    {
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawCube(target, new Vector3(1,1,1));
    }
}

