using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunicatableEnemyTarget {
    private bool isValid;
    private EnemyTarget target;

    public CommunicatableEnemyTarget(
        EnemyTarget t
    ){
        target = t;
        isValid = true;
    }

    public void Invalidate(){
        isValid = false;
    }

    public EnemyTarget GetEnemyTarget(){
        return target;
    }

    public bool IsValid(){
        return isValid;
    }
}
