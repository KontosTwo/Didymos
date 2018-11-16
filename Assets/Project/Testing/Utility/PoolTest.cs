﻿using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PoolTest {
    private Pool<TestPoolableObject> pool;

    [SetUp]
    public void SetUp(){
        pool = new Pool<TestPoolableObject>(3);
    }

    [Test]
    public void PoolExhausts() {
        var obj1 = pool.Get();
        var obj2 = pool.Get();
        Assert.AreNotSame(obj1, obj2);
    }

    [Test]
    public void PoolResizes()
    {
        var obj1 = pool.Get();
        var obj2 = pool.Get();
        var obj3 = pool.Get();
        Assert.AreNotSame(obj1, obj2);
        Assert.AreNotSame(obj2, obj3);
        Assert.AreEqual(4, pool.GetSize());

    }

    [Test]
    public void PoolRecycles()
    {
        var obj1 = pool.Get();
        var obj2 = pool.Get();
        pool.Recycle(obj2);
        var obj3 = pool.Get();
        Assert.AreEqual(3, pool.GetSize());

    }

    [Test]
    public void PoolRecyclesFirst()
    {
        var obj1 = pool.Get();
        pool.Recycle(obj1);

        var obj2 = pool.Get();
        var obj3 = pool.Get();
        Assert.AreEqual(3, pool.GetSize());

    }

    [Test]
    public void PoolRecyclesLast()
    {
        var obj1 = pool.Get();
        var obj2 = pool.Get();
        var obj3 = pool.Get();
        pool.Recycle(obj3);
        Assert.AreEqual(4, pool.GetSize());
        var obj4 = pool.Get();
        Assert.AreEqual(4, pool.GetSize());

    }

    private class TestPoolableObject : Poolable<TestPoolableObject>
    {
        
    }
}
