using System;
using UnityEngine;
using UnityAdditions.Vector3;

//TODO SoftBounds editor that allows for click-and-drag editing

/// <summary>
/// NOT FULLY IMPLEMENTED - 
/// A soft boundary class that does not require the holding owner to possess a collider. Typically redundant if the holding owner already has a collider.
/// Implicitly equivalent to a Bounds object. Bounds size will never be permitted below zero on any dimension.
/// </summary>
public class SoftBounds : MonoBehaviour, IEquatable<SoftBounds>
{
    public bool alwaysDrawGizmos = true;
    public Bounds bounds;

    public SoftBounds() { bounds = new Bounds(); }
    public SoftBounds(Bounds bounds) { this.bounds = bounds; }

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
        Gizmos.color = Color.green;

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

    #region Implementations, overrides and operators
    public bool Equals(SoftBounds other)
    {
        if (other == null) return false;
        else return this == other;
    }


    public override int GetHashCode() => base.GetHashCode();
    public override bool Equals(object other)
    {
        if (other == null || other as SoftBounds == null) return false;
        else return Equals(other as SoftBounds);
    }

    public static bool operator ==(SoftBounds lhs, SoftBounds rhs) => lhs.bounds == rhs.bounds;
    public static bool operator !=(SoftBounds lhs, SoftBounds rhs) => !lhs == rhs;

    public static implicit operator SoftBounds(Bounds b) { return new SoftBounds(b); }
    public static implicit operator Bounds(SoftBounds b) { return new Bounds(b.bounds.center, b.bounds.size); }
    #endregion
}
