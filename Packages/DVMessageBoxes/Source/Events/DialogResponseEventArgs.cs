using Packages.DVMessageBoxes.Source.Dialogs;

namespace Packages.DVMessageBoxes.Source.Events
{
    public class DialogResponseEventArgs
    {
        public Dialog DialogInfo;
        public int SelectedItem = -1;
        public int DialogButton;
        public string InputText;
    }
}