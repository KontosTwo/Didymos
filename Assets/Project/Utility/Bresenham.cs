using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
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

        if (start.ToGridCoord(tileSize).Equals(end.ToGridCoord(tileSize))){
            tiles.Add(start.ToGridCoord(tileSize));
            return tiles;
        }

        float dist = Mathf.Abs(difX) + Mathf.Abs(difY);

        float dx = difX / dist;
        float dy = difY / dist;
        int x = 0;
        int y = 0;
        for (int i = 0; i <= Math.Ceiling(dist); i++) {

            x = (int)Mathf.Round(Mathf.Floor(start.x + dx * i));
            y = (int)Mathf.Round(Mathf.Floor(start.y + dy * i));
            tiles.Add(new Point(x, y));

        }
        HashSet<Point> noDuplicated = Pools.HashSetPoints;
        for(int i = 0; i < tiles.Count; i ++){
            noDuplicated.Add(tiles[i]);
        }
        List<Point> noDuplicatedList = Pools.ListPoints;
        IEnumerator<Point> noDuplicatedIterator = 
            noDuplicated.GetEnumerator();
        while (noDuplicatedIterator.MoveNext()){
            noDuplicatedList.Add(noDuplicatedIterator.Current);
        }
        Pools.HashSetPoints = noDuplicated;
        Pools.ListPoints = tiles;
        return noDuplicatedList;
    }

    private static Point ToGridCoord(this Vector2 v, float tileSize){
        return new Point((int)(v.x / tileSize), (int)(v.y / tileSize));
    }
}

