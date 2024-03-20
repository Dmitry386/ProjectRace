using Assets.Scripts.Core.Networking.Definitions;
using Assets.Scripts.Core.Other;
using Assets.Scripts.Core.Saving;
using Assets.Scripts.Entities.Players;
using Assets.Scripts.World.Locations;
using Cinemachine;
using DVUnityUtilities;
using Packages.DVMessageBoxes.Source.Dialogs;
using Packages.DVVehicle.Core.Serialization;
using Packages.DVVehicle.Entities.Vehicles;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Core.Networking.Sync
{
    internal class SyncPlayerSpawn : MonoBehaviourPunCallbacks
    {
        [SerializeField] private PlayerControllerEntity _playerControllerPrefab;
        [SerializeField] private CinemachineVirtualCamera _virtualCameraPrefab;

        [Inject] private VehicleSwitcher _vehicleSwitcher;
        [Inject] private INetworkControl _networkControl;
        [Inject] private LocationSystem _locationSystem;
        [Inject] private DiContainer _diContainer;
        [Inject] private SaveSystem _saveSystem;

        private PlayerControllerEntity _localPlayer;
        private VehicleEntity _localVehicle;

        private void Awake()
        {
            _locationSystem.OnLocationChanged += OnLocationChanged;
        }

        private void OnLocationChanged(Location loc)
        {
            if (!IsPlayerSpawned(out _, out _))
            {
                if (loc)
                {
                    SpawnPlayer();
                }
            }
            else
            {
                if (!loc || _networkControl.GetNetworkStatus() == NetworkStatus.None)
                {
                    DespawnPlayer();
                }
            }
        }

        public bool IsPlayerSpawned(out PlayerControllerEntity player, out VehicleEntity veh)
        {
            player = _localPlayer;
            veh = _localVehicle;
            return _localPlayer && _localVehicle;
        }

        private void DespawnPlayer()
        {
            if (_localPlayer) _networkControl.DestroyGameObject(_localPlayer.gameObject);
            if (_localVehicle) _networkControl.DestroyGameObject(_localVehicle.gameObject);
        }

        private void SpawnPlayer()
        {
            if (!TryResetVehicleToAnyIfNotBuyed()) return;

            if (IsHaveAnySpawnPoint(_locationSystem.GetCurrentLocation(), out var spawnPoint))
            {
                _localPlayer = SpawnObject(_playerControllerPrefab, spawnPoint);
                _localVehicle = SpawnObject(_vehicleSwitcher.GetSelectedVehiclePrefab(), spawnPoint);

                var data = VehicleSerialization.GetDataFromVehicle(_vehicleSwitcher.GetSelectedVehicleInstance());
                VehicleSerialization.ApplyTuning(_localVehicle, data.Tuning);

                _localPlayer.SetControllableVehicle(_localVehicle);
            }
        }

        private bool TryResetVehicleToAnyIfNotBuyed()
        {
            if (IsBuyedCurrentVehicle()) return true;

            if (IsHaveAnyBuyedVehicleId(out int id))
            {
                _vehicleSwitcher.SetVehicle(id);
                return true;
            }
            else
            {
                _networkControl?.Disconnect();
                new MessageDialog("Error", "You haven't any vehicle to spawn on locations...", "Ok", null, "Cancel").Show();
                return false;
            }
        }

        private bool IsBuyedCurrentVehicle()
        {
            if (_saveSystem.Load(out var save))
            {
                return save.IsBuyed(_vehicleSwitcher.GetSelectedVehiclePrefab()?.ToString());
            }
            return false;
        }

        public bool IsHaveAnyBuyedVehicleId(out int id)
        {
            var prefabs = _vehicleSwitcher.GetAllVehiclePrefabs();

            if (_saveSystem.Load(out var save))
            {
                for (int i = 0; i < prefabs.Length; i++)
                {
                    if (save.IsBuyed(prefabs[i].ToString()))
                    {
                        id = i;
                        return true;
                    }
                }
            }

            id = -1;
            return false;
        }

        private T SpawnObject<T>(T prefab, Transform spawnPoint) where T : Component
        {
            var obj = _networkControl.Instantiate(prefab.gameObject, spawnPoint.position, spawnPoint.rotation);
            _diContainer.InjectGameObject(obj);
            return obj.GetComponent<T>();
        }

        private bool IsHaveAnySpawnPoint(Location loc, out Transform spawnPoint)
        {
            if (loc && loc.TryGetComponent<LocationSpawnPointsContainer>(out var container))
            {
                spawnPoint = container.SpawnPoints.GetRandomElement();
                return true;
            }
            else
            {
                spawnPoint = null;
                return false;
            }
        }

        private void OnDestroy()
        {
            _locationSystem.OnLocationChanged -= OnLocationChanged;
        }
    }
}