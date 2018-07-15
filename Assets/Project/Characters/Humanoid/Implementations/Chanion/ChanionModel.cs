using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanionModel : HumanoidModel {
    [SerializeField]
    private ChanionStressManager stressManager;

    private WeaponType weapon;

    public override void Awake()
    {
        base.Awake();
        weapon = WeaponType.E_SHOTGUN;
        InitializeStressManager(stressManager);
    }

	// Use this for initialization
	public override void Start () {
        base.Start();
        InitializeWeapon(weapon);
	}
	
	// Update is called once per frame
	public override void Update () {
        base.Update();
	}
}
