using DVUnityUtilities.Runtime;
using Packages.DVVehicle.Entities.Vehicles;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Core.Other
{
    internal class VehicleSwitcher : MonoBehaviour
    {
        public UnityEvent<VehicleSwitcher> OnVehicleChanged;

        [SerializeField] private Transform _spawnVehicleParent;

        private VehicleEntity[] _allGameVehiclesPrefabs;
        private VehicleEntity _selected;

        private void Start()
        {
            _allGameVehiclesPrefabs = Resources.LoadAll<VehicleEntity>(string.Empty);
            SetVehicle(0);
        }

        public void SetVehicle(int id)
        {
            if (_allGameVehiclesPrefabs.IsValidIndex(id))
            {
                SetSelected(_allGameVehiclesPrefabs[id]);
            }
            else
            {
                SetSelected(null);
            }
        }

        public void SetSelected(VehicleEntity entity)
        {
            var last_veh = _selected;

            if (_selected)
            {
                GameObject.Destroy(_selected.gameObject);
                _selected = null;
            }

            if (entity)
            {
                _selected = GameObject.Instantiate(entity);
                _selected.transform.SetParent(_spawnVehicleParent, true);
                _selected.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            }

            if (last_veh != _selected) OnVehicleChanged?.Invoke(this);
        }

        public void SwitchVehicle(int side)
        {
            var newId = (GetSelectedVehicleId() + side) % _allGameVehiclesPrefabs.Length;
            SetVehicle(newId);
        }

        public int GetSelectedVehicleId()
        {
            if (!_selected) return -1;
            return Array.IndexOf(_allGameVehiclesPrefabs, _selected);
        }

        public VehicleEntity GetSelectedVehicleInstance()
        {
            return _selected;
        }
    }
}