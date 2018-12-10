using System;
using UnityEngine;
using System.Collections.Generic;
public class DestinationReservations {


    private static HashSet<Point> reservedMapNodes;

    static DestinationReservations(){
        reservedMapNodes = new HashSet<Point>();
    }

    public static bool IsAvailable(Point point){
        return reservedMapNodes.Contains(point);
    }

    public static void Reserve(Point point){
        reservedMapNodes.Add(point);
    }

    public static void Free(Point point){
        reservedMapNodes.Remove(point);
    }
}

