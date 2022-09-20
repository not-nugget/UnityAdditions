using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

#nullable enable

namespace Unity.Additions.Pooling.Abstractions
{
    /// <summary>
    /// Defines base logic for an object pool that stores instances of <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T">Type to pooled</typeparam>
    /// <typeparam name="TCollection">Backing <see cref="IEnumerable{T}"/> used to pool the items</typeparam>
    public abstract class BaseObjectPool<T> : IObjectPool<T> where T : class
    {
        /// <summary>
        /// Defines a method signature which will be invoked when an item needs to be created either for immediate
        /// request or for storage in the pool
        /// </summary>
        /// <returns>The created <typeparamref name="T"/> instance</returns>
        public delegate T ItemFactory();
        /// <summary>
        /// Defines a method signature which will be applied to an item each time an item is retrieved from the pool
        /// </summary>
        /// <param name="item">Item which was retrieved from the pool</param>
        public delegate void OnRetrieveItem(ref T item);
        /// <summary>
        /// Defines a method signature which will be applied to an item each time it is released from user control and
        /// returned to the pool
        /// </summary>
        /// <param name="item">The item being released</param>
        public delegate void OnReleaseItem(ref T item);
        /// <summary>
        /// Defines a method signature which will be applied to an item whenever the item is returned to the pool, but
        /// the pool has reached a threshold for stored items
        /// </summary>
        /// <param name="item">The item being destroyed</param>
        public delegate void OnDestroyItem(ref T item);

        /// <summary>
        /// A count of every item stored in the pool, whether it is released or ready to be released. This does not include ephemeral items that are created
        /// as a result of a full pool
        /// </summary>
        public virtual int Count { get; protected set; }
        /// <summary>
        /// A running count of the stored items that are currently ready to be released. This does not include ephemeral items that are created
        /// as a result of a full pool
        /// </summary>
        public virtual int CurrentCount { get; protected set; }
        /// <summary>
        /// A running count of the items that are currently released from the pool
        /// <para>
        /// <strong>NOTE:</strong> <see cref="ReleasedCount"/> also keeps track of ephemeral items, or items 
        /// which are not stored within the backing container yet are still tracked by the pool. 
        /// This means <see cref="ReleasedCount"/> can exceed both <see cref="Count"/> and <see cref="MaximumCapacity"/>
        /// </para>
        /// </summary>
        public virtual int ReleasedCount { get; protected set; }
        /// <summary>
        /// The total number of items that this pool will keep track of. Note that this pool can still
        /// lend items when <see cref="Count"/> == <see cref="MaximumCapacity"/>, however these lent items
        /// will be created and destroyed as soon as <see cref="Retrieve()"/> and <see cref="Release(IPooledObjectEntity{T})"/>
        /// are called respectively
        /// </summary>
        public int MaximumCapacity { get; }

        protected BaseObjectPool<T>.ItemFactory Factory { get; private set; }
        protected BaseObjectPool<T>.OnRetrieveItem? OnRetrieval { get; private set; }
        protected BaseObjectPool<T>.OnReleaseItem? OnRelease { get; private set; }
        protected BaseObjectPool<T>.OnDestroyItem? OnDestroy { get; private set; }

        protected int InitialCapacity { get; } = -1;
        protected bool CanInitialize { get; set; } = true;

        private const int InitialCapacityIfUnspecified = 100;
        /// <summary>
        /// Create a new <see cref="BaseObjectPool{T}"/> with an item factory and maximum capacity
        /// <para>
        ///     Note: pre-initializing via the constructor may take some time for larger pools. To spread out the instantiation of
        ///     pool-tracked items, consider using <see cref="PreInitializeEnumerable(int?, uint?)"/> or <see cref="PreInitializeAsync(int?)"/>
        /// </para>
        /// </summary>
        /// <param name="itemFactory">Factory method used to construct new <typeparamref name="T"/> instances</param>
        /// <param name="maximumCapacity">Maximum number of <typeparamref name="T"/> instances the pool will keep track of in memory</param>
        /// <param name="preInitialize">Pre-initialize the pool? Note: for larger pools, this may take time and could cause freezes</param>
        public BaseObjectPool(ItemFactory itemFactory, int maximumCapacity, bool preInitialize = false) : this(itemFactory, null, null, null, InitialCapacityIfUnspecified, maximumCapacity, preInitialize) { }
        /// <summary>
        /// Create a new <see cref="BaseObjectPool{T}"/> with an item factory, initial capacity and maximum capacity
        /// <para>
        ///     Note: pre-initializing via the constructor may take some time for larger pools. To spread out the instantiation of
        ///     pool-tracked items, consider using <see cref="PreInitializeEnumerable(int?, uint?)"/> or <see cref="PreInitializeAsync(int?)"/>
        /// </para>
        /// </summary>
        /// <param name="itemFactory">Factory method used to construct new <typeparamref name="T"/> instances</param>
        /// <param name="initialCapacity">Initial amount of <typeparamref name="T"/> instances to create. Negative or zero values will initialize the entire pool based on <paramref name="maximumCapacity"/> Note: large numbers can cause freezing, as the pool is initialized immedately on the calling thread. This can cause freezing</param>
        /// <param name="maximumCapacity">Maximum number of <typeparamref name="T"/> instances the pool will keep track of in memory</param>
        public BaseObjectPool(ItemFactory itemFactory, int initialCapacity, int maximumCapacity) : this(itemFactory, null, null, null, initialCapacity, maximumCapacity, false) => PreInitialize(initialCapacity <= 0 ? null : (uint)initialCapacity);
        /// <summary>
        /// Create a new <see cref="BaseObjectPool{T}"/> with an item factory, tracked-item lifecycle callbacks, and maximum capacity
        /// <para>
        ///     Note: pre-initializing via the constructor may take some time for larger pools. To spread out the instantiation of
        ///     pool-tracked items, consider using <see cref="PreInitializeEnumerable(int?, uint?)"/> or <see cref="PreInitializeAsync(int?)"/>
        /// </para>
        /// </summary>
        /// <param name="itemFactory">Factory method used to construct new <typeparamref name="T"/> instances</param>
        /// <param name="onRetrieval">Callback applied to items when they are retrieved from the pool</param>
        /// <param name="onRelease">Callback applied to items when they are returned to the pool</param>
        /// <param name="onDestroy">Callback applied to items when they are returned to the pool but the pool is full</param>
        /// <param name="maximumCapacity">Maximum number of <typeparamref name="T"/> instances the pool will keep track of in memory</param>
        /// <param name="preInitialize">Pre-initialize the pool? Note: for larger pools, this may take time and could cause freezes</param>
        public BaseObjectPool(ItemFactory itemFactory, OnRetrieveItem? onRetrieval, OnReleaseItem? onRelease, OnDestroyItem? onDestroy, int maximumCapacity, bool preInitialize = false) : this(itemFactory, onRetrieval, onRelease, onDestroy, InitialCapacityIfUnspecified, maximumCapacity, preInitialize) { }
        /// <summary>
        /// Create a new <see cref="BaseObjectPool{T}"/> with an item factory, tracked-item lifecycle callbacks, initial capacity and maximum capacity
        /// <para>
        ///     Note: pre-initializing via the constructor may take some time for larger pools. To spread out the instantiation of
        ///     pool-tracked items, consider using <see cref="PreInitializeEnumerable(int?, uint?)"/> or <see cref="PreInitializeAsync(int?)"/>
        /// </para>
        /// </summary>
        /// <param name="itemFactory">Factory method used to construct new <typeparamref name="T"/> instances</param>
        /// <param name="onRetrieval">Callback applied to items when they are retrieved from the pool</param>
        /// <param name="onRelease">Callback applied to items when they are returned to the pool</param>
        /// <param name="onDestroy">Callback applied to items when they are returned to the pool but the pool is full</param>
        /// <param name="initialCapacity">Initial amount of <typeparamref name="T"/> instances to create. Note: large numbers can cause freezing, as the pool is initialized immedately on the calling thread. This can cause freezing</param>
        /// <param name="initialCapacity">Initial amount of <typeparamref name="T"/> instances to create. Negative or zero values will initialize the entire pool based on <paramref name="maximumCapacity"/> Note: large numbers can cause freezing, as the pool is initialized immedately on the calling thread. This can cause freezing</param>
        /// <param name="preInitialize">Pre-initialize the pool? Note: for larger pools, this may take time and could cause freezes</param>
        public BaseObjectPool(ItemFactory itemFactory, OnRetrieveItem? onRetrieval, OnReleaseItem? onRelease, OnDestroyItem? onDestroy, int initialCapacity, int maximumCapacity, bool preInitialize = false)
        {
            Factory = itemFactory;
            OnRetrieval = onRetrieval;
            OnRelease = onRelease;
            OnDestroy = onDestroy;
            InitialCapacity = initialCapacity;
            MaximumCapacity = maximumCapacity;

            if(preInitialize) PreInitialize();
        }

        public abstract IEnumerator<IPooledObjectEntity<T>> GetEnumerator();
        public abstract IPooledObjectEntity<T> Retrieve();

        /// <summary>
        /// Asynchronously pre-initialize the pool by executing <see cref="Factory"/> <c>n</c> number of times, where <c>n</c> is:
        /// <list type="bullet">
        ///     <item><paramref name="count"/> when not null and positive</item>
        ///     <item><see cref="InitialCapacity"/> when <paramref name="count"/> is null, negative or zero</item>
        ///     <item><see cref="MaximumCapacity"/> when <see cref="InitialCapacity"/> is zero</item>
        /// </list>
        /// <para>
        ///     This method can only be executed once, and may take some time to execute for larger pools
        /// </para>
        /// </summary>
        /// <param name="count">The optional number of items to pre-initialize</param>
        /// <param name="createPerIteration">The number of items to instantiate per iteration</param>
        /// <returns><see cref="Task"/> that completes when the pool's items have been successfully initialized</returns>
        public virtual async Task PreInitializeAsync(uint? count = null) => await Task.Run(() => PreInitialize(count));
        /// <summary>
        /// Pre-initialize the pool by executing <see cref="Factory"/> <c>n</c> number of times, where <c>n</c> is:
        /// <list type="bullet">
        ///     <item><paramref name="count"/> when not null and positive</item>
        ///     <item><see cref="InitialCapacity"/> when <paramref name="count"/> is null, negative or zero</item>
        ///     <item><see cref="MaximumCapacity"/> when <see cref="InitialCapacity"/> is zero</item>
        /// </list>
        /// <para>
        ///     This method can only be executed once, and may take some time to execute for larger pools
        /// </para>
        /// </summary>
        /// <param name="count">The optional number of items to pre-initialize</param>
        /// <param name="createPerIteration">The number of items to instantiate per iteration</param>
        /// <returns><see cref="IEnumerable"/> which can be used to start a coroutine</returns>
        public virtual IEnumerator PreInitializeEnumerable(uint? count = null, uint? createPerIteration = 1)
        {
            if(!CanInitialize) yield break;
            CanInitialize = false;

            uint perIteration = createPerIteration ?? 0;
            uint max = count ?? (uint)(InitialCapacity > 0 ? InitialCapacity : MaximumCapacity);
            int i = 0;

            while(i < max)
            {
                if(perIteration <= 0)
                {
                    for(int j = 0; j < perIteration; j++)
                    {
                        AddItem(Factory.Invoke());
                    }
                    yield break;
                }

                for(int j = 0; j < perIteration; j++)
                {
                    AddItem(Factory.Invoke());
                }
                yield return null;
            }
        }
        /// <summary>
        /// Pre-initialize the pool by executing <see cref="Factory"/> <c>n</c> number of times, where <c>n</c> is:
        /// <list type="bullet">
        ///     <item><paramref name="count"/> when not null and positive</item>
        ///     <item><see cref="InitialCapacity"/> when <paramref name="count"/> is null, negative or zero</item>
        ///     <item><see cref="MaximumCapacity"/> when <see cref="InitialCapacity"/> is zero</item>
        /// </list>
        /// <para>
        ///     This method can only be executed once, and may take some time to execute for larger pools
        /// </para>
        /// </summary>
        /// <param name="count">The optional number of items to pre-initialize</param>
        public virtual void PreInitialize(uint? count = null)
        {
            if(!CanInitialize) return;
            CanInitialize = false;

            uint max = count ?? (uint)(InitialCapacity > 0 ? InitialCapacity : MaximumCapacity);

            for(int i = 0; i < max; i++)
            {
                AddItem(Factory.Invoke());
            }
        }

        protected abstract IPooledObjectEntity<T> AddItem(T item, bool released = false);
        protected internal abstract void Release(IPooledObjectEntity<T> item);
    }
}

#nullable disable