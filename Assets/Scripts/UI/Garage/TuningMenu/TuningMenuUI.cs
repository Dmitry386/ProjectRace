using Assets.Scripts.Core.Other;
using Assets.Scripts.Core.Saving;
using Assets.Scripts.Core.Saving.Data;
using Assets.Scripts.Entities.Buyable;
using DVUnityUtilities;
using Packages.DVVehicle.Entities.Parts;
using Packages.DVVehicle.Entities.Vehicles;
using Packages.DVVehicle.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI.Garage.TuningMenu
{
    internal class TuningMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _sectionTemplate;
        [SerializeField] private BuyOrInstallButton _buyOrInstallButton;

        [Inject] private SaveSystem _saveSystem;
        [Inject] private BuySystem _buySystem;
        [Inject] private VehicleSwitcher _vehicleSwitcher;
        [Inject] private TuningControl _tuningControl;
        private PlayerSaveData _saveData;

        private List<string> _buyedTuning => _saveData.BuyedObjects;
        private List<GameObject> _buttons = new();
        private VehicleEntity _veh => _vehicleSwitcher.GetSelectedVehiclePrefab();

        private void Awake()
        {
            _sectionTemplate.gameObject.SetActive(false);
            _buyOrInstallButton.gameObject.SetActive(false);

            _vehicleSwitcher.OnVehicleChanged.AddListener(OnVehicleSelectionChanged);
        }

        private void OnEnable()
        {
            _saveSystem.Load(out _saveData);
            OpenMainPage();
        }

        private void OnVehicleSelectionChanged(VehicleSwitcher arg0)
        {
            OpenMainPage();
        }

        private Button CreateSectionButton(string objName, Action act)
        {
            var copy = Instantiate(_sectionTemplate);

            copy.name = objName;
            copy.GetComponentInChildren<TextMeshProUGUI>().text = objName;
            copy.transform.SetParent(_sectionTemplate.transform.parent, false);
            copy.gameObject.SetActive(true);
            copy.onClick.AddListener(() => act?.Invoke());

            _buttons.Add(copy.gameObject);
            return copy;
        }

        private BuyOrInstallButton CreateBuyableObjectButton(string buyableObjectName)
        {
            if (_buySystem.IsHaveInAssortiment(buyableObjectName, out var obj))
            {
                var copy = Instantiate(_buyOrInstallButton);

                copy.name = buyableObjectName;
                copy.SetBuyableObject(obj, _saveSystem);
                copy.transform.SetParent(_sectionTemplate.transform.parent, false);
                copy.gameObject.SetActive(true);

                copy.OnClicked += (arg1, arg2) => { _tuningControl.TrySetOrBuyTuning(_veh, arg2); };

                _buttons.Add(copy.gameObject);

                return copy;
            }
            return null;
        }

        private void ClearButtons()
        {
            _buttons.DestroyGameObjectsWithoutClear();
        }

        public void OpenMainPage()
        {
            ClearButtons();
            if (!_veh) return;

            if (VehicleHelper.IsHavePossiblePaintjobs(_veh, out var paintjobs))
            {
                CreateSectionButton("Paintjob", () => OpenMenuWithPaintjobs(paintjobs.Select(x => x.Name)));
            }

            if (VehicleHelper.IsHavePossibleAttaches(_veh, out var attaches))
            {
                foreach (var attachPosition in attaches.GroupBy(x => x.AttachPositionName))
                {
                    CreateSectionButton(attachPosition.Key, () => OpenMenuWithAttaches(attachPosition));
                }
            }
        }

        private void OpenMenuWithPaintjobs(IEnumerable<string> paintjobs)
        {
            OpenMenuWithBuyableObjectButtons(paintjobs.ToList());
        }

        private void OpenMenuWithAttaches(IEnumerable<VehicleAttachObject> attachPosition)
        {
            OpenMenuWithBuyableObjectButtons(attachPosition.Select(x => x.Name).ToList());
        }

        public void OpenMenuWithSectionButtons(List<string> names, List<Action> actions)
        {
            if (names.Count != actions.Count) throw new Exception($"{nameof(names)}.Count != {nameof(actions)}.Count");

            ClearButtons();

            for (int i = 0; i < names.Count; i++)
            {
                CreateSectionButton(names[i], actions[i]);
            }
        }

        public void OpenMenuWithBuyableObjectButtons(List<string> names)
        {
            ClearButtons();

            for (int i = 0; i < names.Count; i++)
            {
                CreateBuyableObjectButton(names[i]);
            }
        }
    }
}