using Packages.DVVehicle.Core.Serialization;
using Packages.DVVehicle.Core.Serialization.Data;
using Packages.DVVehicle.Entities.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Core.Saving.Data
{
    [Serializable]
    internal class PlayerSaveData
    {
        public string Name;

        public double InGameMoney;
        public double RealMoney;

        public List<string> BuyedObjects = new();
        public List<VehicleSaveData> VehiclesActualTuningInfo = new();

        public bool IsBuyed(string name)
        {
            return BuyedObjects.Contains(name);
        }

        public void AddBuyedObject(string name)
        {
            if (!IsBuyed(name))
            {
                BuyedObjects.Add(name);
            }
        }

        public void UpdateVehicleTuningInfoFromVehicleEntity(VehicleEntity veh)
        {
            var data = VehicleSerialization.GetDataFromVehicle(veh);

            if (IsHaveVehicleSaveInfo(veh.ToString(), out var saveData))
            {
                saveData.Tuning = data.Tuning;
            }
            else
            {
                VehiclesActualTuningInfo.Add(data);
            }
        }

        public bool IsHaveVehicleSaveInfo(string name, out VehicleSaveData saveData)
        {
            saveData = VehiclesActualTuningInfo.FirstOrDefault(x => x.VehicleName == name);
            return saveData != null;
        }

        public bool IsHaveRealMoney(double money)
        {
            return RealMoney >= money;
        }

        public bool IsHaveInGameMoney(double money)
        {
            return InGameMoney >= money;
        }
    }
}