using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class HeightPenaltyAttributes {

    [SerializeField]
    public float goingUpPenalty;
    [SerializeField]
    public float goingDownPenalty;
    [SerializeField]
    public float climbUpPenalty;
    [SerializeField]
    public float climbDownPenalty;
    [SerializeField]
    public float climbUpThreshold;
    [SerializeField]
    public float climbDownThreshold;
}
