using Assets.Scripts.Core.Networking.Definitions;
using Assets.Scripts.Core.Saving;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Core.Networking.NetworkControllers
{
    internal class PhotonNetworkControl : MonoBehaviourPunCallbacks, INetworkControl
    {
        public NetworkStatus NetStatus { get; private set; }

        [SerializeField] private bool _autoStartHost = true;
        [Inject] private SaveSystem _saveSystem;

        private void Awake()
        {
            PhotonNetwork.NickName = GetNickName();
        }

        private void Start()
        {
            PhotonNetwork.LogLevel = PunLogLevel.Full;
            PhotonNetwork.ConnectUsingSettings();

            //if (_autoStartHost) StartHost();
        }

        public string GetNickName()
        {
            if (_saveSystem.Load(out var save))
            {
                return save.PlayerName;
            }
            else
            {
                return "NA";
            }
        }

        public void Connect(string address)
        {
            StopHost();
            PhotonNetwork.JoinRoom(address);
        }

        public void StartHost()
        {
            if (_saveSystem.Load(out var save))
            {
                if (PhotonNetwork.InRoom) Disconnect();
                PhotonNetwork.CreateRoom($"{save.PlayerName}");
            }
        }

        public void Disconnect()
        {
            try
            {
                PhotonNetwork.LeaveRoom();
            }
            catch (System.Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        public void Kick(string playerName)
        {
            if (IsHavePlayer(playerName, out var player))
            {
                try
                {
                    PhotonNetwork.CloseConnection(player.NetObject as Player);
                }
                catch (System.Exception ex)
                {
                    Debug.LogException(ex);
                }
            }
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.Log($"{newPlayer.NickName} connected");
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            Debug.Log($"{otherPlayer.NickName} disconnected");
        }

        public void StopHost()
        {
            try
            {
                PhotonNetwork.LeaveRoom();
            }
            catch (System.Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        public bool IsHavePlayer(string name, out NetworkPlayerInfo player)
        {
            var netObj = PhotonNetwork.PlayerList.FirstOrDefault(x => x.NickName == name);
            if (netObj != null)
            {
                player = new NetworkPlayerInfo() { Name = name, NetObject = netObj };
                return true;
            }
            else
            {
                player = null;
                return false;
            }
        }

        public NetworkPlayerInfo[] GetPlayers()
        {
            return PhotonNetwork.PlayerList.Select(x => new NetworkPlayerInfo() { Name = x.NickName, NetObject = x }).ToArray();
        }

        private void OnDestroy()
        {
            StopHost();
            Disconnect();
        }
    }
}