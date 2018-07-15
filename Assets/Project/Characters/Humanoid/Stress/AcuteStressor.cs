using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcuteStressor {
    private float severity;

    public AcuteStressor(float severity){
        this.severity = severity;
    }

    public float GetSeverity(){
        return severity;
    }

}
