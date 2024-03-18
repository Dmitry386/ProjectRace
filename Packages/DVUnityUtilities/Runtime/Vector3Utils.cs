using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DVUnityUtilities
{
    public static class Vector3Utils
    {
        public static bool IsReachDestination(Vector3 a, Vector3 b, float max_offset = 0.1f)
        {
            return Vector3.Distance(a, b) <= max_offset;
        }

        public static Vector3 GetDirectionNormalized(Vector3 to, Vector3 from)
        {
            return (to - from).normalized;
        }

        public static Vector3 SetZ(this Vector3 v, float z)
        {
            v.z = z;
            return v;
        }

        public static Vector3 SetY(this Vector3 v, float y)
        {
            v.y = y;
            return v;
        }

        public static Vector3 SetX(this Vector3 v, float x)
        {
            v.x = x;
            return v;
        }

        public static float CalculateDistance(this IEnumerable<Vector3> points)
        {
            if (points == null || points.Count() == 0) return 0;

            float res = 0;
            Vector3 last_point = points.First();

            foreach (var cur_point in points)
            {
                res += Vector3.Distance(last_point, cur_point);
                last_point = cur_point;
            }

            return res;
        }
    }
}