using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
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
        List<Point> tiles = new List<Point>();
        float difX = end.x - start.x;
        float difY = end.y - start.y;

        if (start.ToGridCoord(tileSize).Equals(end.ToGridCoord(tileSize))){
            tiles.Add(start.ToGridCoord(tileSize));
            return tiles;
        }

        float dist = Math.Abs(difX) + Math.Abs(difY);

        float dx = difX / dist;
        float dy = difY / dist;
        int x = 0;
        int y = 0;

        for (int i = 0; i <= Math.Ceiling(dist); i++) {
            x = (int)Math.Round(Math.Floor(start.x + dx * i));
            y = (int)Math.Round(Math.Floor(start.y + dy * i));
            tiles.Add(new Point(x, y));
        }
        return new HashSet<Point>(tiles).ToList();
    }

    private static Point ToGridCoord(this Vector2 v, float tileSize){
        return new Point((int)(v.x / tileSize), (int)(v.y / tileSize));
    }
}

