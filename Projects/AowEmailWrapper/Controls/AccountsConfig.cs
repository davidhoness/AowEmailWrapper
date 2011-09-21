﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using AowEmailWrapper.ConfigFramework;
using AowEmailWrapper.Localization;


namespace AowEmailWrapper.Controls
{
    public delegate void AccountActivatedEventHandler(object sender, AccountConfigValues theAccount, bool dirty);

    public partial class AccountsConfig : UserControl
    {
        #region Private Members

        private AccountConfigValuesList _accountsList;
        private AccountConfigValuesList _accountsTemplates;
        public EventHandler Config_Changed;
        public AccountActivatedEventHandler Account_Activated;
        private const string AccountsTextKey = "tabAccounts";
        private const string AccountPromptTextKey = "msgAccountPrompt";
        private const string AccountDeletePromptTextKey = "msgAccountDeletePrompt";
        private const string AccountDuplicateTextKey = "msgAccountDuplicate";
        private const string AccountActiveTextKey = "activeAccount";
        private const string AccountStartUpTextKey = "startUpAccount";
        private const string AccountStatusTemplate = "{0} ({1})";
        private const string AccountTwinStatusTemplate = "{0} ({1} {2})";
        private const string Menu_Add_Tag = "menuItemAdd";
        private const string Menu_Remove_Tag = "menuItemRemove";
        private const string Menu_Rename_Tag = "menuItemRename";
        private const string Menu_Activate_Tag = "menuItemActivate";
        private const string Menu_SetStartUp_Tag = "menuItemSetStartUp";
        private string DefaultImageKey = EmailProviderType.Other.ToString();

        private Font ActiveFont = null;
        private Font NormalFont = null;
        
        private bool _configChanged = false;

        ContextMenu _contextMenu;

        #endregion

        #region Public Properties

        public AccountConfigValuesList Config
        {
            get
            {
                if (_configChanged)
                {
                    Scrape();
                    _configChanged = false;
                }
                return _accountsList; 
            }
            set 
            { 
                _accountsList = value;
                Populate();
                _configChanged = false;
            }
        }

        public AccountConfigValuesList AccountsTemplates
        {
            get { return _accountsTemplates; }
            set { _accountsTemplates = value; }
        }

        #endregion

        #region Constructors

        public AccountsConfig()
        {
            InitializeComponent();

            ActiveFont = new Font(this.Font, FontStyle.Bold);
            NormalFont = new Font(this.Font, FontStyle.Regular);

            CreateContextMenu();
            listViewAccounts.DoubleClick += new EventHandler(listViewAccounts_DoubleClick);
            listViewAccounts.SelectedIndexChanged += new EventHandler(listViewAccounts_SelectedIndexChanged);

            EventHandler clearSelected = new EventHandler(listViewAccounts_SelectedItems_Clear);
            tabControlAccountEditor.Enter += clearSelected;
            this.Leave += clearSelected;

            EventHandler raiseConfigChange = new EventHandler(Raise_Config_Changed);
            pollingConfig.Config_Changed += raiseConfigChange;
            smtpConfig.Config_Changed += raiseConfigChange;

            listViewAccounts.KeyDown += new KeyEventHandler(listViewAccounts_KeyDown);
        }

        #endregion

        #region Public Methods

        public override void Refresh()
        {
            base.Refresh();
            Populate();
        }

        #endregion

        #region Event Handlers

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            Remove();
        }

        private void buttonRename_Click(object sender, EventArgs e)
        {
            Rename();
        }        

        private void buttonActivate_Click(object sender, EventArgs e)
        {
            Activate();
        }

        private void listViewAccounts_DoubleClick(object sender, EventArgs e)
        {
            Activate();
        }

        private void buttonSetStartUp_Click(object sender, EventArgs e)
        {
            SetAsStartUp();
        }

        private void listViewAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckEnabled();
        }

        private void listViewAccounts_SelectedItems_Clear(object sender, EventArgs e)
        {
            listViewAccounts.SelectedItems.Clear();
        }

        private void listViewAccounts_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                    Rename();
                    break;
                case Keys.Delete:
                    Remove();
                    break;
            }
        }

        #endregion

        #region Private Methods

        private void CheckEnabled()
        {
            bool enabled = listViewAccounts.SelectedItems.Count > 0;

            foreach (MenuItem menu in _contextMenu.MenuItems)
            {
                if (!menu.Tag.ToString().Equals(Menu_Add_Tag))
                {
                    menu.Enabled = enabled;
                }
            }

            buttonRemove.Enabled = enabled;
            buttonRename.Enabled = enabled;
            buttonActivate.Enabled = enabled;
            buttonSetStartUp.Enabled = enabled;
        }

        private void Raise_Config_Changed()
        {
            Raise_Config_Changed(null, null);
        }

        private void Raise_Config_Changed(object sender, EventArgs e)
        {
            _configChanged = true;
            if (Config_Changed != null)
            {
                Config_Changed(this, e);
            }
        }

        private void Raise_Account_Activated(string theAccountName)
        {
            Raise_Account_Activated(_accountsList.GetAccountByName(theAccountName));
        }

        private void Raise_Account_Activated(AccountConfigValues theAccount)
        {
            Raise_Account_Activated(theAccount, false);
        }

        private void Raise_Account_Activated(AccountConfigValues theAccount, bool dirty)
        {
            if (Account_Activated != null)
            {
                Account_Activated(this, theAccount, dirty);
            }
        }

        public void Add()
        {
            AccountConfigValues theNewAccount = null;

            using (AccountsCreationForm createForm = new AccountsCreationForm())
            {
                createForm.Name = createForm.GetType().Name;
                createForm.AccountTemplates = _accountsTemplates;
                createForm.RadioImages = imageListLargeIcons;
                if (createForm.ShowDialog(this).Equals(DialogResult.OK))
                {
                    theNewAccount = createForm.ChosenTemplate;
                }
            }

            if (theNewAccount != null)
            {
                theNewAccount.Domains = null; //Don't need to save this

                if (_accountsList.CheckAccountExistsByName(theNewAccount.Name))
                {
                    //That name does exist, make a new name

                    int num = 0;
                    bool success = false;
                    string proposedName = null;

                    do
                    {
                        num++;
                        proposedName = string.Format(AccountStatusTemplate, theNewAccount.Name, num);
                        success = !_accountsList.CheckAccountExistsByName(proposedName);

                    } while (!success);

                    theNewAccount.Name = proposedName;
                }

                _accountsList.Accounts.Add(theNewAccount);
                Raise_Account_Activated(theNewAccount, true);
            }
        }

        private void Remove()
        {
            if (_accountsList != null &&
                _accountsList.Accounts != null)
            {
                if (!_accountsList.Accounts.Count.Equals(1))
                {
                    string theName = GetSelectedItem();

                    AccountConfigValues theAccount = _accountsList.GetAccountByName(theName);

                    if (theAccount != null)
                    {
                        if (MessageBox.Show(Translator.Translate(AccountDeletePromptTextKey, theAccount.Name), Translator.Translate(AccountsTextKey), MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
                        {
                            if (theAccount.Equals(_accountsList.ActiveAccount))
                            {
                                _accountsList.Accounts.Remove(theAccount);
                                Raise_Account_Activated(_accountsList.Accounts[0]);
                            }
                            else
                            {
                                _accountsList.Accounts.Remove(theAccount);
                            }

                            Populate();
                            Raise_Config_Changed();
                        }
                    }
                }
            }
        }

        private void Rename()
        {
            if (_accountsList != null &&
                _accountsList.Accounts != null)
            {
                string beforeName = GetSelectedItem();
                string theName = beforeName;

                AccountConfigValues theAccount = _accountsList.GetAccountByName(theName);

                if (theAccount != null)
                {
                    DialogResult dialogResult = InputBox(Translator.Translate(AccountsTextKey), Translator.Translate(AccountPromptTextKey), ref theName);

                    if (!dialogResult.Equals(DialogResult.Cancel) &&
                        !string.IsNullOrEmpty(theName) &&
                        !beforeName.Equals(theName))
                    {
                        if (!_accountsList.CheckAccountExistsByName(theName))
                        {
                            if (theAccount.Equals(_accountsList.ActiveAccount))
                            {
                                _accountsList.ActiveAccountName = theName;
                                theAccount.Name = theName;
                            }
                            else
                            {
                                theAccount.Name = theName;
                            }

                            Populate();
                            Raise_Config_Changed();
                        }
                        else
                        {
                            MessageBox.Show(Translator.Translate(AccountDuplicateTextKey), Translator.Translate(AccountsTextKey), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        private void Activate()
        {
            string theName = GetSelectedItem();
            if (!string.IsNullOrEmpty(theName))
            {
                Raise_Account_Activated(_accountsList.GetAccountByName(theName));
            }
        }

        private void SetAsStartUp()
        {
            if (_accountsList != null &&
                _accountsList.Accounts != null)
            {
                string theName = GetSelectedItem();

                AccountConfigValues theAccount = _accountsList.GetAccountByName(theName);
                if (theAccount != null)
                {
                    _accountsList.StartUpAccountName = theAccount.Name;

                    Populate();
                    Raise_Config_Changed();
                }
            }
        }

        private string GetSelectedItem()
        {
            string theTag = null;

            if (listViewAccounts.SelectedItems.Count.Equals(1))
            {
                theTag = listViewAccounts.SelectedItems[0].Tag.ToString();
            }

            return theTag;
        }

        private int GetSlectedIndex()
        {
            int selected = -1;

            if (listViewAccounts.SelectedIndices.Count.Equals(1))
            {
                selected = listViewAccounts.SelectedIndices[0];
            }

            return selected;
        }

        private void Populate()
        {
            if (_accountsList != null &&
                _accountsList.Accounts != null)
            {
                listViewAccounts.SelectedItems.Clear();
                listViewAccounts.Items.Clear();
                listViewAccounts.BeginUpdate();

                if (_accountsList.Accounts.Count > 0)
                {
                    foreach (AccountConfigValues account in _accountsList.Accounts)
                    {
                        ListViewItem item = CreateListItem(account);

                        listViewAccounts.Items.Add(item);

                        if (account.Equals(_accountsList.ActiveAccount))
                        {
                            pollingConfig.Config = account.PollingConfig;
                            smtpConfig.Config = account.SmtpConfig;
                            item.EnsureVisible();
                        }
                    }
                }

                listViewAccounts.EndUpdate();
            }
        }

        private ListViewItem CreateListItem(AccountConfigValues account)
        {
            ListViewItem item = new ListViewItem();

            if (account.Equals(_accountsList.ActiveAccount))
            {
                if (account.Equals(_accountsList.StartUpAccount))
                {
                    item.Text = string.Format(AccountTwinStatusTemplate, account.Name, Translator.Translate(AccountActiveTextKey), Translator.Translate(AccountStartUpTextKey));
                }
                else
                {
                    item.Text = string.Format(AccountStatusTemplate, account.Name, Translator.Translate(AccountActiveTextKey));
                }

                item.Font = ActiveFont;
            }
            else
            {
                if (account.Equals(_accountsList.StartUpAccount))
                {
                    item.Text = string.Format(AccountStatusTemplate, account.Name, Translator.Translate(AccountStartUpTextKey));
                }
                else
                {
                    item.Text = account.Name;
                }

                item.ForeColor = Color.Gray;
                item.Font = NormalFont;
            }

            item.Tag = account.Name;

            int imageIndex = -1;
            if (account.SmtpConfig != null &&
                !string.IsNullOrEmpty(account.SmtpConfig.EmailAddress))
            {
                imageIndex = imageListLargeIcons.Images.IndexOfKey(GetEmailProviderType(account.SmtpConfig.EmailAddress));
            }

            item.ImageIndex = (imageIndex >= 0) ? imageIndex : imageListLargeIcons.Images.IndexOfKey(EmailProviderType.Other.ToString());

            return item;
        }

        private void Scrape()
        {
            if (_accountsList != null && _accountsList.ActiveAccount != null)
            {
                _accountsList.ActiveAccount.PollingConfig = pollingConfig.Config;
                _accountsList.ActiveAccount.SmtpConfig = smtpConfig.Config;
            }
        }

        private string GetEmailProviderType(string input)
        {
            EmailProviderType returnVal = EmailProviderType.Other;

            if (_accountsTemplates != null &&
                _accountsTemplates.Accounts!=null &&
                _accountsTemplates.Accounts.Count >0)
            {
                bool success = false;

                foreach (AccountConfigValues account in _accountsTemplates.Accounts)
                {
                    if (!string.IsNullOrEmpty(account.Domains))
                    {
                        string[] split = account.Domains.Split(',');
                        if (split.Length > 0)
                        {
                            foreach (string s in split)
                            {
                                if (!string.IsNullOrEmpty(s))
                                {
                                    success = input.Contains(s.Trim());
                                    if (success)
                                    {
                                        returnVal = account.EmailProvider;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    if (success) break;
                }
            }

            return returnVal.ToString();
        }

        private bool CheckDomains(string input, string[] domains)
        {
            bool returnVal = false;

            if (!string.IsNullOrEmpty(input))
            {
                foreach (string s in domains)
                {
                    if (input.ToLower().Contains(s))
                    {
                        returnVal = true;
                        break;
                    }
                }
            }

            return returnVal;
        }

        private static DialogResult InputBox(string title, string promptText, ref string value)
        {
            DialogResult dialogResult;

            using (Form form = new Form())
            {
                Label label = new Label();
                TextBox textBox = new TextBox();
                Button buttonOk = new Button();
                Button buttonCancel = new Button();

                form.Text = title;
                label.Text = promptText;
                textBox.Text = value;

                buttonOk.Text = Translator.Translate("buttonOK");
                buttonCancel.Text = Translator.Translate("buttonCancel");
                buttonOk.DialogResult = DialogResult.OK;
                buttonCancel.DialogResult = DialogResult.Cancel;

                label.SetBounds(9, 20, 372, 13);
                textBox.SetBounds(12, 36, 372, 20);
                buttonOk.SetBounds(228, 72, 75, 23);
                buttonCancel.SetBounds(309, 72, 75, 23);

                label.AutoSize = true;
                textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
                buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

                form.ClientSize = new Size(396, 107);
                form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
                form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.StartPosition = FormStartPosition.CenterParent;
                form.MinimizeBox = false;
                form.MaximizeBox = false;
                form.AcceptButton = buttonOk;
                form.CancelButton = buttonCancel;
                dialogResult = form.ShowDialog();

                if (dialogResult.Equals(DialogResult.OK))
                {
                    value = textBox.Text;
                }

                label.Dispose();
                textBox.Dispose();
                buttonOk.Dispose();
                buttonCancel.Dispose();
            }

            return dialogResult;
        }

        #endregion

        #region Context Menu

        private void CreateContextMenu()
        {
            _contextMenu = new ContextMenu();

            int indexCount = 0;

            EventHandler menuItemClickEvent = new EventHandler(ContextMenu_Click);
            _contextMenu = new ContextMenu();

            MenuItem add = new MenuItem();
            MenuItem remove = new MenuItem();
            MenuItem rename = new MenuItem();
            MenuItem activate = new MenuItem();
            MenuItem setStartUp = new MenuItem();

            _contextMenu.MenuItems.AddRange(new MenuItem[] { add, remove, rename, activate, setStartUp });

            add.Index = indexCount;
            add.Text = Translator.Translate(Menu_Add_Tag);
            add.Tag = Menu_Add_Tag;
            add.Click += menuItemClickEvent;

            indexCount++;
            remove.Index = indexCount;
            remove.Text = Translator.Translate(Menu_Remove_Tag);
            remove.Tag = Menu_Remove_Tag;
            remove.Click += menuItemClickEvent;

            indexCount++;
            rename.Index = indexCount;
            rename.Text = Translator.Translate(Menu_Rename_Tag);
            rename.Tag = Menu_Rename_Tag;
            rename.Click += menuItemClickEvent;

            indexCount++;
            activate.Index = indexCount;
            activate.Text = Translator.Translate(Menu_Activate_Tag);
            activate.Tag = Menu_Activate_Tag;
            activate.Click += menuItemClickEvent;

            indexCount++;
            setStartUp.Index = indexCount;
            setStartUp.Text = Translator.Translate(Menu_SetStartUp_Tag);
            setStartUp.Tag = Menu_SetStartUp_Tag;
            setStartUp.Click += menuItemClickEvent;

            listViewAccounts.ContextMenu = _contextMenu;
        }

        /*
        private void ContextMenu_Popup(object sender, EventArgs e)
        {
            bool enabled = listViewAccounts.SelectedItems.Count > 0;
            foreach (MenuItem menu in _contextMenu.MenuItems)
            {
                if (!menu.Tag.ToString().Equals(Menu_Add_Tag))
                {
                    menu.Enabled = enabled;
                }
            }
        }
        */

        private void ContextMenu_Click(object sender, EventArgs e)
        {
            switch (((MenuItem)sender).Tag.ToString())
            {
                case Menu_Add_Tag:
                    Add();
                    break;
                case Menu_Remove_Tag:
                    Remove();
                    break;
                case Menu_Rename_Tag:
                    Rename();
                    break;
                case Menu_Activate_Tag:
                    Activate();
                    break;
                case Menu_SetStartUp_Tag:
                    SetAsStartUp();
                    break;
            }
        }

        #endregion

    }
}
