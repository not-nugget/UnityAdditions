using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool.Basic
{
    public class SimpleObjectPool : MonoBehaviour
    {
        [BoxGroup("Pool Data"), Tooltip("The object this pool will manage. Can only be modified in edit mode"), DisableInPlayMode]
        public GameObject objectToPool = null;
        [BoxGroup("Pool Data"), MinValue(1), MaxValue(1000000), Tooltip("The number of objects this pool will manage. Can only be modified in edit mode. Range [1..1000000]"), DisableInPlayMode]
        public int objectPoolCapacity = 100;
        [BoxGroup("Pool Data"), Tooltip("The custom parent to set the pooled opbjects under. When null, will parent the pooled objects under this transform. Cannot be modified in edit mode"), DisableInPlayMode]
        public Transform customPooledObjectParent = null;

        Queue<GameObject> pooledObjects;

        private void Awake()
        {
            if (objectToPool is null) throw new NullReferenceException("cannot create an object pool for a null GameObject");

            pooledObjects = new Queue<GameObject>();
            for (int i = 0; i < objectPoolCapacity; i++)
            {
                GameObject go = Instantiate(objectToPool, (customPooledObjectParent is null) ? transform : customPooledObjectParent);
                go.SetActive(false);
                pooledObjects.Enqueue(go);
            }
        }
        private void OnDestroy()
        {
            foreach (var v in pooledObjects)
                Destroy(v);
        }

        /// <summary>
        /// Gets the first GameObject in the queue. Does not modify the state of the object returned in any way
        /// </summary>
        /// <returns>The acquired unused GameObject</returns>
        public GameObject Get()
        {
            GameObject first = pooledObjects.Dequeue();
            pooledObjects.Enqueue(first);

            return first;
        }
        /// <summary>
        /// Gets the first <paramref name="count"/> GameObjects in the queue. Does not modify the state of the objects returned
        /// in any way. Can cause frame lag when retreiving a large number of objects from a pool
        /// </summary>
        /// <param name="count">Number of managed GameObjects to return</param>
        /// <returns><paramref name="count"/> number of GameObjects within the managed queue</returns>
        public GameObject[] Get(int count)
        {
            if (count <= 0 || count >= objectPoolCapacity)
                throw new ArgumentException("cannot get a quantity of objects that is negative, zero, or that exceeds the capacity of this pool", nameof(count));

            GameObject[] retrievedObjects = new GameObject[count];
            for (int i = 0; i < count; i++)
                retrievedObjects[i] = pooledObjects.Dequeue();

            foreach (var v in retrievedObjects) pooledObjects.Enqueue(v);
            return retrievedObjects;
        }
    }
}