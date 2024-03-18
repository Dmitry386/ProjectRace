using Packages.DVMessageBoxes.Source.Core.Controls;
using Packages.DVMessageBoxes.Source.Events;
using System;

namespace Packages.DVMessageBoxes.Source.Dialogs
{
    public class Dialog
    {
        public event Action<DialogResponseEventArgs> OnResponse;
        public bool CloseOnResponse = true;

        public Dialog Show()
        {
            MessageBoxController.ShowDialog(this);
            return this;
        }

        public Dialog Close()
        {
            MessageBoxController.CloseDialog(this);
            OnResponse = null;
            return this;
        }

        internal void InvokeInternalRespose(DialogResponseEventArgs args)
        {
            OnResponse?.Invoke(args);
            if (CloseOnResponse) Close();
        }
    }
}