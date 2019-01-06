using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Profiling;
public static class Bresenham {
    private static readonly float CACHE_VECTOR_CLOSE_ENOUGH_DISTANCE = .2f;

    private static readonly int CACHE_MAX_SIZE = 10000;
    private static LinkedDictionary<Tuple<Vector2, Vector2, float>, List<Point>> cache;

    static Bresenham()
    {
        cache = new LinkedDictionary<Tuple<Vector2, Vector2, float>, List<Point>>(
            new CacheComparer()
        );
    }

    /*
     * Return type is ordered from start to end. start and end are relative to the grid's 
     * bottom right. Return coordinates are in points
     */

    public static List<Point> FindTiles(
        Vector2 start,
        Vector2 end,
        float tileSize
    ) {
        Tuple<Vector2, Vector2, float> data =
            new Tuple<Vector2, Vector2, float>(
                start,
                end,
                tileSize
            );
        if (cache.ContainsKey(data)){
            return cache[data];
        }

        List<Point> tiles = Pools.ListPoints;
        float difX = end.x - start.x;
        float difY = end.y - start.y;

        Point startPoint = start.ToGridCoord(tileSize);
        Point endPoint = end.ToGridCoord(tileSize);
        if (startPoint.Equals(endPoint)) {
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
        for (int i = 0; i < tiles.Count; i++) {
            Point tile = tiles[i];
            if (!noDuplicated.Contains(tile)) {
                noDuplicated.Add(tile);
            }
            else {
                remaining.Add(tile);
            }
        }
        List<Point> noDuplicatedList = Pools.ListPoints;
        HashSet<Point>.Enumerator noDuplicatedIterator =
            noDuplicated.GetEnumerator();
        while (noDuplicatedIterator.MoveNext()) {
            noDuplicatedList.Add(noDuplicatedIterator.Current);
        }
        // Change this
        Pools.FreeListPoints(remaining);

        Pools.HashSetPoints = noDuplicated;
        Pools.ListPoints = tiles;
        Pools.ListPoints = remaining;

        List<Point> cachedPath = Pools.ListPoints;
        for(int i = 0; i < noDuplicatedList.Count; i ++){
            cachedPath.Add(noDuplicatedList[i]);
        }
        cache[data] = cachedPath;
        if(cache.Count > CACHE_MAX_SIZE)
        {
            cache.PopFirst();
        }
        return noDuplicatedList;
    }

    private static Point ToGridCoord(this Vector2 v, float tileSize) {
        Point p = Pools.Point;
        p.Set(
            (int)(v.x / tileSize),
            (int)(v.y / tileSize)
        );
        return p;
    }

    private class CacheComparer : IEqualityComparer<Tuple<Vector2, Vector2, float>>{

        public bool Equals(Tuple<Vector2, Vector2, float> x, Tuple<Vector2, Vector2, float> y)
        {
            return Vector3.Distance(x.Item1, y.Item1) < CACHE_VECTOR_CLOSE_ENOUGH_DISTANCE &&
                Vector3.Distance(x.Item2, y.Item2) < CACHE_VECTOR_CLOSE_ENOUGH_DISTANCE &&
                (x.Item3 - y.Item3).CloseToZero(.01f);
        }

        public int GetHashCode(Tuple<Vector2, Vector2, float> obj)
        {
            int hash = 17;
            // Suitable nullity checks etc, of course :)
            hash = hash * 23 + obj.Item1.GetHashCode();
            hash = hash * 23 + obj.Item2.GetHashCode();
            hash = hash * 23 + obj.Item3.GetHashCode();
            return hash;
        }
    }
}


