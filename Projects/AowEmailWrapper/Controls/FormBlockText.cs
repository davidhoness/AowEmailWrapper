using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AowEmailWrapper.Controls
{
    public partial class FormBlockText : BaseFormBlock
    {
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
            base.SetControls(lblName, txtValue);
        }
    }
}
