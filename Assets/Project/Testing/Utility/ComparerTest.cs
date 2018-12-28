using System;

using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections.Generic;
public class ComparerTest{
    private TestClass a;
    private TestClass b;
    private List<Func<TestClass, int>> waysToCompare;

    [SetUp]
    public void SetUp() {
        a = new TestClass();
        b = new TestClass();
        waysToCompare = new List<Func<TestClass, int>>();
    }

    [Test]
    public void NestedCompare_OneEquals() {
        AddWay(c => 0);
        Assert.AreEqual(0, Calculate());
    }

    [Test]
    public void NestedCompare_OneNotEquals(){
        SetA(2);
        SetB(1);
        AddWay(c => c.num);
        Assert.AreEqual(1, Calculate());
    }

    [Test]
    public void NestedCompare_TwoFirstEquals() {
        SetA(2);
        SetB(1);
        AddWay(c => 0);
        AddWay(c => c.num);
        Assert.AreEqual(1, Calculate());
    }

    [Test]
    public void NestedCompare_TwoSecondEquals() {
        SetA(2);
        SetB(1);
        AddWay(c => c.num);
        AddWay(c => 0);
        Assert.AreEqual(1, Calculate());
    }

    [Test]
    public void NestedCompare_TwoNoneEquals(){
        SetA(2);
        SetB(1);
        AddWay(c => c.num);
        AddWay(c => c.num * 2);
        Assert.AreEqual(1, Calculate());
    }

    [Test]
    public void NestedCompare_TwoAllEquals(){
        SetA(2);
        SetB(1);
        AddWay(c => 0);
        AddWay(c => 0);
        Assert.AreEqual(0, Calculate());
    }

    private void AddWay(Func<TestClass,int> way) {
        waysToCompare.Add(way);
    }

    private int Calculate() {
        return Comparer.NestedCompare(waysToCompare, a, b);
    }

    private void SetA(int num) {
        a.num = num;
    }

    private void SetB(int num){
        b.num = num;
    }

    private class TestClass {
        public int num;
    }

    
}

