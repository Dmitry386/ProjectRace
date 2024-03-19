using Assets.Scripts.Core.Networking.Definitions;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Core.Networking.Helpers
{
    internal class NetworkStatusCallbacks : MonoBehaviour
    {
        [SerializeField] private NetworkStatus _invokeOn;
        [SerializeField] private List<Action> _actions = new();
        [Inject] private INetworkControl _netControl;

        private NetworkStatus _lastStatus;

        private void Update()
        {
            if (_netControl.GetNetworkStatus() != _lastStatus)
            {
                InvokeActions();
                _lastStatus = _netControl.GetNetworkStatus();
            }
        }

        private void InvokeActions()
        {
            if (_netControl.GetNetworkStatus() == _invokeOn)
            {
                _actions.ForEach(x => x.Invoke());
            }
        }
    }
}