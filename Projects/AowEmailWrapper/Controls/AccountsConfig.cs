using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AowEmailWrapper.ConfigFramework;
using AowEmailWrapper.Localization;


namespace AowEmailWrapper.Controls
{
    public delegate void AccountActivatedEventHandler(object sender, AccountConfigValues theAccount);

    public partial class AccountsConfig : UserControl
    {
        #region Private Members

        private AccountConfigValuesList _accountsList;
        public EventHandler Config_Changed;
        public AccountActivatedEventHandler Account_Activated;
        private const string AccountsTextKey = "tabAccounts";
        private const string AccountPromptTextKey = "msgAccountPrompt";
        private const string AccountDuplicateTextKey = "msgAccountDuplicate";
        private const string AccountActiveTextKey = "activeAccount";
        private const string AccountActiveTemplate = "{0} ({1})";
        private const string Menu_Add_Tag = "menuItemAdd";
        private const string Menu_Remove_Tag = "menuItemRemove";
        private const string Menu_Rename_Tag = "menuItemRename";
        private const string Menu_Activate_Tag = "menuItemActivate";

        ContextMenu _contextMenu;

        #endregion

        #region Public Properties

        public AccountConfigValuesList Config
        {
            get
            {
                return _accountsList; 
            }
            set 
            { 
                _accountsList = value;
                Populate();
            }
        }

        #endregion

        #region Constructors

        public AccountsConfig()
        {
            InitializeComponent();
            CreateContextMenu();
            listViewAccounts.DoubleClick += new EventHandler(listViewAccounts_DoubleClick);
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

        #endregion

        #region Private Methods

        private void Raise_Account_Activated(string theAccountName)
        {
            Raise_Account_Activated(_accountsList.GetAccountByName(theAccountName));
        }

        private void Raise_Account_Activated(AccountConfigValues theAccount)
        {
            if (Account_Activated != null)
            {
                Account_Activated(this, theAccount);
            }
        }

        private void Add()
        {
            if (_accountsList != null &&
                _accountsList.Accounts != null)
            {
                string theName = null;
                DialogResult dialogResult = InputBox(Translator.Translate(AccountsTextKey), Translator.Translate(AccountPromptTextKey), ref theName);

                if (!string.IsNullOrEmpty(theName) && !dialogResult.Equals(DialogResult.Cancel))
                {
                    if (!_accountsList.CheckAccountExistsByName(theName))
                    {
                        _accountsList.Accounts.Add(new AccountConfigValues(true, theName));
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

        private string GetSelectedItem()
        {
            string theTag = null;

            if (listViewAccounts.SelectedItems.Count.Equals(1))
            {
                theTag = listViewAccounts.SelectedItems[0].Tag.ToString();                
            }

            return theTag;
        }

        private void Populate()
        {
            if (_accountsList != null &&
                _accountsList.Accounts != null)
            {
                listViewAccounts.Items.Clear();
                listViewAccounts.View = View.Details;
                listViewAccounts.SuspendLayout();

                if (_accountsList.Accounts.Count > 0)
                {
                    foreach (AccountConfigValues account in _accountsList.Accounts)
                    {
                        ListViewItem item = new ListViewItem();
                        if (account.Equals(_accountsList.ActiveAccount))
                        {
                            item.Text = string.Format(AccountActiveTemplate, account.Name, Translator.Translate(AccountActiveTextKey));
                        }
                        else
                        {
                            item.Text = account.Name;
                        }

                        item.Tag = account.Name;

                        listViewAccounts.Items.Add(item);
                    }

                    listViewAccounts.Columns[0].Width = listViewAccounts.Width - 25;
                }

                listViewAccounts.ResumeLayout();
            }
        }

        private void Raise_Config_Changed()
        {
            if (Config_Changed != null)
            {
                Config_Changed(this, new EventArgs());
            }
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

            _contextMenu.MenuItems.AddRange(new MenuItem[] { add, remove, rename, activate });

            _contextMenu.Popup += new EventHandler(ContextMenu_Popup);

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

            listViewAccounts.ContextMenu = _contextMenu;
        }

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
            }
        }

        #endregion
    }
}
