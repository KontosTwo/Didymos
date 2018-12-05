using UnityEngine;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;

public class PathRequestManager : MonoBehaviour{

	Queue<PathResult> results = new Queue<PathResult>();

	static PathRequestManager instance;

	void Awake() {
		instance = this;
	}

	void Update() {
		if (results.Count > 0) {
			int itemsInQueue = results.Count;
			lock (results) {
				for (int i = 0; i < itemsInQueue; i++) {
					PathResult result = results.Dequeue ();
					result.callback (result.path, result.success);
				}
			}
		}
	}

	public static void RequestPath(PathRequest request) {
		ThreadStart threadStart = delegate {
			Pathfinder.FindPath (request, instance.FinishedProcessingPath);
		};
		threadStart.Invoke ();
	}

	public void FinishedProcessingPath(PathResult result) {
		lock (results) {
			results.Enqueue (result);
		}
	}


    public static int counter = 0;

}


