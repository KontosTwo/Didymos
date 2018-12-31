using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class Follower : MonoBehaviour {

	const float minPathUpdateTime = .1f;
	const float pathUpdateMoveThreshold = .5f;

    public PathfinderCostStrategy nonCoverStrategy;
    public PathfinderCostStrategy coverStrategy;

    public float maxLength;

	public Transform target;
	public float speed = 20;
	public float turnSpeed = 3;
	public float turnDst = 5;
	public float stoppingDst = 10;

    private Stopwatch watch;

	Path path;

	void Start() {
        watch = new Stopwatch();
		StartCoroutine (UpdatePath ());
	}

	IEnumerator UpdatePath() {

		if (Time.timeSinceLevelLoad < .3f) {
			yield return new WaitForSeconds (.3f);
		}
        PathResult result = Pathfinder.FindPath(
            new PathRequest(
                transform.position,
                target.position,
                maxLength,
                new BaseImplementation(
                    coverStrategy,
                    new FavorCoverAndStrategyCostCreator()
                )
            )
        );
        bool pathSuccessful = result.success;
        if (pathSuccessful)
        {
            path = new Path(result.path, stoppingDst);
            watch.Stop();
            UnityEngine.Debug.Log("Elapsed in ms: " + watch.ElapsedMilliseconds);
            watch = new Stopwatch();
        }

        float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
		Vector3 targetPosOld = target.position;

		while (true) {
			yield return new WaitForSeconds (minPathUpdateTime);
			if ((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshold) {
                 result = Pathfinder.FindPath(
                    new PathRequest(
                        transform.position,
                        target.position,
                        maxLength,
                        new BaseImplementation(
                            coverStrategy,
                            new FavorCoverAndStrategyCostCreator()
                        )
                    )
                );

                pathSuccessful = result.success;
                if (pathSuccessful)
                {
                    path = new Path(result.path, stoppingDst);
                    watch.Stop();
                    UnityEngine.Debug.Log("Elapsed in ms: " + watch.ElapsedMilliseconds);
                    watch = new Stopwatch();
                }
                targetPosOld = target.position;
                watch.Start();
			}
		}
	}

	IEnumerator FollowPath() {

	/*	bool followingPath = true;
		int pathIndex = 0;
		transform.LookAt (path.lookPoints [0]);

		float speedPercent = 1;

		while (followingPath) {
			Vector2 pos2D = new Vector2 (transform.position.x, transform.position.z);
			while (path.turnBoundaries [pathIndex].HasCrossedLine (pos2D)) {
				if (pathIndex == path.finishLineIndex) {
					followingPath = false;
					break;
				} else {
					pathIndex++;
				}
			}

			if (followingPath) {

				if (pathIndex >= path.slowDownIndex && stoppingDst > 0) {
					speedPercent = Mathf.Clamp01 (path.turnBoundaries [path.finishLineIndex].DistanceFromPoint (pos2D) / stoppingDst);
					if (speedPercent < 0.01f) {
						followingPath = false;
					}
				}

				Quaternion targetRotation = Quaternion.LookRotation (path.lookPoints [pathIndex] - transform.position);
				transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
				transform.Translate (Vector3.forward * Time.deltaTime * speed * speedPercent, Space.Self);
			}

			yield return null;

		}*/
		yield return null;
	}

	public void OnDrawGizmos() {
		if (path != null) {
			path.DrawWithGizmos ();
		}
	}
}