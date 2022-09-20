using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Unity.Additions.Pooling.Abstractions;
using Unity.Additions.Pooling.Assets.Additions.Pooling;
using UnityEngine;
using UnityEngine.TestTools;

public class ObjectPoolTests
{
    const int RandomSeed = 500;
    const int PoolMaximum = 50000;

    private ObjectPool<object> SystemObjectPool;
    private ObjectPool<GameObject> GameObjectPool;

    [SetUp]
    public void Setup()
    {
        Random.InitState(RandomSeed);

        SystemObjectPool = new ObjectPool<object>(() => Random.Range(0, PoolMaximum), PoolMaximum, false);
        GameObjectPool = new ObjectPool<GameObject>(() => GameObject.CreatePrimitive((PrimitiveType)Random.Range(0, 6)), PoolMaximum);
    }

    // A Test behaves as an ordinary method
    [Test]
    public void PoolCounts_AreExpectedValuesAfter50Retrievals()
    {
        const int ExpectedCount = 50;
        const int ExpectedReleased = 50;
        const int ExpectedCurrent = 0;

        PooledObject<object>[] retrieved = new PooledObject<object>[50];

        for(int i = 0; i < retrieved.Length; i++)
        {
            retrieved[i] = (PooledObject<object>)SystemObjectPool.Retrieve();
        }

        Assert.AreEqual(ExpectedCount, SystemObjectPool.Count);
        Assert.AreEqual(ExpectedReleased, SystemObjectPool.ReleasedCount);
        Assert.AreEqual(ExpectedCurrent, SystemObjectPool.CurrentCount);
    }

    [Test]
    public void PoolCounts_AreExpectedValuesAfter50RetrievalsAnd25Releases()
    {
        const int ExpectedCount = 50;
        const int ExpectedHalfCount = 25;
        const int ExpectedReleased = 50;
        const int ExpectedHalfReleased = 25;
        const int ExpectedCurrent = 0;
        const int ExpectedHalfCurrent = 25;

        PooledObject<object>[] retrieved = new PooledObject<object>[ExpectedCount];

        for(int i = 0; i < ExpectedCount; i++)
        {
            retrieved[i] = (PooledObject<object>)SystemObjectPool.Retrieve();
        }

        Assert.AreEqual(ExpectedCount, SystemObjectPool.Count);
        Assert.AreEqual(ExpectedReleased, SystemObjectPool.ReleasedCount);
        Assert.AreEqual(ExpectedCurrent, SystemObjectPool.CurrentCount);

        for(int i = 0; i < ExpectedHalfCount; i++)
        {
            retrieved[i].Return();
        }

        Assert.AreEqual(ExpectedCount, SystemObjectPool.Count);
        Assert.AreEqual(ExpectedHalfReleased, SystemObjectPool.ReleasedCount);
        Assert.AreEqual(ExpectedHalfCurrent, SystemObjectPool.CurrentCount);
    }
}
