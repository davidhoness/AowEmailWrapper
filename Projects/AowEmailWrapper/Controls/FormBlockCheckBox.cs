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
    public partial class FormBlockCheckBox : UserControl
    {
        public FormBlockCheckBox()
        {
            InitializeComponent();
        }
        public string LabelName
        {
            get { return lblName.Text; }
            set { lblName.Text = value; }
        }

        public bool Checked
        {
            get { return checkBox.Checked; }
            set { checkBox.Checked = value; }
        }

        public CheckBox InnerCheckBox
        {
            get { return checkBox; }
        }

        protected override void OnResize(EventArgs e)
        {
            this.SuspendLayout();
            base.OnResize(e);
            double d = this.Width * 0.65;
            checkBox.Width = Convert.ToInt32(Math.Ceiling(d));
            this.ResumeLayout();
        }

        private void lblName_Click(object sender, EventArgs e)
        {
            checkBox.Checked = !checkBox.Checked;
        }
    }
}
