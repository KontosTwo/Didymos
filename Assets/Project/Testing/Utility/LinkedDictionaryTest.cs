using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class LinkedDictionaryTest {
    private LinkedDictionary<int,int> dict;


    [SetUp]
    public void SetUp(){
        dict = new LinkedDictionary<int, int>();
    }

    [Test]
    public void DictPreservesOrder() {
        dict[1] = 10;
        dict[2] = 20;
        dict[3] = 30;
        dict[4] = 40;
        dict[5] = 50;
        dict[6] = 60;
        Assert.AreEqual(10, dict.PopFirst());
        Assert.AreEqual(20, dict.PopFirst());
        Assert.AreEqual(30, dict.PopFirst());
        Assert.AreEqual(40, dict.PopFirst());
        Assert.AreEqual(50, dict.PopFirst());
        Assert.AreEqual(60, dict.PopFirst());

    }

}
