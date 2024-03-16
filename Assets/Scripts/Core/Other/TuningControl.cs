using Assets.Scripts.Core.Saving;
using Assets.Scripts.Entities.Buyable;
using Packages.DVVehicle.Entities.Paintjobs;
using Packages.DVVehicle.Entities.Vehicles;
using Packages.DVVehicle.Helpers;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Core.Other
{
    internal class TuningControl : MonoBehaviour
    {
        [Inject] private SaveSystem _saveSystem;

        public void TrySetOrBuyTuning(VehicleEntity veh, BuyableObjectData obj)
        {
            if (_saveSystem.Load(out var saveData))
            {
                if (saveData.IsBuyed(obj.Name))
                {
                    if (saveData.IsHaveInGameMoney(obj.InGamePrice) && saveData.IsHaveRealMoney(obj.RealPrice))
                    {
                        saveData.AddBuyedObject(obj.Name);
                    }
                }
                else
                {
                    if (obj.ObjectType == "Attach")
                    {
                        if (VehicleHelper.IsAttachedObject(veh, obj.Name, out var attachedObjectPosition))
                        {
                            attachedObjectPosition.SetObject(null);
                        }
                        else if (VehicleHelper.IsHaveAttachPrefabInContainer(veh, obj.Name, out var vehicleAttachObjectData))
                        {
                            VehicleHelper.TryInstallAttach(veh, vehicleAttachObjectData, out _);
                        }
                    }
                    else if(obj.ObjectType == "Paintjob")
                    {
                        if (VehicleHelper.IsPaintjobApplyed(veh, obj.Name, out VehiclePaintjobApplyable paintjobPlace))
                        {
                            paintjobPlace.SetPaintjob(null);
                        }
                        else if (VehicleHelper.IsHavePaintjobInContainer(veh, obj.Name, out PaintjobData paintjobData))
                        {
                            VehicleHelper.TryInstallPaintjob(veh, paintjobData, out _);
                        }
                    }
                }
            }
        }
    }
}