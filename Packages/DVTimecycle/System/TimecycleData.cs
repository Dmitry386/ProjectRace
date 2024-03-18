using Packages.DVTimecycle.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packages.DVTimecycle.System
{
    [Serializable]
    public class TimecycleData
    {
        public int Hour;
        public int Minute;

        public WeatherData Weather;
    }
}