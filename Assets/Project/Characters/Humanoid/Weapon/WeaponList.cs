using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponList : MonoBehaviour
{
    [SerializeField]
    private List<Weapon> weapons;

    private static WeaponList instance;

    public WeaponList(){
        weapons = new List<Weapon>();
        foreach (WeaponType type in Enum.GetValues(typeof(WeaponType)))
        {
            weapons.Add(new Weapon(type));
        }
    }

	private void Awake()
	{
        instance = this;
	}

    public static WeaponRef GetWeapon(WeaponType type){
        foreach(Weapon weapon in instance.weapons){
            if(weapon.GetWeaponType().Equals(type)){
                return new WeaponRef(weapon);
            }
        }
        throw new Exception("Weapon not found: " + type.ToString());
    }
}

