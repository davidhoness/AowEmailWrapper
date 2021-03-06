﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Mozilla.Autoconfig;
using AowEmailWrapper.Helpers;
using AowEmailWrapper.ConfigFramework;
using AowEmailWrapper.Localization;

namespace AowEmailWrapper.Controls
{
    public partial class AutoconfigWizardControl : UserControl
    {
        private delegate void CallBackEvent(object sender, MechanismResponse response, Action<MechanismResponse> action);

        private const string OtherAccountTranslationKey = "accountOther";
        private const string OtherAccountType = "Other";

        private int _stage = 0;
        private string _emailAddress;
        private string _password;

        private Thread _autoConfigThread;
        private MechanismResponse _mechanismSuccess;
        private UserControl[] _stages;
        private AccountConfigValues _chosenTemplate;

        public EventHandler Cancelled;
        public EventHandler ConfigChosen;

        public AutoconfigWizardControl()
        {
            InitializeComponent();

            cmdNext.Click += new EventHandler(cmdNext_Click);
            cmdCancel.Click += new EventHandler(cmdCancel_Click);
            cmdBack.Click += new EventHandler(cmdBack_Click);

            contentPage1.TextKeyDown += new KeyEventHandler(contentPage1_KeyDown);
            contentPage1.Next += new EventHandler(contentPage1_Next);

            _stages = new UserControl[] { contentPage1, contentPage2, contentPage3 };
        }

        public AccountConfigValues ChosenTemplate
        {
            get { return _chosenTemplate; }
        }

        public void Reset()
        {
            _stage = 0;

            contentPage3.Reset();
            contentPage2.Reset();
            contentPage1.Reset();
        }

        private void HideControl(UserControl control)
        {
            control.Visible = false;
            control.SendToBack();
            control.TabStop = false;
        }

        private void ShowControl(UserControl control)
        {
            control.Visible = true;
            control.BringToFront();
            control.TabStop = true;
        }

        public void AbortSearchThread()
        {
            if (_autoConfigThread != null && _autoConfigThread.IsAlive)
            {
                _autoConfigThread.Abort();
            }
        }

        public static void SetHighlight(RadioButton control)
        {
            if (control.Checked)
            {
                control.BackColor = SystemColors.Highlight;
                control.ForeColor = SystemColors.HighlightText;
            }
            else
            {
                control.BackColor = Color.Transparent;
                control.ForeColor = SystemColors.ControlText;
            }
        }

        public static void SetChecked(RadioButton control, params RadioButton[] group)
        {
            if (control.Checked)
            {
                foreach (RadioButton rb in group)
                {
                    if (!control.Equals(rb))
                    {
                        rb.Checked = false;
                    }
                }
            }
        }

        #region Event Handlers 

        private void cmdNext_Click(object sender, EventArgs e)
        {
            if (_stage <= _stages.GetUpperBound(0))
            {
                switch (_stage)
                {
                    case 0:
                        HandleAutoconfigPage1Welcome();
                        break;
                    case 1:
                        HandleAutoconfigPage2Search();
                        break;
                    case 2:
                        HandleAutoconfigPage3Select();
                        break;
                }
            }
        }

        private void HandleAutoconfigPage1Welcome()
        {
            if (contentPage1.Outcome == AutoconfigPage1Welcome.AutoconfigPage1Outcome.Success)
            {
                ShowControl(_stages[_stage + 1]);
                HideControl(_stages[_stage]);
                _emailAddress = contentPage1.EmailAddress;
                _password = contentPage1.Password;
                cmdNext.Enabled = false;
                TryAutoConfig(RequestType.Standard);
                _stage++;
            }    
        }

        private void HandleAutoconfigPage2Search()
        {
            switch (contentPage2.Outcome)
            {
                case AutoconfigPage2Search.AutoconfigPage2Outcome.DoMxLookup:
                    cmdNext.Enabled = false;
                    TryAutoConfig(RequestType.MxLookup);
                    break;
                case AutoconfigPage2Search.AutoconfigPage2Outcome.DoGuess:
                    cmdNext.Enabled = false;
                    TryAutoConfig(RequestType.Guess);
                    break;
                case AutoconfigPage2Search.AutoconfigPage2Outcome.Manual:
                    _chosenTemplate = CreateOther(_emailAddress, _password);
                    if (ConfigChosen != null)
                    {
                        ConfigChosen(this, new EventArgs());
                    }
                    break;
                case AutoconfigPage2Search.AutoconfigPage2Outcome.Success:
                    ShowControl(_stages[_stage + 1]);
                    HideControl(_stages[_stage]);
                    contentPage3.Reset();
                    _stage++;
                    break;
            }
        }

        private void HandleAutoconfigPage3Select()
        {
            switch (contentPage3.Outcome)
            {
                case AutoconfigPage3Select.AutoconfigPage3Outcome.WrapperDecides:
                    EmailProvider provider = _mechanismSuccess.ClientConfig.EmailProvider;
                    _chosenTemplate = AutoconfigurationHelper.MapMechanismResponse(_mechanismSuccess, _emailAddress, _password, contentPage3.IncomingPreference);
                    if (ConfigChosen != null)
                    {
                        ConfigChosen(this, new EventArgs());
                    }
                    break;
                case AutoconfigPage3Select.AutoconfigPage3Outcome.UserDecides:
                    ServerChoiceForm form = new ServerChoiceForm();

                    form.EmailProvider = _mechanismSuccess.ClientConfig.EmailProvider;

                    if (form.ShowDialog(this).Equals(DialogResult.OK))
                    {
                        _chosenTemplate = AutoconfigurationHelper.MapManualChoice(form.EmailProvider, _emailAddress, _password);

                        if (ConfigChosen != null)
                        {
                            ConfigChosen(this, new EventArgs());
                        }
                    }
                    break;
            }
        }

        private void cmdBack_Click(object sender, EventArgs e)
        {
            if (_stage > 0)
            {
                ShowControl(_stages[_stage - 1]);
                HideControl(_stages[_stage]);
                _stage--;

                switch (_stage)
                {
                    case 0:
                        AbortSearchThread();
                        cmdNext.Enabled = contentPage1.Outcome == AutoconfigPage1Welcome.AutoconfigPage1Outcome.Success;
                        break;
                    case 1:
                        cmdNext.Enabled = true;
                        break;
                }
            }
            
            cmdBack.Visible = _stage != 0;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            if (Cancelled != null)
            {
                Cancelled(this, e);
            }
        }

        private void contentPage1_KeyDown(object sender, KeyEventArgs e)
        {
            cmdNext.Enabled = contentPage1.Outcome == AutoconfigPage1Welcome.AutoconfigPage1Outcome.Success;
        }

        private void contentPage1_Next(object sender, EventArgs e)
        {
            cmdNext_Click(sender, e);
        }

        #endregion

        #region Threading Code

        private void TryAutoConfig(RequestType requestType)
        {
            contentPage2.Reset();
            contentPage2.LastRequestType = requestType;
            cmdBack.Visible = true;

            _autoConfigThread = new Thread(new ParameterizedThreadStart(AutoConfig_Search_Thread));
            _autoConfigThread.Start(requestType.ToString());
        }

        private void AutoConfig_Search_Thread(object obj)
        {
            try
            {
                RequestType requestType = ConfigHelper.ParseEnumString<RequestType>(obj as string);

                MechanismResponse response = IspDbHandler.GetAutoconfig(_emailAddress, requestType);

                if (response.IsGuess)
                {
                    //Excludes servers that fail and determines socket type for Plain/TLS ports
                    EmailProvider provider = response.ClientConfig.EmailProvider;
                    AutoconfigurationHelper.TestAllEmailServers(provider, 5000);

                    if (provider.IncomingServers.Count == 0 ||
                        provider.OutgoingServers.Count == 0)
                    {
                        response = new MechanismResponse() { ResponseType = MechanismResponseType.NotFound };
                    }
                }

                this.Invoke(
                    new CallBackEvent(CallBackMechanismResponse),
                    this,
                    response,
                    new Action<MechanismResponse>(resp =>
                {
                    if (resp != null && resp.IsSuccess)
                    {
                        _mechanismSuccess = resp;
                        contentPage2.Success();
                    }
                    else
                    {
                        contentPage2.Failed();
                    }
                    cmdNext.Enabled = true;
                    cmdNext.Focus();
                }));
            }
            catch
            { }
        }

        private void CallBackMechanismResponse(object sender,
            MechanismResponse response,
            Action<MechanismResponse> action)
        {
            action(response);
        }

        #endregion

        #region Mapping Code

        private AccountConfigValues CreateOther(string emailAddress, string password)
        {
            AccountConfigValues other = new AccountConfigValues();

            other.Name = Translator.Translate(OtherAccountTranslationKey);
            other.PollingConfig = new PollingConfigValues();
            other.PollingConfig.UsePolling = true;
            other.PollingConfig.Username = emailAddress;
            other.PollingConfig.PasswordTrue = password;
            other.PollingConfig.EmailType = EmailType.POP3;
            other.PollingConfig.Port = 110;

            other.SmtpConfig = new SmtpConfigValues();
            other.SmtpConfig.Authentication = true;
            other.SmtpConfig.EmailAddress = emailAddress;
            other.SmtpConfig.Port = 25;
            other.SmtpConfig.SmtpSSLType = SSLType.None;
            other.SmtpConfig.UsePollingCredentials = true;

            return other;
        }

        #endregion
    }
}
