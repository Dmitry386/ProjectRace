using Assets.Scripts.Core.Saving;
using Assets.Scripts.Entities.Buyable;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Garage.TuningMenu
{
    internal class BuyOrInstallButton : MonoBehaviour
    {
        public event Action<BuyOrInstallButton, BuyableObjectData> OnClicked;

        [SerializeField] private string _inGameMoneyFormat = "{0} $";

        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _inGameMoneyPrice;

        [SerializeField] private Button _button;
        private BuyableObjectData _data;
        private SaveSystem _saveSystem;

        private void Awake()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            OnClicked?.Invoke(this, _data);
            SetBuyableObject(_data, _saveSystem);
        }

        public void SetBuyableObject(BuyableObjectData obj, SaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
            _data = obj;

            _inGameMoneyPrice.text = string.Empty;
            _name.text = obj.Name;

            if (saveSystem.Load(out var save) && !save.IsBuyed(obj.Name))
            {
                _inGameMoneyPrice.text = string.Format(_inGameMoneyFormat, obj.InGamePrice.ToString());
            }
        }

        private void OnDestroy()
        {
            OnClicked = null;
        }
    }
}