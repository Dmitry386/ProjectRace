using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DVUnityUtilities
{
    public static class ReflectionUtils
    {
        public static IEnumerable<Type> GetAllClassesThenInerhitOfClass<T>(Assembly ass)
        {
            return ass.GetTypes()
            .Where(t => t.IsSubclassOf(typeof(T)) && !t.IsAbstract);
        }

        public static bool IsHaveAttribute<T>(this System.Type f, out T attribute) where T : System.Attribute
        {
            attribute = f.GetCustomAttribute<T>();
            return attribute != null;
        }

        public static bool IsHaveAttribute<T>(this FieldInfo f, out T attribute) where T : System.Attribute
        {
            attribute = f.GetCustomAttribute<T>();
            return attribute != null;
        }

        public static bool IsHaveAttribute<T>(this MethodInfo f, out T attribute) where T : System.Attribute
        {
            attribute = f.GetCustomAttribute<T>();
            return attribute != null;
        }
    }
}