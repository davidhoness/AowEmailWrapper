using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AowEmailWrapper.ConfigFramework;
using AowEmailWrapper.Helpers;
using AowEmailWrapper.Localization;

namespace AowEmailWrapper.Controls
{
    public partial class PollingConfig : UserControl
    {
        public EventHandler Config_Changed;

        private PollingConfigValues _config;

        public PollingConfig()
        {
            InitializeComponent();
            foreach (EmailType value in Enum.GetValues(typeof(EmailType)))
            {
                fbEmailType.AddItem(value.ToString(), Translator.TranslateEnum(value));
            }
            
            fbEmailType.SelectedIndex = 0;

            foreach (int i in new int[] { 1, 2, 3, 4, 5, 10, 15, 30, 60 })
            {
                fbPollingSetup.AddItem(i);
            }

            fbPollingSetup.SelectedIndex = 0;

            EventHandler raiseConfigChange = new EventHandler(Raise_Config_Changed);

            fbEmailType.InnerComboBox.SelectedIndexChanged += raiseConfigChange;
            fbServer.InnerTextBox.TextChanged += raiseConfigChange;
            fbPort.InnerTextBox.TextChanged += raiseConfigChange;
            fbUserName.InnerTextBox.TextChanged += raiseConfigChange;
            fbPassword.InnerTextBox.TextChanged += raiseConfigChange;
            fbPollingSetup.InnerComboBox.SelectedIndexChanged += raiseConfigChange;

            fbPollingSetup.InnerCheckBox.CheckedChanged += raiseConfigChange;
            fbPollingSetup.InnerCheckBox.CheckedChanged += new EventHandler(fbPollingSetup_CheckedChanged);
            fbUseSSL.InnerCheckBox.CheckedChanged += raiseConfigChange;
        }

        public string Prefix
        {
            get { return "PollingConfig"; }
        }

        public PollingConfigValues Config
        {
            get
            {
                Scrape();
                return _config;
            }
            set
            {
                _config = value;
                Populate();
            }
        }

        private void Scrape()
        {
            _config = new PollingConfigValues();

            _config.UsePolling = fbPollingSetup.Checked;

            _config.EmailType = ConfigHelper.ParseEnumString<EmailType>(fbEmailType.SelectedValue);

            _config.Server = fbServer.TextValue;

            int port = 110;
            if (int.TryParse(fbPort.TextValue, out port))
            {
                _config.Port = port;
            }

            _config.UseSSL = fbUseSSL.Checked;
            _config.Username = fbUserName.TextValue;
            _config.PasswordTrue = fbPassword.TextValue;

            int poll = 10;
            if (int.TryParse(fbPollingSetup.SelectedValue, out poll))
            {
                _config.PollInterval = poll;
            }
        }

        private void Populate()
        {
            fbPollingSetup.Checked = _config.UsePolling;
            fbEmailType.SelectedValue = _config.EmailType.ToString();
            fbServer.TextValue = _config.Server;
            fbPort.TextValue = _config.Port.ToString();
            fbUseSSL.Checked = _config.UseSSL;
            fbUserName.TextValue = _config.Username;
            fbPassword.TextValue = _config.PasswordTrue;
            fbPollingSetup.SelectedValue = _config.PollInterval.ToString();
        }

        private void fbPollingSetup_CheckedChanged(object sender, EventArgs e)
        {
            this.SuspendLayout();

            groupBoxAuth.Visible = fbPollingSetup.Checked;
            groupBoxServer.Visible = fbPollingSetup.Checked;

            this.ResumeLayout();
        }

        private void Raise_Config_Changed(object sender, EventArgs e)
        {
            if (Config_Changed != null)
            {
                Config_Changed(this, e);
            }
        }
    }
}
