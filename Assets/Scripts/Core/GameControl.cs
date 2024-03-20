using Assets.Scripts.Core.Networking;
using Assets.Scripts.Core.Saving;
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
        [Inject] private SaveSystem _saveSystem;
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

        public void ShowSettingsDialog()
        {
            new MessageDialog("Settings menu", "NOT IMPLEMENTED SETTINGS MENU", "Bad", "Very bad", null).Show();
        }

        public void ShowDonateDialog()
        {
            var dial = new ListDialog("Donate (DEMO, ONLY FOR TESTS)");
            dial.MultiSelection = false;
            dial.AddValues(new string[] { "100 000 $", "200 000 $", "300 000 $" }).Show().OnResponse += DonateSelected;
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        private void DonateSelected(DialogResponseEventArgs obj)
        {
            if (obj.DialogButton == 0)
            {
                if (_saveSystem.Load(out var save))
                {
                    save.TryGiveMoney(100000 * (obj.SelectedItems[0] + 1));
                }
            }
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