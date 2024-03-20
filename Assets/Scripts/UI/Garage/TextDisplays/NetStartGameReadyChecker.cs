using Assets.Scripts.Core.Networking;
using Assets.Scripts.Core.Networking.Definitions;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI.Garage.TextDisplays
{
    internal class NetStartGameReadyChecker : MonoBehaviour
    {
        [SerializeField] private GameObject _controlStateOfObject;
        [Inject] private INetworkControl _networkControl;

        private void Update()
        {
            if (_networkControl.GetNetworkStatus() == NetworkStatus.Host)
            {
                _controlStateOfObject.gameObject.SetActive(true);
            }
            else
            {
                _controlStateOfObject.gameObject.SetActive(false);
            }
        }
    }
}