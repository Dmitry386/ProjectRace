using Assets.Scripts.Core.Other;
using Assets.Scripts.Entities.Players;
using Assets.Scripts.World.Locations;
using Cinemachine;
using Packages.DVVehicle.Entities.Vehicles;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Core.Networking.Functions
{
    [DefaultExecutionOrder(1000)]
    internal class NetworkPlayerSpawn : MonoBehaviourPunCallbacks
    {
        [SerializeField] private PlayerControllerEntity _playerControllerPrefab;
        [SerializeField] private CinemachineVirtualCamera _virtualCameraPrefab;

        [Inject] private INetworkControl _networkControl;
        [Inject] private LocationSystem _locationSystem;
        [Inject] private VehicleSwitcher _vehicleSwitcher;

        private PlayerControllerEntity _localPlayer;
        private VehicleEntity _localVehicle;

        private int _nextSpawnPointId = 0;

        private void Start()
        {
            Initialization();
        }

        private void Initialization()
        {
            // if (_networkControl.GetNetworkStatus() == Definitions.NetworkStatus.Host)
            {
                if (IsHaveFreeSpawnPoint(out var spawnPoint))
                {
                    _localPlayer = SpawnObject(_playerControllerPrefab, spawnPoint);
                    _localVehicle = SpawnObject(_vehicleSwitcher.GetSelectedVehiclePrefab(), spawnPoint);

                    _localPlayer.SetControllableVehicle(_localVehicle);
                }
            }
        }

        private T SpawnObject<T>(T prefab, Transform spawnPoint) where T : Component
        {
            var obj = _networkControl.Instantiate(prefab.gameObject, spawnPoint.position, spawnPoint.rotation);
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
            if (_localPlayer) _networkControl.DestroyGameObject(_localPlayer.gameObject);
            if (_localVehicle) _networkControl.DestroyGameObject(_localVehicle.gameObject);
        }
    }
}