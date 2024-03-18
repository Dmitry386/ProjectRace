using System.Collections.Generic;
using UnityEngine;

public static class UtilGeometry
{
    public static Rect TwoPointsToRect(Vector3 p1, Vector3 p2)
    {
        Vector3 scale = p2 - p1;
        scale.x = Mathf.Abs(scale.x);
        scale.y = Mathf.Abs(scale.y);
        scale.z = Mathf.Abs(scale.z);

        return new Rect((p1 + p2) * 0.5f, scale);
    }

    public static bool PointInTriangle(Vector3 a, Vector3 b, Vector3 c, Vector3 p)
    {
        Vector3 d, e;
        double w1, w2;
        d = b - a;
        e = c - a;

        if (Mathf.Approximately(e.y, 0))
        {
            e.y = 0.0001f;
        }

        w1 = (e.x * (a.y - p.y) + e.y * (p.x - a.x)) / (d.x * e.y - d.y * e.x);
        w2 = (p.y - a.y - w1 * d.y) / e.y;
        return (w1 >= 0f) && (w2 >= 0.0) && ((w1 + w2) <= 1.0);
    }

    public static bool TriTriIntersect(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 u0, Vector3 u1, Vector3 u2)
    {
        Vector3 e1, e2;
        Vector3 n1, n2;
        Vector3 dd;
        Vector2 isect1 = Vector2.zero, isect2 = Vector2.zero;

        float du0, du1, du2, dv0, dv1, dv2, d1, d2;
        float du0du1, du0du2, dv0dv1, dv0dv2;
        float vp0, vp1, vp2;
        float up0, up1, up2;
        float bb, cc, max;

        short index;

        // compute plane equation of triangle(v0,v1,v2) 
        e1 = v1 - v0;
        e2 = v2 - v0;
        n1 = Vector3.Cross(e1, e2);
        d1 = -Vector3.Dot(n1, v0);
        // plane equation 1: N1.X+d1=0 */

        // put u0,u1,u2 into plane equation 1 to compute signed distances to the plane
        du0 = Vector3.Dot(n1, u0) + d1;
        du1 = Vector3.Dot(n1, u1) + d1;
        du2 = Vector3.Dot(n1, u2) + d1;

        // coplanarity robustness check 
        if (Mathf.Abs(du0) < Mathf.Epsilon) { du0 = 0.0f; }
        if (Mathf.Abs(du1) < Mathf.Epsilon) { du1 = 0.0f; }
        if (Mathf.Abs(du2) < Mathf.Epsilon) { du2 = 0.0f; }

        du0du1 = du0 * du1;
        du0du2 = du0 * du2;

        // same sign on all of them + not equal 0 ? 
        if (du0du1 > 0.0f && du0du2 > 0.0f)
        {
            // no intersection occurs
            return false;
        }

        // compute plane of triangle (u0,u1,u2)
        e1 = u1 - u0;
        e2 = u2 - u0;
        n2 = Vector3.Cross(e1, e2);
        d2 = -Vector3.Dot(n2, u0);

        // plane equation 2: N2.X+d2=0 
        // put v0,v1,v2 into plane equation 2
        dv0 = Vector3.Dot(n2, v0) + d2;
        dv1 = Vector3.Dot(n2, v1) + d2;
        dv2 = Vector3.Dot(n2, v2) + d2;

        if (Mathf.Abs(dv0) < Mathf.Epsilon) { dv0 = 0.0f; }
        if (Mathf.Abs(dv1) < Mathf.Epsilon) { dv1 = 0.0f; }
        if (Mathf.Abs(dv2) < Mathf.Epsilon) { dv2 = 0.0f; }


        dv0dv1 = dv0 * dv1;
        dv0dv2 = dv0 * dv2;

        // same sign on all of them + not equal 0 ? 
        if (dv0dv1 > 0.0f && dv0dv2 > 0.0f)
        {
            // no intersection occurs
            return false;
        }

        // compute direction of intersection line 
        dd = Vector3.Cross(n1, n2);

        // compute and index to the largest component of D 
        max = (float)Mathf.Abs(dd[0]);
        index = 0;
        bb = (float)Mathf.Abs(dd[1]);
        cc = (float)Mathf.Abs(dd[2]);
        if (bb > max) { max = bb; index = 1; }
        if (cc > max) { max = cc; index = 2; }

        // this is the simplified projection onto L
        vp0 = v0[index];
        vp1 = v1[index];
        vp2 = v2[index];

        up0 = u0[index];
        up1 = u1[index];
        up2 = u2[index];

        // compute interval for triangle 1 
        float a = 0, b = 0, c = 0, x0 = 0, x1 = 0;
        if (ComputeIntervals(vp0, vp1, vp2, dv0, dv1, dv2, dv0dv1, dv0dv2, ref a, ref b, ref c, ref x0, ref x1))
        {
            return TriTriCoplanar(n1, v0, v1, v2, u0, u1, u2);
        }

        // compute interval for triangle 2 
        float d = 0, e = 0, f = 0, y0 = 0, y1 = 0;
        if (ComputeIntervals(up0, up1, up2, du0, du1, du2, du0du1, du0du2, ref d, ref e, ref f, ref y0, ref y1))
        {
            return TriTriCoplanar(n1, v0, v1, v2, u0, u1, u2);
        }

        float xx, yy, xxyy, tmp;
        xx = x0 * x1;
        yy = y0 * y1;
        xxyy = xx * yy;

        tmp = a * xxyy;
        isect1[0] = tmp + b * x1 * yy;
        isect1[1] = tmp + c * x0 * yy;

        tmp = d * xxyy;
        isect2[0] = tmp + e * xx * y1;
        isect2[1] = tmp + f * xx * y0;

        Sort(isect1);
        Sort(isect2);

        return !(isect1[1] < isect2[0] || isect2[1] < isect1[0]);
    }

    private static void Sort(Vector2 v)
    {
        if (v.x > v.y)
        {
            float c;
            c = v.x;
            v.x = v.y;
            v.y = c;
        }
    }

    /// <summary>
    /// This edge to edge test is based on Franlin Antonio's gem: "Faster Line Segment Intersection", in Graphics Gems III, pp. 199-202 
    /// </summary>
    private static bool EdgeEdgeTest(Vector3 v0, Vector3 v1, Vector3 u0, Vector3 u1, int i0, int i1)
    {
        float Ax, Ay, Bx, By, Cx, Cy, e, d, f;
        Ax = v1[i0] - v0[i0];
        Ay = v1[i1] - v0[i1];

        Bx = u0[i0] - u1[i0];
        By = u0[i1] - u1[i1];
        Cx = v0[i0] - u0[i0];
        Cy = v0[i1] - u0[i1];
        f = Ay * Bx - Ax * By;
        d = By * Cx - Bx * Cy;
        if ((f > 0 && d >= 0 && d <= f) || (f < 0 && d <= 0 && d >= f))
        {
            e = Ax * Cy - Ay * Cx;
            if (f > 0)
            {
                if (e >= 0 && e <= f) { return true; }
            }
            else
            {
                if (e <= 0 && e >= f) { return true; }
            }
        }

        return false;
    }

    private static bool EdgeAgainstTriEdges(Vector3 v0, Vector3 v1, Vector3 u0, Vector3 u1, Vector3 u2, short i0, short i1)
    {
        // test edge u0,u1 against v0,v1
        if (EdgeEdgeTest(v0, v1, u0, u1, i0, i1)) { return true; }

        // test edge u1,u2 against v0,v1 
        if (EdgeEdgeTest(v0, v1, u1, u2, i0, i1)) { return true; }

        // test edge u2,u1 against v0,v1 
        if (EdgeEdgeTest(v0, v1, u2, u0, i0, i1)) { return true; }

        return false;
    }

    private static bool PointInTri(Vector3 v0, Vector3 u0, Vector3 u1, Vector3 u2, short i0, short i1)
    {
        float a, b, c, d0, d1, d2;

        // is T1 completly inside T2?
        // check if v0 is inside tri(u0,u1,u2)
        a = u1[i1] - u0[i1];
        b = -(u1[i0] - u0[i0]);
        c = -a * u0[i0] - b * u0[i1];
        d0 = a * v0[i0] + b * v0[i1] + c;

        a = u2[i1] - u1[i1];
        b = -(u2[i0] - u1[i0]);
        c = -a * u1[i0] - b * u1[i1];
        d1 = a * v0[i0] + b * v0[i1] + c;

        a = u0[i1] - u2[i1];
        b = -(u0[i0] - u2[i0]);
        c = -a * u2[i0] - b * u2[i1];
        d2 = a * v0[i0] + b * v0[i1] + c;

        if (d0 * d1 > 0.0f)
        {
            if (d0 * d2 > 0.0f) { return true; }
        }

        return false;
    }

    private static bool TriTriCoplanar(Vector3 N, Vector3 v0, Vector3 v1, Vector3 v2, Vector3 u0, Vector3 u1, Vector3 u2)
    {
        float[] A = new float[3];
        short i0, i1;

        // first project onto an axis-aligned plane, that maximizes the area
        // of the triangles, compute indices: i0,i1. 
        A[0] = Mathf.Abs(N[0]);
        A[1] = Mathf.Abs(N[1]);
        A[2] = Mathf.Abs(N[2]);
        if (A[0] > A[1])
        {
            if (A[0] > A[2])
            {
                // A[0] is greatest
                i0 = 1;
                i1 = 2;
            }
            else
            {
                // A[2] is greatest
                i0 = 0;
                i1 = 1;
            }
        }
        else
        {
            if (A[2] > A[1])
            {
                // A[2] is greatest 
                i0 = 0;
                i1 = 1;
            }
            else
            {
                // A[1] is greatest 
                i0 = 0;
                i1 = 2;
            }
        }

        // test all edges of triangle 1 against the edges of triangle 2 
        if (EdgeAgainstTriEdges(v0, v1, u0, u1, u2, i0, i1)) { return true; }
        if (EdgeAgainstTriEdges(v1, v2, u0, u1, u2, i0, i1)) { return true; }
        if (EdgeAgainstTriEdges(v2, v0, u0, u1, u2, i0, i1)) { return true; }

        // finally, test if tri1 is totally contained in tri2 or vice versa 
        if (PointInTri(v0, u0, u1, u2, i0, i1)) { return true; }
        if (PointInTri(u0, v0, v1, v2, i0, i1)) { return true; }

        return false;
    }



    private static bool ComputeIntervals(float VV0, float VV1, float VV2,
                              float D0, float D1, float D2, float D0D1, float D0D2,
                              ref float A, ref float B, ref float C, ref float X0, ref float X1)
    {
        if (D0D1 > 0.0f)
        {
            // here we know that D0D2<=0.0 
            // that is D0, D1 are on the same side, D2 on the other or on the plane 
            A = VV2; B = (VV0 - VV2) * D2; C = (VV1 - VV2) * D2; X0 = D2 - D0; X1 = D2 - D1;
        }
        else if (D0D2 > 0.0f)
        {
            // here we know that d0d1<=0.0 
            A = VV1; B = (VV0 - VV1) * D1; C = (VV2 - VV1) * D1; X0 = D1 - D0; X1 = D1 - D2;
        }
        else if (D1 * D2 > 0.0f || D0 != 0.0f)
        {
            // here we know that d0d1<=0.0 or that D0!=0.0 
            A = VV0; B = (VV1 - VV0) * D0; C = (VV2 - VV0) * D0; X0 = D0 - D1; X1 = D0 - D2;
        }
        else if (D1 != 0.0f)
        {
            A = VV1; B = (VV0 - VV1) * D1; C = (VV2 - VV1) * D1; X0 = D1 - D0; X1 = D1 - D2;
        }
        else if (D2 != 0.0f)
        {
            A = VV2; B = (VV0 - VV2) * D2; C = (VV1 - VV2) * D2; X0 = D2 - D0; X1 = D2 - D1;
        }
        else
        {
            return true;
        }

        return false;
    }


    /// <summary>
    /// /////////////////
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="sphere_pos"></param>
    /// <param name="sphere_rad"></param>
    /// <param name="intersection_point"></param>
    /// <returns></returns>
    public static bool IsLineIntersectSphere(in Vector3 p1, in Vector3 p2, in Vector3 sphere_pos, in float sphere_rad, out Vector3 intersection_point)
    {
        intersection_point = UtilGeometry.GetClosestPointOnFiniteLine(p1, p2, sphere_pos);
        return Vector3.Distance(intersection_point, sphere_pos) <= sphere_rad;
    }

    //  public static bool IntersectRaySphere(
    //Vector3 ro, Vector3 rd, Vector3 center, float r,
    //out Vector3 nearest, out Vector3 entry, out Vector3 exit)
    //  {

    //      bool isApproxZero(float v) => System.Math.Abs(v) < 1E-6f;

    //      var o2c = center - ro;

    //      var dot = Vector3.Dot(rd, o2c);
    //      if (isApproxZero(dot)) dot = 0f;

    //      nearest = entry = exit = new Vector3(float.NaN, float.NaN, float.NaN);

    //      var flag = false;

    //      if (dot >= 0f)
    //      {
    //          var proj = ro + dot * rd;
    //          var c2p = proj - center;
    //          var o2p = proj - ro;

    //          nearest = center + r * c2p.normalized;
    //          var p2n = nearest - proj;

    //          var contact = Vector3.Dot(p2n, c2p);
    //          if (isApproxZero(contact)) contact = 0f;

    //          if (contact >= 0f)
    //          {
    //              var h = p2n.magnitude;
    //              if (contact == 0f && isApproxZero(h)) h = 0f;

    //              float a = (h > 0f) ? System.MathF.Sqrt(-h * (h - 2f * r)) : r;

    //              o2p.Normalize();

    //              var i1 = proj - a * o2p;
    //              var i2 = proj + a * o2p;

    //              if ((ro - i1).sqrMagnitude < (ro - i2).sqrMagnitude)
    //              {
    //                  entryPoint = i1; exitPoint = i2;
    //              }
    //              else
    //              {
    //                  entryPoint = i2; exitPoint = i1;
    //              }

    //              flag = true;
    //          }
    //      }

    //      return flag;
    //  }


    //  public static bool IsLineIntersectWithSphere(Vector3 p1, Vector3 p2, Vector3 center, float r, out Vector3 hitPoint)
    //  {
    //      var delta = p2 - p1;
    //      var sqlen = delta.sqrMagnitude;
    //      var dir = delta / System.MathF.Sqrt(sqlen);

    //      if (IntersectRaySphere(p1, dir, center, r, out _, out hitPoint, out _))
    //          return testDist(p1, hitPoint, sqlen);

    //      if (IntersectRaySphere(p2, -dir, center, r, out _, out hitPoint, out _))
    //          return testDist(p2, hitPoint, sqlen);

    //      bool testDist(Vector3 p, Vector3 h, float l) => (p - h).sqrMagnitude <= l;

    //      hitPoint = new Vector3(float.NaN, float.NaN, float.NaN);
    //      return false;
    //  }

    //public static Quaternion GetSlopeCollidersRotation(Vector3 rightDir, in Vector3 lhit, in Vector3 rhit)
    //{
    //    var upright = Vector3.Cross(rightDir, -(rhit - lhit).normalized);
    //    return Quaternion.LookRotation(Vector3.Cross(rightDir, upright));
    //}

    public static void GetBoundsOfRenderer(Renderer[] renderers, out Vector3 center, out Vector3 size)
    {
        //using (new PerformanceAnalyzer())
        {
            Renderer r;
            float last_magnitude = float.NegativeInfinity;
            float magnitude;
            center = Vector3.one;
            size = Vector3.one;

            float max_height = 0;

            for (int i = 0; i < renderers.Length; i++)
            {
                r = renderers[i];
                magnitude = r.bounds.size.magnitude;

                if (magnitude > last_magnitude)
                {
                    last_magnitude = magnitude;
                    center = r.bounds.center;
                    size = r.bounds.size;
                }

                if (r.bounds.size.y > max_height)
                {
                    max_height = r.bounds.size.y;
                }
            }

            size.y = max_height;
        }
    }

    public static Vector3 GetClosestPointOnFiniteLine(Vector3 line_start, Vector3 line_end, Vector3 point)
    {
        Vector3 line_direction = line_end - line_start;
        float line_length = line_direction.magnitude;
        line_direction.Normalize();
        float project_length = Mathf.Clamp(Vector3.Dot(point - line_start, line_direction), 0f, line_length);
        return line_start + line_direction * project_length;
    }

    public static bool IsLineIntersects(Vector3 linePoint1,
        Vector3 lineVec1, Vector3 linePoint2, Vector3 lineVec2, out Vector3 intersection)
    {

        Vector3 lineVec3 = linePoint2 - linePoint1;
        Vector3 crossVec1and2 = Vector3.Cross(lineVec1, lineVec2);
        Vector3 crossVec3and2 = Vector3.Cross(lineVec3, lineVec2);

        float planarFactor = Vector3.Dot(lineVec3, crossVec1and2);

        //is coplanar, and not parallel
        if (Mathf.Abs(planarFactor) < 0.0001f
                && crossVec1and2.sqrMagnitude > 0.0001f)
        {
            float s = Vector3.Dot(crossVec3and2, crossVec1and2)
                    / crossVec1and2.sqrMagnitude;
            intersection = linePoint1 + (lineVec1 * s);
            return true;
        }
        else
        {
            intersection = Vector3.zero;
            return false;
        }
    }

    public static Vector3 GetPointInWorldCoordsByReference(Vector3 pos, Vector3 forward_dir, Vector3 reference_pos, float dist)
    {
        var new_pos = pos + (reference_pos * dist);
        var angle_of_rotation = Quaternion.FromToRotation(Vector3.forward, forward_dir).eulerAngles;
        return RotatePointAroundPivot(new_pos, pos, angle_of_rotation);
    }

    public static Vector3 FindCenter(List<Vector3> transforms)
    {
        var bound = new Bounds(transforms[0], Vector3.zero);
        for (int i = 1; i < transforms.Count; i++)
        {
            bound.Encapsulate(transforms[i]);
        }
        return bound.center;
    }

    public static Vector3 FindCenter(params Vector3[] transforms)
    {
        var bound = new Bounds(transforms[0], Vector3.zero);
        for (int i = 1; i < transforms.Length; i++)
        {
            bound.Encapsulate(transforms[i]);
        }
        return bound.center;
    }

    public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        var dir = point - pivot; // get point direction relative to pivot
        dir = Quaternion.Euler(angles) * dir; // rotate it
        point = dir + pivot; // calculate rotated point
        return point; // return it
    }
}
