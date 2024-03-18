using Assets.Scripts.Core.Networking;
using Packages.DVMessageBoxes.Source.Dialogs;
using Packages.DVMessageBoxes.Source.Events;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Core
{
    internal class GameControl : MonoBehaviour
    {
        [Inject] private INetworkControl _networkControl;

        public void ShowConnectDialog()
        {
            new InputDialog("Connect to", "Enter the username you want to connect to. It is indicated at the top right of the garage screen.").Show().OnResponse += OnInputedConnectAddress;
        }

        private void OnInputedConnectAddress(DialogResponseEventArgs obj)
        {
            if (obj.DialogButton == 0 && !string.IsNullOrEmpty(obj.InputText))
            {
                _networkControl.Connect(obj.InputText);
            }
        }
    }
}