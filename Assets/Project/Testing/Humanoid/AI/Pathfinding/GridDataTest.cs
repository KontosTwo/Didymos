using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class GridDataTest {
    private GridData data;
    [SetUp]
    public void SetUp(){
        data = new GridData(3);
    }

    [Test]
    public void PreservesOrder() {
        MapNode n1 = new MapNode(1,1,true);
        MapNode n2 = new MapNode(1, 2, true);
        MapNode n3 = new MapNode(1, 3, true);
        MapNode n4 = new MapNode(1, 4, true);
        MapNode n5 = new MapNode(1, 5, true);
        MapNode n6 = new MapNode(1, 6, true);

        data[new Point(1,0)] = n1;
        data[new Point(2, 0)] = n2;
        data[new Point(3, 0)] = n3;
        data[new Point(4, 0)] = n4;
        data[new Point(5, 0)] = n5;
        data[new Point(6, 0)] = n6;
        Assert.AreEqual(n1, data.EvictFirst());
        Assert.AreEqual(n2, data.EvictFirst());
        Assert.AreEqual(n3, data.EvictFirst());
        Assert.AreEqual(n4, data.EvictFirst());
        Assert.AreEqual(n5, data.EvictFirst());
        Assert.AreEqual(n6, data.EvictFirst());
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
}
