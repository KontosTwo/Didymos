using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector2Ext {
    public static Vector3 To3D(this Vector2 original,float height){
        return new Vector3(
            original.x,
            height,
            original.y
        );
    }
}
