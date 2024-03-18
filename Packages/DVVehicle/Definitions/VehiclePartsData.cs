using Packages.DVVehicle.Entities.Paintjobs;
using Packages.DVVehicle.Entities.Parts;
using Packages.DVVehicle.Entities.Parts.Wheels;
using System;
using System.Collections.Generic;

namespace Packages.DVVehicle.Definitions
{
    [Serializable]
    public class VehiclePartsData
    {
        public List<WheelPart> WheelsFront = new();
        public List<WheelPart> WheelsBack = new();
        public List<VehicleAttachPosition> AttachPositions = new();
    }
}