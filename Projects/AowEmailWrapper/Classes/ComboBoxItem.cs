using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AowEmailWrapper.Classes
{
    public class ComboBoxItem
    {
        private string _value;
        private string _text;

        public string Value { get { return _value; } }
        public string Text { get { return _text; } }

        public ComboBoxItem(string value, string text)
        {
            _value = value;
            _text = text;
        }
    }
}
