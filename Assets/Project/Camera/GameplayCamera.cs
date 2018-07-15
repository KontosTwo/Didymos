using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using Environment;

public partial class GameplayCamera : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private int lowerAngleRotateLimit;
    [SerializeField]
    private int upperAngleRotateLimit;
    [SerializeField]
    private float minimumHeightFromTerrain;
    [SerializeField]
    private float cameraRotationSpeed;
    [SerializeField]
    private int initialDistance;
    [SerializeField]
    private int initialVerticalAngle;
    [SerializeField]
    private int initialHorizontalAngle;
    [SerializeField]
    private float zoomSpeed;
    [SerializeField]
    private float minimumDistanceFromFocus;
    [SerializeField]
    private float maximumDistanceFromFocus;
    [SerializeField]
    private Transform focus;
   


    /*
     * calculated camera position that does not take 
     * into account outside factors
     */
    [SerializeField]
    private Transform cameraIndependentLocation;

    private IMovementStrategy currentMovementStrategy;
    private IdleStrategy idleStrategy;
    private FocusStrategy focusStrategy;

    public interface IMovementStrategy
    {
        void Initialize();
        void Update();
        void Cleanup();
        Vector3 GetLocation();
        Vector3 GetDirection();
    }

    public GameplayCamera()
    {
        
    }

    public void Awake()
    {
        currentMovementStrategy = new IdleStrategy(cam);
        idleStrategy = new IdleStrategy(cam);
    }

    public void Start()
    {

    }

	public void FixedUpdate()
	{
        /*
         * 
         * must be FixedUpdate, otherwise you cannot move
         * and rotate camera at the same time
         */
        currentMovementStrategy.Update();
	}

	public void LateUpdate()
	{
        
	}



	public void Idle()
    {
        if (idleStrategy == null)
        {
            idleStrategy = new IdleStrategy(cam);
        }
        SetAsStrategy(idleStrategy);
    }

    public void FocusOn(Transform focus)
    {
        if(focusStrategy == null){
            focusStrategy = new FocusStrategy(cam,
                                              cameraIndependentLocation,
                                              focus,
                                              lowerAngleRotateLimit,
                                              upperAngleRotateLimit,
                                              minimumHeightFromTerrain,
                                              cameraRotationSpeed,
                                              minimumDistanceFromFocus,
                                              maximumDistanceFromFocus,
                                              initialDistance,
                                              initialVerticalAngle,
                                              initialHorizontalAngle);
        }
        SetAsStrategy(focusStrategy);
    }

    public void PlayerRotateRight(){
        focusStrategy.RotateRight();
    }
    public void PlayerRotateLeft()
    {
        focusStrategy.RotateLeft();
    }
    public void PlayerRotateUp()
    {
        focusStrategy.RotateUp();
    }
    public void PlayerRotateDown()
    {
        focusStrategy.RotateDown();
    }

    public void UpdateMousePosition(Vector3 mouseScreenPos){
        focusStrategy.UpdateMousePosition(mouseScreenPos);
    }

    public void ZoomIn(){
        focusStrategy.ZoomIn(zoomSpeed);
    }

    public void ZoomOut(){
        focusStrategy.ZoomOut(zoomSpeed);
    }

    private void SetAsStrategy(IMovementStrategy strategy)
    {
        this.currentMovementStrategy.Cleanup();
        this.currentMovementStrategy = strategy;
        this.currentMovementStrategy.Initialize();
    }

    public float GetMaxDistanceFromFocus(){
        return maximumDistanceFromFocus;
    }

    public Vector3 ScreenToWorldPoint(Vector3 screenSpace){
        return cam.ScreenToWorldPoint(screenSpace);
    }

    public Transform GetCameraTransform(){
        return cam.transform;
    }

    public Camera GetCamera(){
        return cam;
    }

}
