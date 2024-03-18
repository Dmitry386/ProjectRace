using Packages.DVMessageBoxes.Source.Core.Controls;
using Packages.DVMessageBoxes.Source.Dialogs;
using System;
using UnityEngine;

namespace Packages.DVMessageBoxes.Source.Wrappers
{
    public abstract class DialogWrapper : MonoBehaviour
    {
        [HideInInspector] internal Dialog DialogData;

        public abstract Type GetWrapDialogType();

        public void Close()
        {
            MessageBoxController.CloseDialog(DialogData);
        }
    }
}