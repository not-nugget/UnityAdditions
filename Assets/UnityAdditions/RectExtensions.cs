﻿namespace UnityAdditions.Rect
{
    using UnityEngine;

    /// <summary>
    /// Suite of extension methods for operating on a bounds to perform a number of tasks
    /// </summary>
    public static class RectExtensions
    {
        /// <summary>
        /// Return a random point within the bounds
        /// </summary>
        /// <param name="b">Rect instance this method operates on</param>
        /// <param name="suppressWarnings">Suppress warnings which would otherwise be generated by this method</param>
        /// <returns>A random Vector2 based on the min and max of the provided bounds</returns>
        public static Vector2 GetRandomPointWithin(this ref Rect b, bool suppressWarnings = false)
        {
            if (b.size == Vector2.zero)
            {
                if (!suppressWarnings || !UnityAdditionSettings.SuppressWarnings)
                    Debug.LogWarning($"{nameof(RectExtensions)}.{nameof(GetRandomPointsWithin)} was called on a bounds whose size was Zero. No action was taken.");
                return Vector2.zero;
            }

            Vector2 min = b.min, max = b.max;
            return new Vector2()
            {
                x = (min.x == max.x) ? min.x : Random.Range(min.x, max.x),
                y = (min.y == max.y) ? min.y : Random.Range(min.y, max.y),
            };
        }

        /// <summary>
        /// Retrun an array of random points within the bounds. NOTE: Runs on the thread you call it on. If its on the main thread, can cause major slowdowns. Consider seperating from the main thread if necessary.
        /// </summary>
        /// <param name="b">Rect instance this method operates on</param>
        /// <param name="numberOfPoints">The number of points to get within the bounds</param>
        /// <param name="suppressWarnings">Suppress warnings which would otherwise be generated by this method</param>
        /// <param name="suppressIntermediaryWarning">Suppress this method's exclusive intermediary warning if numberOfPoints is equal to 1. Set to true if this warning does not apply to you, or you are using this method as you are intending</param>
        /// <returns>An array of random points which are all inside the bounds. Returns null when numberOfPoints is 0, or the bound's size is Vector2.zero</returns>
        public static Vector2[] GetRandomPointsWithin(this ref Rect b, int numberOfPoints, bool suppressWarnings = false, bool suppressIntermediaryWarning = false)
        {
            if (b.size == Vector2.zero || numberOfPoints == 0)
            {
                if (!suppressWarnings || !UnityAdditionSettings.SuppressWarnings)
                    Debug.LogWarning($"{nameof(RectExtensions)}.{nameof(GetRandomPointsWithin)} was called on a bounds:{b} whos size was Zero, or numberOfPoints was equal to Zero. No action was taken.");
                return null;
            }

            if (numberOfPoints < 0)
            {
                if (!UnityAdditionSettings.SuppressErrors)
                    Debug.LogError($"{nameof(RectExtensions)}.{nameof(GetRandomPointsWithin)} was called with a negative value for numberOfPoints. This is not allowed.");
                return null;
            }

            if (!suppressIntermediaryWarning && !suppressWarnings && !UnityAdditionSettings.SuppressWarnings && numberOfPoints == 1)
                Debug.LogWarning($"{nameof(RectExtensions)}.{nameof(GetRandomPointsWithin)} was called with a value of One for numberOfPoints. Why not call {nameof(RectExtensions)}.{nameof(GetRandomPointWithin)} instead?");

            Vector2[] points = new Vector2[numberOfPoints];
            Vector2 min = b.min, max = b.max;
            for (int i = 0; i < numberOfPoints; i++)
            {
                Vector2 point = new Vector2
                {
                    x = (min.x == max.x) ? min.x : Random.Range(min.x, max.x),
                    y = (min.y == max.y) ? min.y : Random.Range(min.y, max.y),
                };

                points[i] = point;
            }

            return points;
        }

    }
}
