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
        private const string RadioButtonNameTemplate = "radio{0}";

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
            EventHandler textBoxTextChanged = new EventHandler(textBox_TextChanged);
            KeyEventHandler textBoxKeyDown = new KeyEventHandler(textBox_KeyDown);
            
            fbEmailAddress.InnerTextBox.TextChanged += textBoxTextChanged;
            fbEmailAddress.InnerTextBox.KeyDown += textBoxKeyDown;

            fbPassword.InnerTextBox.TextChanged += textBoxTextChanged;
            fbPassword.InnerTextBox.KeyDown += textBoxKeyDown;
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
                _chosenTemplate.Name = theButton.Text;

                CheckCreateEnabled();

                switch (_chosenTemplate.EmailProvider)
                {
                    case EmailProviderType.Google:
                    case EmailProviderType.WindowsLive:
                    case EmailProviderType.Yahoo:
                        labelDomainsMessage.Text = _chosenTemplate.Domains;
                        break;
                    default:
                        labelDomainsMessage.Text = Translator.Translate(InputEmailSettingsManual);
                        fbEmailAddress.TextValue = string.Empty;
                        fbPassword.TextValue = string.Empty;
                        break;
                }
            }
        }

        private void radioButton_MouseUp(object sender, MouseEventArgs e)
        {
            SetTextBoxFocus();
        }

        private void radioButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                SetTextBoxFocus();
            }
        }

        private void SetTextBoxFocus()
        {
            if (_chosenTemplate != null)
            {
                switch (_chosenTemplate.EmailProvider)
                {
                    case EmailProviderType.Google:
                    case EmailProviderType.WindowsLive:
                    case EmailProviderType.Yahoo:
                        fbEmailAddress.InnerTextBox.Focus();
                        break;
                    default:
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

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            if (buttonCreate.Enabled)
            {
                UpdateChosenTemplate(fbEmailAddress.TextValue, fbPassword.TextValue);
                if (CreateClicked != null)
                {
                    CreateClicked(this, e);
                }
            }
        }

        private void CreateRadioButtons()
        {
            if (_accountTemplates != null &&
                _accountTemplates.Accounts != null &&
                _accountTemplates.Accounts.Count > 0)
            {
                this.SuspendLayout();

                foreach (AccountConfigValues account in _accountTemplates.Accounts)
                {
                    RadioButton theButton = CreateRadioButton(account.EmailProvider, account.Name);
                    theButton.TabIndex = panelInnerRadio.Controls.Count + 1;
                    panelInnerRadio.Controls.Add(theButton);
                    theButton.BringToFront();
                }

                this.ResumeLayout();
            }
        }

        private void SetIcons(ImageList images)
        {
            if (images != null && panelInnerRadio.Controls.Count > 0)
            {
                this.SuspendLayout();

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

                this.ResumeLayout();
            }
        }

        private void UpdateChosenTemplate(string emailAddress, string password)
        {
            if (_chosenTemplate != null &&
                !string.IsNullOrEmpty(emailAddress) &&
                !string.IsNullOrEmpty(password))
            {
                string emailUser = GetEmailUser(emailAddress);
                bool shortUser = false;
                bool.TryParse(_chosenTemplate.ShortUserName, out shortUser);

                _chosenTemplate.PollingConfig.Username = (shortUser && !string.IsNullOrEmpty(emailUser)) ? emailUser : emailAddress;
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
            returnVal.Name = string.Format(RadioButtonNameTemplate, type.ToString());
            returnVal.Padding = new Padding(0, 0, 30, 0);
            returnVal.Size = new Size(72, 72);
            returnVal.TabStop = true;
            returnVal.Tag = type.ToString();
            string translated = Translator.Translate(returnVal.Name);
            returnVal.Text = !string.IsNullOrEmpty(translated) ? translated : text;
            returnVal.TextAlign = ContentAlignment.BottomCenter;
            returnVal.UseVisualStyleBackColor = true;
            returnVal.CheckedChanged += new EventHandler(this.radioButton_CheckedChanged);
            returnVal.KeyDown += new KeyEventHandler(this.radioButton_KeyDown);
            returnVal.MouseUp += new MouseEventHandler(radioButton_MouseUp);

            return returnVal;
        }

        private string GetEmailUser(string emailAddress)
        {
            string username = null;

            int atPos = emailAddress.IndexOf('@');
            if (atPos > 0)
            {
                username = emailAddress.Substring(0, atPos);
            }

            return username;
        }
    }
}
