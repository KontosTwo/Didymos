using System;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections.Generic;

public class ConvexHullTest{
    private Tuple<List<TypeOne>, List<TypeTwo>> results;

    private List<TypeOne> t1Results;
    private List<TypeTwo> t2Results;

    private List<TypeOne> typeOnes;
    private List<TypeTwo> typeTwos;
    private Func<TypeOne, Vector2> typeOneExtractor;
    private Func<TypeTwo, Vector2> typeTwoExtractor;

    [SetUp]
    public void SetUp() {
        typeOnes = new List<TypeOne>();
        typeTwos = new List<TypeTwo>();
        typeOneExtractor = t => new Vector2(t.x, t.y);
        typeTwoExtractor = t => new Vector2(t.x, t.y);
    }

    [Test]
    public void MakeHullTwo_OnlyT1(){
        AddT1(0, 0);
        AddT1(1, 0);
        AddT1(0, 1);
        AddT1(1, 1);
        Calculate();
        Assert.AreEqual(4, t1Results.Count);
        Assert.AreEqual(0, t2Results.Count);

    }

    [Test]
    public void MakeHullTwo_OnlyT2(){
        AddT2(0, 0);
        AddT2(1, 0);
        AddT2(0, 1);
        AddT2(1, 1);
        Calculate();
        Assert.AreEqual(0, t1Results.Count);
        Assert.AreEqual(4, t2Results.Count);

    }

    [Test]
    public void MakeHullTwo_Mixed(){
        AddT2(0, 0);
        AddT2(1, 0);
        AddT1(0, 1);
        AddT1(1, 1);
        Calculate();
        Assert.AreEqual(2, t1Results.Count);
        Assert.AreEqual(2, t2Results.Count);
    }

    [Test]
    public void MakeHullTwo_MixedIgnoreSome(){
        AddT2(0, 0);
        AddT2(1, 0);
        AddT1(0, 1);
        AddT1(1, 1);

        AddT2(.5f, .5f);
        AddT1(.1f, .1f);
        Calculate();
        Assert.AreEqual(2, t1Results.Count);
        Assert.AreEqual(2, t2Results.Count);
    }





    private class TypeOne {
        public float x;
        public float y;

        public TypeOne(float x,float y){
            this.x = x;
            this.y = y;
        }
    }
    private class TypeTwo {
        public float x;
        public float y;

        public TypeTwo(float x, float y){
            this.x = x;
            this.y = y;
        }
    }

    private void AddT1(float x, float y) {
        typeOnes.Add(new TypeOne(x, y));
    }

    private void AddT2(float x, float y){
        typeTwos.Add(new TypeTwo(x, y));
    }

    private void Calculate(){
        results = ConvexHull.MakeHullTwo<TypeOne, TypeTwo>(
            typeOnes,
            typeTwos,
            typeOneExtractor,
            typeTwoExtractor
        );

        t1Results = results.Item1;
        t2Results = results.Item2;
    }
}

