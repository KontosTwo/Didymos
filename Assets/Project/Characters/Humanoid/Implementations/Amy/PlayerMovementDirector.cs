using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class PlayerMovementDirector : MonoBehaviour {
    [SerializeField]
    private MovementController movement;
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private HumanoidActionCoordinator coordinator;

    private bool moveLeft;
    private bool moveRight;
    private bool moveUp;
    private bool moveDown;

    private bool canMove;


    public PlayerMovementDirector()
    {

    }

    public void Awake()
    {
        moveLeft = false;
        moveRight = false;
        moveUp = false;
        moveDown = false;
        canMove = true;
    }

    public void Start()
    {

    }

    public void FixedUpdate()
    {
        float camAngle = 360 - cam.transform.eulerAngles.y;
        if (canMove)
        {
            if (moveUp)
            {
                if (moveRight)
                {
                    movement.Move(camAngle + 45);
                    coordinator.SetMoving(true);
                }
                else if (moveLeft)
                {
                    movement.Move(camAngle + 135);
                    coordinator.SetMoving(true);

                }
                else
                {
                    movement.Move(camAngle + 90);
                    coordinator.SetMoving(true);

                }
            }
            else if (moveDown)
            {
                if (moveRight)
                {
                    movement.Move(camAngle + 315);
                    coordinator.SetMoving(true);

                }
                else if (moveLeft)
                {
                    movement.Move(camAngle + 225);
                    coordinator.SetMoving(true);


                }
                else
                {
                    movement.Move(camAngle + 270);
                    coordinator.SetMoving(true);

                }
            }
            else
            {
                if (moveRight)
                {
                    movement.Move(camAngle);
                    coordinator.SetMoving(true);

                }
                else if (moveLeft)
                {
                    movement.Move(camAngle + 180);
                    coordinator.SetMoving(true);

                }else{
                    coordinator.SetMoving(false);

                }
            }
        }
    }
    public void pendLeft(bool b)
    {
        if (moveRight && b)
        {
            moveRight = false;
        }
        moveLeft = b;
    }
    public void pendRight(bool b)
    {
        if (moveLeft && b)
        {
            moveLeft = false;
        }
        moveRight = b;
    }
    public void pendUp(bool b)
    {
        if (moveDown && b)
        {
            moveDown = false;
        }
        moveUp = b;
    }
    public void pendDown(bool b)
    {
        if (moveUp && b)
        {
            moveUp = false;
        }
        moveDown = b;
    }
}
