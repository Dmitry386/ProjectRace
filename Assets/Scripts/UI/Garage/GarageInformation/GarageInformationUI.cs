using Assets.Scripts.Core.Other;
using Assets.Scripts.Core.Saving;
using Packages.DVVehicle.Entities.Vehicles;
using System.Linq;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI.Garage.GarageInformation
{
    internal class GarageInformationUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _playerName;
        [SerializeField] private TextMeshProUGUI _vehicleName;
        [SerializeField] private TextMeshProUGUI _vehicleDetails;

        [Inject] private SaveSystem _saveSystem;
        [Inject] private VehicleSwitcher _vehSwitcher;

        private void Awake()
        {
            _vehSwitcher.OnVehicleChanged.AddListener(OnVehicleChanged);
            UpdateInfos();
        }

        private void OnVehicleChanged(VehicleSwitcher arg0)
        {
            UpdateInfos();
        }

        private void UpdateInfos()
        {
            if (_saveSystem.Load(out var save))
            {
                var veh = _vehSwitcher.GetSelectedVehiclePrefab();

                _playerName.text = save.PlayerName;
                _vehicleName.text = veh?.ToString();
                _vehicleDetails.text = GetVehicleDetails(veh);
            }
        }

        private static string GetVehicleDetails(VehicleEntity veh)
        {
            if (!veh) return string.Empty;

            string res =
                $"InitialDriveForce: {veh.Handling.InitialDriveForce} H\n" +
                $"SteeringLock: {veh.Handling.SteeringLock} H\n" +
                $"HandBrakeForce: {veh.Handling.HandBrakeForce} H\n" +
                $"Mass: {veh.Handling.Mass} kg\n" +
                $"CenterOfMass: {veh.Handling.CenterOfMass}\n" +
                $"WheelsBack: {veh.Parts.WheelsBack.Count}\n" +
                $"WheelsFront: {veh.Parts.WheelsFront.Count}\n" +
                $"AttachPositions: {string.Join(',', veh.Parts.AttachPositions.Select(x => x.GetAttachPositionName()))}\n";

            return res;
        }
    }
}