using Packages.DVVehicle.Core.Serialization.Data;
using Packages.DVVehicle.Entities.Vehicles;
using Packages.DVVehicle.Helpers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Packages.DVVehicle.Core.Serialization
{
    public static class VehicleSerialization
    {
        public static VehicleSaveData GetDataFromVehicle(VehicleEntity veh)
        {
            var data = new VehicleSaveData();
            data.VehicleName = veh.ToString();

            data.Transforms = new VehicleTransformData();
            data.Transforms.Position = veh.transform.position;
            data.Transforms.Rotation = veh.transform.rotation;

            // todo: sync wheels transforms

            data.Tuning = new();
            data.Tuning.PaintJob = VehicleHelper.IsHaveAnyPaintjob(veh, out var place, out var paintjob) ? paintjob.Name : string.Empty;
            data.Tuning.Attaches = VehicleHelper.IsHaveAnyAttaches(veh, out var datas, out _) ? datas.Select(x => x.Name).ToArray() : new string[] { };

            return data;
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

        public static void ApplyAttaches(VehicleEntity veh, IEnumerable<string> attaches)
        {
            foreach (var attach in attaches)
            {
                ApplyAttach(veh, attach);
            }
        }

        public static void ApplyAttach(VehicleEntity veh, string attachName)
        {
            if (VehicleHelper.IsHaveAttachPrefabInContainer(veh, attachName, out var attachData))
            {
                VehicleHelper.TryInstallAttach(veh, attachData, out _);
            }
        }

        public static void ApplyPaintjobs(VehicleEntity veh, string paintJobName)
        {
            if (VehicleHelper.IsHavePaintjobInContainer(veh, paintJobName, out var paintjobData))
            {
                VehicleHelper.TryInstallPaintjob(veh, paintjobData, out _);
            }
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