using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConvexHull
{
    public static Tuple<List<T1>,List<T2>> MakeHullTwo<T1,T2>(
        List<T1> objectsOne,
        List<T2> objectsTwo,
        Func<T1,Vector2> objectOnePointExtractor,
        Func<T2,Vector2> objectTwoPointExtractor
    ) where T1 : class where T2 : class{
        List<Point> points = new List<Point>();

        Dictionary<Point, T1> objectsOneMapping = 
            new Dictionary<Point, T1>();
        Dictionary<Point, T2> objectsTwoMapping =
            new Dictionary<Point, T2>();

        List<T1> objectOneRemainder = new List<T1>();
        List<T2> objectTwoRemainder = new List<T2>();


        Tuple<List<T1>, List<T2>> remainders = 
            new Tuple<List<T1>, List<T2>>(
                objectOneRemainder,
                objectTwoRemainder
            );

        foreach(T1 objectOne in objectsOne){
            Point point =
                new Point(
                    objectOnePointExtractor.Invoke(objectOne)
                );
            objectsOneMapping.Add(point, objectOne);
            points.Add(point);
        }

        foreach (T2 objectTwo in objectsTwo){
            Point point =
                new Point(
                    objectTwoPointExtractor.Invoke(objectTwo)
                );
            objectsTwoMapping.Add(point, objectTwo);
            points.Add(point);
        }

        List<Point> remainingPoints = MakeHull(points);

        foreach(Point point in remainingPoints){
            T1 remainingOne = default(T1);
            T2 remainingTwo = default(T2);

            objectsOneMapping.TryGetValue(point, out remainingOne);
            objectsTwoMapping.TryGetValue(point, out remainingTwo);

            if(remainingOne != default(T1)){
                objectOneRemainder.Add(remainingOne);
            }else if(remainingTwo != default(T2)){
                objectTwoRemainder.Add(remainingTwo);
            }
        }

        return remainders;
    }


    private static List<Point> MakeHull(List<Point> points)
    {
        List<Point> newPoints = new List<Point>(points);
        newPoints.Sort();
        return MakeHullPresorted(newPoints);
    }


    // Returns the convex hull, assuming that each points[i] <= points[i + 1]. Runs in O(n) time.
    private static List<Point> MakeHullPresorted(List<Point> points)
    {
        if (points.Count <= 1)
            return new List<Point>(points);

        // Andrew's monotone chain algorithm. Positive y coordinates correspond to "up"
        // as per the mathematical convention, instead of "down" as per the computer
        // graphics convention. This doesn't affect the correctness of the result.

        List<Point> upperHull = new List<Point>();
        foreach (Point p in points)
        {
            while (upperHull.Count >= 2)
            {
                Point q = upperHull[upperHull.Count - 1];
                Point r = upperHull[upperHull.Count - 2];
                if ((q.x - r.x) * (p.y - r.y) >= (q.y - r.y) * (p.x - r.x))
                    upperHull.RemoveAt(upperHull.Count - 1);
                else
                    break;
            }
            upperHull.Add(p);
        }
        upperHull.RemoveAt(upperHull.Count - 1);

        List<Point> lowerHull = new List<Point>();
        for (int i = points.Count - 1; i >= 0; i--)
        {
            Point p = points[i];
            while (lowerHull.Count >= 2)
            {
                Point q = lowerHull[lowerHull.Count - 1];
                Point r = lowerHull[lowerHull.Count - 2];
                if ((q.x - r.x) * (p.y - r.y) >= (q.y - r.y) * (p.x - r.x))
                    lowerHull.RemoveAt(lowerHull.Count - 1);
                else
                    break;
            }
            lowerHull.Add(p);
        }
        lowerHull.RemoveAt(lowerHull.Count - 1);

        if (!(upperHull.Count == 1 && Enumerable.SequenceEqual(upperHull, lowerHull)))
            upperHull.AddRange(lowerHull);
        return upperHull;
    }

    private struct Point : IComparable<Point>
    {

        public float x;
        public float y;


        public Point(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Point(Vector2 vector){
            this.x = vector.x;
            this.y = vector.y;
        }


        public int CompareTo(Point other)
        {
            if (x < other.x)
                return -1;
            else if (x > other.x)
                return +1;
            else if (y < other.y)
                return -1;
            else if (y > other.y)
                return +1;
            else
                return 0;
        }

        public static implicit operator Point(Func<object, Vector2> v)
        {
            throw new NotImplementedException();
        }
    }

}

