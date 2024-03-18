using Packages.DVVehicle.Core.Serialization.Data;
using Packages.DVVehicle.Entities.Vehicles;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Packages.DVVehicle.Core.Serialization
{
    public static class VehicleSerialization
    {
        public static VehicleSaveData GetDataFromVehicle(VehicleEntity veh)
        {
            throw new NotImplementedException();
        }

        public static void ApplyDataToVehicle(VehicleEntity veh, VehicleSaveData data)
        {
            ApplyTransforms(veh, data.Transforms.Position, data.Transforms.Rotation);
            ApplyWheelRotations(veh, data.Transforms.FrontWheelRotation, data.Transforms.BackWheelRotation);
            ApplyTuning(veh, data.Tuning);
        }

        // -------------------------

        public static void ApplyTuning(VehicleEntity veh, TuningData tuning)
        {
            ApplyPaintjobs(veh, tuning.PaintJob);
            ApplyAttaches(veh, tuning.Attaches);
        }

        public static void ApplyAttaches(VehicleEntity veh, List<string> attaches)
        {
            throw new NotImplementedException();
        }

        public static void ApplyPaintjobs(VehicleEntity veh, string paintJob)
        {
            throw new NotImplementedException();
        }

        public static void ApplyTransforms(VehicleEntity veh, Vector3 pos, Quaternion rot)
        {
            veh.transform.position = pos;
            veh.transform.rotation = rot;
        }

        public static void ApplyWheelRotations(VehicleEntity veh, List<Quaternion> frontWheels, List<Quaternion> backWheels)
        {
            for (int i = 0; i < veh.Parts.WheelsFront.Count; i++)
            {
                veh.Parts.WheelsFront[i].SetRotation(frontWheels[i]);
            }

            for (int i = 0; i < veh.Parts.WheelsBack.Count; i++)
            {
                veh.Parts.WheelsBack[i].SetRotation(backWheels[i]);
            }
        }
    }
}