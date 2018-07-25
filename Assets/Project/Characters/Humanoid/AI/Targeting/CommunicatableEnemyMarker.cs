using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunicatableEnemyMarker {
    private EnemyMarker enemyMarker;
    private bool valid;
    private List<Vector3> secondaryLocations;

    private float radius;
    private static readonly float STEPLENGTH = 1.0f;
    private static readonly float ACCEPTABLEHEIGHTDIFFERENCEBETWEENSTEPS = .5f;

    public CommunicatableEnemyMarker(EnemyMarker marker,float radius)
    {
        this.enemyMarker = marker;
        valid = true;
        this.radius = radius;
        CreateSecondaryLocations();
    }

    public CommunicatableEnemyMarker GetNewMarker(){
        CommunicatableEnemyMarker newMarker = new CommunicatableEnemyMarker(
            this.enemyMarker,
            this.radius
        );
        newMarker.valid = this.valid;
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
        return secondaryLocations[0];
    }

    public void InvalidateSecondaryLocation(Vector3 toBeInvalidated){
        secondaryLocations.Remove(toBeInvalidated);
    }

    private void CreateSecondaryLocations(){
        secondaryLocations = new List<Vector3>();
        CreateLocationInDirection(new Vector2(0, 1));
        CreateLocationInDirection(new Vector2(0, -1));
        CreateLocationInDirection(new Vector2(1, 0));
        CreateLocationInDirection(new Vector2(-1, 0));
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
        for (int i = 0; i < numberOfSteps; i ++){
            secondaryLocation = center + (dx * i);
            heightAtSecondaryLocation = 
                EnvironmentPhysics.FindHeightAt(
                    secondaryLocation.x,
                    secondaryLocation.y
                );
            if(Mathf.Abs(
                    heightAtSecondaryLocation - heightAtPreviousSecondaryLocation
                ) > ACCEPTABLEHEIGHTDIFFERENCEBETWEENSTEPS){
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
        secondaryLocations.Add(secondaryLocation3);
    }
}
