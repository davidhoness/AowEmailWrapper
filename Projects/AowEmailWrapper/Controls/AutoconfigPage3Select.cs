using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AowEmailWrapper.Helpers;
using AowEmailWrapper.ConfigFramework;
using AowEmailWrapper.Localization;
using AowEmailWrapper.Classes;
using Mozilla.Autoconfig;

namespace AowEmailWrapper.Controls
{
    public partial class AutoconfigPage3Select : UserControl
    {
        public enum AutoconfigPage3Outcome
        {
            Unknown,
            WrapperDecides,
            UserDecides
        }

        private const string ServerPreferenceNoPreferenceKey = "serverPreferenceNoPreference";

        public AutoconfigPage3Select()
        {
            InitializeComponent();

            fbServerPreference.AddItem(ServerType.Unknown.ToString(), Translator.Translate(ServerPreferenceNoPreferenceKey));
            fbServerPreference.AddItem(ServerType.IMAP.ToString(), Translator.TranslateEnum(ServerType.IMAP));
            fbServerPreference.AddItem(ServerType.POP3.ToString(), Translator.TranslateEnum(ServerType.POP3));
            fbServerPreference.SelectedIndex = 0;

            EventHandler allCheckedChangedEvent = new EventHandler(allCheckedChanged);

            radioAutoconfigPage3Select1.CheckedChanged += allCheckedChangedEvent;
            radioAutoconfigPage3Select2.CheckedChanged += allCheckedChangedEvent;
        }

        public ServerType IncomingPreference
        {
            get { return ConfigHelper.ParseEnumString<ServerType>(fbServerPreference.SelectedValue); }
        }

        public AutoconfigPage3Outcome Outcome
        {
            get
            {
                AutoconfigPage3Outcome returnVal = AutoconfigPage3Outcome.Unknown;

                if (radioAutoconfigPage3Select1.Checked && !radioAutoconfigPage3Select2.Checked)
                {
                    returnVal = AutoconfigPage3Outcome.WrapperDecides;
                }
                else if (!radioAutoconfigPage3Select1.Checked && radioAutoconfigPage3Select2.Checked)
                {
                    returnVal = AutoconfigPage3Outcome.UserDecides;
                }

                return returnVal;
            }
        }

        public void Reset()
        {
            EnsureControlSequence();

            radioAutoconfigPage3Select1.Checked = false;
            radioAutoconfigPage3Select2.Checked = false;

            radioAutoconfigPage3Select1.Checked = true;
        }

        private void allCheckedChanged(object sender, EventArgs e)
        {
            RadioButton senderCast = sender as RadioButton;
            if (senderCast != null)
            {
                AutoconfigWizardControl.SetChecked(senderCast, radioAutoconfigPage3Select1, radioAutoconfigPage3Select2);
                AutoconfigWizardControl.SetHighlight(senderCast);
            }
        }

        private void EnsureControlSequence()
        {
            lblAutoconfigPage3ServerMessage.SendToBack();
            groupBoxOptions.BringToFront();
        }
    }
}
