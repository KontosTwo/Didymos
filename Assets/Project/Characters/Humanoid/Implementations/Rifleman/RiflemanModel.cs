using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiflemanModel : AIHumanoidModel {
    [SerializeField]
    private AIStressManager stressManager;

    private WeaponType weapon;
	// Use this for initialization

	public override void Awake()
	{
        base.Awake();
        weapon = WeaponType.D_RIFLE;
        InitializeStressManager(stressManager);
	}
	public override void Start () {
        base.Start();
        InitializeWeapon(weapon);

	}
	
	// Update is called once per frame
    public override void Update () {
        base.Update();
	}
}
