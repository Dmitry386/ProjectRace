using System;

namespace Packages.DVVehicle.Core.Serialization.Data
{
    [Serializable]
    public class VehicleSaveData
    {
        public string VehicleName;
        public VehicleTransformData Transforms;
        public TuningData Tuning;
    }
}