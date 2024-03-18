namespace DVUnityUtilities
{
    public class PercentUtils
    {
        /// <summary>
        /// returns 0 - 100%
        /// </summary>
        /// <param name="value"></param>
        /// <param name="ofValue"></param>
        /// <returns></returns>
        public static float HowManyPercentValueOfValue(float value, float ofValue)
        {
            return (100 * value) / ofValue;
        }

        /// <summary>
        /// returns 0.0 - 1.0
        /// </summary>
        /// <param name="value"></param>
        /// <param name="ofValue"></param>
        /// <returns></returns>
        public static float HowManyPercentValueOfValue01(float value, float ofValue)
        {
            return HowManyPercentValueOfValue(value, ofValue) / 100f;
        }
    }
}