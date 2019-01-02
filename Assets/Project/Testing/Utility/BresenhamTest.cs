using System;

using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections.Generic;

public class BresenhamTest{
    private List<Point> tiles;

    [SetUp]
    public void SetUp(){
        tiles = new List<Point>();
    }

    [Test]
    public void FindTiles_PerfectVertical() {
        tiles = Bresenham.FindTiles(
            new Vector2(.5f, .5f),
            new Vector2(.5f,4.5f),
            1
        );
        Debug.Log(tiles.Count);
        tiles.ForEach(t =>
        {
            Debug.Log(t.ToString());
        });
        Assert.True(Contains(0, 1));
        Assert.True(Contains(0, 2));
        Assert.True(Contains(0, 3));
        Assert.True(Contains(0, 4));

    }


    private bool Contains(int x,int y){
        return tiles.Contains(new Point(x, y));
    }
}

