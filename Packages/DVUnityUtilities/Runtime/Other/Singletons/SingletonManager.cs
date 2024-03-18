using System;
using System.Collections.Generic;
using UnityEngine;

namespace DVUnityUtilities.Other.Singletons
{
    public class SingletonManager
    {
        private static List<object> _singletons = new();

        public static void Add(object s)
        {
            if (s == null) return;
            _singletons.Add(s);
        }

        public static void Remove(object s)
        {
            _singletons.Remove(s);
        }

        public static void DisposeAll()
        {
            while (_singletons.Count > 0)
            {
                var o = _singletons[0];
                if (o is IDisposable d)
                {
                    d.Dispose();
                }
                else if (o is UnityEngine.Object obj)
                {
                    GameObject.Destroy(obj);
                }
                Remove(o);
            }
        }
    }
}
