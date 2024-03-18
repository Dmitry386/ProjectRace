using System;
using System.Collections.Generic;
using UnityEngine;

namespace DVUnityUtilities.Other.Pools
{
    [DefaultExecutionOrder(-1000)]
    public class WCache : MonoBehaviour
    {
        private static WCache _singleton;

        private Dictionary<Type, HashSet<object>> _registryCache = new();
        private bool _quitting;

        private void Awake()
        {
            if (_singleton != null)
            {
                Debug.Log($"Dublicate singleton {GetType()}");
                Destroy(this);
                return;
            }

            _singleton = this;
        }

        private void OnApplicationQuit()
        {
            _quitting = true;
        }

        private static bool IsSingletonAvailable()
        {
            return _singleton != null && !_singleton._quitting;
        }

        public static void Register(object obj)
        {
            if (!IsSingletonAvailable()) return;

            var hs = GetOrCreate(_singleton._registryCache, obj.GetType());
            hs.Add(obj);
        }

        public static void DeRegister(object obj)
        {
            if (!IsSingletonAvailable()) return;

            if (_singleton._registryCache.TryGetValue(obj.GetType(), out var value))
            {
                value.Remove(obj);
            }
        }

        public static void GetAll<T>(ref HashSet<T> set)
        {
            if (!IsSingletonAvailable()) return;

            set.Clear();
            var collection = GetOrCreate(_singleton._registryCache, typeof(T));

            foreach (T item in collection)
            {
                set.Add(item);
            }
        }

        public static HashSet<T> GetAll<T>()
        {
            if (!IsSingletonAvailable()) return new HashSet<T>();

            var hash = new HashSet<T>();
            GetAll<T>(ref hash);
            return hash;
        }

        public static TValue GetOrCreate<TKey, TValue>(IDictionary<TKey, TValue> dict, TKey key)
    where TValue : new()
        {
            if (!dict.TryGetValue(key, out TValue val))
            {
                val = new TValue();
                dict.Add(key, val);
            }

            return val;
        }

        private void OnDestroy()
        {
            _registryCache.Clear();

            if (_singleton == this)
            {
                _singleton = null;
            }
        }
    }
}