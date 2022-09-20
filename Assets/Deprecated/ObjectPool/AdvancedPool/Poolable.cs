using UnityEngine;

namespace ObjectPool.Advanced
{
    /// <summary>
    /// If your pooled object requires more functionality when bein reset, extend this class for the reset process to be executed
    /// </summary>
    public abstract class Poolable : MonoBehaviour
    {
        /// <summary>
        /// Extra reset functionality to be executed when reset or deactivated in the pool
        /// </summary>
        public abstract void ResetPooledObject();
    }
}