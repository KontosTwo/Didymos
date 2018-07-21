using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyMarker{
    private Vector3 location;
    private HumanoidModel target;
    private List<CommunicatableEnemyMarker> communicators;

    public List<HumanoidTargeter> usedBy;

    public EnemyMarker(HumanoidModel target,Vector3 location,HumanoidTargeter founder){
        this.location = location;
        this.target = target;
        usedBy = new List<HumanoidTargeter>();
    }

    public EnemyMarker(EnemyTarget target,HumanoidTargeter founder)
        : this(target.GetEnemy(), target.GetLocation(), founder){

    }

    public HumanoidModel GetEnemy()
    {
        return target;
    }
    public Vector3 GetLocation()
    {
        return location; 
    }

}
