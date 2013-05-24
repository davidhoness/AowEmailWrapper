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
        public EventHandler WrapperDecides;
        public EventHandler UserDecides;

        private const string ServerPreferenceNoPreferenceKey = "serverPreferenceNoPreference";

        public AutoconfigPage3Select()
        {
            InitializeComponent();

            cmdWrapperDecides.Click += new EventHandler(cmdWrapperDecides_Click);
            cmdUserDecides.Click += new EventHandler(cmdUserDecides_Click);

            fbServerPreference.AddItem(ServerType.Unknown.ToString(), Translator.Translate(ServerPreferenceNoPreferenceKey));
            fbServerPreference.AddItem(ServerType.IMAP.ToString(), Translator.TranslateEnum(ServerType.IMAP));
            fbServerPreference.AddItem(ServerType.POP3.ToString(), Translator.TranslateEnum(ServerType.POP3));
            fbServerPreference.SelectedIndex = 0;
        }

        public ServerType IncomingPreference
        {
            get { return ConfigHelper.ParseEnumString<ServerType>(fbServerPreference.SelectedValue); }
        }

        private void cmdWrapperDecides_Click(object sender, EventArgs e)
        {
            if (WrapperDecides != null)
            {
                WrapperDecides(this, e);
            }
        }

        private void cmdUserDecides_Click(object sender, EventArgs e)
        {
            if (UserDecides != null)
            {
                UserDecides(this, e);
            }
        }

        public void Reset()
        {
            cmdWrapperDecides.Focus();
        }
    }
}
