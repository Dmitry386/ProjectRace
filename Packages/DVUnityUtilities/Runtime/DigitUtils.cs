namespace DVUnityUtilities
{
    public static class DigitUtils
    {
        public static bool IsPositive(double val)
        {
            return val > 0;
        }

        public static bool IsNegative(double val)
        {
            return val < 0;
        }

        public static bool SameSign(double val1, double val2)
        {
            if (val1 == 0 && val2 == 0) return true;

            if (IsNegative(val1) && IsNegative(val2)) return true;
            if (IsPositive(val1) && IsPositive(val2)) return true;
            return false;
        }

        /// <summary>
        /// return -1 if negative, +1 if positive, 0 if 0
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int GetSign(double val)
        {
            if (IsNegative(val)) return -1;
            if (IsPositive(val)) return 1;
            return 0;
        }
    }
}