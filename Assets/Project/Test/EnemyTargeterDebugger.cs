using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyTargeterDebugger : MonoBehaviour {

    private static EnemyTargeterDebugger instance;

    private List<EnemyMarker> enemyMarkers;

    private void Awake()
    {
        instance = this;
    }
    // Use this for initialization
    void Start () {
        enemyMarkers = EnemyMarkerStore.GetEnemyMarkers();

	}
	
	// Update is called once per frame
	void Update () {
	}


    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        foreach (EnemyMarker marker in enemyMarkers)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(marker.GetLocation(), new Vector3(1, 1, 1));

            string names = "";
            foreach (HumanoidTargeter targeter in marker.GetUsers())
            {
                names += targeter.gameObject.name + "\n";
            }
            Handles.Label(
                marker.GetLocation(),
                names
            );
        }
    }

    private class HiddenEnemyComparer : IEqualityComparer<EnemyMarker>
    {
        public bool Equals(EnemyMarker a, EnemyMarker b)
        {
            return a.GetEnemy().Equals(b.GetEnemy()) &&
                    a.GetLocation().Equals(b.GetLocation());
        }

        public int GetHashCode(EnemyMarker marker)
        {
            int hashCode = 1;
            hashCode = 37 * hashCode + marker.GetEnemy().GetHashCode();
            hashCode = 37 * hashCode + marker.GetLocation().GetHashCode();
            return hashCode;
        }
    }
}
