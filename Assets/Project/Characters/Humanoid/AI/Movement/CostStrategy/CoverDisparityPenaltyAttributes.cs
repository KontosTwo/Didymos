using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class CoverDisparityPenaltyAttributes{
    [SerializeField]
    public float coverDisparityPenalty;
    [SerializeField]
    public float exposedPenalty;
    [SerializeField]
    public float notCoverPenalty;
}
