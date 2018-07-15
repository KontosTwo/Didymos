using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
	public interface Controllable{

		void onSpace ();
		void onShift ();
		void onF ();
        void onI();
        void onK();
        void onJ();
        void onL();
        void onU();
        void onO();

		void onWUp ();
		void onSUp ();
		void onAUp ();
		void onDUp ();
		void onSpaceUp ();
		void onShiftUp ();
		void onFUp ();

		void onWDown ();
		void onSDown ();
		void onADown ();
		void onDDown();
		void onSpaceDown ();
		void onShiftDown ();
        void onRDown();
        void onFDown();
        void onVDown();
        void onEDown();
        void onQDown();

		void onRClickDown(Vector3 pos);
        void onRClickUp(Vector3 pos);
        void onLClickDown(Vector3 pos);
        void onLClickUp (Vector3 pos);
        void onMouseMove (Vector3 pos);

        void on1();
        void on2();
        void on3();
        void on4();
        void on5();
        void on6();
        void on7();
        void on8();
        void on9();
        void on0();
	}

	private Controllable controllable;


	void Awake(){
		
	}

	void Start () {
		
	}

	public void setControllable(Controllable c){
		controllable = c;
	}
	
	void Update () {
		if(Input.GetKey(KeyCode.Space)){
			controllable.onSpace ();
		}
		if(Input.GetKey(KeyCode.LeftShift)){
			controllable.onShift ();
		}
		if(Input.GetKey(KeyCode.F)){
			controllable.onF ();
		}
        if (Input.GetKey(KeyCode.I))
        {
            controllable.onI();
        }
        if (Input.GetKey(KeyCode.K))
        {
            controllable.onK();
        }
        if (Input.GetKey(KeyCode.J))
        {
            controllable.onJ();
        }
        if (Input.GetKey(KeyCode.L))
        {
            controllable.onL();
        }
        if (Input.GetKey(KeyCode.U))
        {
            controllable.onU();
        }
        if (Input.GetKey(KeyCode.O))
        {
            controllable.onO();
        }

		if(Input.GetKeyUp(KeyCode.W)){
			controllable.onWUp ();
		}
		if(Input.GetKeyUp(KeyCode.S)){
			controllable.onSUp ();
		}
		if(Input.GetKeyUp(KeyCode.A)){
			controllable.onAUp();
		}
		if(Input.GetKeyUp(KeyCode.D)){
			controllable.onDUp();
		}
		if(Input.GetKeyUp(KeyCode.Space)){
			controllable.onSpaceUp ();
		}
		if(Input.GetKeyUp(KeyCode.LeftShift)){
			controllable.onShiftUp ();
		}
		if(Input.GetKeyUp(KeyCode.F)){
			controllable.onFUp ();
		}

		if(Input.GetKeyDown(KeyCode.W)){
			controllable.onWDown ();
		}
		if(Input.GetKeyDown(KeyCode.S)){
			controllable.onSDown ();
		}
		if(Input.GetKeyDown(KeyCode.A)){
			controllable.onADown();
		}
		if(Input.GetKeyDown(KeyCode.D)){
			controllable.onDDown();
		}
		if(Input.GetKeyDown(KeyCode.Space)){
			controllable.onSpaceDown ();
		}
		if(Input.GetKeyDown(KeyCode.LeftShift)){
			controllable.onShiftDown ();
		}
        if (Input.GetKeyDown(KeyCode.R))
        {
            controllable.onRDown();
        }
		if(Input.GetKeyDown(KeyCode.F)){
			controllable.onFDown ();
		}
        if (Input.GetKeyDown(KeyCode.V))
        {
            controllable.onVDown();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            controllable.onEDown();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            controllable.onQDown();
        }

		if(Input.GetMouseButtonDown(0)){
            controllable.onLClickDown (Input.mousePosition);
		}
        if (Input.GetMouseButtonUp(0))
        {
            controllable.onLClickUp(Input.mousePosition);
        }
        if (Input.GetMouseButtonDown(1))
        {
            controllable.onRClickDown(Input.mousePosition);
        }
		if(Input.GetMouseButtonUp(1)){
            controllable.onRClickUp(Input.mousePosition);
		}

        controllable.onMouseMove (Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            controllable.on0();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            controllable.on1();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            controllable.on2();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            controllable.on3();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            controllable.on4();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            controllable.on5();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            controllable.on6();
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            controllable.on7();
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            controllable.on8();
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            controllable.on9();
        }


	}
}
