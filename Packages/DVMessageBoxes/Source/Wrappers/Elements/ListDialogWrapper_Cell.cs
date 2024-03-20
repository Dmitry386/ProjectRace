using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Packages.DVMessageBoxes.Source.Wrappers.Elements
{
    [RequireComponent(typeof(Button))]
    internal class ListDialogWrapper_Cell : MonoBehaviour
    {
        public event Action<ListDialogWrapper_Cell> OnCellClick;

        private TextMeshProUGUI _text => GetComponentInChildren<TextMeshProUGUI>();
        private Color32 _selectionColor;
        private Color32 _notSelectedColor;
        private Button _button;

        internal bool IsSelected { get; private set; }

        private void Awake()
        {
            _button = GetComponent<Button>();
            _selectionColor = _button.colors.selectedColor;
            _notSelectedColor = _button.colors.normalColor;
            _button.onClick.AddListener(OnClickButton);
        }

        private void OnClickButton()
        {
            OnCellClick?.Invoke(this);
        }

        public void SetText(string text)
        {
            _text.text = text;
        }

        public void SetSelectionStatus(bool isSelected)
        {
            IsSelected = isSelected;
    
            var colors = _button.colors;
            colors.normalColor = isSelected ? _selectionColor : _notSelectedColor;
            _button.colors = colors;
        }

        private void OnDestroy()
        {
            OnCellClick = null;
        }
    }
}