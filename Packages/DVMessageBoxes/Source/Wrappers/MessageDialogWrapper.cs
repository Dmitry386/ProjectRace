using Packages.DVMessageBoxes.Source.Dialogs;
using Packages.DVMessageBoxes.Source.Helpers;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Packages.DVMessageBoxes.Source.Wrappers
{
    internal class MessageDialogWrapper : DialogWrapper
    {
        [SerializeField] private TextMeshProUGUI _caption;
        [SerializeField] private TextMeshProUGUI _message;
        [SerializeField] private Button _button1;
        [SerializeField] private Button _button2;
        [SerializeField] private Button _button3;

        public override Type GetWrapDialogType()
        {
            return typeof(MessageDialog);
        }

        private void OnEnable()
        {
            if (DialogData is MessageDialog dial)
            {
                _caption.text = dial.Caption;
                _message.text = dial.Message;

                DialogHelper.SetButtonTextOrDeactivate(_button1, dial.Button1);
                DialogHelper.SetButtonTextOrDeactivate(_button2, dial.Button2);
                DialogHelper.SetButtonTextOrDeactivate(_button3, dial.Button3);

                DialogHelper.SetButtonResponse(_button1, dial, new() { DialogInfo = DialogData, DialogButton = 0 }, dial.Act1);
                DialogHelper.SetButtonResponse(_button2, dial, new() { DialogInfo = DialogData, DialogButton = 1 }, dial.Act2);
                DialogHelper.SetButtonResponse(_button3, dial, new() { DialogInfo = DialogData, DialogButton = 2 }, dial.Act3);
            }
        }
    }
}