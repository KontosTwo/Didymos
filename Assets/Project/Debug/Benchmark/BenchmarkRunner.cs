using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenchmarkRunner : MonoBehaviour {

    public Transform start;
    public Transform end;




    private void Start()
    {
        /*Benchmark.Test(() =>
        {
            Vector3 shooter = start.position;
            Vector3 target = end.position;
            Projectile shooterProj = new Projectile(100, 2);
            Projectile enemyProj = new Projectile(100, 2);

            for (int i = 0; i < 100; i ++){
                EnvironmentPhysics.CalculateTerrainDisparityBetween(
                    shooterProj,
                    enemyProj,
                    shooter,
                    target
                );
            }
        });*/
    }
    // Update is called once per frame
    void Update () {
		
	}
}
