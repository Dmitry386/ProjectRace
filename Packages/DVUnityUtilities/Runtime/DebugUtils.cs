using System.Collections.Generic;
using UnityEngine;

namespace DVUnityUtilities
{
    public static class DebugUtils
    {
        public static void DebugDrawArrow(this Ray ray, Color color, float dist = 1, float duration = 0)
        {
            DebugDrawArrowWithDuration(color, ray.origin, ray.direction * dist, 1, 20, duration);
        }

        public static void DebugDraw(this Ray ray, Color color, float dist = 1, float duration = 0)
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * dist, color, duration);
        }

        public static void DebugDraw(this IEnumerable<Vector3> points, Color color, float scale = 1f, float duration = 0)
        {
            foreach (var p in points)
            {
                p.DebugDraw(color, scale, duration);
            }
        }

        public static void DebugDraw(this Vector3 p, Color color, float scale = 1f, float duration = 0)
        {
            DebugDrawWireCube(p, Vector3.one * scale, Quaternion.identity, color, duration);
        }

        public static void GizmosDrawArrow(in Vector3 pos, in Vector3 direction, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
        {
#if UNITY_EDITOR
            Gizmos.DrawRay(pos, direction);

            var lr = direction == Vector3.zero ? Quaternion.identity : Quaternion.LookRotation(direction);

            Vector3 right = lr * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Vector3 left = lr * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Gizmos.DrawRay(pos + direction, right * arrowHeadLength);
            Gizmos.DrawRay(pos + direction, left * arrowHeadLength);
#endif
        }

        public static void DebugDrawArrow(Color c, in Vector3 pos, in Vector3 direction, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
        {
#if UNITY_EDITOR
            Debug.DrawRay(pos, direction, c);

            var lr = direction == Vector3.zero ? Quaternion.identity : Quaternion.LookRotation(direction);

            Vector3 right = lr * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Vector3 left = lr * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Debug.DrawRay(pos + direction, right * arrowHeadLength, c);
            Debug.DrawRay(pos + direction, left * arrowHeadLength, c);
#endif
        }

        public static void DebugDrawArrowWithDuration(Color c, in Vector3 pos, in Vector3 direction, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f, float duration = 1)
        {
            Debug.DrawRay(pos, direction, c, duration);

            var lr = direction == Vector3.zero ? Quaternion.identity : Quaternion.LookRotation(direction);

            Vector3 right = lr * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Vector3 left = lr * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Debug.DrawRay(pos + direction, right * arrowHeadLength, c, duration);
            Debug.DrawRay(pos + direction, left * arrowHeadLength, c, duration);
        }

        public static void DebugDrawWireCube(in Vector3 pos, in Vector3 scale, in Quaternion rot, in Color c, float duration = 0)
        {
            // create matrix
            var m = new Matrix4x4();
            m.SetTRS(pos, rot, scale);

            var point1 = m.MultiplyPoint(new Vector3(-0.5f, -0.5f, 0.5f));
            var point2 = m.MultiplyPoint(new Vector3(0.5f, -0.5f, 0.5f));
            var point3 = m.MultiplyPoint(new Vector3(0.5f, -0.5f, -0.5f));
            var point4 = m.MultiplyPoint(new Vector3(-0.5f, -0.5f, -0.5f));

            var point5 = m.MultiplyPoint(new Vector3(-0.5f, 0.5f, 0.5f));
            var point6 = m.MultiplyPoint(new Vector3(0.5f, 0.5f, 0.5f));
            var point7 = m.MultiplyPoint(new Vector3(0.5f, 0.5f, -0.5f));
            var point8 = m.MultiplyPoint(new Vector3(-0.5f, 0.5f, -0.5f));

            Debug.DrawLine(point1, point2, c, duration);
            Debug.DrawLine(point2, point3, c, duration);
            Debug.DrawLine(point3, point4, c, duration);
            Debug.DrawLine(point4, point1, c, duration);

            Debug.DrawLine(point5, point6, c, duration);
            Debug.DrawLine(point6, point7, c, duration);
            Debug.DrawLine(point7, point8, c, duration);
            Debug.DrawLine(point8, point5, c, duration);

            Debug.DrawLine(point1, point5, c, duration);
            Debug.DrawLine(point2, point6, c, duration);
            Debug.DrawLine(point3, point7, c, duration);
            Debug.DrawLine(point4, point8, c, duration);
        }
    }
}