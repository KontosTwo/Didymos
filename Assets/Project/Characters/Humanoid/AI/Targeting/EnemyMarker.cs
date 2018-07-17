using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyMarker{
    private Vector3 location;
    private HumanoidModel target;
    private Stack<HumanoidTargeter> founders;
    private HashSet<HumanoidTargeter> alreadyCommunicatedAdd;
    private HashSet<HumanoidTargeter> alreadyCommunicatedDelete;

    public List<HumanoidTargeter> usedBy;

    public EnemyMarker(HumanoidModel target,Vector3 location,HumanoidTargeter founder){
        this.location = location;
        this.target = target;
        founders = new Stack<HumanoidTargeter>();
        founders.Push(founder);
        alreadyCommunicatedAdd = new HashSet<HumanoidTargeter>();
        alreadyCommunicatedDelete = new HashSet<HumanoidTargeter>();

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
    public HumanoidTargeter GetFounder(){
        return founders.Peek();
    }

    public void SwitchToNewFounder(HumanoidTargeter newFounder){
        if(newFounder != founders.Peek()){
            founders.Push(newFounder);
        }
    }

    public bool AlreadyCommunicatedAdd(HumanoidTargeter targeter){
        return alreadyCommunicatedAdd.Contains(targeter);
    }
    public void CommunicateAddBy(HumanoidTargeter targeter){
        alreadyCommunicatedAdd.Add(targeter);
    }

    public bool AlreadyCommunicatedDelete(HumanoidTargeter targeter)
    {
        return alreadyCommunicatedDelete.Contains(targeter);
    }
    public void CommunicateDeleteBy(HumanoidTargeter targeter)
    {
        alreadyCommunicatedDelete.Add(targeter);
    }
}
