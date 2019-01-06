using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PoolTest {
    private Pool<TestPoolableObject> pool;

    [SetUp]
    public void SetUp(){
        pool = new Pool<TestPoolableObject>(3,1.4f);
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

    [Test]
    public void PoolRecyclesAll()
    {
        var obj1 = pool.Get();
        var obj2 = pool.Get();
        var obj3 = pool.Get();
        pool.Recycle(obj3);
        pool.Recycle(obj1);
        pool.Recycle(obj2);

        var obj4 = pool.Get();
        var obj5 = pool.Get();
        var obj6 = pool.Get();

        Assert.AreEqual(4, pool.GetSize());

    }

    [Test]
    public void PoolRecyclesMultipleTrials()
    {
        var obj1 = pool.Get();
        obj1.Set(1);
        var obj2 = pool.Get();
        obj2.Set(2);
        var obj3 = pool.Get();
        obj3.Set(3);
        pool.Recycle(obj3);
        pool.Recycle(obj1);
        pool.Recycle(obj2);

        var obj4 = pool.Get();
        var obj5 = pool.Get();
        var obj6 = pool.Get();

        pool.Recycle(obj4);
        pool.Recycle(obj6);
        pool.Recycle(obj5);

        var obj7 = pool.Get();
        var obj8 = pool.Get();
        var obj9 = pool.Get();

        Assert.AreEqual(4, pool.GetSize());

    }

    [Test]
    public void Pool_RecycleNoDuplicate()
    {
        var obj1 = pool.Get();
        obj1.Set(1);
        var obj2 = pool.Get();
        obj2.Set(2);
        var obj3 = obj2;
        obj3.Set(3);
        pool.Recycle(obj3);
        pool.Recycle(obj1);
        pool.Recycle(obj2);

        var obj4 = pool.Get();
        var obj5 = pool.Get();
        var obj6 = pool.Get();
        var obj7 = pool.Get();

        Assert.AreEqual(5, pool.GetSize());

    }
    /*
    [Test]
    public void Pool_PinsOnlyElement()
    {
        var obj1 = pool.Get();
        pool.Pin(obj1);
        pool.Recycle(obj1);
        var obj2 = pool.Get();
        pool.Recycle(obj2);

        var obj3 = pool.Get();
        Assert.AreNotSame(obj1, obj3);
        Assert.AreEqual(3, pool.GetSize());
    }

    [Test]
    public void Pool_UnpinOnlyElement()
    {
        var obj1 = pool.Get();
        pool.Pin(obj1);
        pool.Recycle(obj1);

        var obj2 = pool.Get();
        pool.Recycle(obj2);

        var obj3 = pool.Get();

        pool.Unpin(obj1);

        var obj4 = pool.Get();

        var obj5 = pool.Get();

        Assert.AreEqual(4, pool.GetSize());

        Assert.AreSame(obj4, obj2);

        Assert.AreSame(obj1, obj5);
    }
    */
    private class TestPoolableObject
    {
        private int lol = 0;

        public TestPoolableObject()
        {

        }

        public void Set(int i)
        {
            lol = i;
        }

        public override string ToString()
        {
            return "" + lol;
        }
    }
}
