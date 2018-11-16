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
        
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
}
