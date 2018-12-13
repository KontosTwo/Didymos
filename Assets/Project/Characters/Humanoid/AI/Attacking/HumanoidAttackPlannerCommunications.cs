using System;
using System.Collections.Generic;
using UnityEngine;

public  class HumanoidAttackPlannerCommunications : MonoBehaviour{
    private  List<HumanoidAttackPlanner> planners;
    private  List<AttackableObject<EnemyMarker>> markersForAttack;
    private  List<AttackableObject<EnemyTarget>> targetsForAttack;

    private static HumanoidAttackPlannerCommunications instance;

    static HumanoidAttackPlannerCommunications(){

    }

    private void Awake(){
        planners = new List<HumanoidAttackPlanner>();
        markersForAttack = new List<AttackableObject<EnemyMarker>> ();
        targetsForAttack = new List<AttackableObject<EnemyTarget>>();
        instance = this;
    }

    public static void AddPlanner(
        HumanoidAttackPlanner planner
    ){
        instance.planners.Add(planner);
    }

    public static void FlankRightHiddenEnemy(EnemyMarker enemy){
        AttackableObject<EnemyMarker> info = instance.markersForAttack.Find(m => {
            return m.GetObject() == enemy;
        });

        if (info == null)
        {
            Debug.LogError("No enemy marker found!");
        }
        else
        {
            info.AddRightFlanker();
        }
    }

    public static void FlankLeftHiddenEnemy(EnemyMarker enemy)
    {
        AttackableObject<EnemyMarker> info = instance.markersForAttack.Find(m => {
            return m.GetObject() == enemy;
        });

        if (info == null)
        {
            Debug.LogError("No enemy marker found!");
        }
        else
        {
            info.AddLeftFlanker();
        }
    }

    public static void SuppressHiddenEnemy(EnemyMarker enemy)
    {
        AttackableObject<EnemyMarker> info = instance.markersForAttack.Find(m => {
            return m.GetObject() == enemy;
        });

        if (info == null)
        {
            Debug.LogError("No enemy marker found!");
        }
        else
        {
            info.AddSuppressor();
        }
    }

    public static void FlankRightVisibleEnemy(EnemyTarget enemy)
    {
        AttackableObject<EnemyTarget> info = instance.targetsForAttack.Find(m => {
            return m.GetObject() == enemy;
        });

        if (info == null)
        {
            Debug.LogError("No enemy target found!");
        }
        else
        {
            info.AddRightFlanker();
        }
    }

    public static void FlankLeftVisibleEnemy(EnemyTarget enemy)
    {
        AttackableObject<EnemyTarget> info = instance.targetsForAttack.Find(m => {
            return m.GetObject() == enemy;
        });

        if (info == null)
        {
            Debug.LogError("No enemy target found!");
        }
        else
        {
            info.AddLeftFlanker();
        }
    }

    public static void SuppressVisibleEnemy(EnemyTarget enemy)
    {
        AttackableObject<EnemyTarget> info = instance.targetsForAttack.Find(m => {
            return m.GetObject() == enemy;
        });

        if (info == null)
        {
            Debug.LogError("No enemy target found!");
        }
        else
        {
            info.AddSuppressor();
        }
    }

    public static void NoLongerFlankRightHiddenEnemy(EnemyMarker enemy)
    {
        AttackableObject<EnemyMarker> info = instance.markersForAttack.Find(m => {
            return m.GetObject() == enemy;
        });

        if (info == null)
        {
            Debug.LogError("No enemy marker found!");
        }
        else
        {
            info.RemoveRightFlanker();
        }
    }

    public static void NoLongerFlankLeftHiddenEnemy(EnemyMarker enemy)
    {
        AttackableObject<EnemyMarker> info = instance.markersForAttack.Find(m => {
            return m.GetObject() == enemy;
        });

        if (info == null)
        {
            Debug.LogError("No enemy marker found!");
        }
        else
        {
            info.RemoveLeftFlanker();
        }
    }

    public static void NoLongerSuppressHiddenEnemy(EnemyMarker enemy)
    {
        AttackableObject<EnemyMarker> info = instance.markersForAttack.Find(m => {
            return m.GetObject() == enemy;
        });

        if (info == null)
        {
            Debug.LogError("No enemy marker found!");
        }
        else
        {
            info.RemoveSuppressor();
        }
    }

    public static void NoLongerFlankRightVisibleEnemy(EnemyTarget enemy)
    {
        AttackableObject<EnemyTarget> info = instance.targetsForAttack.Find(m => {
            return m.GetObject() == enemy;
        });

        if (info == null)
        {
            Debug.LogError("No enemy target found!");
        }
        else
        {
            info.RemoveRightFlanker();
        }
    }

    public static void NoLongerFlankLeftVisibleEnemy(EnemyTarget enemy)
    {
        AttackableObject<EnemyTarget> info = instance.targetsForAttack.Find(m => {
            return m.GetObject() == enemy;
        });

        if (info == null)
        {
            Debug.LogError("No enemy target found!");
        }
        else
        {
            info.RemoveLeftFlanker();
        }
    }

    public static void NoLongerSuppressVisibleEnemy(EnemyTarget enemy)
    {
        AttackableObject<EnemyTarget> info = instance.targetsForAttack.Find(m => {
            return m.GetObject() == enemy;
        });

        if (info == null)
        {
            Debug.LogError("No enemy target found!");
        }
        else
        {
            info.RemoveSuppressor();
        }
    }





    public static int GetRightFlankersOnHiddenEnemy(
        EnemyMarker target
    ){
        AttackableObject < EnemyMarker> info = instance.markersForAttack.Find(m =>{
            return m.GetObject() == target;        
        });

        if(info == null){
            Debug.LogError("No enemy marker found!");
            return 0;
        }else{
            return info.GetRightFlankers();
        }
    }

    public static int GetLeftFlankersOnHiddenEnemy(
       EnemyMarker target
    ){
        AttackableObject<EnemyMarker> info = instance.markersForAttack.Find(m => {
            return m.GetObject() == target;
        });

        if (info == null){
            Debug.LogError("No enemy marker found!");
            return 0;
        }
        else{
            return info.GetLeftFlankers();
        }
    }

    public static int GetSuppressorsOnHiddenEnemy(
       EnemyMarker target
    ){
        AttackableObject<EnemyMarker> info = instance.markersForAttack.Find(m => {
            return m.GetObject() == target;
        });

        if (info == null){
            Debug.LogError("No enemy marker found!");
            return 0;
        }
        else{
            return info.GetCoveringFireAttackers();
        }
    }

    public static int GetRightFlankersOnVisibleEnemy(
        EnemyTarget target
    ){
        AttackableObject<EnemyTarget> info = instance.targetsForAttack.Find(m => {
            return m.GetObject() == target;
        });

        if (info == null){
            Debug.LogError("No enemy target found!");
            return 0;
        }
        else{
            return info.GetRightFlankers();
        }
    }

    public static int GetLeftFlankersOnVisibleEnemy(
       EnemyTarget target
    ){
        AttackableObject<EnemyTarget> info = instance.targetsForAttack.Find(m => {
            return m.GetObject() == target;
        });

        if (info == null){
            Debug.LogError("No enemy target found!");
            return 0;
        }
        else{
            return info.GetLeftFlankers();
        }
    }

    public static int GetSuppressorsOnVisibleEnemy(
       EnemyTarget target
    ){
        AttackableObject<EnemyTarget> info = instance.targetsForAttack.Find(m => {
            return m.GetObject() == target;
        });

        if (info == null){
            Debug.LogError("No enemy target found!");
            return 0;
        }
        else{
            return info.GetCoveringFireAttackers();
        }
    }

    public static List<HumanoidAttackPlanner> GetRightRelocators(){
        return instance.planners.FindAll(p =>{
            return p.IsRelocatingRight();
        });
    }

    public static List<HumanoidAttackPlanner> GetLeftRelocators(){
        return instance.planners.FindAll(p => {
            return p.IsRelocatingLeft();
        });
    }



    private class AttackableObject<T>{
        private T target;

        private int coveringFireAttackers;
        private int leftFlankers;
        private int rightFlankers;

        public AttackableObject(T obj){
            target = obj;
        }

        public T GetObject(){
            return target;
        }

        public int GetCoveringFireAttackers(){
            return coveringFireAttackers;
        }

        public int GetRightFlankers(){
            return rightFlankers;
        }

        public int GetLeftFlankers(){
            return leftFlankers;
        }

        public void AddRightFlanker(){
            rightFlankers++;
        }

        public void AddLeftFlanker(){
            leftFlankers++;
        }

        public void AddSuppressor(){
            coveringFireAttackers++;
        }

        public void RemoveRightFlanker(){
            rightFlankers--;
        }

        public void RemoveLeftFlanker(){
            leftFlankers--;
        }

        public void RemoveSuppressor(){
            coveringFireAttackers--;
        }
    }
}

