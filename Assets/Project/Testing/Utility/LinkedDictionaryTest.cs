using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class LinkedDictionaryTest {
    private LinkedDictionary<Integer, Integer> dict;


    [SetUp]
    public void SetUp(){
        dict = new LinkedDictionary<Integer, Integer>();
    }

    [Test]
    public void DictPreservesOrder() {

    }

}
