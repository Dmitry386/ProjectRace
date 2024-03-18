using UnityEngine;

namespace DVUnityUtilities.Other.Cooldowners
{
    public class Cooldown
    {
        private float _lastUseTime;

        public void UpdateLastUseTime()
        {
            _lastUseTime = Time.timeSinceLevelLoad;
        }

        /// <summary>
        /// Time in seconds
        /// </summary>
        /// <param name="time_in_seconds"></param>
        /// <returns></returns>
        public bool IsTimeOver(float time_in_seconds)
        {
            return _lastUseTime + time_in_seconds <= Time.timeSinceLevelLoad;
        }
    }
}