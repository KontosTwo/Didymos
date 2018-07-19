using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

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

    private HashSet<CommunicatableEnemyMarker> hiddenEnemies;
    private EnemyMarker currentHiddenEnemy;
    private bool hasHiddenEnemy;
    private Dictionary<HumanoidModel,EnemyTarget> viewableEnemies;
    private DateTime lastUpdated;

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
        //Debug.Log(gameObject.name + ": " + hiddenEnemies.Count + " markers, of which " + ValidMarkers() + " is valid");
	}

    private int ValidMarkers(){
        return hiddenEnemies.Where(m => m.valid).Count();
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
                if (Vector3.Distance(location, marker.enemyMarker.GetLocation()) < hiddenEnemyRadius){
                    marker.valid = false;
                    HumanoidTargeterCommunication.Communicate(
                        new CommunicationPackage<CommunicatableEnemyMarker>(
                            hiddenEnemies,
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
                newMarker
            );
            // avoid clustering enemy markers in one spot
            if (NoMarkerTooCloseTo(newCMarker)){
                AddHiddenEnemy(newCMarker);
                HumanoidTargeterCommunication.Communicate(
                    new CommunicationPackage<CommunicatableEnemyMarker>(
                        hiddenEnemies,
                        this
                    )
                );
            }
        }
    }

    private bool NoMarkerTooCloseTo(CommunicatableEnemyMarker target){
        Vector3 targetLocation = target.enemyMarker.GetLocation();
        foreach(CommunicatableEnemyMarker marker in hiddenEnemies){
            Vector3 markerLocation = marker.enemyMarker.GetLocation();
            if(Vector3.Distance(markerLocation,targetLocation) < hiddenEnemyRadius){
                return false;
            }
        }
        return true;
    }

    public void RecieveCommunication(CommunicationPackage<CommunicatableEnemyMarker> package){
        HashSet<CommunicatableEnemyMarker> markerPayload = package.GetPayload();
        HashSet<CommunicatableEnemyMarker> toBeRemoved = new HashSet<CommunicatableEnemyMarker>();
        foreach(CommunicatableEnemyMarker marker in markerPayload){
            if(marker.valid){
                if (NoMarkerTooCloseTo(marker)){
                    AddHiddenEnemy(marker.GetNewMarker());
                }
            }else{
                Debug.Log("removing");
                hiddenEnemies.Add(marker);
            }
        }

        HumanoidTargeterCommunication.Communicate(package.RecievedBy(this,hiddenEnemies));
        foreach(var remove in toBeRemoved){
            RemoveHiddenEnemy(remove);
        }
    }

    public void InterruptCommunication(){
        HumanoidTargeterCommunication.InterruptUpdate(this);
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


    private class HiddenEnemyComparer : IEqualityComparer<CommunicatableEnemyMarker>{
        public bool Equals(CommunicatableEnemyMarker a,CommunicatableEnemyMarker b ){
            return a.enemyMarker.GetEnemy().Equals(b.enemyMarker.GetEnemy()) &&
                    a.enemyMarker.GetLocation().Equals(b.enemyMarker.GetLocation());
        }

        public int GetHashCode(CommunicatableEnemyMarker marker){
            int hashCode = 1;
            hashCode = 37 * hashCode + marker.enemyMarker.GetEnemy().GetHashCode();
            hashCode = 37 * hashCode + marker.enemyMarker.GetLocation().GetHashCode();
            return hashCode;
        }
    }

    private void AddHiddenEnemy(CommunicatableEnemyMarker marker)
    {
        hiddenEnemies.Add(marker);
        EnemyTargeterDebugger.AddEnemyMarker(marker.enemyMarker);
        marker.enemyMarker.usedBy.Add(this);
    }

    private void RemoveHiddenEnemy(CommunicatableEnemyMarker marker){
        hiddenEnemies.Remove(marker);
        marker.enemyMarker.usedBy.Remove(this);
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
