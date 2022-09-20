using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Additions.Pooling.Abstractions;

#nullable enable

namespace Unity.Additions.Pooling.Assets.Additions.Pooling
{
    /// <summary>
    /// An object pool which is backed by <see cref="List{T}"/>
    /// </summary>
    public sealed class ObjectPool<T> : BaseObjectPool<T> where T : class
    {
        public override int Count => PoolCollection.Count;
        public override int ReleasedCount { get => base.ReleasedCount; protected set => base.ReleasedCount = value; }

        private readonly List<PooledObject<T>> PoolCollection;
        private readonly List<PooledObject<T>> EphemeralPool;

        public ObjectPool(ItemFactory itemFactory, int maximumCapacity, bool preInitialize = false) : base(itemFactory, maximumCapacity, preInitialize)
        {
            PoolCollection = new(MaximumCapacity);
            EphemeralPool = new();
        }
        public ObjectPool(ItemFactory itemFactory, int initialCapacity, int maximumCapacity) : base(itemFactory, initialCapacity, maximumCapacity)
        {
            PoolCollection = new(MaximumCapacity);
            EphemeralPool = new();
        }
        public ObjectPool(ItemFactory itemFactory, OnRetrieveItem onRetrieval, OnReleaseItem onRelease, OnDestroyItem onDestroy, int maximumCapacity, bool preInitialize = false) : base(itemFactory, onRetrieval, onRelease, onDestroy, maximumCapacity, preInitialize)
        {
            PoolCollection = new(MaximumCapacity);
            EphemeralPool = new();
        }
        public ObjectPool(ItemFactory itemFactory, OnRetrieveItem onRetrieval, OnReleaseItem onRelease, OnDestroyItem onDestroy, int initialCapacity, int maximumCapacity, bool preInitialize = false) : base(itemFactory, onRetrieval, onRelease, onDestroy, initialCapacity, maximumCapacity, preInitialize)
        {
            PoolCollection = new(MaximumCapacity);
            EphemeralPool = new();
        }

        public override IEnumerator<IPooledObjectEntity<T>> GetEnumerator() => PoolCollection.GetEnumerator();

        public override IPooledObjectEntity<T> Retrieve()
        {
            CannotInitialize();

            PooledObject<T>? beingRetrieved = null;

            if(Count < MaximumCapacity)
            {
                beingRetrieved = (PooledObject<T>)AddItem(Factory.Invoke(), true);
                beingRetrieved.Pooled = true;
            }

            if(beingRetrieved is null && CurrentCount > 0)
            {
                CurrentCount--;
                beingRetrieved = PoolCollection.First(x => !x.Released);
            }

            if(beingRetrieved is null)
            {
                ReleasedCount++;
                beingRetrieved = new(this, Factory.Invoke(), true)
                {
                    Pooled = false
                };
                EphemeralPool.Add(beingRetrieved);
            }

            OnRetrieval?.Invoke(ref beingRetrieved.Item);
            return beingRetrieved;
        }
        protected internal override void Release(IPooledObjectEntity<T> item)
        {
            CannotInitialize();

            if(item is not PooledObject<T> beingReleased) return;

            if(beingReleased.Pooled)
            {
                ReleasedCount--;
                CurrentCount++;
                beingReleased.Released = false;
                OnRelease?.Invoke(ref beingReleased.Item);
                return;
            }

            if(EphemeralPool.Remove(beingReleased))
            {
                ReleasedCount--;
                OnDestroy?.Invoke(ref beingReleased.Item);
            }
        }

        protected override IPooledObjectEntity<T> AddItem(T item, bool released = false)
        {
            PooledObject<T> tracked = new(this, item, true);
            PoolCollection.Add(tracked);

            if(released)
            {
                ReleasedCount++;
            }
            else
            {
                CurrentCount++;
            }

            return tracked;
        }

        private void CannotInitialize()
        {
            if(CanInitialize) CanInitialize = false;
        }
    }

#nullable disable

    /// <summary>
    /// An item that is tracked by an <see cref="IObjectPool{T}"/>
    /// </summary>
    /// <typeparam name="T">Type being tracked</typeparam>
    public sealed class PooledObject<T> : IEquatable<PooledObject<T>>, IPooledObjectEntity<T> where T : class
    {
        public ref T Item => ref item;

        public bool Released { get; internal set; }
        public bool Pooled { get; internal set; }

        private BaseObjectPool<T> Owner { get; }

        private Guid id;
        private T item;

        internal PooledObject(BaseObjectPool<T> owner, T item, bool released)
        {
            id = Guid.NewGuid();

            Owner = owner;
            Released = released;

            this.item = item;
        }

        public void Return() => Owner.Release(this);

        public bool Equals(PooledObject<T> other) => other is not null && other.id == id;

        public static implicit operator T(PooledObject<T> obj) => obj.Item;
    }
}
