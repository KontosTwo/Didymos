using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerSetter : MonoBehaviour {
    [SerializeField]
    private GameObject targeterObject;

    private AmyModel amyTarget;
    private List<HumanoidTargeter> targeters;

    [SerializeField]
    private List<Transform> enemyMarkerLocations;


    private void Awake()
    {
        
        targeters = new List<HumanoidTargeter>();
        foreach (Transform enemy in targeterObject.transform)
        {
            HumanoidTargeter targeter = enemy.GetComponent<HumanoidTargeter>();
            if (targeter != null)
            {
                targeters.Add(targeter);
            }
        }

    }

    void Start () {
        amyTarget = HumanoidStore.GetAmyModel();
        enemyMarkerLocations.ForEach(m =>
        {
            EnemyMarker newMarker = new EnemyMarker(amyTarget, m.position, null);
            targeters.ForEach(t =>
            {
                CommunicatableEnemyMarker newCMarker = new CommunicatableEnemyMarker(
                    newMarker,
                    5
                );
                t.AddEnemyMarker(newCMarker);
            });
        });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
