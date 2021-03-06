﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
/*
 * If there is a bug, chances are you didn't
 * create a deep copy of something
 */
public class HumanoidTargeter : MonoBehaviour {
    [SerializeField]
    private Transform centerBottom;

    [SerializeField]
    private float timeToCommunicateByMouth;
    [SerializeField]
    private float communicateRadius;
    [SerializeField]
    private bool hasRadio;
    [SerializeField]
    private float timeToCommunicateByRadio;
    [SerializeField]
    private float hiddenEnemyRadius;
    [SerializeField]
    private float checkHiddenEnemyRadius;


    private HashSet<CommunicatableEnemyMarker> hiddenEnemies;
    private EnemyMarker currentHiddenEnemy;
    private bool hasHiddenEnemy;
    private Dictionary<HumanoidModel,EnemyTarget> viewableEnemies;

    private void Awake()
    {
        hiddenEnemies = new HashSet<CommunicatableEnemyMarker>(new HiddenEnemyComparer());
        viewableEnemies = new Dictionary<HumanoidModel, EnemyTarget>();
        hasHiddenEnemy = false;
    }
    // Use this for initialization
    void Start () {
        HumanoidTargeterCommunication.AddBlackBoardSubscriber(this);
	}
	
	// Update is called once per frame
	void Update () {
       // Debug.Log(gameObject.name + ": " + hiddenEnemies.Count + " markers, of which " + ValidMarkers() + " is valid");
	}

    public void SeesEnemy(HumanoidModel enemy, Vector3 location){
        /*
         * first remove the nearest enemy marker that is in range
         * if seeing enemy for the first time
         */
        EnemyTarget target = null;
        viewableEnemies.TryGetValue(enemy, out target);
        if(target == null){
            foreach(CommunicatableEnemyMarker marker in hiddenEnemies){
                if (Vector3.Distance(location, marker.GetEnemyMarker().GetLocation()) < hiddenEnemyRadius){
                    marker.Invalidate();
                    HumanoidTargeterCommunication.Communicate(
                        new CommunicationPackage(
                            GetDeepCopyOfHiddenEnemies(),
                            this
                        )
                    );
                    RemoveHiddenEnemy(marker);
                    break;
                }
            }
        }

        // next add viewable enemy
        viewableEnemies[enemy] = new EnemyTarget(enemy,location);
    }

    public void DoesNotSeeEnemy(HumanoidModel enemy){
        EnemyTarget enemyTarget;
        viewableEnemies.TryGetValue(enemy, out enemyTarget);
        if(enemyTarget != null){
            viewableEnemies.Remove(enemy);
            EnemyMarker newMarker = new EnemyMarker(enemyTarget,this);
            CommunicatableEnemyMarker newCMarker = new CommunicatableEnemyMarker(
                newMarker,
                checkHiddenEnemyRadius
            );
            // avoid clustering enemy markers in one spot
            if (NoMarkerTooCloseTo(newCMarker)){
                AddHiddenEnemy(newCMarker);
                HumanoidTargeterCommunication.Communicate(
                    new CommunicationPackage(
                        GetDeepCopyOfHiddenEnemies(),
                        this
                    )
                );

            }
        }
    }

    private bool NoMarkerTooCloseTo(CommunicatableEnemyMarker target){
        Vector3 targetLocation = target.GetEnemyMarker().GetLocation();
        foreach(CommunicatableEnemyMarker marker in hiddenEnemies){
            Vector3 markerLocation = marker.GetEnemyMarker().GetLocation();
            if(Vector3.Distance(markerLocation,targetLocation) < hiddenEnemyRadius){
                return false;
            }
        }
        return true;
    }
    /*
     * Communicator must be in hiddenEnemies
     */
    public void InvalidateMarker(CommunicatableEnemyMarker communicator){
        communicator.Invalidate();
        HumanoidTargeterCommunication.Communicate(
            new CommunicationPackage(
                GetDeepCopyOfHiddenEnemies(),
                this
            )
        );
        RemoveHiddenEnemy(communicator);
    }

    public void RecieveCommunication(CommunicationPackage package){
        //Debug.Log("Sent by: " + package.GetIssuer().gameObject.name + " to " + this.gameObject.name + " for package " + package.id);
        HashSet<CommunicatableEnemyMarker> markerPayload = package.GetPayload();
        foreach(CommunicatableEnemyMarker marker in markerPayload){
            if(marker.IsValid()){
                if (NoMarkerTooCloseTo(marker)){
                    AddHiddenEnemy(marker.GetNewMarker());
                }
            }else{
                RemoveHiddenEnemy(marker);
            }
        }

        HumanoidTargeterCommunication.Communicate(package.RecievedBy(this));
    }

    public void InterruptCommunication(){
        HumanoidTargeterCommunication.InterruptUpdate(this);
    }

    public HashSet<CommunicatableEnemyMarker> GetEnemyMarkers(){
        return hiddenEnemies;
    }

    public List<HumanoidVantage> GetAllKnownVantages(){
        List<HumanoidVantage> allVantages = new List<HumanoidVantage>();
        foreach(CommunicatableEnemyMarker marker in hiddenEnemies){
            allVantages.Add(marker.GetEnemyMarker().GetVantage());
        }
        foreach(HumanoidModel seenEnemy in viewableEnemies.Keys){
            allVantages.Add(seenEnemy.InfoGetVantageData());
        }
        return allVantages;
    }

    public bool HasHiddenEnemy(){
        return hasHiddenEnemy;
    }

    public bool HasRadio(){
        return hasRadio;
    }

    public float GetTimeToCommunicateByMouth(){
        return timeToCommunicateByMouth;
    }

    public float GetTimeToCommunicateByRadio()
    {
        return timeToCommunicateByRadio;
    }

    public bool CanCommunicate(HumanoidTargeter other){
        return Vector3.Distance(centerBottom.position, other.centerBottom.position) < communicateRadius;
    }

    public ConvexPolygon GetEnemyBounds(){
        List<Vector3> enemyLocations = new List<Vector3>();
        enemyLocations.AddRange(hiddenEnemies.Select(enemy => enemy.GetEnemyMarker().GetLocation()));
        enemyLocations.AddRange(viewableEnemies.Keys.Select(enemy => enemy.InfoGetCenterBottom()));
        List<Vector2> enemyLocations2D = enemyLocations.Select(location => new Vector2(location.x, location.z)).ToList();
        ConvexPolygon enemyBounds = new ConvexPolygon(enemyLocations2D);
        return enemyBounds;
    }

    public Tuple<List<EnemyMarker>,List<EnemyTarget>> GetBoundingEnemies(){
        return ConvexHull.MakeHullTwo<EnemyMarker, EnemyTarget>(
            hiddenEnemies.Select(h => h.GetEnemyMarker()).ToList(),
            new List<EnemyTarget>(),
            m => m.GetLocation().To2D(),
            t => new Vector2()
        );
    }

    private void RecalculateEnemyBounds(){

    }

    private HashSet<CommunicatableEnemyMarker> GetDeepCopyOfHiddenEnemies(){
        HashSet<CommunicatableEnemyMarker> copy = new HashSet<CommunicatableEnemyMarker>();
        foreach(var marker in hiddenEnemies){
            copy.Add(marker.GetNewMarker());
        }
        return copy;
    }


    private class HiddenEnemyComparer : IEqualityComparer<CommunicatableEnemyMarker>{
        public bool Equals(CommunicatableEnemyMarker a,CommunicatableEnemyMarker b ){
            return a.GetEnemyMarker().GetEnemy().Equals(b.GetEnemyMarker().GetEnemy()) &&
                    a.GetEnemyMarker().GetLocation().Equals(b.GetEnemyMarker().GetLocation());
        }

        public int GetHashCode(CommunicatableEnemyMarker marker){
            int hashCode = 1;
            hashCode = 37 * hashCode + marker.GetEnemyMarker().GetEnemy().GetHashCode();
            hashCode = 37 * hashCode + marker.GetEnemyMarker().GetLocation().GetHashCode();
            return hashCode;
        }
    }

    private void AddHiddenEnemy(CommunicatableEnemyMarker marker)
    {
        hiddenEnemies.Add(marker);
        EnemyMarkerStore.AddCommunicatorSubscriber(marker);

        marker.GetEnemyMarker().AddUser(this);
    }

    private void RemoveHiddenEnemy(CommunicatableEnemyMarker marker){
        hiddenEnemies.Remove(marker);
        EnemyMarkerStore.RemoveCommunicatorSubscriber(marker);

        marker.GetEnemyMarker().RemoveUser(this);
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        foreach (EnemyTarget target in viewableEnemies.Values)
        {
            Gizmos.color = Color.grey;
            Gizmos.DrawCube(target.GetLocation(), new Vector3(1, 1, 1));
        }
    }

    public void AddEnemyMarker(CommunicatableEnemyMarker newCMarker){
        AddHiddenEnemy(newCMarker);
    }
}
