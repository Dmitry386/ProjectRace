using System;

namespace Packages.DVMessageBoxes.Source.Dialogs
{
    public class MessageDialog : Dialog
    {
        public string Caption;
        public string Message;
        public string Button1;
        public string Button2;
        public string Button3;

        public Action Act1;
        public Action Act2;
        public Action Act3;

        public MessageDialog()
        {
        }

        public MessageDialog(string caption, string msg, string button1 = "Apply", string button2 = "Cancel")
        {
            this.Caption = caption;
            this.Message = msg;
            this.Button1 = button1;
            this.Button2 = button2;
        }

        public MessageDialog(string caption, string msg, string button1 = "Apply", string button2 = "Retry", string button3 = "Cancel", Action act1 = null, Action act2 = null, Action act3 = null)
        {
            this.Caption = caption;
            this.Message = msg;
            this.Button1 = button1;
            this.Button2 = button2;
            this.Button3 = button3;

            Act1 = act1;
            Act2 = act2;
            Act3 = act3;
        }
    }
}