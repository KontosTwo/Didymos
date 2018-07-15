using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronicStressor  {
    private float severity;

    public ChronicStressor(float severity){
        this.severity = severity;
    }

    public float GetSeverity(){
        return severity;
    }

    public void ModifySeverity(float newSeverity){
        severity = newSeverity;
    }
}
