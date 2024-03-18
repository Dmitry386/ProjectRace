using Packages.DVMessageBoxes.Source.Dialogs;
using System.Collections.Generic;

namespace Packages.DVMessageBoxes.Source.Events
{
    public class DialogResponseEventArgs
    {
        public Dialog DialogInfo;
        public List<int> SelectedItems = new();
        public int DialogButton;
        public string InputText;
    }
}