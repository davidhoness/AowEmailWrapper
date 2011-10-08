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

namespace AowEmailWrapper.Controls
{
    public partial class AccountsCreationWizzard : UserControl
    {
        private AccountConfigValuesList _accountTemplates;
        private AccountConfigValues _chosenTemplate;
        private const string InputEmailSettingsManual = "msgInputEmailSettingsManual";
        private const string OtherAccountTranslationKey = "accountOther";
        private const string OtherAccountType = "Other";

        private ImageList _templateIcons;

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
                Populate();
            }
        }

        public ImageList TemplateIcons
        {
            get { return _templateIcons; }
            set
            {
                _templateIcons = value;
                listViewTemplates.SmallImageList = _templateIcons;
            }
        }

        public AccountsCreationWizzard()
        {
            InitializeComponent();
            EventHandler textBoxTextChanged = new EventHandler(textBox_TextChanged);
            KeyEventHandler textBoxKeyDown = new KeyEventHandler(textBox_KeyDown);

            fbEmailAddress.InnerTextBox.TextChanged += textBoxTextChanged;
            fbEmailAddress.InnerTextBox.Validated += new EventHandler(textBox_Validated);
            fbEmailAddress.InnerTextBox.KeyDown += textBoxKeyDown;

            fbPassword.InnerTextBox.TextChanged += textBoxTextChanged;
            fbPassword.InnerTextBox.KeyDown += textBoxKeyDown;

            listViewTemplates.SelectedIndexChanged += new EventHandler(listViewTemplates_SelectedIndexChanged);
        }

        private void listViewTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSelectedTemplate();
        }

        private void UpdateSelectedTemplate()
        {
            if (_accountTemplates != null &&
                _accountTemplates.Accounts != null &&
                _accountTemplates.Accounts.Count > 0 &&
                listViewTemplates.SelectedIndices.Count.Equals(1))
            {
                string selectedEmailType = listViewTemplates.SelectedItems[0].Tag.ToString();
                AccountConfigValues selectedTemplate = _accountTemplates.Accounts.Find(account => account.EmailProvider.Equals(selectedEmailType));

                txtDomainInfo.Text = GetDomains(selectedTemplate.TemplateDomains);

                //Create a copy of it in memory (don't modify the original)
                _chosenTemplate = XmlHelper.Deserialize<AccountConfigValues>(XmlHelper.Serialize(selectedTemplate));
                _chosenTemplate.Name = selectedTemplate.Name;

                CheckCreateEnabled();

                if (_chosenTemplate.EmailProvider.Equals(OtherAccountType, StringComparison.InvariantCultureIgnoreCase))
                {
                    txtDomainInfo.Text = Translator.Translate(InputEmailSettingsManual);
                    _chosenTemplate.Name = Translator.Translate(OtherAccountTranslationKey);
                }
            }
        }

        private void CheckCreateEnabled()
        {
            if (_chosenTemplate != null)
            {
                buttonCreate.Enabled = fbEmailAddress.TextValue.Length > 0 && fbPassword.TextValue.Length > 0;
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

        private void textBox_Validated(object sender, EventArgs e)
        {
            SelectCorrectEmailProvider();
        }

        private void SelectCorrectEmailProvider()
        {
            string theType = _accountTemplates.GetEmailProviderType(fbEmailAddress.TextValue);

            if (string.IsNullOrEmpty(theType))
            {
                theType = OtherAccountType;
            }

            AccountConfigValues theAccountType = _accountTemplates.Accounts.Find(item => item.EmailProvider.Equals(theType, StringComparison.InvariantCultureIgnoreCase));

            ListViewItem[] theItems = listViewTemplates.Items.Find(theAccountType.EmailProvider, true);

            //Only change the selection if it's incorrect
            if (theItems != null &&
                theItems.Length > 0)
            {
                listViewTemplates.SelectedItems.Clear();
                theItems[0].Selected = true;
                theItems[0].EnsureVisible();
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

        private void Populate()
        {
            if (_accountTemplates != null &&
                _accountTemplates.Accounts != null &&
                _accountTemplates.Accounts.Count > 0)
            {
                listViewTemplates.BeginUpdate();

                foreach (AccountConfigValues account in _accountTemplates.Accounts)
                {
                    bool isTypeOther = account.EmailProvider.Equals(OtherAccountType, StringComparison.InvariantCultureIgnoreCase);
                    string name = isTypeOther ? Translator.Translate(OtherAccountTranslationKey) : account.Name;

                    int imageIndex = _templateIcons.Images.IndexOfKey(account.EmailProvider);

                    listViewTemplates.Items.Add(account.EmailProvider, name, imageIndex > 0 ? imageIndex : 0);
                    listViewTemplates.Items[listViewTemplates.Items.Count-1].Tag = account.EmailProvider;
                }

                listViewTemplates.EndUpdate();
            }
        }

        private string GetDomains(List<string> domains)
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i<domains.Count;i++)
            {
                sb.Append(domains[i]);
                if (i < domains.Count - 1)
                {
                    sb.Append(", ");
                }
            }

            return sb.ToString();
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
