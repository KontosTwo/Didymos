using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxCreatorProjectileInstant :HitboxCreator{
    [SerializeField]
    private int power;
    [SerializeField]
    private float suppressiveRadius;

    public override void Attack(Vector3 start,Vector3 target){
        Projectile projectile = new Projectile(power, suppressiveRadius);
        EnvironmentPhysics.SendProjectile(projectile, start, target);
    }

    public override Projectile GetProjectile(){
        return new Projectile(power, suppressiveRadius);
    }
}
