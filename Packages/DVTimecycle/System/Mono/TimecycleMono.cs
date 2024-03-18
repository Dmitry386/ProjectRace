using UnityEngine;

namespace Packages.DVTimecycle.System.Mono
{
    internal class TimecycleMono : MonoBehaviour
    {
        [SerializeField] public Timecycle ActualTimecycle;

        public void SetHour(int hour)
        {
            ActualTimecycle.SetTime(hour, ActualTimecycle.Minute);
        }

        public void SetMinute(int minute)
        {
            ActualTimecycle.SetTime(minute, ActualTimecycle.Hour);
        }

        public void UpdateVisualization()
        {
            ActualTimecycle.UpdateWeatherVisualization();
        }
    }
}