using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HitboxCreator : MonoBehaviour {

    public abstract void Attack(Vector3 start, Vector3 target);
    public abstract Projectile GetProjectile();

}
