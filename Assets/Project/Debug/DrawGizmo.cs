using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DrawGizmo : MonoBehaviour {
    private HashSet<DebugGizmo> gizmos;

    private static DrawGizmo instance;

    private void Awake()
    {
        instance = this;
        instance.gizmos = new HashSet<DebugGizmo>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void AddGizmo(
        Color color,
        string text,
        Vector3 location
    ){
        instance.gizmos.Add(
            new DebugGizmo(color, text, location)
        );
    }

	public static void ClearGizmo(){
		instance.gizmos.Clear();
	}

    private class DebugGizmo{
        public Color color;
        public string text;
        public Vector3 location;

        public DebugGizmo(Color c, string t, Vector3 l){
            color = c;
            text = t;
            location = l;
        }

        public override int GetHashCode()
        {
            return location.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return ((DebugGizmo)obj).location.Equals(this.location);
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        foreach (DebugGizmo gizmo in gizmos){
            Gizmos.color = gizmo.color;
            Gizmos.DrawCube(gizmo.location, new Vector3(.3f, .3f, .3f));
            Handles.Label(gizmo.location, gizmo.text);
        }
    }
}
