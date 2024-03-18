using System;

namespace DVUnityUtilities
{
    public static class CastUtils
    {
        public static bool IsCanBeParseToType(this string input, System.Type t)
        {
            try
            {
                System.Convert.ChangeType(input, t);
                return true;
            }
            catch
            {
            }

            return false;
        }

        public static bool IsCanBeParseToType(this char input, System.Type t)
        {
            try
            {
                System.Convert.ChangeType(input.ToString(), t);
                return true;
            }
            catch
            {
            }

            return false;
        }

        public static T CastTo<T>(this Object o, out T casted)
        {
            casted = (T)o;
            return casted;
        }

        public static T CastTo<T>(this Object o)
        {
            return (T)o;
        }

        public static bool IsType<T>(this object o, out T obj) where T : class
        {
            if (o is T t)
            {
                obj = t;
                return true;
            }
            obj = null;
            return false;
        }

        public static bool OneZeroToBoolean(string command_text)
        {
            if (command_text == "1")
            {
                return true;
            }
            else if (command_text == "0")
            {
                return false;
            }
            else
            {
                throw new InvalidCastException($"{command_text} can't be cast to boolean.");
            }
        }
    }
}