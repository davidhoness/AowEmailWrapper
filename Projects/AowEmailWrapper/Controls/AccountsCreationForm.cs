using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AowEmailWrapper.ConfigFramework;
using AowEmailWrapper.Localization;

namespace AowEmailWrapper.Controls
{
    public partial class AccountsCreationForm : Form
    {
        public AccountsCreationForm()
        {
            InitializeComponent();
            this.KeyPreview = true;

            autoconfigWizardControl.Cancelled += new EventHandler(autoconfigWizardControl_Cancelled);
            this.KeyPress += new KeyPressEventHandler(AccountsCreationForm_KeyPress);

            autoconfigWizardControl.ConfigChosen += new EventHandler(autoconfigWizardControl_ConfigChosen);
            Translator.TranslateForm(this);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            autoconfigWizardControl.Reset();
            this.Activate();
        }

        protected override void OnClosed(EventArgs e)
        {
            autoconfigWizardControl.AbortSearchThread();
            base.OnClosed(e);
        }

        public AccountConfigValues ChosenTemplate
        {
            get { return (autoconfigWizardControl != null) ? autoconfigWizardControl.ChosenTemplate : null; }
        }

        private void autoconfigWizardControl_ConfigChosen(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void autoconfigWizardControl_Cancelled(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void AccountsCreationForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals((char)Keys.Escape))
            {
                autoconfigWizardControl_Cancelled(sender, e);
            }
        }
    }
}
