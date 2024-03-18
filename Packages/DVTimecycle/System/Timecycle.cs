using Packages.DVTimecycle.Helpers;
using Packages.DVTimecycle.Weather;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Packages.DVTimecycle.System
{
    [Serializable]
    public class Timecycle
    {
        public List<TimecycleData> Data = new();

        public int Hour;
        public int Minute;

        public WeatherSystem WeatherSystem;
        //public bool LerpWeather; // todo: LerpWeather

        public void SetTime(int hour, int minute)
        {
            Hour = hour;
            Minute = minute;

            UpdateWeatherVisualization();
        }

        public void UpdateWeatherVisualization()
        {
            var timecycleToDisplay = Data.FirstOrDefault(x => x.Hour == Hour && x.Minute == Minute);
            timecycleToDisplay ??= TimecycleHelper.GetNearestTimecycle(Data, Hour, Minute);

            WeatherSystem.SetWeather(timecycleToDisplay.Weather);
        }
    }
}