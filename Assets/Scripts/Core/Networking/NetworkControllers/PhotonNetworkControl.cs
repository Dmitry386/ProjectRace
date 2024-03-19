using Assets.Scripts.Core.Networking.Definitions;
using Assets.Scripts.Core.Saving;
using Packages.DVMessageBoxes.Source.Dialogs;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Core.Networking.NetworkControllers
{
    internal class PhotonNetworkControl : MonoBehaviourPunCallbacks, INetworkControl
    {
        [Inject] private SaveSystem _saveSystem;

        private void Start()
        {
            PhotonNetwork.NickName = GetNickName();
            PhotonNetwork.LogLevel = PunLogLevel.Full;
            PhotonNetwork.EnableCloseConnection = true;
            PhotonNetwork.ConnectUsingSettings();
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
            if (GetNetworkStatus() == NetworkStatus.None)
            {
                PhotonNetwork.JoinRoom(address);
            }
            else
            {
                new MessageDialog("Error", "Impossible to connect. Please close your room.", null, null, "Ok").Show();
            }
        }

        public void StartOrStopHost()
        {
            if (GetNetworkStatus() != NetworkStatus.None)
            {
                Disconnect();
            }
            else
            {
                StartHost();
            }
        }

        public void StartHost()
        {
            if (_saveSystem.Load(out var save))
            {
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

        public NetworkStatus GetNetworkStatus()
        {
            if (PhotonNetwork.IsMasterClient) return NetworkStatus.Host;
            else if (PhotonNetwork.InRoom) return NetworkStatus.Client;
            else return NetworkStatus.None;
        }

        public string GetCurrentAddress()
        {
            return PhotonNetwork.CurrentRoom?.Name;
        }

        public int GetCurrentPlayerPing()
        {
            return PhotonNetwork.GetPing();
        }

        public GameObject Instantiate(GameObject prefab, Vector3 pos, Quaternion rot)
        {
            return PhotonNetwork.Instantiate(prefab.name, pos, rot);
        }

        public void DestroyGameObject(GameObject gameObject)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}