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
        Assert.True(Contains(0, 1));
        Assert.True(Contains(0, 2));
        Assert.True(Contains(0, 3));
        Assert.True(Contains(0, 4));
        Assert.AreEqual(5, tiles.Count);
    }

    [Test]
    public void FindTiles_PerfectHorizontal(){
        tiles = Bresenham.FindTiles(
            new Vector2(.5f, .5f),
            new Vector2(4.5f, .5f),
            1
        );

        Assert.True(Contains(1, 0));
        Assert.True(Contains(2, 0));
        Assert.True(Contains(3, 0));
        Assert.True(Contains(4, 0));
        Assert.AreEqual(5, tiles.Count);

    }

    [Test]
    public void FindTiles_PerfectVerticalReverse()
    {
        tiles = Bresenham.FindTiles(
            new Vector2(.5f, 4.5f),
            new Vector2(.5f, .5f),
            1
        );
        Assert.True(Contains(0, 1));
        Assert.True(Contains(0, 2));
        Assert.True(Contains(0, 3));
        Assert.True(Contains(0, 4));
        Assert.AreEqual(5, tiles.Count);

    }

    [Test]
    public void FindTiles_PerfectHorizontalReverse()
    {
        tiles = Bresenham.FindTiles(
            new Vector2(4.5f, .5f),
            new Vector2(.5f, .5f),
            1
        );

        Assert.True(Contains(1, 0));
        Assert.True(Contains(2, 0));
        Assert.True(Contains(3, 0));
        Assert.True(Contains(4, 0));
        Assert.AreEqual(5, tiles.Count);

    }


    [Test]
    public void FindTiles_Diagonal()
    {
        tiles = Bresenham.FindTiles(
            new Vector2(.5f, .5f),
            new Vector2(4.5f, 4.5f),
            1
        );

        Assert.True(Contains(1, 1));
        Assert.True(Contains(2, 2));
        Assert.True(Contains(3, 3));
        Assert.True(Contains(4, 4));
    }

    [Test]
    public void FindTiles_DiagonalReverse()
    {
        tiles = Bresenham.FindTiles(
            new Vector2(4.5f, 4.5f),
            new Vector2(.5f, .5f),
            1
        );


        Assert.True(Contains(1, 1));
        Assert.True(Contains(2, 2));
        Assert.True(Contains(3, 3));
        Assert.True(Contains(4, 4));
    }

    [Test]
    public void FindTiles_DiagonalIrregular()
    {
        tiles = Bresenham.FindTiles(
            new Vector2(3f, 5f),
            new Vector2(0f, 0f),
            1
        );

        // looks about right
    }

    [Test]
    public void FindTiles_VerticalRightNext()
    {
        tiles = Bresenham.FindTiles(
            new Vector2(.5f, 1.5f),
            new Vector2(.5f, .5f),
            1
        );

        Assert.True(Contains(0, 0));
        Assert.True(Contains(0, 1));
        Assert.AreEqual(2, tiles.Count);
    }

    [Test]
    public void FindTiles_HorizontalRightNext()
    {
        tiles = Bresenham.FindTiles(
            new Vector2(.5f, .5f),
            new Vector2(1.5f, .5f),
            1
        );

        Assert.True(Contains(0, 0));
        Assert.True(Contains(1, 0));
        Assert.AreEqual(2, tiles.Count);
    }

    [Test]
    public void FindTiles_Same()
    {
        tiles = Bresenham.FindTiles(
            new Vector2(.5f, .6f),
            new Vector2(.5f, .5f),
            1
        );
        tiles.ForEach(t =>
        {
           // Debug.Log(t.ToString());
        });
        Assert.True(Contains(0, 0));
        Assert.AreEqual(1, tiles.Count);

       
    }


    private bool Contains(int x,int y){
        return tiles.Contains(new Point(x, y));
    }
}

