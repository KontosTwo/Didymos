using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class HeightPenaltyAttributes {

    [SerializeField]
    public float goingUpPenalty = 1f;
    [SerializeField]
    public float goingDownPenalty = 1f;
    [SerializeField]
    public float climbUpPenalty = 1f;
    [SerializeField]
    public float climbDownPenalty = 1f;
    [SerializeField]
    public float climbUpThreshold = 1f;
    [SerializeField]
    public float climbDownThreshold = 1f;
}
