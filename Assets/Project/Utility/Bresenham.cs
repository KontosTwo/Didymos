using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Profiling;
public static class Bresenham{
    /*
     * Return type is ordered from start to end. start and end are relative to the grid's 
     * bottom right. Return coordinates are in points
     */
    
    public static List<Point> FindTiles(
        Vector2 start,
        Vector2 end,
        float tileSize
    ){
        List<Point> tiles = Pools.ListPoints;
        float difX = end.x - start.x;
        float difY = end.y - start.y;

        Point startPoint = start.ToGridCoord(tileSize);
        Point endPoint = end.ToGridCoord(tileSize);
        if (startPoint.Equals(endPoint)){
            tiles.Add(startPoint);
            Pools.Point = endPoint;
            return tiles;
        }
        Pools.Point = startPoint;
        Pools.Point = endPoint;

        float dist = Mathf.Abs(difX) + Mathf.Abs(difY);

        float dx = difX / dist;
        float dy = difY / dist;
        int x = 0;
        int y = 0;
        for (int i = 0; i <= Math.Ceiling(dist); i++) {

            x = (int)Mathf.Round(Mathf.Floor(start.x + dx * i));
            y = (int)Mathf.Round(Mathf.Floor(start.y + dy * i));
            Point newPoint = Pools.Point;
            newPoint.Set(x, y);

            tiles.Add(newPoint);
        }
        HashSet<Point> noDuplicated = Pools.HashSetPoints;
        List<Point> remaining = Pools.ListPoints;
        for(int i = 0; i < tiles.Count; i ++){
            Point tile = tiles[i];
            if (!noDuplicated.Contains(tile)){
                noDuplicated.Add(tile);
            }
            else{
                remaining.Add(tile);
            }
        }
        List<Point> noDuplicatedList = Pools.ListPoints;
        HashSet<Point>.Enumerator noDuplicatedIterator = 
            noDuplicated.GetEnumerator();
        while (noDuplicatedIterator.MoveNext()){
            noDuplicatedList.Add(noDuplicatedIterator.Current);
        }
        // Change this
        Pools.FreeListPoints(remaining);

        Pools.HashSetPoints = noDuplicated;
        Pools.ListPoints = tiles;
        Pools.ListPoints = remaining;
        return noDuplicatedList;
    }

    private static Point ToGridCoord(this Vector2 v, float tileSize){
        Point p = Pools.Point;
        p.Set(
            (int)(v.x / tileSize), 
            (int)(v.y / tileSize)
        );
        return p;
    }
}

