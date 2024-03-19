using Assets.Scripts.Core.Networking.Definitions;
using Assets.Scripts.World.Locations;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Core.Networking.Sync
{
    [RequireComponent(typeof(PhotonView))]
    internal class SyncLocation : MonoBehaviourPunCallbacks
    {
        [Inject] private LocationSystem _locationSystem;
        [Inject] private INetworkControl _netControl;

        private PhotonView _view;
        private Location _lastSyncMap;

        private void Awake()
        {
            _view = GetComponent<PhotonView>();
        }

        private void Update()
        {
            UpdateCurrentLocationAndSync();
        }

        private void UpdateCurrentLocationAndSync()
        {
            if (_netControl.GetNetworkStatus() == NetworkStatus.Host)
            {
                var currentLocation = _locationSystem.GetCurrentLocation();

                if (_lastSyncMap != currentLocation)
                {
                    _lastSyncMap = currentLocation;
                    SyncMapForAllPlayers(_lastSyncMap?.ToString());
                }
            }
            else if (_netControl.GetNetworkStatus() == NetworkStatus.None)
            {
                if (_locationSystem.GetCurrentLocation() != null)
                {
                    _locationSystem.SetLocation(locationPrefab: null);
                }
            }
        }

        private void SyncMapForAllPlayers(string map)
        {
            var players = PhotonNetwork.PlayerList;

            for (int i = 0; i < players.Length; i++)
            {
                _view.RPC(nameof(SyncMapRPC), players[i], map);
            }
        }

        [PunRPC]
        private void SyncMapRPC(string mapName)
        {
            if (_netControl.GetNetworkStatus() == NetworkStatus.Client)
            {
                _locationSystem.SetLocation(mapName);
                Debug.Log($"Location synced to {mapName}");
            }
        }
    }
}