using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget {
    private Vector3 location;
    private HumanoidModel target;

    public EnemyTarget(HumanoidModel target, Vector3 location)
    {
        this.location = location;
        this.target = target;
    }

    public HumanoidModel GetEnemy(){
        return target;
    }
    public Vector3 GetLocation(){
        return location;
    }
}
