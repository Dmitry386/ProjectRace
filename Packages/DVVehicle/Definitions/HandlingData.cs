using System;
using UnityEngine;

namespace Packages.DVVehicle.Definitions
{
    [Serializable]
    public class HandlingData
    {
        [Tooltip("Mass of vehicle (kg)")]
        public float Mass = 1500;

        public float HandBrakeForce = 3000f;

        public float InitialDriveForce = 1000f;

        [Tooltip("Max angle of steering (degrees)")]
        [Range(0f, 180f)]
        public float SteeringLock = 45f;

        public Vector3 CenterOfMass;
    }
}