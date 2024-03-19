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
            _locationSystem.OnLocationChanged += OnLocationChanged;
        }

        private void OnLocationChanged(Location obj)
        {
            UpdateLocationSync();
        }

        private void UpdateLocationSync()
        {
            if (_netControl.GetNetworkStatus() == Definitions.NetworkStatus.Host)
            {
                var currentLocation = _locationSystem.GetCurrentLocation();

                if (_lastSyncMap != currentLocation)
                {
                    _lastSyncMap = currentLocation;
                    SyncMapForAllPlayers(_lastSyncMap?.ToString());
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
            if (_netControl.GetNetworkStatus() == Definitions.NetworkStatus.Client)
            {
                _locationSystem.SetLocation(mapName);
                Debug.Log($"Location synced to {mapName}");
            }
        }

        private void OnDestroy()
        {
            _locationSystem.OnLocationChanged -= OnLocationChanged;
        }
    }
}