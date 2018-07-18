using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunicatableEnemyMarker {
    public EnemyMarker enemyMarker;
    public bool valid;

    public CommunicatableEnemyMarker(EnemyMarker marker)
    {
        this.enemyMarker = marker;
        valid = true;
    }

    public CommunicatableEnemyMarker GetNewMarker(){
        CommunicatableEnemyMarker newMarker = new CommunicatableEnemyMarker(
            this.enemyMarker
        );
        newMarker.valid = this.valid;
        return newMarker;
    }
}
