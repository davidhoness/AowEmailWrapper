using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mozilla.Autoconfig;
using AowEmailWrapper.Localization;

namespace AowEmailWrapper.Controls
{
    public partial class AutoconfigPage2Search : UserControl
    {
        public enum AutoconfigPage2Outcome
        {
            Unknown,
            DoMxLookup,
            DoGuess,
            Manual,
            Success
        }

        private const string AutoconfigPage2SuccessKey = "msgAutoconfigPage2Success";
        private const string AutoconfigPage2FailedKey = "msgAutoconfigPage2Failed";

        private RequestType _lastRequestType;
        private AutoconfigPage2Outcome _outcome = AutoconfigPage2Outcome.Unknown;

        public RequestType LastRequestType
        {
            get { return _lastRequestType; }
            set { _lastRequestType = value; }
        }

        public AutoconfigPage2Outcome Outcome
        {
            get { return _outcome; }
        }

        public AutoconfigPage2Search()
        {
            InitializeComponent();

            EventHandler allCheckedChangedEvent = new EventHandler(allCheckedChanged);

            radioAutoconfigPage2Search1.CheckedChanged += allCheckedChangedEvent;
            radioAutoconfigPage2Search2.CheckedChanged += allCheckedChangedEvent;
            radioAutoconfigPage2Search3.CheckedChanged += allCheckedChangedEvent;
        }

        public void Reset()
        {
            EnsureControlSequence();

            progressBar.Visible = true;

            groupBoxResult.Visible = false;
            groupBoxNext.Visible = false;
            panelManual.Visible = false;

            pictureBoxSuccess.Visible = false;
            pictureBoxFailed.Visible = false;

            labelResultMessage.Text = string.Empty;

            radioAutoconfigPage2Search1.Checked = false;
            radioAutoconfigPage2Search2.Checked = false;
            radioAutoconfigPage2Search3.Checked = false;

            radioAutoconfigPage2Search1.Checked = true;
        }

        public void Success()
        {
            _outcome = AutoconfigPage2Outcome.Success;

            progressBar.Visible = false;
            groupBoxResult.Visible = true;

            pictureBoxSuccess.Visible = true;
            pictureBoxFailed.Visible = false;

            labelResultMessage.Text = Translator.Translate(AutoconfigPage2SuccessKey);
        }

        public void Failed()
        {
            progressBar.Visible = false;
            panelManual.Visible = true;
            groupBoxNext.Visible = true;
            groupBoxResult.Visible = true;

            pictureBoxSuccess.Visible = false;
            pictureBoxFailed.Visible = true;
            labelResultMessage.Text = Translator.Translate(AutoconfigPage2FailedKey);
            
            SetButtonFocus();
        }

        private void SetButtonFocus()
        {
            switch (LastRequestType)
            {
                case RequestType.Standard:
                    radioAutoconfigPage2Search1.Checked = true;
                    break;
                case RequestType.MxLookup:
                    radioAutoconfigPage2Search2.Checked = true;
                    break;
                case RequestType.Guess:
                    radioAutoconfigPage2Search3.Checked = true;
                    break;
            }
        }

        private void radioAutoconfigPage2Search1_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked) _outcome = AutoconfigPage2Outcome.DoMxLookup;
        }

        private void radioAutoconfigPage2Search2_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked) _outcome = AutoconfigPage2Outcome.DoGuess;
        }

        private void radioAutoconfigPage2Search3_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked) _outcome = AutoconfigPage2Outcome.Manual;
        }

        private void allCheckedChanged(object sender, EventArgs e)
        {
            RadioButton senderCast = sender as RadioButton;
            if (senderCast != null)
            {
                AutoconfigWizardControl.SetChecked(senderCast, radioAutoconfigPage2Search1, radioAutoconfigPage2Search2, radioAutoconfigPage2Search3);
                AutoconfigWizardControl.SetHighlight(senderCast);
            }
        }

        private void EnsureControlSequence()
        {
            this.SuspendLayout();
            lblAutoconfigPage2SearchMessage.BringToFront();
            progressBar.BringToFront();
            groupBoxNext.BringToFront();
            groupBoxResult.BringToFront();
            panelManual.BringToFront();
            this.ResumeLayout();
        }
    }
}
