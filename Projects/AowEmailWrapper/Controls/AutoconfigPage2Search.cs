using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mozilla.Autoconfig;

namespace AowEmailWrapper.Controls
{
    public partial class AutoconfigPage2Search : UserControl
    {
        public EventHandler MxLookupClick;
        public EventHandler GuessClick;
        public EventHandler ManualClick;

        private RequestType _lastRequestType;

        public RequestType LastRequestType
        {
            get { return _lastRequestType; }
            set { _lastRequestType = value; }
        }

        public AutoconfigPage2Search()
        {
            InitializeComponent();

            cmdMxLookup.Click += new EventHandler(cmdMxLookup_Click);
            cmdGuess.Click += new EventHandler(cmdGuess_Click);
            cmdManual.Click += new EventHandler(cmdManual_Click);
        }

        private void cmdMxLookup_Click(object sender, EventArgs e)
        {
            if (MxLookupClick != null)
            {
                MxLookupClick(this, e);
            }
        }

        private void cmdGuess_Click(object sender, EventArgs e)
        {
            if (GuessClick != null)
            {
                GuessClick(this, e);
            }
        }

        private void cmdManual_Click(object sender, EventArgs e)
        {
            if (ManualClick != null)
            {
                ManualClick(this, e);
            }
        }

        public void Reset()
        {
            progressBar.Visible = true;

            groupBoxResult.Visible = false;
            groupBoxNext.Visible = false;
            panelManual.Visible = false;

            pictureBoxSuccess.Visible = false;
            pictureBoxFailed.Visible = false;

            labelResultMessage.Text = string.Empty;
        }

        public void Success()
        {
            progressBar.Visible = false;
            groupBoxResult.Visible = true;

            pictureBoxSuccess.Visible = true;
            pictureBoxFailed.Visible = false;
            labelResultMessage.Text = "Success, email settings found.  Click Next to continue.";
        }

        public void Failed()
        {
            progressBar.Visible = false;
            panelManual.Visible = true;
            groupBoxNext.Visible = true;
            groupBoxResult.Visible = true;
            cmdManual.Visible = true;

            pictureBoxSuccess.Visible = false;
            pictureBoxFailed.Visible = true;
            labelResultMessage.Text = "Unsuccessful, no email settings were found in the online database.  Please choose one of the options below.";
            
            SetButtonFocus();
        }

        private void SetButtonFocus()
        {
            switch (LastRequestType)
            { 
                case RequestType.Standard:
                    cmdMxLookup.Focus();
                    break;
                case RequestType.MxLookup:
                    cmdGuess.Focus();
                    break;
                case RequestType.Guess:
                    cmdManual.Focus();
                    break;
            }
        }
    }
}
