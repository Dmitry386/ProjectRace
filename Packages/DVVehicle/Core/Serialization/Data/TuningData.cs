using System;
using System.Collections.Generic;

namespace Packages.DVVehicle.Core.Serialization.Data
{
    [Serializable]
    public class TuningData
    {
        public string PaintJob;
        public List<string> Attaches = new();
    }
}