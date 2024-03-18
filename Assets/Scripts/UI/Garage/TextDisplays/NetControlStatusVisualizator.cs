using Assets.Scripts.Core.Networking;
using DVUnityUtilities.Other.Coroutines;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI.Garage.TextDisplays
{
    internal class NetControlStatusVisualizator : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textMesh;
        [SerializeField] private float _updateInterval = 0.5f;
        [Inject] private INetworkControl _netControl;

        private void OnEnable()
        {
            new CoroutineTimer(this, _updateInterval, true).Start().OnTick += OnTick;
            OnTick(null);
        }

        private void OnTick(CoroutineTimer obj)
        {
            _textMesh.text = $"NetStatus: {_netControl.GetNetworkStatus()}\n" +
                $"Address: {_netControl.GetCurrentAddress()}";
        }
    }
}