using Assets.Scripts.Core.Networking;
using Assets.Scripts.World.Locations;
using Packages.DVMessageBoxes.Source.Dialogs;
using Packages.DVMessageBoxes.Source.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Core
{
    internal class GameControl : MonoBehaviour
    {
        [SerializeField] private List<Location> _locations = new();
        [Inject] private INetworkControl _networkControl;
        [Inject] private LocationSystem _locationSystem;

        [Inject] private DiContainer _container;

        private void Awake()
        {
            _locationSystem.InstantiateMethod = InstantiateLocationMethod;
        }

        private Location InstantiateLocationMethod(Location location)
        { 
            return _container.InstantiatePrefabForComponent<Location>(location);
        }

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

        public void ShowMapSelectionDialog()
        {
            var dial = new ListDialog("Maps");
            dial.MultiSelection = false;
            dial.AddValues(GetMapNames()).Show().OnResponse += GameControl_OnResponse;
        }

        private IEnumerable<string> GetMapNames()
        {
            var arr = _locations.Select(x => x.ToString());
            return arr;
        }

        private void GameControl_OnResponse(DialogResponseEventArgs obj)
        {
            if (obj.DialogButton == 0)
            {
                try
                {
                    _locationSystem.SetLocation(_locations[obj.SelectedItems[0]]);
                }
                catch (Exception ex)
                {
                    new MessageDialog("Error", ex.Message, "Ok", null, null).Show();
                    Debug.LogException(ex);
                }
            }
        }
    }
}