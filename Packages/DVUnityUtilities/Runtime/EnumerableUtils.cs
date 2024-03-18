using System.Collections.Generic;
using System.Linq;

namespace DVUnityUtilities.Runtime
{
    public static class EnumerableUtils
    {
        public static bool IsValidIndex<T>(this IEnumerable<T> arr, int id)
        {
            return id >= 0 && id < arr.Count();
        }
    }
}