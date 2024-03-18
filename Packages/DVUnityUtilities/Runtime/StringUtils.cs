using System.Linq;
using System.Text.RegularExpressions;

namespace DVUnityUtilities
{
    public static class StringUtils
    {
        /// <summary>
        /// [TAG] Name Name(Clone) ==> Name Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string FromSceneNameToObjectName(string name)
        {
            var local_name = Regex.Replace(name, @"\((?:\d+|Clone)\)", string.Empty).Trim();
            var s = local_name.Split(' '); // [I] Name ==> Name
            if (s.Length >= 2) return string.Join(" ", s.Skip(1));
            else return s[0];
        }
    }
}
