using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tetryds.Tools
{
    public static class ExtraGizmos
    {
        /// <summary>
        /// Draws an ugly but useful cone as Unity gizmos.
        /// </summary>
        /// <param name="tipPos">The position of the tip of the cone.</param>
        /// <param name="downDir">The downwards direction from the tip.</param>
        /// <param name="angle">The opening width angle of the cone.</param>
        /// <param name="height">The height of the cone.</param>
        /// <param name="pointCount">The resolution of the cone drawing.</param>
        public static void DrawCone(Vector3 tipPos, Vector3 downDir, float angle, float height, int pointCount)
        {
            if (angle >= 180f) throw new ArgumentException("Cannot draw cone with angle equal to or wider than 180 degrees");
            if (pointCount <= 0) throw new ArgumentException("Cannot draw cone with 0 points");
            if (downDir == Vector3.zero) throw new ArgumentException("Cannot draw a cone with no direction");

            // Compute the angle between the center of the cone and the cone walls
            float halfAngle = angle / 2.0f;
            Vector3 bottom = tipPos + downDir * height;
            float step = 360f / pointCount;
            Vector3[] points = new Vector3[pointCount];
            float radius = Mathf.Sin(angle * Mathf.Deg2Rad / 2f) * height;

            // Compute points on the bottom of the cone
            for (int i = 0; i < pointCount; i++)
            {
                points[i] = bottom + Quaternion.AngleAxis(step * i, downDir) * Vector3.up * radius;
            }

            // Draw rays from the tip of the cone to the bottom points
            Gizmos.DrawRay(tipPos, bottom);
            for (int i = 0; i < points.Length; i++)
            {
                Gizmos.DrawLine(tipPos, points[i]);
                Gizmos.DrawLine(bottom, points[i]);
            }

            // Draw rays between the bottom points
            Gizmos.DrawLine(points[points.Length - 1], points[0]);
            for (int i = 0; i < points.Length - 1; i++)
            {
                Gizmos.DrawLine(points[i], points[i + 1]);
            }
        }
    }
}
