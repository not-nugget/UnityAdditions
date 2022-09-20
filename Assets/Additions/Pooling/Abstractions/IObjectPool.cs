
namespace Unity.Additions.Pooling.Abstractions
{
    /// <summary>
    /// Exposes methods that are used to interface with the items stored within an object pool
    /// </summary>
    /// <typeparam name="T">Type pooled by the implementing class</typeparam>
    public interface IObjectPool<T> where T : class
    {
        /// <summary>
        /// Retrieves an item from the pool, or creates one if the pool has no instance to provide
        /// </summary>
        /// <returns>Retrieved or created instance that is tracked by the pool</returns>
        IPooledObjectEntity<T> Retrieve();
    }
}