using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidVantage {
    private float standingHeight;
    private float kneelingHeight;
    private float layingHeight;

    public HumanoidVantage(float stand,float lay, float kneel)
    {
        standingHeight = stand;
        layingHeight = lay;
        kneelingHeight = kneel;
    }

    public float GetStandingHeight(){
        return standingHeight;
    }

    public float GetKneelingHeight()
    {
        return kneelingHeight;
    }

    public float GetLayingHeight()
    {
        return layingHeight;
    }

    public Vector3 GetStandingVantage(Vector3 centerBottom){
        return centerBottom.AddY(standingHeight);
    }
    public Vector3 GetKneelingVantage(Vector3 centerBottom)
    {
        return centerBottom.AddY(kneelingHeight);
    }
    public Vector3 GetLayingVantage(Vector3 centerBottom)
    {
        return centerBottom.AddY(layingHeight);
    }
}
