using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool.Advanced
{
    public class ObjectPoolManager : MonoBehaviour
    {
        Dictionary<int, ObjectPool> activePools = new Dictionary<int, ObjectPool>();

        /// <summary>
        /// Attepmt to create a pool that manages <paramref name="poolSize"/> number of <paramref name="prefab"/> instances 
        /// </summary>
        /// <param name="prefab">GameObject prefab that this pool will manage</param>
        /// <param name="poolSize">Total capacity of GameObject instances this pool will manage</param>
        /// <returns>True if a pool was successflly created</returns>
        public bool TryCreatePool(GameObject prefab, int poolSize)
        {
            if (prefab is null) throw new ArgumentNullException(nameof(prefab), "cannot pool a prefab that is null");
            if (poolSize < 1) throw new ArgumentException($"cannot create a pool of {poolSize} elements, as {poolSize} is less than 1", nameof(poolSize));

            int poolKey = prefab.GetInstanceID();

            if (activePools.ContainsKey(poolKey)) return false;
            activePools[poolKey] = new GameObject($"pool.{prefab.name}.{poolKey}").AddComponent<ObjectPool>();
            activePools[poolKey].transform.parent = transform;
            activePools[poolKey].Initialize(prefab, poolSize);

            return true;
        }
        /// <summary>
        /// Attempts to destroy the pool that manages <paramref name="prefab"/> instances. Can cause frame lag when destroying very large pools
        /// </summary>
        /// <param name="prefab">Pool managing this prefab should be destroyed</param>
        /// <param name="immediate">Should this method use <c>DestroyImmediate</c> instead of <c>Destroy</c></param>
        /// <returns>True if a pool was successfully destroyed</returns>
        public bool TryDestroyPool(GameObject prefab, bool immediate = false)
        {
            if (prefab is null) throw new ArgumentNullException(nameof(prefab), "cannot destroy a pool managing a null prefab");

            int instanceID = prefab.GetInstanceID();
            if (activePools.ContainsKey(instanceID))
            {
                activePools[instanceID].DestroyManagedObjects(immediate);

                if (immediate)
                    DestroyImmediate(activePools[instanceID]);
                else
                    Destroy(activePools[instanceID]);

                activePools.Remove(instanceID);
                return true;
            }
            return false;
        }
    }
}