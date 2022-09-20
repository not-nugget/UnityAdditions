#nullable enable

namespace Unity.Additions.Pooling.Abstractions
{
    /// <summary>
    /// Exposes methods which indicate an item is pooled by an <see cref="IObjectPool{T}"/>
    /// </summary>
    /// <typeparam name="T">Type being pooled</typeparam>
    public interface IPooledObjectEntity<T> where T : class
    {
        /// <summary>
        /// Item instance that is tracked by an object pool
        /// </summary>
        public ref T Item { get; }
    }

    /// <summary>
    /// Exposes <see cref="Return"/>, which allows an item to return itself to an <see cref="IObjectPool{T}"/>
    /// </summary>
    public interface IPooledObjectEntity
    {
        /// <summary>
        /// Flag indicating whether or not this item is released from the pool or if it is currently being tracked by the pool
        /// </summary>
        public bool Released { get; }
        /// <summary>
        /// Flag indicating whether or not this item is actually pooled, or if it was created because the pool is full
        /// </summary>
        public bool Pooled { get; }

        /// <summary>
        /// Instruct this item to return itself to the pool that owns it
        /// </summary>
        void Return();
    }
}
