namespace UnityAdditions.LooseRect
{
    using System;
    using UnityEngine;
    using UnityAdditions.Vector2;

    /// <summary>
    /// Essentially a means of attaching a rect object to a game object. A soft rectangle class that does not require the
    /// holding owner to possess a collider. Typically redundant if the holding owner already has a collider. Implicitly 
    /// equivalent to a Bounds object. Rect size will never be permitted below zero on any dimension.
    /// 
    /// Looking into: Subscribing this component to normal unity OnTrigger events
    /// </summary>
    public class LooseRect : MonoBehaviour, IEquatable<LooseRect>
    {
        #region Fields and constructors
        public bool alwaysDrawGizmos = true;
        public Color gizmoColor = Color.green;

        public Rect rect;

        public LooseRect() { }
        public LooseRect(Rect rect) { this.rect = rect; }
        #endregion

        #region Unity Message Events
        private void OnValidate()
        {
            Vector2 clamp = rect.size;
            clamp.ClampValuesPositive();
            rect.size = clamp;
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

            Vector2[] points = new Vector2[]
            {
                new Vector2(rect.max.x, rect.max.y),
                new Vector2(rect.min.x, rect.max.y),
                new Vector2(rect.max.x, rect.min.y),
                new Vector2(rect.min.x, rect.min.y)
            };

            if (selected)
            {
                Gizmos.DrawWireSphere(rect.center, 0.045f);
                foreach (Vector2 v in points)
                    Gizmos.DrawWireCube(v, Vector2.one * 0.085f);
            }

            Gizmos.DrawLine(points[0], points[1]);
            Gizmos.DrawLine(points[0], points[2]);
            Gizmos.DrawLine(points[3], points[2]);
            Gizmos.DrawLine(points[3], points[1]);
        }
        #endregion

        #region Implementations, overrides and operators
        public bool Equals(LooseRect other)
        {
            if (other == null) return false;
            else return this == other;
        }

        public override int GetHashCode() => base.GetHashCode();
        public override bool Equals(object other)
        {
            if (other == null || other as LooseRect == null) return false;
            else return Equals(other as LooseRect);
        }

        /// <summary>
        /// Check if the supplied Vector2 is contained within the SoftRect
        /// </summary>
        /// <param name="v">this SoftRect instance</param>
        /// <returns>True if supplied point is within the rect</returns>
        public bool this[Vector2 v] { get { return rect.Contains(v); } }

        public static bool operator ==(LooseRect lhs, LooseRect rhs) => lhs.Equals(rhs);
        public static bool operator !=(LooseRect lhs, LooseRect rhs) => !(lhs == rhs);
        public static bool operator ==(LooseRect lhs, Rect rhs) => lhs.Equals(rhs);
        public static bool operator !=(LooseRect lhs, Rect rhs) => !(lhs == rhs);
        public static bool operator ==(Rect lhs, LooseRect rhs) => lhs.Equals(rhs);
        public static bool operator !=(Rect lhs, LooseRect rhs) => !(lhs == rhs);

        public static implicit operator Rect(LooseRect r) => new Rect(r.rect.center, r.rect.size);
        #endregion
    }
}