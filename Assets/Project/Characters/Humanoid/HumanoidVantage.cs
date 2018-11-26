using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidVantage {
    private Vector3 centerBottom;
    private Weapon weapon;
    private float standingHeight;
    private float kneelingHeight;
    private float layingHeight;

    public HumanoidVantage(
                           float stand,
                           float kneel, 
                           float lay)
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

    public void SetWeapon(Weapon weapon){
        this.weapon = weapon;
    }

    public void SetLocation(Vector3 location){
        centerBottom = location;
    }

    public Projectile GetWeaponThreat(){
        return weapon.GetProjectile();
    }

    public Vector3 GetStandingVantage(){
        return centerBottom.AddY(standingHeight);
    }
    public Vector3 GetKneelingVantage()
    {
        return centerBottom.AddY(kneelingHeight);
    }
    public Vector3 GetLayingVantage()
    {
        return centerBottom.AddY(layingHeight);
    }

    public bool HigherThan(HumanoidVantage other){
        return this.centerBottom.y > other.centerBottom.y;
    }
}
