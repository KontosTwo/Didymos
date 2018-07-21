using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyMarkerStore : MonoBehaviour {
    //private List<CommunicatableEnemyMarker> communicators;
    //private HashSet<EnemyMarker> enemyMarkers;

    private Dictionary<EnemyMarker, Integer> enemyMarkers;
    private List<EnemyMarker> enemyMarkersPublic;

    private static EnemyMarkerStore instance;

    private void Awake()
    {
        enemyMarkers = new Dictionary<EnemyMarker, Integer>();
        enemyMarkersPublic = new List<EnemyMarker>();
        instance = this;
    }
    void Start () {
		
	}

    private void Update()
    {
        Debug.Log(GetEnemyMarkers().Count);
    }


    public static void AddCommunicatorSubscriber(CommunicatableEnemyMarker communicator){
        Integer communicators = null;
        EnemyMarker enemyMarker = communicator.enemyMarker;
        instance.enemyMarkers.TryGetValue(enemyMarker, out communicators);
        if(communicators == null){
            instance.enemyMarkers.Add(enemyMarker, new Integer(0));
            instance.enemyMarkersPublic.Add(enemyMarker);
        }
        Integer existingCommunicator = instance.enemyMarkers[enemyMarker];
        existingCommunicator.value++;
    }

    public static void RemoveCommunicatorSubscriber(CommunicatableEnemyMarker communicator)
    {
        Integer communicators = null;
        EnemyMarker enemyMarker = communicator.enemyMarker;
        instance.enemyMarkers.TryGetValue(enemyMarker, out communicators);
        if (communicators != null)
        {
            communicators.value--;
            if(communicators.value == 0){
                instance.enemyMarkers.Remove(enemyMarker);
                instance.enemyMarkersPublic.Remove(enemyMarker);
            }
        }
    }

    public static List<EnemyMarker> GetEnemyMarkers(){
        return instance.enemyMarkersPublic;
    }
}
