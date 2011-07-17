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
    public partial class FormBlockText : UserControl
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

        protected override void OnResize(EventArgs e)
        {
            this.SuspendLayout();
            base.OnResize(e);
            double d = this.Width * 0.65;
            txtValue.Width = Convert.ToInt32(Math.Ceiling(d));
            this.ResumeLayout();
        }

        public bool IsPassword
        {
            get { return txtValue.UseSystemPasswordChar; }
            set { txtValue.UseSystemPasswordChar = value; }
        }

        public FormBlockText()
        {
            InitializeComponent();
        }
    }
}
