using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DVUnityUtilities.Other.Coroutines
{
    /// <summary>
    /// Couroutine Extensions
    /// </summary>
    public static class CExt
    {
        public static IEnumerable<T> ForAllYield<T>(this IEnumerable<T> list, MonoBehaviour mono, Action<int> act)
        {
            new CoroutineFor(mono, act, 0, list.Count()).Start();
            return list;
        }

        public static IEnumerable<T> ForAllYield<T>(this IEnumerable<T> list, Action<int> act, int yieldEveryObjectCount = 1)
        {
            new CoroutineFor(act, 0, list.Count(), yieldEveryObjectCount).Start();
            return list;
        }

        public static IEnumerable<T> ForAllYield<T>(this IEnumerable<T> list, Action<int> act)
        {
            new CoroutineFor(act, 0, list.Count()).Start();
            return list;
        }

        public static IEnumerable<T> ForAllYield<T>(this IEnumerable<T> list, MonoBehaviour mono, int start, Action<int> act)
        {
            new CoroutineFor(mono, act, start, list.Count()).Start();
            return list;
        }

        public static IEnumerable<T> ForAllYield<T>(this IEnumerable<T> list, MonoBehaviour mono, int start, int end, Action<int> act)
        {
            new CoroutineFor(mono, act, start, end).Start();
            return list;
        }
    }
}