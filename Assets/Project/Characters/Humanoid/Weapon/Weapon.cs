using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[Serializable]
public class Weapon
{
    /*
     * Make sure to modify Instantiate upon adding a 
     * new field
     */
    [SerializeField]
    private WeaponType type;
    [SerializeField]
    private int maxAmmo;
    [SerializeField]
    private int maxClips;

    [SerializeField]
    private HitboxCreator hitboxCreator;
    [SerializeField]
    private AnimationClip standAttackBehaviour;
    [SerializeField]
    private float timeForAttack;
    [SerializeField]
    private AnimationClip kneelAttackBehaviour;
    [SerializeField]
    private AnimationClip layAttackBehaviour;
    [SerializeField]
    private AnimationClip reloadBehaviour;
    [SerializeField]
    private float timeForReload;
    [SerializeField]
    private AnimationClip stashBehaviour;
    [SerializeField]
    private float timeForStash;
    [SerializeField]
    private AnimationClip takeOutBehaviour;
    [SerializeField]
    private float timeForTakeOut;

    private Transform releasePoint;

    private int currentAmmo;
    private int currentClips;
    private Vector3 target;




    public Weapon(WeaponType type){
        this.type = type;
    }

    /*
     * Remember to test if I can play the same clip multiple times.
     * Without them "clashing". 
     */
    public Weapon Instantiate(Transform releasePoint){
        Weapon weapon = new Weapon(type);
        weapon.maxAmmo = maxAmmo;
        weapon.maxClips = maxClips;
        weapon.currentAmmo = maxAmmo;
        weapon.currentClips = maxClips;

        weapon.hitboxCreator = hitboxCreator;
        weapon.standAttackBehaviour = standAttackBehaviour;
        weapon.kneelAttackBehaviour = kneelAttackBehaviour;
        weapon.layAttackBehaviour = layAttackBehaviour;
        weapon.stashBehaviour = stashBehaviour;
        weapon.takeOutBehaviour = takeOutBehaviour;
        weapon.reloadBehaviour = reloadBehaviour;
        weapon.releasePoint = releasePoint;

        weapon.timeForStash = timeForStash;
        weapon.timeForAttack = timeForAttack;
        weapon.timeForReload = timeForReload;
        weapon.timeForTakeOut = timeForTakeOut;
        return weapon;
    }
    /*
     *  Remember to SetTarget in HumanoidModel derivatives!
     */
    public void SetTarget(Vector3 target){
        this.target = target;
    }
    public WeaponType GetWeaponType(){
        return type;
    }
    public AnimationClip GetStandAttackBehaviour(){
        return standAttackBehaviour;
    }
    public AnimationClip GetKneelAttackBehaviour()
    {
        return kneelAttackBehaviour;
    }
    public AnimationClip GetLayAttackBehaviour()
    {
        return layAttackBehaviour;
    }
    public float GetTimeForAttack(){
        return timeForAttack;
    }
    public AnimationClip GetReloadBehaviour(){
        return reloadBehaviour;
    }
    public float GetTimeForReload()
    {
        return timeForReload;
    }
    public AnimationClip GetStashBehaviour()
    {
        return stashBehaviour;
    }
    public float GetTimeForStash()
    {
        return timeForStash;
    }
    public AnimationClip GetTakeoutBehaviour()
    {
        return takeOutBehaviour;
    }
    public float GetTimeForTakeOut()
    {
        return timeForTakeOut;
    }

    public Projectile GetProjectile(){
        return hitboxCreator.GetProjectile();
    }

    public virtual void Attack(){
        Vector3 start = releasePoint.position;
        hitboxCreator.Attack(start,target);
        currentAmmo--;
    }
    public virtual bool HasAmmo(){
        return currentAmmo > 0;;
    }
    public virtual void Reload(){
        currentAmmo = maxAmmo;
        currentClips--;
    }
    public virtual bool HasClips(){
        return currentClips > 0;
    }
    public virtual void Restock(){
        currentClips = maxClips;
    }
}

