using System;
using System.Collections.Generic;

namespace Packages.DVMessageBoxes.Source.Dialogs
{
    public class ListDialog : Dialog
    {
        public string Caption;

        public string Button1;
        public string Button2;

        public Action Act1;
        public Action Act2;

        public bool MultiSelection = true;

        internal List<string> Values = new();

        public ListDialog()
        {
        }

        public ListDialog(string caption, string button1 = "Apply", string button2 = "Cancel", Action act1 = null, Action act2 = null)
        {
            this.Caption = caption;
            this.Button1 = button1;
            this.Button2 = button2;
            this.Act1 = act1;
            this.Act2 = act2;
        }

        public ListDialog AddValue(string value)
        {
            Values.Add(value);
            return this;
        }

        public ListDialog AddValues(IEnumerable<string> values)
        {
            Values.AddRange(values);
            return this;
        }

        public ListDialog RemoveValue(string value)
        {
            Values.Remove(value); 
            return this;
        }

        public ListDialog ClearValues()
        {
            Values.Clear(); 
            return this;
        }
    }
}