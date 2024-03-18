using System;

namespace Packages.DVMessageBoxes.Source.Dialogs
{
    public class InputDialog : Dialog
    {
        public string Caption;
        public string Message;
        public string StartInputText = string.Empty;

        public string Button1;
        public string Button2;

        public Action Act1;
        public Action Act2;

        public InputDialog()
        {
        }

        public InputDialog(string caption, string msg, string button1 = "Apply", string button2 = "Cancel", Action act1 = null, Action act2 = null)
        {
            this.Caption = caption;
            this.Message = msg;
            this.Button1 = button1;
            this.Button2 = button2;

            Act1 = act1;
            Act2 = act2;
        }
    }
}