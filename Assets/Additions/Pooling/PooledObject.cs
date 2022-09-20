using System;
using Unity.Additions.Pooling.Abstractions;

namespace Unity.Additions.Pooling.Assets.Additions.Pooling
{
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
