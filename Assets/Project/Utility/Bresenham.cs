using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public static class Bresenham{
    /*
     * Return type is ordered from start to end. start and end are relative to the grid's 
     * bottom right. Return coordinates are in points
     */
    /*public static List<Point> FindTiles(
        Vector2 start,
        Vector2 end,
        float tileSize
    ){
        Point startPoint = start.ToGridCoord(tileSize);
        Point endPoint = end.ToGridCoord(tileSize);

        List<Vector2> intersections = new List<Vector2>();

        for(int vert = startPoint.x; vert < endPoint.x + 1; vert++){
            Vector2 vertStart = new Vector2(vert * tileSize,start.y);
            Vector2 vertEnd = new Vector2(vert * tileSize, end.y);
            Vector2 intersection = FastMath.LineIntersection(
                start,
                end,
                vertStart,
                vertEnd
            );
            intersections.Add(intersection);
        }

        for (int horiz = startPoint.y; horiz < endPoint.y + 1; horiz++){
            Vector2 horizStart = new Vector2(start.x,horiz * tileSize);
            Vector2 horizEnd = new Vector2(end.x,horiz * tileSize);
            Vector2 intersection = FastMath.LineIntersection(
                start,
                end,
                horizStart,
                horizEnd
            );
            intersections.Add(intersection);
        }

        List<Vector2> sortedIntersections =
            intersections.OrderBy(v => v.x).ToList();
            
        List<Point> obstructingTiles = new List<Point>();
        for(int i = 0; i < sortedIntersections.Count - 1; i++){
            Vector2 first = sortedIntersections[i];
            Vector2 second = sortedIntersections[i + 1];
            Vector2 midpoint = (first + second) / 2;
            Point tile = midpoint.ToGridCoord(tileSize);
            obstructingTiles.Add(tile);
        }

        return obstructingTiles;
    }*/

    /*public static List<Point> FindTiles(
        Vector2 start, 
        Vector2 end,
        float tileSize
    ){
        List<Point> cells = new List<Point>();
        float BeginX = start.x/tileSize;
        float BeginY = start.y / tileSize;
        float EndX = end.x / tileSize;
        float EndY = end.y / tileSize;


        int cx = (int)Math.Round(Math.Floor(BeginX)); // Begin/current cell coords
        int cy = (int)Math.Round(Math.Floor(BeginY));
        int ex = (int)Math.Round(Math.Floor(EndX)); // End cell coords
        int ey = (int)Math.Round(Math.Floor(EndY));

        // Delta or direction
        float dx = EndX - BeginX;
        float dy = EndY - BeginY;

        while (cx < ex && cy < ey)
        {
            // find intersection "time" in x dir
            float t0 = (float)((Math.Ceiling(BeginX) - BeginX) / dx);
            float t1 = (float)((Math.Ceiling(BeginY) - BeginY) / dy);

            cells.Add(new Point(cx, cy));

            if (t0 < t1) // cross x boundary first=?
            {
                ++cx;
                BeginX += t0 * dx;
                BeginY += t0 * dy;
            }
            else
            {
                ++cy;
                BeginX += t1 * dx;
                BeginY += t1 * dy;
            }
        }
        return cells;
    }*/
    public static List<Point> FindTiles(
        Vector2 start,
        Vector2 end,
        float tileSize
    ){
        List<Point> tiles = new List<Point>();
        float difX = end.x - start.x;
        float difY = end.y - start.y;
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
        return tiles;
    }
}

