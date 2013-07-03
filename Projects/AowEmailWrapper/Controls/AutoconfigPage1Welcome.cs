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
    public partial class AutoconfigPage1Welcome : UserControl
    {
        public enum AutoconfigPage1Outcome
        {
            Unknown,
            Success
        }

        private const string ServerPreferenceNoPreferenceKey = "serverPreferenceNoPreference";

        public KeyEventHandler TextKeyDown;
        public EventHandler Next;

        public AutoconfigPage1Welcome()
        {
            InitializeComponent();

            KeyEventHandler textBoxKeyDown = new KeyEventHandler(textBox_KeyDown);

            fbEmailAddress.InnerTextBox.KeyDown += textBoxKeyDown;
            fbPassword.InnerTextBox.KeyDown += textBoxKeyDown;
        }

        public AutoconfigPage1Outcome Outcome
        {
            get
            {
                AutoconfigPage1Outcome returnVal = AutoconfigPage1Outcome.Unknown;
                if (fbEmailAddress.TextValue.Length > 0 && fbPassword.TextValue.Length > 0)
                {
                    returnVal = AutoconfigPage1Outcome.Success;
                }
                return returnVal;
            }
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (TextKeyDown != null)
            {
                TextKeyDown(sender, e);
            }

            if (e.KeyCode.Equals(Keys.Enter) && 
                sender.Equals(fbPassword.InnerTextBox) &&
                Next !=null)
            {
                Next(this, e);
            }
        }

        public string EmailAddress
        {
            get { return fbEmailAddress.TextValue; }
        }

        public string Password
        {
            get { return fbPassword.TextValue; }
        }

        public void Reset()
        {
            fbEmailAddress.InnerTextBox.Focus();
        }
    }
}
