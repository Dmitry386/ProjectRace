using Packages.DVMessageBoxes.Source.Dialogs;
using Packages.DVMessageBoxes.Source.Helpers;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Packages.DVMessageBoxes.Source.Wrappers
{
    internal class InputDialogWrapper : DialogWrapper
    {
        [SerializeField] private TextMeshProUGUI _caption;
        [SerializeField] private TextMeshProUGUI _message;
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _button1;
        [SerializeField] private Button _button2;

        public override Type GetWrapDialogType()
        {
            return typeof(InputDialog);
        }

        private void OnEnable()
        {
            if (DialogData is InputDialog dial)
            {
                _caption.text = dial.Caption;
                _message.text = dial.Message;
                _inputField.text = dial.StartInputText;

                DialogHelper.SetButtonTextOrDeactivate(_button1, dial.Button1);
                DialogHelper.SetButtonTextOrDeactivate(_button2, dial.Button2);

                RegisterButtonListener(_button1, dial.Act1, 0);
                RegisterButtonListener(_button2, dial.Act2, 1);
            }
        }

        private void RegisterButtonListener(Button button, Action act, int buttonId)
        {
            button.onClick.AddListener(() =>
            {
                act?.Invoke();
                var args = new Events.DialogResponseEventArgs() { DialogButton = buttonId, DialogInfo = DialogData, InputText = _inputField.text };
                DialogData.InvokeInternalRespose(args);
            });
        }
    }
}