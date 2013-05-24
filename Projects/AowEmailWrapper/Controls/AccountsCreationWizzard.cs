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
    public partial class AccountsCreationWizzard : UserControl
    {
        private AccountConfigValues _chosenTemplate;
        private EventHandler _autoConfigFinishEvent;
        private System.Threading.Thread _autoConfigThread;
        private bool _abortRequest;

        private const string InputEmailSettingsManual = "msgInputEmailSettingsManual";
        private const string OtherAccountTranslationKey = "accountOther";
        private const string OtherAccountType = "Other";
        private const string ServerPreferenceNoPreferenceKey = "serverPreferenceNoPreference";

        public EventHandler CreateClicked;
        

        public AccountConfigValues ChosenTemplate
        {
            get { return _chosenTemplate; }
        }

        public void AbortRequest()
        {
            _abortRequest = true;

            if (_autoConfigThread != null)
            {
                _autoConfigThread.Abort();
                _autoConfigThread = null;
            }
        }

        public AccountsCreationWizzard()
        {
            InitializeComponent();

            EventHandler textBoxTextChanged = new EventHandler(textBox_TextChanged);
            KeyEventHandler textBoxKeyDown = new KeyEventHandler(textBox_KeyDown);

            _autoConfigFinishEvent = new EventHandler(Finish_AutoConfig);

            EnableForm(true);

            fbEmailAddress.InnerTextBox.TextChanged += textBoxTextChanged;
            fbEmailAddress.InnerTextBox.Validated += new EventHandler(textBox_Validated);
            fbEmailAddress.InnerTextBox.KeyDown += textBoxKeyDown;

            fbPassword.InnerTextBox.TextChanged += textBoxTextChanged;
            fbPassword.InnerTextBox.KeyDown += textBoxKeyDown;

            fbServerPreference.AddItem(ServerType.Unknown.ToString(), Translator.Translate(ServerPreferenceNoPreferenceKey));
            fbServerPreference.AddItem(ServerType.IMAP.ToString(), Translator.TranslateEnum(ServerType.IMAP));
            fbServerPreference.AddItem(ServerType.POP3.ToString(), Translator.TranslateEnum(ServerType.POP3));
            fbServerPreference.SelectedIndex = 0;
        }

        private void CheckCreateEnabled()
        {
            buttonCreate.Enabled = fbEmailAddress.TextValue.Length > 0 && fbPassword.TextValue.Length > 0;
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter) && sender.Equals(fbPassword.InnerTextBox))
            {
                buttonCreate.Focus();
                buttonCreate_Click(sender, e);
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {            
            CheckCreateEnabled();
        }

        private void textBox_Validated(object sender, EventArgs e)
        {
            //SelectCorrectEmailProvider();
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            EnableForm(false);
            
            _autoConfigThread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(Start_AutoConfig));
            string[] args = new string[] { fbEmailAddress.TextValue, fbPassword.TextValue, fbServerPreference.SelectedValue };

            _autoConfigThread.Start(args);
        }

        private void Start_AutoConfig(object obj)
        {
            try
            {
                _abortRequest = false;
                string[] args = obj as string[];
                HandleResponse(IspDbHandler.GetAutoconfig(args[0], RequestType.Standard), args);
            }
            catch (System.Threading.ThreadAbortException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void HandleResponse(MechanismResponse mozillaResponse, string[] args)
        {            
            if (!_abortRequest)
            {
                if (mozillaResponse != null && mozillaResponse.IsSuccess)
                {
                    EmailProvider provider = mozillaResponse.ClientConfig.EmailProvider;
                    _chosenTemplate = AutoconfigurationHelper.MapMechanismResponse(mozillaResponse, args[0], args[1], ConfigHelper.ParseEnumString<ServerType>(args[2]));
                }
                else
                {
                    _chosenTemplate = null;
                }

                this.Invoke(_autoConfigFinishEvent);
            }
        }

        private void Finish_AutoConfig(object sender, EventArgs e)
        {
            EnableForm(true);

            bool raiseEvent = false;

            if (_chosenTemplate != null)
            {
                if (_chosenTemplate.IsGuess)
                {
                    DialogResult dialogResult = MessageBox.Show(
                        "Email settings have been guessed, you may need to manually edit the settings before they will work.",
                        this.Parent.Text,
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Warning);

                    raiseEvent = dialogResult == DialogResult.OK;
                }
                else
                {
                    raiseEvent = true;
                }
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show(
                    Translator.Translate(InputEmailSettingsManual),
                    this.Parent.Text,
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.OK)
                {
                    _chosenTemplate = CreateOther(fbEmailAddress.TextValue, fbPassword.TextValue);
                    raiseEvent = true;
                }
            }

            if (raiseEvent && CreateClicked != null)
            {
                CreateClicked(this, new EventArgs());
            }

            _autoConfigThread = null;
        }

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

        private void EnableForm(bool enabled)
        {
            fbEmailAddress.Enabled = enabled;
            fbPassword.Enabled = enabled;
            fbServerPreference.Enabled = enabled;
            buttonCreate.Enabled = enabled;
            progressBar.Visible = !enabled;
        }
    }
}
