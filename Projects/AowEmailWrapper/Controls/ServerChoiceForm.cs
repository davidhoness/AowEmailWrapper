using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mozilla.Autoconfig;
using AowEmailWrapper.Localization;

namespace AowEmailWrapper.Controls
{
    public partial class ServerChoiceForm : Form
    {
        public ServerChoiceForm()
        {
            InitializeComponent();

            serverChoiceControl.Cancelled += new EventHandler(serverChoiceControl_Cancelled);
            serverChoiceControl.ConfigChosen += new EventHandler(serverChoiceControl_ConfigChosen);

            Translator.TranslateForm(this);
        }

        public EmailProvider EmailProvider
        {
            get { return serverChoiceControl.EmailProvider; }
            set { serverChoiceControl.EmailProvider = value; }
        }

        private void serverChoiceControl_Cancelled(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void serverChoiceControl_ConfigChosen(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
