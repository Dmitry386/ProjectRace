using UnityEngine;

namespace DVUnityUtilities
{
    public static class ColorUtils
    {
        public static Color SetR(this Color c, float value)
        {
            c.r = value;
            return c;
        }

        public static Color SetG(this Color c, float value)
        {
            c.g = value;
            return c;
        }

        public static Color SetB(this Color c, float value)
        {
            c.b = value;
            return c;
        }

        public static Color SetA(this Color c, float value)
        {
            c.a = value;
            return c;
        }

        // ----------------------

        public static Color SetRefR(this ref Color c, float value)
        {
            c.r = value;
            return c;
        }

        public static Color SetRefG(this ref Color c, float value)
        {
            c.g = value;
            return c;
        }

        public static Color SetRefB(this ref Color c, float value)
        {
            c.b = value;
            return c;
        }

        public static Color SetRefA(this ref Color c, float value)
        {
            c.a = value;
            return c;
        }

        // ----------------------- 32

        public static Color32 SetR(this Color32 c, byte value)
        {
            c.r = value;
            return c;
        }

        public static Color32 SetG(this Color32 c, byte value)
        {
            c.g = value;
            return c;
        }

        public static Color32 SetB(this Color32 c, byte value)
        {
            c.b = value;
            return c;
        }

        public static Color32 SetA(this Color32 c, byte value)
        {
            c.a = value;
            return c;
        }

        // ----------------------

        public static Color32 SetRefR(this ref Color32 c, byte value)
        {
            c.r = value;
            return c;
        }

        public static Color32 SetRefG(this ref Color32 c, byte value)
        {
            c.g = value;
            return c;
        }

        public static Color32 SetRefB(this ref Color32 c, byte value)
        {
            c.b = value;
            return c;
        }

        public static Color32 SetRefA(this ref Color32 c, byte value)
        {
            c.a = value;
            return c;
        }
    }
}