using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 class Path {

	public readonly Vector3[] lookPoints;
	public readonly int finishLineIndex;
	public readonly int slowDownIndex;

	public Path(Vector3[] waypoints, float stoppingDst) {
		lookPoints = waypoints;



		float dstFromEndPoint = 0;
		for (int i = lookPoints.Length - 1; i > 0; i--) {
			dstFromEndPoint += Vector3.Distance (lookPoints [i], lookPoints [i - 1]);
			if (dstFromEndPoint > stoppingDst) {
				slowDownIndex = i;
				break;
			}
		}
	}
	

	public void DrawWithGizmos() {

		Gizmos.color = Color.black;
		foreach (Vector3 p in lookPoints) {
            Gizmos.DrawCube (new Vector3(p.x,EnvironmentPhysics.FindHeightAt(p.x,p.z) + 1,p.z), Vector3.one);
		}

		//Gizmos.color = Color.white;
		/*foreach (Line l in turnBoundaries) {
			l.DrawWithGizmos (10);
		}*/

	}

}