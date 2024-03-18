using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DVUnityUtilities.Other.StateMachines
{
    public static class DefaultStateMachineManager
    {
        private static List<DefaultStateMachine> _cache = new();

        public static void AddToCache(DefaultStateMachine sm)
        {
            if (_cache.Contains(sm) || IsHaveStateMachine(sm.Name, out _))
            {
                Debug.LogWarning($"{sm.Name} already registered");
                return;
            }
            _cache.Add(sm);
        }

        public static void RemoveFromCache(string name)
        {
            if (IsHaveStateMachine(name, out var sm))
            {
                RemoveFromCache(sm);
            }
        }

        public static void RemoveFromCache(DefaultStateMachine sm)
        {
            _cache.Remove(sm);
        }

        public static bool IsHaveStateMachine(string name, out DefaultStateMachine sm)
        {
            sm = _cache.FirstOrDefault(x => x.Name == name);
            return sm != null;
        }
    }
}