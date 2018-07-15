using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    private Transform body;
    [SerializeField]
    private float defaultSpeed;
    [SerializeField]
    private Transform centerBottom;
    [SerializeField]
    private HumanoidDirection direction;


    private bool canMove;
    private Vector3 displacementFromCenterBottom;

    [SerializeField]
    private ClimbEvent climbEvent;

    [Serializable]
    public class ClimbEvent : UnityEvent
    {

    }

    public MovementController()
    {
        
    }

    public void Awake()
    {
        displacementFromCenterBottom = body.position - centerBottom.position;
        canMove = true;
    }

    public void Start(){
        
    }

	public void Update()
	{
        
	}
	public void Move(float angle){
        MoveWithTerrain(angle);
    }

	public void SetSpeed(int speed){
        this.defaultSpeed = speed;
    }


    

    private float calculateSpeed(){
        return defaultSpeed;
    }

    private void MoveWithTerrain(float angle)
    {
        angle = angle * Mathf.Deg2Rad;
        Vector3 currentPosition = centerBottom.position;

        float newX = (Mathf.Cos(angle) * calculateSpeed()) + currentPosition.x;                                                     
        float newZ = (Mathf.Sin(angle) * calculateSpeed()) + currentPosition.z;
        float newY = EnvironmentPhysics.FindHeightAt(newX,newZ);
        Vector3 newPosition = new Vector3(newX, newY, newZ);

        direction.Face(newPosition);

        if(EnvironmentPhysics.WalkableAt(newPosition.x,newPosition.z,1)){
            float twoDDistance = FastMath.Hyp(currentPosition.x
                                          , currentPosition.z
                                         , newPosition.x
                                          , newPosition.z);
            Vector3 hypotenuse = newPosition - currentPosition;
            Vector3 unitHypotenuse = hypotenuse.normalized;
            unitHypotenuse.Scale(FastMath.CreateVectorCube(twoDDistance));


            Vector3 finalPosition = unitHypotenuse + currentPosition;

            centerBottom.position = new Vector3(finalPosition.x
                                         , EnvironmentPhysics.FindWalkableHeightAt(finalPosition.x
                                                                                  , finalPosition.z)
                               , finalPosition.z
                               );
        }
        /*
         * centerBottom is implied to be the child of
         * the gameobject movementcontroller is attached to
         */
        Transform centerBottomParent = centerBottom.parent;
        centerBottom.parent = null;
        body.position = centerBottom.position + displacementFromCenterBottom;
        centerBottom.parent = centerBottomParent;
    }


}

