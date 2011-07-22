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
    public partial class FormBlockCheckBox : BaseFormBlock
    {
        public FormBlockCheckBox()
        {
            InitializeComponent();
            base.SetControls(lblName, checkBox);
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

        private void lblName_Click(object sender, EventArgs e)
        {
            checkBox.Checked = !checkBox.Checked;
        }
    }
}
