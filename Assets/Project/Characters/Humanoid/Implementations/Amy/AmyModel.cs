using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class AmyModel : HumanoidModel
{
    [SerializeField]
    private PlayerMovementDirector movementDirector;
    [SerializeField]
    private AmyStressManager stressManager;


    [SerializeField]
    private List<WeaponType> beginningWeapons;

    private CircularLinkedList<WeaponType> weapons;

    public AmyModel()
    {
        
    }

	public override void Awake()
	{
        base.Awake();
        weapons = new CircularLinkedList<WeaponType>(beginningWeapons);
        InitializeStressManager(stressManager);
    }

	public override void Start()
	{
        base.Start();
        InitializeWeapon(weapons.Get());
	}

	public override void Update()
	{
		base.Update();

	}

	public override void EffectCancelAction()
	{
        base.EffectCancelAction();
	}

    public void EffectSwitchWeaponLeft(){
        weapons.ShiftLeft();
        SwitchToWeapon(weapons.Get());
    }

    public void EffectSwitchWeaponRight()
    {
        weapons.ShiftRight();
        SwitchToWeapon(weapons.Get());
    }

    public void E_Machinegun(){
        Debug.Log("E_Machinegunattack");
    }

    public void E_Shotgun(){
        Debug.Log("E_ShotgunAttack");
    }

	public void pendLeft(bool b)
    {
        movementDirector.pendLeft(b);
    }
    public void pendRight(bool b)
    {
        movementDirector.pendRight(b);
    }
    public void pendUp(bool b)
    {
        movementDirector.pendUp(b);
    }
    public void pendDown(bool b)
    {
        movementDirector.pendDown(b);
    }
}

