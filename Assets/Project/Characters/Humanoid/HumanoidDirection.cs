using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidDirection : MonoBehaviour{
    private Vector2 direction;
    [SerializeField]
    private Transform center;


    public void Face(Vector3 position){
        direction = position.To2D() - center.position.To2D();
        direction = direction.normalized;
    }

    public bool WithinRangeOfVision(Vector3 target, float degrees){
        Vector2 targetVector = target.To2D() - center.position.To2D();
        float degreesBetween = Vector2.Angle(targetVector, direction);

        return degreesBetween < (degrees / 2);
    }


}
