using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

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

    private HashSet<EnemyMarker> hiddenEnemies;
    private EnemyMarker currentHiddenEnemy;
    private bool hasHiddenEnemy;
    private Dictionary<HumanoidModel,EnemyTarget> viewableEnemies;
    private DateTime lastUpdated;

    private void Awake()
    {
        hiddenEnemies = new HashSet<EnemyMarker>(new HiddenEnemyComparer());
        viewableEnemies = new Dictionary<HumanoidModel, EnemyTarget>();
        hasHiddenEnemy = false;
    }
    // Use this for initialization
    void Start () {
        HumanoidTargeterCommunicator.AddBlackBoardSubscriber(this);
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(gameObject.name + ": " + hiddenEnemies.Count + " markers");
	}

    public void SeesEnemy(HumanoidModel enemy, Vector3 location){
        /*
         * first remove the nearest enemy marker that is in range
         * if seeing enemy for the first time
         */
        EnemyTarget target = null;
        viewableEnemies.TryGetValue(enemy, out target);
        if(target == null){
            foreach(EnemyMarker marker in hiddenEnemies){
                if (Vector3.Distance(location, marker.GetLocation()) < hiddenEnemyRadius){
                    RemoveHiddenEnemy(marker);
                    //HumanoidTargeterCommunicator.CommunicateDeleteEnemyMarker(this, marker);
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
            // avoid clustering enemy markers in one spot
            if (NoMarkerTooCloseTo(newMarker)){
                AddHiddenEnemy(newMarker);
                //HumanoidTargeterCommunicator.CommunicateAddEnemyMarker(this, newMarker);
            }
        }
    }

    private bool NoMarkerTooCloseTo(EnemyMarker target){
        Vector3 targetLocation = target.GetLocation();
        foreach(EnemyMarker marker in hiddenEnemies){
            Vector3 markerLocation = marker.GetLocation();
            if(Vector3.Distance(markerLocation,targetLocation) < hiddenEnemyRadius){
                return false;
            }
        }
        return true;
    }

    public void ReceiveEnemyMarkerFromFriend(EnemyMarker marker){
        //if(!hiddenEnemies.Contains(marker)){
            AddHiddenEnemy(marker);
            //marker.SwitchToNewFounder(this);
            //HumanoidTargeterCommunicator.CommunicateAddEnemyMarker(this, marker);
        //}
    }

    public void DeleteEnemyMarkerFromFriend(EnemyMarker marker)
    {
        //if (hiddenEnemies.Contains(marker))
        //{
            RemoveHiddenEnemy(marker);
            //marker.SwitchToNewFounder(this);
            //HumanoidTargeterCommunicator.CommunicateDeleteEnemyMarker(this,marker);
        //}
    }

    public void InterruptCommunication(){
        HumanoidTargeterCommunicator.InterruptAddEnemyMarker(this);
        HumanoidTargeterCommunicator.InterruptDeleteEnemyMarker(this);
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

    public bool AlreadyHasMarker(EnemyMarker marker){
        return hiddenEnemies.Contains(marker);
    }


    private class HiddenEnemyComparer : IEqualityComparer<EnemyMarker>{
        public bool Equals(EnemyMarker a,EnemyMarker b ){
            return a.GetEnemy().Equals(b.GetEnemy()) &&
                    a.GetLocation().Equals(b.GetLocation());
        }

        public int GetHashCode(EnemyMarker marker){
            int hashCode = 1;
            hashCode = 37 * hashCode + marker.GetEnemy().GetHashCode();
            hashCode = 37 * hashCode + marker.GetLocation().GetHashCode();
            return hashCode;
        }
    }

    private void AddHiddenEnemy(EnemyMarker marker){
        hiddenEnemies.Add(marker);
        //Debug.Log("Adding enemy for " + gameObject.name + "new size: " + hiddenEnemies.Count);
        EnemyTargeterDebugger.AddEnemyMarker(marker);
        //Debug.Log("old size:"  + marker.usedBy.Count);
        marker.usedBy.Add(this);
        //Debug.Log("new size:" + marker.usedBy.Count);

    }

    private void RemoveHiddenEnemy(EnemyMarker marker){
        hiddenEnemies.Remove(marker);
        marker.usedBy.Remove(this);
    }

    private void CommunicateUpdate()
    {
        lastUpdated = DateTime.Now;
        HumanoidTargeterCommunication2.CommunicateUpdate();
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
}
