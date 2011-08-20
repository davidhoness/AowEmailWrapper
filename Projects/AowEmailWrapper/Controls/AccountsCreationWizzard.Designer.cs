namespace AowEmailWrapper.Controls
{
    partial class AccountsCreationWizzard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountsCreationWizzard));
            this.panelWhite = new System.Windows.Forms.Panel();
            this.panelInnerRadio = new System.Windows.Forms.Panel();
            this.panelMessagePadder = new System.Windows.Forms.Panel();
            this.panelMessage = new System.Windows.Forms.Panel();
            this.labelDomainsMessage = new System.Windows.Forms.Label();
            this.panelAuthentication = new System.Windows.Forms.Panel();
            this.panelCreateButton = new System.Windows.Forms.Panel();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.groupBoxAuth = new System.Windows.Forms.GroupBox();
            this.fbPassword = new AowEmailWrapper.Controls.FormBlockText();
            this.fbEmailAddress = new AowEmailWrapper.Controls.FormBlockText();
            this.panelWhite.SuspendLayout();
            this.panelMessagePadder.SuspendLayout();
            this.panelMessage.SuspendLayout();
            this.panelAuthentication.SuspendLayout();
            this.panelCreateButton.SuspendLayout();
            this.groupBoxAuth.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelWhite
            // 
            this.panelWhite.BackColor = System.Drawing.Color.White;
            this.panelWhite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelWhite.Controls.Add(this.panelInnerRadio);
            this.panelWhite.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelWhite.Location = new System.Drawing.Point(0, 0);
            this.panelWhite.Name = "panelWhite";
            this.panelWhite.Padding = new System.Windows.Forms.Padding(10);
            this.panelWhite.Size = new System.Drawing.Size(437, 94);
            this.panelWhite.TabIndex = 10;
            // 
            // panelInnerRadio
            // 
            this.panelInnerRadio.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelInnerRadio.Location = new System.Drawing.Point(10, 10);
            this.panelInnerRadio.Name = "panelInnerRadio";
            this.panelInnerRadio.Size = new System.Drawing.Size(757, 72);
            this.panelInnerRadio.TabIndex = 0;
            // 
            // panelMessagePadder
            // 
            this.panelMessagePadder.Controls.Add(this.panelMessage);
            this.panelMessagePadder.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMessagePadder.Location = new System.Drawing.Point(0, 94);
            this.panelMessagePadder.Name = "panelMessagePadder";
            this.panelMessagePadder.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.panelMessagePadder.Size = new System.Drawing.Size(437, 30);
            this.panelMessagePadder.TabIndex = 22;
            // 
            // panelMessage
            // 
            this.panelMessage.BackColor = System.Drawing.SystemColors.Info;
            this.panelMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelMessage.Controls.Add(this.labelDomainsMessage);
            this.panelMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMessage.Location = new System.Drawing.Point(0, 5);
            this.panelMessage.Name = "panelMessage";
            this.panelMessage.Size = new System.Drawing.Size(437, 25);
            this.panelMessage.TabIndex = 21;
            // 
            // labelDomainsMessage
            // 
            this.labelDomainsMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelDomainsMessage.Location = new System.Drawing.Point(0, 0);
            this.labelDomainsMessage.Name = "labelDomainsMessage";
            this.labelDomainsMessage.Size = new System.Drawing.Size(433, 21);
            this.labelDomainsMessage.TabIndex = 0;
            this.labelDomainsMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelAuthentication
            // 
            this.panelAuthentication.Controls.Add(this.panelCreateButton);
            this.panelAuthentication.Controls.Add(this.groupBoxAuth);
            this.panelAuthentication.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelAuthentication.Location = new System.Drawing.Point(0, 124);
            this.panelAuthentication.Name = "panelAuthentication";
            this.panelAuthentication.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.panelAuthentication.Size = new System.Drawing.Size(437, 109);
            this.panelAuthentication.TabIndex = 23;
            // 
            // panelCreateButton
            // 
            this.panelCreateButton.Controls.Add(this.buttonCreate);
            this.panelCreateButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelCreateButton.Location = new System.Drawing.Point(322, 74);
            this.panelCreateButton.Name = "panelCreateButton";
            this.panelCreateButton.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.panelCreateButton.Size = new System.Drawing.Size(115, 35);
            this.panelCreateButton.TabIndex = 14;
            // 
            // buttonCreate
            // 
            this.buttonCreate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonCreate.Enabled = false;
            this.buttonCreate.Location = new System.Drawing.Point(0, 5);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(115, 30);
            this.buttonCreate.TabIndex = 14;
            this.buttonCreate.Text = "Create";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // groupBoxAuth
            // 
            this.groupBoxAuth.Controls.Add(this.fbPassword);
            this.groupBoxAuth.Controls.Add(this.fbEmailAddress);
            this.groupBoxAuth.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxAuth.Enabled = false;
            this.groupBoxAuth.Location = new System.Drawing.Point(0, 5);
            this.groupBoxAuth.Name = "groupBoxAuth";
            this.groupBoxAuth.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.groupBoxAuth.Size = new System.Drawing.Size(437, 69);
            this.groupBoxAuth.TabIndex = 12;
            this.groupBoxAuth.TabStop = false;
            this.groupBoxAuth.Text = "Authentication Details";
            // 
            // fbPassword
            // 
            this.fbPassword.Dock = System.Windows.Forms.DockStyle.Top;
            this.fbPassword.IsPassword = true;
            this.fbPassword.LabelName = "Password:";
            this.fbPassword.Location = new System.Drawing.Point(3, 37);
            this.fbPassword.Margin = new System.Windows.Forms.Padding(2);
            this.fbPassword.MinimumSize = new System.Drawing.Size(0, 24);
            this.fbPassword.Name = "fbPassword";
            this.fbPassword.Size = new System.Drawing.Size(431, 24);
            this.fbPassword.TabIndex = 1;
            this.fbPassword.TextValue = "";
            this.fbPassword.ValidationRegEx = ".";
            // 
            // fbEmailAddress
            // 
            this.fbEmailAddress.Dock = System.Windows.Forms.DockStyle.Top;
            this.fbEmailAddress.IsPassword = false;
            this.fbEmailAddress.LabelName = "Email Address:";
            this.fbEmailAddress.Location = new System.Drawing.Point(3, 13);
            this.fbEmailAddress.Margin = new System.Windows.Forms.Padding(2);
            this.fbEmailAddress.MinimumSize = new System.Drawing.Size(0, 24);
            this.fbEmailAddress.Name = "fbEmailAddress";
            this.fbEmailAddress.Size = new System.Drawing.Size(431, 24);
            this.fbEmailAddress.TabIndex = 0;
            this.fbEmailAddress.TextValue = "";
            this.fbEmailAddress.ValidationRegEx = resources.GetString("fbEmailAddress.ValidationRegEx");
            // 
            // AccountsCreationWizzard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelAuthentication);
            this.Controls.Add(this.panelMessagePadder);
            this.Controls.Add(this.panelWhite);
            this.Name = "AccountsCreationWizzard";
            this.Size = new System.Drawing.Size(437, 261);
            this.panelWhite.ResumeLayout(false);
            this.panelMessagePadder.ResumeLayout(false);
            this.panelMessage.ResumeLayout(false);
            this.panelAuthentication.ResumeLayout(false);
            this.panelCreateButton.ResumeLayout(false);
            this.groupBoxAuth.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelWhite;
        private System.Windows.Forms.Panel panelInnerRadio;
        private System.Windows.Forms.Panel panelMessagePadder;
        private System.Windows.Forms.Panel panelMessage;
        private System.Windows.Forms.Label labelDomainsMessage;
        private System.Windows.Forms.Panel panelAuthentication;
        private System.Windows.Forms.GroupBox groupBoxAuth;
        private FormBlockText fbPassword;
        private FormBlockText fbEmailAddress;
        private System.Windows.Forms.Panel panelCreateButton;
        private System.Windows.Forms.Button buttonCreate;
    }
}
