using Packages.DVTimecycle.System;
using System.Collections.Generic;

namespace Packages.DVTimecycle.Helpers
{
    internal static class TimecycleHelper
    {
        public static TimecycleData GetNearestTimecycle(List<TimecycleData> data, int hour, int min)
        {
            if (data.Count == 0) return null;
            TimecycleData nearest = null;

            foreach (var dataObj in data)
            {
                if (data == null
                    || (dataObj.Hour > nearest.Hour && dataObj.Minute > nearest.Minute
                     && dataObj.Hour <= hour && dataObj.Minute <= min))
                {
                    nearest = dataObj;
                }
            }

            return nearest;
        }
    }
}