using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRef {

    private Weapon weapon;

    public WeaponRef(Weapon weapon){
        this.weapon = weapon;
    }

    public Weapon Instantiate(Transform releasePoint){
        return weapon.Instantiate(releasePoint);
    }
}
