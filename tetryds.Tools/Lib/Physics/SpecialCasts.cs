using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tetryds.Tools
{
    public static class SpecialCasts
    {
        /// <summary>
        /// Cast a ray through the Scene and store the hits into the buffer.
        /// </summary>
        /// <param name="origin">The starting point and direction of the ray.</param>
        /// <param name="angle">The cone opening angle, in degrees.</param>
        /// <param name="direction">The direction of the ray.</param>
        /// <param name="results">The buffer to store the hits into. Use a larger array than the minimum collisions you want for the cast to ignore false posisives.</param>
        /// <param name="maxDistance">The max distance the rayhit is allowed to be from the start of the ray. Big vaules can cause performance issues!</param>
        /// <param name="layerMask">A that is used to selectively ignore colliders when casting a ray.</param>
        /// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
        /// <returns>The amount of hits stored into the results buffer.</returns>
        public static int RaycastConeNonAlloc(Vector3 origin, float angle, Vector3 direction, RaycastHit[] results, float maxDistance, int layerMask = Physics.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
        {
            if (angle >= 180f) throw new ArgumentException("Cannot raycast for angle equal to or wider than 180 degrees");
            if (maxDistance <= 0f) throw new ArgumentException("Distance has to be a non-zero positive value");
            if (direction == Vector3.zero) throw new ArgumentException("Direction has to be a non-zero vector");

            // Compute the angle between the center of the cone and the cone walls
            float halfAngle = angle / 2f;
            // Compute the radius of the cone given its distance
            float radius = Mathf.Sin(halfAngle * Mathf.Deg2Rad) * maxDistance;
            // Compute the dot product to validate if a hit is within the cone or not
            float maxDot = Mathf.Cos(halfAngle * Mathf.Deg2Rad);

            // The plane of the bottom of the cone, since this cast has an icecream cone shape
            Plane bottomPlane = new Plane(-direction, origin + maxDistance * direction);

            // Start point is behind origin to compensate for the fact that we are casting a sphere
            Vector3 startPoint = origin - direction * radius;

            int count = Physics.SphereCastNonAlloc(startPoint, radius, direction, results, maxDistance, layerMask, queryTriggerInteraction);

            for (int i = 0; i < count; i++)
            {
                RaycastHit hit = results[i];
                Vector3 hitDir = (hit.point - origin).normalized;

                // inConeAngle remove hits outside of the cone angle
                bool inConeAngle = (Vector3.Dot(direction, hitDir) < maxDot || hit.point == Vector3.zero);
                // inConeDistance removes items past the cone bottom
                bool inConeDistance = bottomPlane.GetSide(hit.point);
                if (inConeDistance && inConeAngle)
                {
                    //Shift array to the left to remove current hit because it is outside cone
                    for (int j = i; j < results.Length - 1; j++)
                    {
                        results[j] = results[j + 1];
                    }
                    count--;
                    i--;
                }
            }

            return count;
        }
    }
}
