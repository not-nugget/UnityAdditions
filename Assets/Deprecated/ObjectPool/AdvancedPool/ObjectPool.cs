using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool.Advanced
{
    public class ObjectPool : MonoBehaviour
    {
        Queue<GameObject> pooledObjects;

        /// <summary>
        /// Initialize this object pool. Must be called for the pool to function as intended, and should only be called once. 
        /// Can cause frame drops in severe scenarios, or for large pools
        /// </summary>
        /// <param name="prefab">Prefab to manage</param>
        /// <param name="poolSize">Number of prefabs to manage</param>
        public void Initialize(GameObject prefab, int poolSize)
        {
            pooledObjects = new Queue<GameObject>();
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(prefab);
                if (obj.TryGetComponent(out Poolable poolable))
                    poolable.ResetPooledObject();

                obj.SetActive(false);
                pooledObjects.Enqueue(obj);
            }
        }

        public void ReuseObject()
        {
            //reuse an object using its default poolable reset feature, if it exists
        }
        public void ReuseObject(Vector3 position)
        {
            //use normal reuse, then reset the position
        }
        public void ReuseObject(Quaternion rotation)
        {
            //use normal reuse, then reset the rotation
        }
        public void ReuseObjcet(Vector3 position, Quaternion rotation)
        {
            //use normal reuse, then reset the position and rotation
        }

        /// <summary>
        /// Destroy this pool contents
        /// </summary>
        /// <param name="immediate">Should this method use <c>DestroyImmediate</c> instead of <c>Destroy</c></param>
        public void DestroyManagedObjects(bool immediate = false)
        {
            if (pooledObjects is null || pooledObjects.Count == 0) return;
            foreach (var v in pooledObjects)
                if (immediate)
                    DestroyImmediate(v);
                else
                    Destroy(v);
        }
    }
}
