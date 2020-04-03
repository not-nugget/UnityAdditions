namespace UnityAdditions.LooseBounds
{
    using System;
    using UnityEngine;
    using UnityAdditions.Vector3;

    //TODO SoftBounds editor that allows for click-and-drag editing

    /// <summary>
    /// Essentially a means of attaching a bounds object to a game object. A soft boundary class that does not require the
    /// holding owner to possess a collider. Typically redundant if the holding owner already has a collider. Implicitly 
    /// equivalent to a Bounds object. Bounds size will never be permitted below zero on any dimension.
    /// 
    /// Looking into: Subscribing to normal unity OnTrigger events
    /// </summary>
    public class LooseBounds : MonoBehaviour, IEquatable<LooseBounds>
    {
        #region Fields and Constructors
        public bool alwaysDrawGizmos = true;
        public Color gizmoColor = Color.green;

        public Bounds bounds;

        public LooseBounds() { bounds = new Bounds(); }
        public LooseBounds(Bounds bounds) { this.bounds = bounds; }
        #endregion

        #region Unity Message Events
        private void OnValidate()
        {
            Vector3 clamp = bounds.extents;
            clamp.ClampValuesPositive();
            bounds.extents = clamp;
        }

        private void OnDrawGizmos()
        {
            if (alwaysDrawGizmos) DrawGizmos();
        }

        private void OnDrawGizmosSelected()
        {
            DrawGizmos(true);
        }

        private void DrawGizmos(bool selected = false)
        {
            Gizmos.color = gizmoColor;

            Vector3[] points = new Vector3[]
            {
            new Vector3(bounds.max.x, bounds.max.y, bounds.max.z),
            new Vector3(bounds.max.x, bounds.max.y, bounds.min.z),
            new Vector3(bounds.max.x, bounds.min.y, bounds.max.z),
            new Vector3(bounds.max.x, bounds.min.y, bounds.min.z),
            new Vector3(bounds.min.x, bounds.max.y, bounds.max.z),
            new Vector3(bounds.min.x, bounds.max.y, bounds.min.z),
            new Vector3(bounds.min.x, bounds.min.y, bounds.max.z),
            new Vector3(bounds.min.x, bounds.min.y, bounds.min.z)
            };

            if (selected)
            {
                Gizmos.DrawWireSphere(bounds.center, .15f);
                foreach (Vector3 v in points)
                    Gizmos.DrawCube(v, Vector3.one * 0.085f);
            }

            Gizmos.DrawLine(points[0], points[1]);
            Gizmos.DrawLine(points[0], points[2]);
            Gizmos.DrawLine(points[0], points[4]);
            Gizmos.DrawLine(points[7], points[5]);
            Gizmos.DrawLine(points[7], points[6]);
            Gizmos.DrawLine(points[7], points[3]);
            Gizmos.DrawLine(points[4], points[5]);
            Gizmos.DrawLine(points[4], points[6]);
            Gizmos.DrawLine(points[1], points[3]);
            Gizmos.DrawLine(points[1], points[5]);
            Gizmos.DrawLine(points[2], points[3]);
            Gizmos.DrawLine(points[2], points[6]);
        }
        #endregion 

        #region Implementations, overrides and operators
        public bool Equals(LooseBounds other)
        {
            if (other == null) return false;
            else return this == other;
        }

        public override int GetHashCode() => base.GetHashCode();
        public override bool Equals(object other)
        {
            if (other == null || other as LooseBounds == null) return false;
            else return Equals(other as LooseBounds);
        }

        /// <summary>
        /// Check if the supplied Vector3 is contained within the SoftBounds
        /// </summary>
        /// <param name="v">this SoftBounds instance</param>
        /// <returns>True if supplied point is within the bounds</returns>
        public bool this[Vector3 v] { get { return bounds.Contains(v); } }

        public static bool operator ==(LooseBounds lhs, LooseBounds rhs) => lhs.Equals(rhs);
        public static bool operator !=(LooseBounds lhs, LooseBounds rhs) => !(lhs == rhs);
        public static bool operator ==(LooseBounds lhs, Rect rhs) => lhs.Equals(rhs);
        public static bool operator !=(LooseBounds lhs, Rect rhs) => !(lhs == rhs);
        public static bool operator ==(Rect lhs, LooseBounds rhs) => lhs.Equals(rhs);
        public static bool operator !=(Rect lhs, LooseBounds rhs) => !(lhs == rhs);

        public static implicit operator Bounds(LooseBounds b) => new Bounds(b.bounds.center, b.bounds.size);
        #endregion
    }
}