using Assets.Scripts.Core.Networking.Definitions;
using Assets.Scripts.Core.Other;
using Assets.Scripts.Entities.Players;
using Assets.Scripts.World.Locations;
using Cinemachine;
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

        private PlayerControllerEntity _localPlayer;
        private VehicleEntity _localVehicle;
        private int _nextSpawnPointId = 0;

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
            if (IsHaveFreeSpawnPoint(out var spawnPoint))
            {
                _localPlayer = SpawnObject(_playerControllerPrefab, spawnPoint);
                _localVehicle = SpawnObject(_vehicleSwitcher.GetSelectedVehiclePrefab(), spawnPoint);

                var data = VehicleSerialization.GetDataFromVehicle(_vehicleSwitcher.GetSelectedVehicleInstance());
                VehicleSerialization.ApplyTuning(_localVehicle, data.Tuning);

                _localPlayer.SetControllableVehicle(_localVehicle);
            }
        }

        private T SpawnObject<T>(T prefab, Transform spawnPoint) where T : Component
        {
            var obj = _networkControl.Instantiate(prefab.gameObject, spawnPoint.position, spawnPoint.rotation);
            _diContainer.InjectGameObject(obj);
            return obj.GetComponent<T>();
        }

        private bool IsHaveFreeSpawnPoint(out Transform spawnPoint)
        {
            if (_locationSystem.TryGetComponent<LocationSpawnPointsContainer>(out var container)
                && _nextSpawnPointId < container.SpawnPoints.Count)
            {
                spawnPoint = container.SpawnPoints[_nextSpawnPointId];
                _nextSpawnPointId++;
                return true;
            }
            else
            {
                spawnPoint = transform;
                return true; // todo: return false;
            }
        }

        private void OnDestroy()
        {
            _locationSystem.OnLocationChanged -= OnLocationChanged;
        }
    }
}