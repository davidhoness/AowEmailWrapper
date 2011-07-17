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
    public partial class PreferencesConfig : UserControl
    {
        public EventHandler Config_Changed;
        private const string EmailInSelectedMessageKey = "msgEmailInSelected";
        private const string SaveSelectedMessageKey = "msgSaveSelected";
        private PreferencesConfigValues _config;

        public PreferencesConfig()
        {
            InitializeComponent();

            EventHandler raiseConfigChange = new EventHandler(Raise_Config_Changed);

            foreach (EmailSaveFolder value in Enum.GetValues(typeof(EmailSaveFolder)))
            {
                fbSaveFolder.AddItem(value.ToString(), Translator.TranslateEnum(value));
            }
            
            Translator.ComboBoxItems.ForEach(item => fbLocalization.AddItem(item));

            fbSaveFolder.SelectedIndex = 0;

            fbEmailSound.InnerCheckBox.CheckedChanged += raiseConfigChange;
            fbSentSound.InnerCheckBox.CheckedChanged += raiseConfigChange;
            fbAutostart.InnerCheckBox.CheckedChanged += raiseConfigChange;
            fbSaveFolder.InnerComboBox.SelectedIndexChanged += raiseConfigChange;
            fbSaveFolder.InnerComboBox.SelectedIndexChanged += new EventHandler(SaveFolder_SelectedIndexChanged);
            fbLocalization.InnerComboBox.SelectedIndexChanged += raiseConfigChange;
        }

        public string Prefix
        {
            get { return "Preferences"; }
        }

        public PreferencesConfigValues Config
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
            _config = new PreferencesConfigValues();

            _config.PlaySoundOnEmail = fbEmailSound.Checked;
            _config.PlaySoundOnSend = fbSentSound.Checked;
            _config.Autostart = fbAutostart.Checked;
            _config.SaveFolder = ConfigHelper.ParseEnumString<EmailSaveFolder>(fbSaveFolder.SelectedValue);
            _config.LanguageCode = !string.IsNullOrEmpty(fbLocalization.SelectedValue) ? fbLocalization.SelectedValue : Translator.CurrentLanguageCode;
        }

        private void Populate()
        {
            fbEmailSound.Checked = _config.PlaySoundOnEmail;
            fbSentSound.Checked = _config.PlaySoundOnSend;
            fbAutostart.Checked = _config.Autostart;
            fbSaveFolder.SelectedValue = _config.SaveFolder.ToString();
            fbLocalization.SelectedValue = _config.LanguageCode;
        }

        private void Raise_Config_Changed(object sender, EventArgs e)
        {
            if (Config_Changed != null)
            {
                Config_Changed(this, e);
            }
        }

        private void SaveFolder_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ConfigHelper.ParseEnumString<EmailSaveFolder>(fbSaveFolder.SelectedValue))
            {
                case EmailSaveFolder.EmailIn:
                    labelMessage.Text = Translator.Translate(EmailInSelectedMessageKey);
                    break;
                case EmailSaveFolder.Save:
                    labelMessage.Text = Translator.Translate(SaveSelectedMessageKey);
                    break;
            }
        }
    }
}
