using System;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections.Generic;
public class ConvexPolygonTest{
    private List<Vector2> points;
    private ConvexPolygon polygon;

    [SetUp]
    public void SetUp(){
        points = new List<Vector2>();
    }

    [Test]
    public void FormsSquare(){
        Add(1, 0);
        Add(0, 1);
        Add(0, 0);
        Add(1, 1);
        Calculate();
        Assert.AreEqual(4, polygon.GetCount());
        Assert.IsTrue(Contains(1, 0));
        Assert.IsTrue(Contains(1, 1));
        Assert.IsTrue(Contains(0, 0));
        Assert.IsTrue(Contains(0, 1));

    }

    [Test]
    public void FormsSquareScrambled(){
        Add(0, 0);
        Add(1, 0);
        Add(1, 1);
        Add(0, 1);
        Calculate();
        Assert.AreEqual(4, polygon.GetCount());
        Assert.IsTrue(Contains(1, 0));
        Assert.IsTrue(Contains(1, 1));
        Assert.IsTrue(Contains(0, 0));
        Assert.IsTrue(Contains(0, 1));
    }

    [Test]
    public void IgnoresCenterOfSquare(){
        Add(0, 0);
        Add(1, 0);
        Add(1, 1);
        Add(0, 1);
        Add(.5f, .5f);
        Calculate();
        Assert.AreEqual(4,polygon.GetCount());
        Assert.IsTrue(Contains(1, 0));
        Assert.IsTrue(Contains(1, 1));
        Assert.IsTrue(Contains(0, 0));
        Assert.IsTrue(Contains(0, 1));
    }

    [Test]
    public void WithinRange_LineSegment(){
        Vector2 foci = new Vector2(-.5f,.5f);
        Add(0, 0);
        Add(1, 0);
        Add(1, 1);
        Add(0, 1);
        Calculate();
        Assert.IsTrue( polygon.WithinRange(
            foci,
            .6f
        ));
    }

    [Test]
    public void WithinRange_NotInRangeLineSegment(){
        Vector2 foci = new Vector2(-.5f, .5f);
        Add(0, 0);
        Add(1, 0);
        Add(1, 1);
        Add(0, 1);
        Calculate();
        Assert.IsFalse(polygon.WithinRange(
            foci,
            .4f
        ));
    }

    [Test]
    public void WithinRange_Vertex(){
        Vector2 foci = new Vector2(-1f, 2f);
        Add(0, 0);
        Add(1, 0);
        Add(1, 1);
        Add(0, 1);
        Calculate();
        Assert.IsTrue(polygon.WithinRange(
            foci,
            1.5f
        ));
    }

    [Test]
    public void WithinRange_NotInRangeVertex(){
        Vector2 foci = new Vector2(-1f, 2f);
        Add(0, 0);
        Add(1, 0);
        Add(1, 1);
        Add(0, 1);
        Calculate();
        Assert.IsFalse(polygon.WithinRange(
            foci,
            1.4f
        ));
    }

    [Test]
    public void Contains_Success(){
        Vector2 foci = new Vector2(.5f, .5f);
        Add(0, 0);
        Add(1, 0);
        Add(1, 1);
        Add(0, 1);
        Calculate();
        Assert.IsTrue(polygon.Contains(
            foci
        ));
    }

    [Test]
    public void Contains_Failure(){
        Vector2 foci = new Vector2(5f, .5f);
        Add(0, 0);
        Add(1, 0);
        Add(1, 1);
        Add(0, 1);
        Calculate();
        Assert.IsFalse(polygon.Contains(
            foci
        ));
    }

    private void Calculate(){
        polygon = new ConvexPolygon(points);
    }

    private void Add(float x,float y){
        points.Add(new Vector2(x, y));
    }

    private bool Contains(float x,float y){
        List<Vector2> vertices = polygon.GetVertices();
        return vertices.Contains(new Vector2(x, y));
    }
}

