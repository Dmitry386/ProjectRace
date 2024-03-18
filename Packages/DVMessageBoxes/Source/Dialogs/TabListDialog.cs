using System;

namespace Packages.DVMessageBoxes.Source.Dialogs
{
    public class TabListDialog : Dialog
    {
        public string Caption;

        public string Button1;
        public string Button2;

        public Action Act1;
        public Action Act2;

        public TabListDialog()
        {
        }

        public TabListDialog(string caption, string button1 = "Apply", string button2 = "Cancel", Action act1 = null, Action act2 = null)
        {
            this.Caption = caption;
            this.Button1 = button1;
            this.Button2 = button2;
            this.Act1 = act1;
            this.Act2 = act2;
        }
    }
}