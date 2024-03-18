using Assets.Scripts.Core.Networking;
using Assets.Scripts.Core.Networking.Definitions;
using DVUnityUtilities;
using DVUnityUtilities.Other.Coroutines;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI.Garage.PlayerList
{
    internal class PlayerListUI : MonoBehaviour
    {
        [SerializeField] private PlayerInfoCellUI _templateCell;
        [SerializeField] private float _updateInterval = 1f;
        [Inject] private INetworkControl _netControl;

        private List<PlayerInfoCellUI> _activeCells = new();

        private void OnEnable()
        {
            _templateCell.gameObject.SetActive(false);
            new CoroutineTimer(this, _updateInterval, true).Start().OnTick += OnTick;
        }

        private void OnTick(CoroutineTimer obj)
        {
            UpdatePlayerList();
        }

        private void UpdatePlayerList()
        {
            if (_netControl.GetNetworkStatus() == NetworkStatus.None)
            {
                SetPlayerList(null);
            }
            else
            {
                SetPlayerList(_netControl.GetPlayers());
            }
        }

        private void SetPlayerList(NetworkPlayerInfo[] networkPlayerInfos)
        {
            ClearList();

            if (networkPlayerInfos != null)
            {
                foreach (NetworkPlayerInfo player in networkPlayerInfos)
                {
                    CreatePlayerCell(player);
                }
            }
        }

        private void CreatePlayerCell(NetworkPlayerInfo player)
        {
            var cell = Instantiate(_templateCell);

            cell.SetData(player, _netControl, _netControl.GetNetworkStatus() == NetworkStatus.Host);
            cell.transform.SetParent(_templateCell.transform.parent, false);
            cell.gameObject.SetActive(true);
            cell.OnKickClicked += OnKickRequested;

            _activeCells.Add(cell);
        }

        private void OnKickRequested(NetworkPlayerInfo info)
        {
            _netControl.Kick(info.Name);
        }

        private void ClearList()
        {
            _activeCells.DestroyGameObjectsAndClear();
        }
    }
}