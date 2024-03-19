using Assets.Scripts.Core.Other;
using Assets.Scripts.Core.Saving;
using Assets.Scripts.Entities.Buyable;
using Packages.DVVehicle.Entities.Vehicles;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI.Garage.BuyVehicleMenu
{
    internal class BuyVehicleMenuUI : MonoBehaviour
    {
        [SerializeField] private string _textFormat = "{0} $";
        [SerializeField] private Button _buyButton;

        [Inject] private VehicleSwitcher _vehicleSwitcher;
        [Inject] private BuySystem _buySystem;
        [Inject] private SaveSystem _saveSystem;

        private VehicleEntity _veh => _vehicleSwitcher.GetSelectedVehicleInstance();

        private void Awake()
        {
            _vehicleSwitcher.OnVehicleChanged.AddListener(OnVehicleChanged);
            _buyButton.onClick.AddListener(OnBuyButtonClicked);
        }

        private void OnVehicleChanged(VehicleSwitcher vehSwitcher)
        {
            UpdateVehicleVisualization();
        }
        private void UpdateVehicleVisualization()
        {
            string vehName = _veh.ToString();
            if (_saveSystem.Load(out var save) && save.IsBuyed(vehName))
            {
                _buyButton.gameObject.SetActive(false);
            }
            else
            {
                if (_buySystem.IsHaveInAssortiment(vehName, out var product))
                {
                    _buyButton.GetComponentInChildren<TextMeshProUGUI>().text = string.Format(_textFormat, product.InGamePrice);
                    _buyButton.gameObject.SetActive(true);
                }
            }
        }

        private void OnBuyButtonClicked()
        {
            if (_buySystem.IsHaveInAssortiment(_veh.ToString(), out var product))
            {
                _buySystem.TryBuy(product);
                UpdateVehicleVisualization();
            }
        }
    }
}