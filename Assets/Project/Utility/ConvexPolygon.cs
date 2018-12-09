using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ConvexPolygon{

    private List<Vector2> vertices;
    private List<LineSegment> sides;

    public ConvexPolygon(List<Vector2> locations){
        List<Point> points = locations.Select(l => new Point(l.x, l.y)).ToList();
        vertices = MakeHull(points).Select(p => new Vector2(p.x,p.y)).ToList();
        sides = new List<LineSegment>();
        for (int i = 0; i < vertices.Count - 1; i ++){
            sides.Add(new LineSegment(vertices[i],vertices[i + 1]));
        }
        sides.Add(new LineSegment(vertices[0], vertices[vertices.Count - 1]));
    }

    public int GetCount(){
        return vertices.Count;
    }

    public bool WithinRange(
        Vector2 location,
        float distance
    ){
        if(vertices.Count == 0){
            Debug.LogError("Polygon has 0 vertices!");
            return false;
        }else if(vertices.Count == 1){
            return Vector2.Distance(location, vertices[0]) < distance;
        }else{
            // there exists > 1 line segments
            foreach(LineSegment side in sides){
                if (WithinRangeOfLineSegment(
                    location,
                    side,
                    distance
                )) {
                    return true;
                }
            }
            return false;

        }
    }


    private static bool WithinRangeOfLineSegment(
        Vector2 location,
        LineSegment segment,
        float distance
    ){
        Vector2 closestPointOnSegment =
            FindClosestPointOnSegment(
                location,
                segment
            );

        return Vector2.Distance(location, closestPointOnSegment)
                      < distance;
    }

    // Calculate the distance between
    // point pt and the segment p1 --> p2.
    private static Vector2 FindClosestPointOnSegment(
        Vector2 point, LineSegment segment)
    {
        Vector2 p1 = segment.ep1;
        Vector2 p2 = segment.ep2;

        Vector2 closest = new Vector2();

        float dx = p2.x - p1.x;
        float dy = p2.y - p1.y;
        if ((FastMath.FloatCloseToZero(dx)) && (FastMath.FloatCloseToZero(dy)))
        {
            // It's a point not a line segment.
            closest = p1;
            return closest;
        }

        // Calculate the t that minimizes the distance.
        float t = ((point.x - p1.x) * dx + (point.y - p1.y) * dy) /
            (dx * dx + dy * dy);

        // See if this represents one of the segment's
        // end points or a point in the middle.
        if (t < 0)
        {
            closest = new Vector2(p1.x, p1.y);

        }
        else if (t > 1)
        {
            closest = new Vector2(p2.x, p2.y);

        }
        else
        {
            closest = new Vector2(p1.x + t * dx, p1.y + t * dy);

        }

        return closest;
    }


    // Returns a new list of points representing the convex hull of
    // the given set of points. The convex hull excludes collinear points.
    // This algorithm runs in O(n log n) time.
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

    }

    private struct LineSegment{
        public Vector2 ep1;
        public Vector2 ep2;

        public LineSegment(Vector2 one, Vector2 two){
            this.ep1 = one;
            this.ep2 = two;
        }
    }

}

