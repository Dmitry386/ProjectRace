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

        private VehicleEntity _spawnedPrefab;
        private VehicleEntity _activePrefab;

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
            var last_veh = _spawnedPrefab;

            if (_spawnedPrefab)
            {
                GameObject.Destroy(_spawnedPrefab.gameObject);
                _spawnedPrefab = null;
                _activePrefab = null;
            }

            if (entity)
            {
                _spawnedPrefab = GameObject.Instantiate(entity);
                _spawnedPrefab.transform.SetParent(_spawnVehicleParent, true);
                _spawnedPrefab.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                _activePrefab = entity;
            }

            if (last_veh != _spawnedPrefab) OnVehicleChanged?.Invoke(this);
        }

        public void SwitchVehicle(int side)
        {
            var newId = (GetSelectedVehicleId() + side) % _allGameVehiclesPrefabs.Length;
            SetVehicle(newId);
        }

        public int GetSelectedVehicleId()
        {
            if (!_activePrefab) return -1;
            return Array.IndexOf(_allGameVehiclesPrefabs, _activePrefab);
        }

        public VehicleEntity GetSelectedVehiclePrefab()
        {
            return _activePrefab;
        }
    }
}