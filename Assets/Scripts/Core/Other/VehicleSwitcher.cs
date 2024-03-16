using Assets.Scripts.Core.Saving;
using DVUnityUtilities.Runtime;
using Packages.DVVehicle.Entities.Vehicles;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Assets.Scripts.Core.Other
{
    internal class VehicleSwitcher : MonoBehaviour
    {
        public UnityEvent<VehicleSwitcher> OnVehicleChanged;

        [SerializeField] private Transform _spawnVehicleParent;
        [Inject] private SaveSystem _saveSystem;

        private VehicleEntity[] _allGameVehiclesPrefabs;
        private List<VehicleEntity> _buyedVehiclesPrefabs;

        private VehicleEntity _selected;

        private void Start()
        {
            _allGameVehiclesPrefabs = Resources.LoadAll<VehicleEntity>(string.Empty);
            UpdateBuyedVehicles();
            SetVehicle(0);
        }

        public void SetVehicle(int id)
        {
            if (_buyedVehiclesPrefabs.IsValidIndex(id))
            {
                SetSelected(_buyedVehiclesPrefabs[id]);
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
            var newId = (GetSelectedVehicleId() + side) % _buyedVehiclesPrefabs.Count;
            SetVehicle(newId);
        }

        public int GetSelectedVehicleId()
        {
            if (!_selected) return -1;
            return _buyedVehiclesPrefabs.IndexOf(_selected);
        }

        public VehicleEntity GetSelectedVehicleInstance()
        {
            return _selected;
        }

        private void UpdateBuyedVehicles()
        {
            if (_saveSystem.Load(out var currentSaveData))
            {
                _buyedVehiclesPrefabs = _allGameVehiclesPrefabs.Where(x => currentSaveData.IsBuyed(x.ToString())).ToList();
            }
        }
    }
}