using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace AowEmailWrapper.Controls
{
    public partial class FormBlockText : BaseFormBlock
    {
        private string _validationRegEx = null;

        public string ValidationRegEx
        {
            get { return _validationRegEx; }
            set { _validationRegEx = value; }
        }

        public string LabelName
        {
            get { return lblName.Text; }
            set { lblName.Text = value; }
        }

        public string TextValue
        {
            get { return txtValue.Text; }
            set { txtValue.Text = value; }
        }

        public TextBox InnerTextBox
        {
            get { return txtValue; }
        }

        public bool IsPassword
        {
            get { return txtValue.UseSystemPasswordChar; }
            set { txtValue.UseSystemPasswordChar = value; }
        }

        public FormBlockText()
        {
            InitializeComponent();
            txtValue.TextChanged += new EventHandler(ValidateRegEx);
            base.SetControls(lblName, txtValue);
        }

        private void ValidateRegEx(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_validationRegEx))
            {
                bool isMatch = Regex.IsMatch(txtValue.Text, _validationRegEx);
                txtValue.BackColor = isMatch ? SystemColors.Window : Color.MistyRose;
            }
        }
    }
}
