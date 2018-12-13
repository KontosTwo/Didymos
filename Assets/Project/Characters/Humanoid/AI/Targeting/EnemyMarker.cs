using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyMarker{
    private Vector3 location;
    private HumanoidModel target;

    private HumanoidVantage enemyVantage;
    private HashSet<HumanoidTargeter> usedBy;


    public EnemyMarker(HumanoidModel target,Vector3 location,HumanoidTargeter founder){
        this.location = location;
        this.target = target;
        usedBy = new HashSet<HumanoidTargeter>();
        enemyVantage = target.InfoGetVantageData();
        enemyVantage.SetLocation(location);
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
    public HumanoidVantage GetVantage(){
        return enemyVantage;
    }

    public HashSet<HumanoidTargeter> GetUsers(){
        return usedBy;
    }
    public void AddUser(HumanoidTargeter targeter){
        usedBy.Add(targeter);
    }
    public void RemoveUser(HumanoidTargeter targeter){
        usedBy.Remove(targeter);
    }
    public bool IsUsedBy(HumanoidTargeter targeter){
        return usedBy.Contains(targeter);
    }

    public bool StillInUse(){
        return usedBy.Count != 0;
    }
}
