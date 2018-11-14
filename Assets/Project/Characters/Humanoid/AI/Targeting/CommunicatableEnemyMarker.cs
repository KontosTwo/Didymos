using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CommunicatableEnemyMarker {
    private EnemyMarker enemyMarker;
    private bool valid;
    private Queue<Vector3> secondaryLocations;

    private float radius;
    private static readonly float STEPLENGTH = 1.0f;
    private static readonly float ACCEPTABLEHEIGHTDIFFERENCEBETWEENSTEPS = 1.5f;
    private static readonly Projectile WHATTHEENEMYCANPASSTHROUGH = new Projectile(
                0,
                0.01f
            );
    public CommunicatableEnemyMarker(EnemyMarker marker,float radius)
    {
        this.enemyMarker = marker;
        valid = true;
        this.radius = radius;
        CreateSecondaryLocations();
        foreach(Vector3 location in secondaryLocations){
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = location;
        }
    }

    private CommunicatableEnemyMarker(){
        
    }

    public CommunicatableEnemyMarker GetNewMarker(){
        CommunicatableEnemyMarker newMarker = new CommunicatableEnemyMarker();
        newMarker.radius = this.radius;
        newMarker.enemyMarker = this.enemyMarker;
        newMarker.valid = this.valid;
        newMarker.secondaryLocations = new Queue<Vector3>(this.secondaryLocations);
        return newMarker;
    }

    public bool IsValid(){
        return valid;
    }

    public void Invalidate(){
        valid = false;
    }

    public EnemyMarker GetEnemyMarker(){
        return enemyMarker;
    }

    public bool HasSecondaryLocations(){
        return secondaryLocations.Count != 0;
    }

    public Vector3 GetSecondaryLocation(){
        return secondaryLocations.Peek();
    }

    public void InvalidateSecondaryLocation(){
        secondaryLocations.Dequeue();
    }

    private void CreateSecondaryLocations(){
        secondaryLocations = new Queue<Vector3>();
        CreateLocationInDirection(new Vector2(0, 1));
        CreateLocationInDirection(new Vector2(0, -1));
        CreateLocationInDirection(new Vector2(1, 0));
        CreateLocationInDirection(new Vector2(-1, 0));

        CreateLocationInDirection(new Vector2(1, 1));
        CreateLocationInDirection(new Vector2(-1, 1));
        CreateLocationInDirection(new Vector2(1, -1));
        CreateLocationInDirection(new Vector2(-1, -1));
    }

    private void CreateLocationInDirection(Vector2 direction){
        Vector2 center = enemyMarker.GetLocation().To2D();

        Vector2 secondaryLocation;
        float heightAtSecondaryLocation;
        Vector2 previousSecondaryLocation = center;
        float heightAtPreviousSecondaryLocation =
            EnvironmentPhysics.FindHeightAt(
                previousSecondaryLocation.x, 
                previousSecondaryLocation.y
            );

        direction = direction.normalized;
        Vector2 dx = direction * STEPLENGTH;

        int numberOfSteps = (int)(radius / STEPLENGTH);
        for (int i = 0; i < numberOfSteps; i++){
            secondaryLocation = center + (dx * i);
            heightAtSecondaryLocation =
                EnvironmentPhysics.FindHeightAt(
                    secondaryLocation.x,
                    secondaryLocation.y
                );

            if(Mathf.Abs(
                    heightAtSecondaryLocation - heightAtPreviousSecondaryLocation
                ) > ACCEPTABLEHEIGHTDIFFERENCEBETWEENSTEPS || 
               !EnvironmentPhysics.WalkableAt(secondaryLocation.x,secondaryLocation.y,ConversionHub.GetSoldierRadius())){
                break;
            }

            heightAtPreviousSecondaryLocation = heightAtSecondaryLocation;
            previousSecondaryLocation = secondaryLocation;
        }
        Vector3 secondaryLocation3 = new Vector3(
            previousSecondaryLocation.x,
            heightAtPreviousSecondaryLocation,
            previousSecondaryLocation.y
        );
        secondaryLocations.Enqueue(secondaryLocation3);
    }
}
