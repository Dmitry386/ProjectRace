using System;
using System.Collections.Generic;
using UnityEngine;

namespace Packages.DVVehicle.Core.Serialization.Data
{
    [Serializable]
    public class VehicleTransformData
    {
        public Vector3 Position;
        public Quaternion Rotation;

        public List<Quaternion> FrontWheelRotation = new();
        public List<Quaternion> BackWheelRotation = new();
    }
}