using Assets.Scripts.Core.Networking;
using Assets.Scripts.Core.Networking.Definitions;
using Photon.Realtime;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Garage.PlayerList
{
    internal class PlayerInfoCellUI : MonoBehaviour
    {
        public event Action<NetworkPlayerInfo> OnKickClicked;

        [SerializeField] private TextMeshProUGUI _playerName;
        [SerializeField] private TextMeshProUGUI _ping;
        [SerializeField] private Button _kickButton;

        private NetworkPlayerInfo _netData;

        private void Awake()
        {
            _kickButton.onClick.AddListener(OnKick);
        }

        private void OnKick()
        {
            OnKickClicked?.Invoke(_netData);
        }

        public void SetData(NetworkPlayerInfo netData, INetworkControl netControl, bool canKickAnyone)
        {
            _netData = netData;
            _playerName.text = netData.Name;

            if (netData.NetObject is Player player)
            {
                _ping.text = netControl.GetCurrentPlayerPing().ToString(); // todo: НАЙДИ ПИНГ!!!        
                _kickButton.gameObject.SetActive(canKickAnyone);
            }
        }

        private void OnDestroy()
        {
            OnKickClicked = null;
        }
    }
}