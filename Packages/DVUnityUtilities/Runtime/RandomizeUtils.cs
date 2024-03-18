using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DVUnityUtilities
{
    public static class RandomizeUtils
    {
        public static Vector3 RandomAxisValue(this Vector3 axis, float min = 0, float max = 360)
        {
            return Axis(axis, min, max);
        }

        public static Vector3 Axis(Vector3 axis, float min = 0, float max = 360)
        {
            axis.x *= RandomizeUtils.Range(min, max);
            axis.y *= RandomizeUtils.Range(min, max);
            axis.z *= RandomizeUtils.Range(min, max);

            return axis;
        }

        public static T GetRandomElement<T>(this T[] objects)
        {
            if (objects.Length == 0) return default;
            return objects[RandomizeUtils.Range(0, objects.Length)];
        }

        public static T GetRandomElement<T>(this IEnumerable<T> objects)
        {
            if (objects.Count() == 0) return default;
            return objects.ElementAt(RandomizeUtils.Range(0, objects.Count()));
        }

        public static T GetRandomElement<T>(this List<T> objects)
        {
            if (objects.Count == 0) return default;
            return objects[RandomizeUtils.Range(0, objects.Count)];
        }

        public static T GetRandom<T>(List<T> objects)
        {
            if (objects.Count == 0) return default;
            return objects[RandomizeUtils.Range(0, objects.Count)];
        }

        public static List<T> Get_N_Random_Elements<T>(this IEnumerable<T> objs, int count)
        {
            return objs.OrderBy(x => Random.value).Take(count).ToList();
        }

        public static int Range(int min, int max)
        {
            return Random.Range(min, max);
        }

        public static float Range(float min, float max)
        {
            double val = Random.value * (max - min) + min;
            return (float)val;
        }

        public static bool Chance(in float chance)
        {
            return Range(0f, 100f) <= chance;
        }
    }
}