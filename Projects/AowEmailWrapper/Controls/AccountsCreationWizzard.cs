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

namespace AowEmailWrapper.Controls
{
    public partial class AccountsCreationWizzard : UserControl
    {
        private AccountConfigValuesList _accountTemplates;
        private AccountConfigValues _chosenTemplate;
        private const string InputEmailSettingsManual = "msgInputEmailSettingsManual";

        public EventHandler CreateClicked;

        public AccountConfigValues ChosenTemplate
        {
            get { return _chosenTemplate; }
        }

        public AccountConfigValuesList AccountTemplates
        {
            get { return _accountTemplates; }
            set
            {
                _accountTemplates = value;
                CreateRadioButtons();
            }
        }

        public ImageList RadioImages
        {
            get { return null; }
            set
            {
                SetIcons(value);
            }
        }

        public AccountsCreationWizzard()
        {
            InitializeComponent();
            fbEmailAddress.InnerTextBox.KeyDown += new KeyEventHandler(textBox_Changed);
            fbPassword.InnerTextBox.KeyDown += new KeyEventHandler(textBox_Changed);
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {            
            RadioButton theButton = (RadioButton)sender;
            string buttonArg = theButton.Tag.ToString();

            if (_accountTemplates != null &&
                _accountTemplates.Accounts != null &&
                _accountTemplates.Accounts.Count > 0)
            {
                EmailProviderType selectedEmailType = ConfigHelper.ParseEnumString<EmailProviderType>(buttonArg);
                AccountConfigValues selectedTemplate = _accountTemplates.Accounts.Find(account => account.EmailProvider.Equals(selectedEmailType));

                //Create a copy of it in memory (don't modify the original)
                _chosenTemplate = XmlHelper.Deserialize<AccountConfigValues>(XmlHelper.Serialize(selectedTemplate));

                CheckCreateEnabled();

                switch (_chosenTemplate.EmailProvider)
                {
                    case EmailProviderType.Google:
                    case EmailProviderType.WindowsLive:
                    case EmailProviderType.Yahoo:
                        labelDomainsMessage.Text = _chosenTemplate.Domains;
                        fbEmailAddress.InnerTextBox.Focus();
                        break;
                    default:
                        labelDomainsMessage.Text = Translator.Translate(InputEmailSettingsManual);
                        fbEmailAddress.TextValue = string.Empty;
                        fbPassword.TextValue = string.Empty;
                        buttonCreate.Focus();
                        break;
                }
            }
        }

        private void CheckCreateEnabled()
        {
            if (_chosenTemplate != null)
            {
                switch (_chosenTemplate.EmailProvider)
                {
                    case EmailProviderType.Google:
                    case EmailProviderType.WindowsLive:
                    case EmailProviderType.Yahoo:
                        buttonCreate.Enabled = fbEmailAddress.TextValue.Length > 0 && fbPassword.TextValue.Length > 0;
                        groupBoxAuth.Enabled = true;
                        break;
                    default:
                        buttonCreate.Enabled = true;
                        groupBoxAuth.Enabled = false;
                        break;
                }
            }
        }

        private void textBox_Changed(object sender, KeyEventArgs e)
        {
            CheckCreateEnabled();
            if (e.KeyCode.Equals(Keys.Enter) && sender.Equals(fbPassword.InnerTextBox))
            {
                buttonCreate.Focus();
                buttonCreate_Click(sender, e);
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            UpdateChosenTemplate(fbEmailAddress.TextValue, fbPassword.TextValue);
            if (CreateClicked != null)
            {
                CreateClicked(this, e);
            }
        }

        private void CreateRadioButtons()
        {
            if (_accountTemplates != null &&
                _accountTemplates.Accounts != null &&
                _accountTemplates.Accounts.Count > 0)
            {
                for (int i = _accountTemplates.Accounts.Count - 1; i >= 0; i--)
                {
                    AccountConfigValues account = _accountTemplates.Accounts[i];
                    panelInnerRadio.Controls.Add(CreateRadioButton(account.EmailProvider, account.Name));
                }
            }
        }

        private void SetIcons(ImageList images)
        {
            if (images != null && panelInnerRadio.Controls.Count > 0)
            {
                foreach (Control control in panelInnerRadio.Controls)
                {
                    if (control is RadioButton)
                    {
                        RadioButton radioButton = (RadioButton)control;
                        int imageIndex = images.Images.IndexOfKey(radioButton.Tag.ToString());
                        if (imageIndex >= 0)
                        {
                            radioButton.Image = images.Images[imageIndex];
                        }
                    }
                }
            }
        }

        private void UpdateChosenTemplate(string emailAddress, string password)
        {
            if (_chosenTemplate != null &&
                !string.IsNullOrEmpty(emailAddress) &&
                !string.IsNullOrEmpty(password))
            {
                string username = emailAddress;

                if (!string.IsNullOrEmpty(_chosenTemplate.ShortUserName))
                {
                    //Take everything on the left of the @ symbol for the username
                    bool shortUserName = false;
                    bool.TryParse(_chosenTemplate.ShortUserName, out shortUserName);
                    if (shortUserName)
                    {
                        int atPos = emailAddress.IndexOf('@');
                        if (atPos > 0)
                        {
                            username = emailAddress.Substring(0, atPos);
                        }
                    }
                }
                _chosenTemplate.PollingConfig.Username = username;
                _chosenTemplate.PollingConfig.PasswordTrue = password;
                _chosenTemplate.SmtpConfig.EmailAddress = emailAddress;
            }
        }

        private RadioButton CreateRadioButton(EmailProviderType type, string text)
        {
            RadioButton returnVal = new RadioButton();

            returnVal.AutoSize = true;
            returnVal.Dock = DockStyle.Left;
            returnVal.ImageAlign = ContentAlignment.TopCenter;
            returnVal.Location = new Point(0, 0);
            returnVal.Name = "radio" + type.ToString();
            returnVal.Padding = new Padding(0, 0, 30, 0);
            returnVal.Size = new Size(72, 72);
            returnVal.Tag = type.ToString();
            returnVal.Text = text;
            returnVal.TextAlign = ContentAlignment.BottomCenter;
            returnVal.UseVisualStyleBackColor = true;
            returnVal.CheckedChanged += new EventHandler(this.radioButton_CheckedChanged);

            return returnVal;
        }
    }
}
