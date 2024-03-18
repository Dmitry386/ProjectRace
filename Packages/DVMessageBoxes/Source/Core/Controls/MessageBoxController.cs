using Packages.DVMessageBoxes.Source.Dialogs;
using Packages.DVMessageBoxes.Source.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Packages.DVMessageBoxes.Source.Core.Controls
{
    [DefaultExecutionOrder(-1000)]
    internal class MessageBoxController : MonoBehaviour
    {
        private static MessageBoxController _instance;

        [SerializeField] private bool _multipleDialogsOnScreen = true;
        [SerializeField] private DialogWrapper[] _wrappers;

        private List<DialogWrapper> _activeDialogs = new();

        private void Awake()
        {
            if (_instance)
            {
                Debug.LogError(@$"Repeat of ""{GetType().Name}"". Scene object with name ""{name}"" will be destroyed.");
                Destroy(this.gameObject);
                return;
            }

            _instance = this;
            //DontDestroyOnLoad(this.gameObject);

            foreach (var item in _wrappers)
            {
                item.gameObject.SetActive(false);
            }
        }

        internal static void ShowDialog(Dialog dialog)
        {
            if (CheckIsActiveMessageBoxController(out var controller))
            {
                controller.InternalShowDialog(dialog);
            }
        }

        private void InternalShowDialog(Dialog dialog)
        {
            DialogWrapper wrapper = GetDialogWrapper(dialog.GetType());

            if (wrapper != null)
            {
                var newDialog = CreateNewDialogWrapper(wrapper, dialog);
                newDialog.gameObject.SetActive(true);
            }
            else
            {
                ErrorNoWrapper(dialog.GetType());
            }
        }

        private DialogWrapper CreateNewDialogWrapper(DialogWrapper wrapper, Dialog notWrappedDialog)
        {
            if (IsActiveWrapperForDialog(notWrappedDialog, out var activeWrapper)) return activeWrapper;

            if (!_multipleDialogsOnScreen)
            {
                CloseAllDialogs();
            }

            var dialog = Instantiate(wrapper);
            dialog.transform.SetParent(wrapper.transform.parent, false);
            dialog.DialogData = notWrappedDialog;
            _activeDialogs.Add(dialog);

            return dialog;
        }

        private bool IsActiveWrapperForDialog(Dialog notWrappedDialog, out DialogWrapper wrap)
        {
            wrap = _activeDialogs.FirstOrDefault(x => x.DialogData == notWrappedDialog);
            return wrap != null;
        }

        private void CloseAllDialogs()
        {
            _activeDialogs.ForEach(x => x.Close());
            _activeDialogs.Clear();
        }

        private DialogWrapper GetDialogWrapper(Type dialogType)
        {
            return _wrappers.FirstOrDefault(x => x.GetWrapDialogType() == dialogType);
        }

        internal static bool CheckIsActiveMessageBoxController(out MessageBoxController mb)
        {
            mb = _instance;
            if (!mb) ErrorNoController();
            return mb;
        }

        internal static void CloseDialog(Dialog dialog)
        {
            if (CheckIsActiveMessageBoxController(out var control))
            {
                control.CloseDialogInternal(dialog);
            }
        }

        private void CloseDialogInternal(Dialog dialog)
        {
            if (IsActiveWrapperForDialog(dialog, out var wrapper))
            {
                GameObject.Destroy(wrapper.gameObject);
            }
        }

        private void OnDestroy()
        {
            CloseAllDialogs();
        }

        #region ERRORS
        private static void ErrorNoWrapper(Type type)
        {
            Debug.LogWarning($"No dialog wrapper for {type.Name}");
        }

        private static void ErrorNoController()
        {
            Debug.LogWarning($"Impossible show dialogs. No {nameof(MessageBoxController)}");
        }
        #endregion
    }
}