using Packages.DVVehicle.Entities.Paintjobs;
using Packages.DVVehicle.Entities.Parts;
using Packages.DVVehicle.Entities.Vehicles;
using System.Collections.Generic;
using System.Linq;

namespace Packages.DVVehicle.Helpers
{
    public static class VehicleHelper
    {
        public static bool TryInstallAttach(VehicleEntity veh, VehicleAttachObject objPrefab, out VehicleAttachPosition attachedToPosition)
        {
            if (IsHaveAttachPosition(veh, objPrefab.AttachPositionName, out attachedToPosition))
            {
                attachedToPosition.SetObject(objPrefab);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsAttachedObject(VehicleEntity veh, string attachName, out VehicleAttachPosition position)
        {
            position = veh.Parts.AttachPositions.FirstOrDefault(x => x.IsAttachedObject(out var data, out _) && data.Name == attachName);
            return position;
        }

        public static bool IsHaveAttachPrefabInContainer(VehicleEntity veh, string attachName, out VehicleAttachObject obj)
        {
            obj = null;

            if (IsHavePossibleAttaches(veh, out var attaches))
            {
                obj = attaches.FirstOrDefault(x => x.Name == attachName);
            }

            return obj != null;
        }

        public static bool IsHavePossibleAttaches(VehicleEntity veh, out List<VehicleAttachObject> possibleAttaches)
        {
            if (veh.TryGetComponent<VehicleAttachPrefabContainer>(out var comp))
            {
                possibleAttaches = comp.PossibleAttaches.Where(x => IsHaveAttachPosition(veh, x.AttachPositionName, out _)).ToList();
                return possibleAttaches.Count > 0;
            }
            else
            {
                possibleAttaches = null;
                return false;
            }
        }

        public static bool IsHavePossiblePaintjobs(VehicleEntity veh, out List<PaintjobData> possiblePaints)
        {
            if (veh.TryGetComponent<VehiclePaintjobApplyable>(out var paintjobApplyable))
            {
                possiblePaints = paintjobApplyable.AvailablePaintjobs;
                return paintjobApplyable.AvailablePaintjobs.Count > 0;
            }
            else
            {
                possiblePaints = null;
                return false;
            }
        }

        public static bool IsHaveAttachPosition(VehicleEntity veh, string attachPositionName, out VehicleAttachPosition vap)
        {
            vap = veh.Parts.AttachPositions.FirstOrDefault(x => x.GetAttachPositionName() == attachPositionName);
            return vap != null;
        }

        public static bool IsPaintjobApplyed(VehicleEntity veh, string paintjobName, out VehiclePaintjobApplyable paintjobPlace)
        {
            if (veh.TryGetComponent<VehiclePaintjobApplyable>(out paintjobPlace))
            {
                return paintjobPlace.IsHavePaintjob(out var data) && data.Name == paintjobName;
            }

            return false;
        }

        public static bool IsHavePaintjobInContainer(VehicleEntity veh, string paintjobName, out PaintjobData paintjobData)
        {
            paintjobData = null;

            if (IsHavePossiblePaintjobs(veh, out var paintjobs))
            {
                paintjobData = paintjobs.FirstOrDefault(x => x.Name == paintjobName);
            }

            return paintjobData != null;
        }

        public static bool TryInstallPaintjob(VehicleEntity veh, PaintjobData paintjobData, out VehiclePaintjobApplyable attachedTo)
        {
            if (veh.TryGetComponent<VehiclePaintjobApplyable>(out attachedTo))
            {
                attachedTo.SetPaintjob(paintjobData);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}