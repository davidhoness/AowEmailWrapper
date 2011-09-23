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
            this.KeyPress += new KeyPressEventHandler(AccountsCreationForm_KeyPress);

            accountsCreationWizzard.CreateClicked += new EventHandler(AccountsCreationWizzard_CreateClicked);            
            Translator.TranslateForm(this);
        }

        public AccountConfigValues ChosenTemplate
        {
            get { return (accountsCreationWizzard != null) ? accountsCreationWizzard.ChosenTemplate : null; }
        }

        public AccountConfigValuesList AccountTemplates
        {
            get { return (accountsCreationWizzard != null) ? accountsCreationWizzard.AccountTemplates : null; }
            set
            {
                if (accountsCreationWizzard != null)
                {
                    accountsCreationWizzard.AccountTemplates = value;
                }
            }
        }

        public ImageList TemplateIcons
        {
            get { return (accountsCreationWizzard != null) ? accountsCreationWizzard.TemplateIcons : null; }
            set
            {
                if (accountsCreationWizzard != null)
                {
                    accountsCreationWizzard.TemplateIcons = value;
                }
            }
        }

        private void AccountsCreationWizzard_CreateClicked(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void AccountsCreationForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals((char)Keys.Escape))
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}
