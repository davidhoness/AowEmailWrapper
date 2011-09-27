namespace AowEmailWrapper.Controls
{
    partial class AccountsConfig
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountsConfig));
            AowEmailWrapper.ConfigFramework.PollingConfigValues pollingConfigValues1 = new AowEmailWrapper.ConfigFramework.PollingConfigValues();
            AowEmailWrapper.ConfigFramework.SmtpConfigValues smtpConfigValues1 = new AowEmailWrapper.ConfigFramework.SmtpConfigValues();
            this.panelTopHalf = new System.Windows.Forms.Panel();
            this.listViewAccounts = new System.Windows.Forms.ListView();
            this.colName = new System.Windows.Forms.ColumnHeader();
            this.colAddress = new System.Windows.Forms.ColumnHeader();
            this.colDefault = new System.Windows.Forms.ColumnHeader();
            this.colHidden = new System.Windows.Forms.ColumnHeader();
            this.imageListIcons = new System.Windows.Forms.ImageList(this.components);
            this.panelButtons = new System.Windows.Forms.Panel();
            this.panelSetStartUp = new System.Windows.Forms.Panel();
            this.buttonSetStartUp = new System.Windows.Forms.Button();
            this.panelActivate = new System.Windows.Forms.Panel();
            this.buttonActivate = new System.Windows.Forms.Button();
            this.panelRename = new System.Windows.Forms.Panel();
            this.buttonRename = new System.Windows.Forms.Button();
            this.panelRemove = new System.Windows.Forms.Panel();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.panelAdd = new System.Windows.Forms.Panel();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.panelBottomHalf = new System.Windows.Forms.Panel();
            this.tabControlAccountEditor = new System.Windows.Forms.TabControl();
            this.tabIncoming = new System.Windows.Forms.TabPage();
            this.tabOutgoing = new System.Windows.Forms.TabPage();
            this.pollingConfig = new AowEmailWrapper.Controls.PollingConfig();
            this.smtpConfig = new AowEmailWrapper.Controls.SmtpConfig();
            this.panelTopHalf.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.panelSetStartUp.SuspendLayout();
            this.panelActivate.SuspendLayout();
            this.panelRename.SuspendLayout();
            this.panelRemove.SuspendLayout();
            this.panelAdd.SuspendLayout();
            this.panelBottomHalf.SuspendLayout();
            this.tabControlAccountEditor.SuspendLayout();
            this.tabIncoming.SuspendLayout();
            this.tabOutgoing.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTopHalf
            // 
            this.panelTopHalf.Controls.Add(this.listViewAccounts);
            this.panelTopHalf.Controls.Add(this.panelButtons);
            this.panelTopHalf.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopHalf.Location = new System.Drawing.Point(0, 0);
            this.panelTopHalf.Name = "panelTopHalf";
            this.panelTopHalf.Size = new System.Drawing.Size(531, 205);
            this.panelTopHalf.TabIndex = 0;
            // 
            // listViewAccounts
            // 
            this.listViewAccounts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colAddress,
            this.colDefault,
            this.colHidden});
            this.listViewAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewAccounts.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewAccounts.FullRowSelect = true;
            this.listViewAccounts.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewAccounts.Location = new System.Drawing.Point(0, 0);
            this.listViewAccounts.MultiSelect = false;
            this.listViewAccounts.Name = "listViewAccounts";
            this.listViewAccounts.Size = new System.Drawing.Size(418, 205);
            this.listViewAccounts.SmallImageList = this.imageListIcons;
            this.listViewAccounts.TabIndex = 3;
            this.listViewAccounts.UseCompatibleStateImageBehavior = false;
            this.listViewAccounts.View = System.Windows.Forms.View.Details;
            // 
            // colName
            // 
            this.colName.Tag = "ContentHeaderMax";
            this.colName.Text = "Account";
            this.colName.Width = 100;
            // 
            // colAddress
            // 
            this.colAddress.Tag = "Fill";
            this.colAddress.Text = "Address";
            // 
            // colDefault
            // 
            this.colDefault.Tag = "ContentHeaderMax";
            this.colDefault.Text = "Default";
            // 
            // colHidden
            // 
            this.colHidden.Tag = "Fixed;0";
            this.colHidden.Text = "";
            this.colHidden.Width = 25;
            // 
            // imageListIcons
            // 
            this.imageListIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListIcons.ImageStream")));
            this.imageListIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListIcons.Images.SetKeyName(0, "Other");
            this.imageListIcons.Images.SetKeyName(1, "Google");
            this.imageListIcons.Images.SetKeyName(2, "WindowsLive");
            this.imageListIcons.Images.SetKeyName(3, "Yahoo");
            this.imageListIcons.Images.SetKeyName(4, "AOL");
            this.imageListIcons.Images.SetKeyName(5, "Mail.ru");
            this.imageListIcons.Images.SetKeyName(6, "Yandex.ru");
            this.imageListIcons.Images.SetKeyName(7, "Rambler.ru");
            this.imageListIcons.Images.SetKeyName(8, "Inbox.com");
            this.imageListIcons.Images.SetKeyName(9, "Mail.com1");
            this.imageListIcons.Images.SetKeyName(10, "Mail.com");
            this.imageListIcons.Images.SetKeyName(11, "GMX1");
            this.imageListIcons.Images.SetKeyName(12, "GMX");
            this.imageListIcons.Images.SetKeyName(13, "Web.de");
            this.imageListIcons.Images.SetKeyName(14, "Freenet.de");
            this.imageListIcons.Images.SetKeyName(15, "t-online.de");
            this.imageListIcons.Images.SetKeyName(16, "Onet.pl");
            this.imageListIcons.Images.SetKeyName(17, "Wp.pl");
            this.imageListIcons.Images.SetKeyName(18, "o2.pl");
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.panelSetStartUp);
            this.panelButtons.Controls.Add(this.panelActivate);
            this.panelButtons.Controls.Add(this.panelRename);
            this.panelButtons.Controls.Add(this.panelRemove);
            this.panelButtons.Controls.Add(this.panelAdd);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtons.Location = new System.Drawing.Point(418, 0);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.panelButtons.Size = new System.Drawing.Size(113, 205);
            this.panelButtons.TabIndex = 2;
            // 
            // panelSetStartUp
            // 
            this.panelSetStartUp.Controls.Add(this.buttonSetStartUp);
            this.panelSetStartUp.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSetStartUp.Location = new System.Drawing.Point(5, 168);
            this.panelSetStartUp.Name = "panelSetStartUp";
            this.panelSetStartUp.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.panelSetStartUp.Size = new System.Drawing.Size(108, 42);
            this.panelSetStartUp.TabIndex = 4;
            // 
            // buttonSetStartUp
            // 
            this.buttonSetStartUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSetStartUp.Enabled = false;
            this.buttonSetStartUp.Location = new System.Drawing.Point(0, 0);
            this.buttonSetStartUp.Name = "buttonSetStartUp";
            this.buttonSetStartUp.Size = new System.Drawing.Size(108, 37);
            this.buttonSetStartUp.TabIndex = 1;
            this.buttonSetStartUp.Text = "Set as StartUp";
            this.buttonSetStartUp.UseVisualStyleBackColor = true;
            this.buttonSetStartUp.Click += new System.EventHandler(this.buttonSetStartUp_Click);
            // 
            // panelActivate
            // 
            this.panelActivate.Controls.Add(this.buttonActivate);
            this.panelActivate.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelActivate.Location = new System.Drawing.Point(5, 126);
            this.panelActivate.Name = "panelActivate";
            this.panelActivate.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.panelActivate.Size = new System.Drawing.Size(108, 42);
            this.panelActivate.TabIndex = 3;
            // 
            // buttonActivate
            // 
            this.buttonActivate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonActivate.Enabled = false;
            this.buttonActivate.Location = new System.Drawing.Point(0, 0);
            this.buttonActivate.Name = "buttonActivate";
            this.buttonActivate.Size = new System.Drawing.Size(108, 37);
            this.buttonActivate.TabIndex = 1;
            this.buttonActivate.Text = "Activate";
            this.buttonActivate.UseVisualStyleBackColor = true;
            this.buttonActivate.Click += new System.EventHandler(this.buttonActivate_Click);
            // 
            // panelRename
            // 
            this.panelRename.Controls.Add(this.buttonRename);
            this.panelRename.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelRename.Location = new System.Drawing.Point(5, 84);
            this.panelRename.Name = "panelRename";
            this.panelRename.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.panelRename.Size = new System.Drawing.Size(108, 42);
            this.panelRename.TabIndex = 2;
            // 
            // buttonRename
            // 
            this.buttonRename.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonRename.Enabled = false;
            this.buttonRename.Location = new System.Drawing.Point(0, 0);
            this.buttonRename.Name = "buttonRename";
            this.buttonRename.Size = new System.Drawing.Size(108, 37);
            this.buttonRename.TabIndex = 1;
            this.buttonRename.Text = "Rename";
            this.buttonRename.UseVisualStyleBackColor = true;
            this.buttonRename.Click += new System.EventHandler(this.buttonRename_Click);
            // 
            // panelRemove
            // 
            this.panelRemove.Controls.Add(this.buttonRemove);
            this.panelRemove.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelRemove.Location = new System.Drawing.Point(5, 42);
            this.panelRemove.Name = "panelRemove";
            this.panelRemove.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.panelRemove.Size = new System.Drawing.Size(108, 42);
            this.panelRemove.TabIndex = 1;
            // 
            // buttonRemove
            // 
            this.buttonRemove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonRemove.Enabled = false;
            this.buttonRemove.Location = new System.Drawing.Point(0, 0);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(108, 37);
            this.buttonRemove.TabIndex = 1;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // panelAdd
            // 
            this.panelAdd.Controls.Add(this.buttonAdd);
            this.panelAdd.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelAdd.Location = new System.Drawing.Point(5, 0);
            this.panelAdd.Name = "panelAdd";
            this.panelAdd.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.panelAdd.Size = new System.Drawing.Size(108, 42);
            this.panelAdd.TabIndex = 0;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonAdd.Location = new System.Drawing.Point(0, 0);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(108, 37);
            this.buttonAdd.TabIndex = 0;
            this.buttonAdd.Text = "Add New";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // panelBottomHalf
            // 
            this.panelBottomHalf.Controls.Add(this.tabControlAccountEditor);
            this.panelBottomHalf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBottomHalf.Location = new System.Drawing.Point(0, 205);
            this.panelBottomHalf.Name = "panelBottomHalf";
            this.panelBottomHalf.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.panelBottomHalf.Size = new System.Drawing.Size(531, 376);
            this.panelBottomHalf.TabIndex = 3;
            // 
            // tabControlAccountEditor
            // 
            this.tabControlAccountEditor.Controls.Add(this.tabIncoming);
            this.tabControlAccountEditor.Controls.Add(this.tabOutgoing);
            this.tabControlAccountEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlAccountEditor.Location = new System.Drawing.Point(0, 10);
            this.tabControlAccountEditor.Name = "tabControlAccountEditor";
            this.tabControlAccountEditor.SelectedIndex = 0;
            this.tabControlAccountEditor.Size = new System.Drawing.Size(531, 366);
            this.tabControlAccountEditor.TabIndex = 0;
            // 
            // tabIncoming
            // 
            this.tabIncoming.Controls.Add(this.pollingConfig);
            this.tabIncoming.Location = new System.Drawing.Point(4, 22);
            this.tabIncoming.Name = "tabIncoming";
            this.tabIncoming.Padding = new System.Windows.Forms.Padding(3);
            this.tabIncoming.Size = new System.Drawing.Size(523, 340);
            this.tabIncoming.TabIndex = 0;
            this.tabIncoming.Text = "Incoming Email";
            this.tabIncoming.UseVisualStyleBackColor = true;
            // 
            // tabOutgoing
            // 
            this.tabOutgoing.Controls.Add(this.smtpConfig);
            this.tabOutgoing.Location = new System.Drawing.Point(4, 22);
            this.tabOutgoing.Name = "tabOutgoing";
            this.tabOutgoing.Padding = new System.Windows.Forms.Padding(3);
            this.tabOutgoing.Size = new System.Drawing.Size(523, 340);
            this.tabOutgoing.TabIndex = 1;
            this.tabOutgoing.Text = "Outgoing Email";
            this.tabOutgoing.UseVisualStyleBackColor = true;
            // 
            // pollingConfig
            // 
            pollingConfigValues1.EmailType = AowEmailWrapper.ConfigFramework.EmailType.IMAP;
            pollingConfigValues1.Password = "";
            pollingConfigValues1.PasswordTrue = "";
            pollingConfigValues1.PollInterval = 1;
            pollingConfigValues1.Port = 0;
            pollingConfigValues1.Server = "";
            pollingConfigValues1.UsePolling = false;
            pollingConfigValues1.Username = "";
            pollingConfigValues1.UseSSL = false;
            this.pollingConfig.Config = pollingConfigValues1;
            this.pollingConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.pollingConfig.Location = new System.Drawing.Point(3, 3);
            this.pollingConfig.Margin = new System.Windows.Forms.Padding(2);
            this.pollingConfig.Name = "pollingConfig";
            this.pollingConfig.Size = new System.Drawing.Size(517, 322);
            this.pollingConfig.TabIndex = 1;
            // 
            // smtpConfig
            // 
            smtpConfigValues1.Authentication = false;
            smtpConfigValues1.BCCMyself = false;
            smtpConfigValues1.EmailAddress = "";
            smtpConfigValues1.Password = "";
            smtpConfigValues1.PasswordTrue = "";
            smtpConfigValues1.Port = 0;
            smtpConfigValues1.SmtpServer = "";
            smtpConfigValues1.SmtpSSLType = AowEmailWrapper.ConfigFramework.SmtpSSLType.None;
            smtpConfigValues1.UsePollingCredentials = false;
            smtpConfigValues1.Username = "";
            this.smtpConfig.Config = smtpConfigValues1;
            this.smtpConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtpConfig.Location = new System.Drawing.Point(3, 3);
            this.smtpConfig.Margin = new System.Windows.Forms.Padding(2);
            this.smtpConfig.Name = "smtpConfig";
            this.smtpConfig.Size = new System.Drawing.Size(517, 340);
            this.smtpConfig.TabIndex = 1;
            // 
            // AccountsConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelBottomHalf);
            this.Controls.Add(this.panelTopHalf);
            this.Name = "AccountsConfig";
            this.Size = new System.Drawing.Size(531, 581);
            this.panelTopHalf.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.panelSetStartUp.ResumeLayout(false);
            this.panelActivate.ResumeLayout(false);
            this.panelRename.ResumeLayout(false);
            this.panelRemove.ResumeLayout(false);
            this.panelAdd.ResumeLayout(false);
            this.panelBottomHalf.ResumeLayout(false);
            this.tabControlAccountEditor.ResumeLayout(false);
            this.tabIncoming.ResumeLayout(false);
            this.tabOutgoing.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTopHalf;
        private System.Windows.Forms.ListView listViewAccounts;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Panel panelActivate;
        private System.Windows.Forms.Button buttonActivate;
        private System.Windows.Forms.Panel panelRename;
        private System.Windows.Forms.Button buttonRename;
        private System.Windows.Forms.Panel panelRemove;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Panel panelAdd;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Panel panelBottomHalf;
        private System.Windows.Forms.TabControl tabControlAccountEditor;
        private System.Windows.Forms.TabPage tabIncoming;
        private PollingConfig pollingConfig;
        private System.Windows.Forms.TabPage tabOutgoing;
        private SmtpConfig smtpConfig;
        private System.Windows.Forms.Panel panelSetStartUp;
        private System.Windows.Forms.Button buttonSetStartUp;
        private System.Windows.Forms.ColumnHeader colAddress;
        private System.Windows.Forms.ColumnHeader colDefault;
        private System.Windows.Forms.ColumnHeader colHidden;
        private System.Windows.Forms.ImageList imageListIcons;

    }
}
