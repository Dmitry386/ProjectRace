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

        [SerializeField] private string _realMoneyFormat = "{0} DONATE";
        [SerializeField] private string _inGameMoneyFormat = "{0} $";

        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _realMoneyPrice;
        [SerializeField] private TextMeshProUGUI _inGameMoneyPrice;

        [SerializeField] private Button _button;
        private BuyableObjectData _data;

        private void Awake()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            OnClicked?.Invoke(this, _data);
        }

        public void SetBuyableObject(BuyableObjectData obj, bool isBuyed)
        {
            _data = obj;

            _realMoneyPrice.text = string.Empty;
            _inGameMoneyPrice.text = string.Empty;
            _name.text = obj.Name;

            if (!isBuyed)
            {
                _realMoneyPrice.text = string.Format(_realMoneyFormat, obj.RealPrice.ToString());
                _inGameMoneyPrice.text = string.Format(_inGameMoneyFormat, obj.InGamePrice.ToString());
            }
        }

        private void OnDestroy()
        {
            OnClicked = null;
        }
    }
}