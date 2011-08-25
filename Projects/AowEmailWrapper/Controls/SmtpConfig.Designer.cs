namespace AowEmailWrapper.Controls
{
    partial class SmtpConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SmtpConfig));
            this.groupBoxServer = new System.Windows.Forms.GroupBox();
            this.fbBccMyself = new AowEmailWrapper.Controls.FormBlockCheckBox();
            this.fbAuthentication = new AowEmailWrapper.Controls.FormBlockCheckBox();
            this.fbSSLType = new AowEmailWrapper.Controls.FormBlockCombo();
            this.fbEmailAddress = new AowEmailWrapper.Controls.FormBlockText();
            this.fbPort = new AowEmailWrapper.Controls.FormBlockText();
            this.fbSmtpServer = new AowEmailWrapper.Controls.FormBlockText();
            this.groupBoxAuth = new System.Windows.Forms.GroupBox();
            this.fbUsePolling = new AowEmailWrapper.Controls.FormBlockCheckBox();
            this.fbPassword = new AowEmailWrapper.Controls.FormBlockText();
            this.fbUserName = new AowEmailWrapper.Controls.FormBlockText();
            this.groupBoxServer.SuspendLayout();
            this.groupBoxAuth.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxServer
            // 
            this.groupBoxServer.AutoSize = true;
            this.groupBoxServer.Controls.Add(this.fbBccMyself);
            this.groupBoxServer.Controls.Add(this.fbAuthentication);
            this.groupBoxServer.Controls.Add(this.fbSSLType);
            this.groupBoxServer.Controls.Add(this.fbEmailAddress);
            this.groupBoxServer.Controls.Add(this.fbPort);
            this.groupBoxServer.Controls.Add(this.fbSmtpServer);
            this.groupBoxServer.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxServer.Location = new System.Drawing.Point(0, 0);
            this.groupBoxServer.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxServer.Name = "groupBoxServer";
            this.groupBoxServer.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.groupBoxServer.Size = new System.Drawing.Size(363, 164);
            this.groupBoxServer.TabIndex = 2;
            this.groupBoxServer.TabStop = false;
            this.groupBoxServer.Text = "Server";
            // 
            // fbBccMyself
            // 
            this.fbBccMyself.Checked = false;
            this.fbBccMyself.Dock = System.Windows.Forms.DockStyle.Top;
            this.fbBccMyself.LabelName = "BCC myself:";
            this.fbBccMyself.Location = new System.Drawing.Point(2, 135);
            this.fbBccMyself.MinimumSize = new System.Drawing.Size(0, 24);
            this.fbBccMyself.Name = "fbBccMyself";
            this.fbBccMyself.Size = new System.Drawing.Size(359, 24);
            this.fbBccMyself.TabIndex = 31;
            // 
            // fbAuthentication
            // 
            this.fbAuthentication.Checked = false;
            this.fbAuthentication.Dock = System.Windows.Forms.DockStyle.Top;
            this.fbAuthentication.LabelName = "Authentication Required:";
            this.fbAuthentication.Location = new System.Drawing.Point(2, 111);
            this.fbAuthentication.MinimumSize = new System.Drawing.Size(0, 24);
            this.fbAuthentication.Name = "fbAuthentication";
            this.fbAuthentication.Size = new System.Drawing.Size(359, 24);
            this.fbAuthentication.TabIndex = 30;
            // 
            // fbSSLType
            // 
            this.fbSSLType.Dock = System.Windows.Forms.DockStyle.Top;
            this.fbSSLType.LabelName = "SSL Type:";
            this.fbSSLType.Location = new System.Drawing.Point(2, 87);
            this.fbSSLType.Margin = new System.Windows.Forms.Padding(2);
            this.fbSSLType.MinimumSize = new System.Drawing.Size(0, 24);
            this.fbSSLType.Name = "fbSSLType";
            this.fbSSLType.SelectedIndex = -1;
            this.fbSSLType.SelectedValue = "";
            this.fbSSLType.Size = new System.Drawing.Size(359, 24);
            this.fbSSLType.TabIndex = 26;
            this.fbSSLType.Tag = "The email protocol to use.";
            // 
            // fbEmailAddress
            // 
            this.fbEmailAddress.Dock = System.Windows.Forms.DockStyle.Top;
            this.fbEmailAddress.IsPassword = false;
            this.fbEmailAddress.LabelName = "Email Address:";
            this.fbEmailAddress.Location = new System.Drawing.Point(2, 63);
            this.fbEmailAddress.Margin = new System.Windows.Forms.Padding(2);
            this.fbEmailAddress.MinimumSize = new System.Drawing.Size(0, 24);
            this.fbEmailAddress.Name = "fbEmailAddress";
            this.fbEmailAddress.Size = new System.Drawing.Size(359, 24);
            this.fbEmailAddress.TabIndex = 6;
            this.fbEmailAddress.Tag = "The email address that you use.";
            this.fbEmailAddress.TextValue = "";
            this.fbEmailAddress.ValidationRegEx = resources.GetString("fbEmailAddress.ValidationRegEx");
            // 
            // fbPort
            // 
            this.fbPort.Dock = System.Windows.Forms.DockStyle.Top;
            this.fbPort.IsPassword = false;
            this.fbPort.LabelName = "Port:";
            this.fbPort.Location = new System.Drawing.Point(2, 39);
            this.fbPort.Margin = new System.Windows.Forms.Padding(2);
            this.fbPort.MinimumSize = new System.Drawing.Size(0, 24);
            this.fbPort.Name = "fbPort";
            this.fbPort.Size = new System.Drawing.Size(359, 24);
            this.fbPort.TabIndex = 2;
            this.fbPort.Tag = "The port number to connect to the outgoing email server on.";
            this.fbPort.TextValue = "";
            this.fbPort.ValidationRegEx = "^(0|[1-9][0-9]*)$";
            // 
            // fbSmtpServer
            // 
            this.fbSmtpServer.Dock = System.Windows.Forms.DockStyle.Top;
            this.fbSmtpServer.IsPassword = false;
            this.fbSmtpServer.LabelName = "Smtp Server:";
            this.fbSmtpServer.Location = new System.Drawing.Point(2, 15);
            this.fbSmtpServer.Margin = new System.Windows.Forms.Padding(2);
            this.fbSmtpServer.MinimumSize = new System.Drawing.Size(0, 24);
            this.fbSmtpServer.Name = "fbSmtpServer";
            this.fbSmtpServer.Size = new System.Drawing.Size(359, 24);
            this.fbSmtpServer.TabIndex = 1;
            this.fbSmtpServer.Tag = "The DNS name of the outgoing email server.";
            this.fbSmtpServer.TextValue = "";
            this.fbSmtpServer.ValidationRegEx = "^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\\.){3}([0-9]|[1-9][0-9]|1[0-9]{" +
                "2}|2[0-4][0-9]|25[0-5])$|^(([\\w]|[\\w][\\w0-9\\-]*[\\w0-9])\\.)*([\\w]|[\\w][\\w0-9\\-]*[" +
                "\\w0-9])$";
            // 
            // groupBoxAuth
            // 
            this.groupBoxAuth.AutoSize = true;
            this.groupBoxAuth.Controls.Add(this.fbPassword);
            this.groupBoxAuth.Controls.Add(this.fbUserName);
            this.groupBoxAuth.Controls.Add(this.fbUsePolling);
            this.groupBoxAuth.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxAuth.Location = new System.Drawing.Point(0, 164);
            this.groupBoxAuth.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxAuth.Name = "groupBoxAuth";
            this.groupBoxAuth.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.groupBoxAuth.Size = new System.Drawing.Size(363, 92);
            this.groupBoxAuth.TabIndex = 3;
            this.groupBoxAuth.TabStop = false;
            this.groupBoxAuth.Text = "Authentication Details";
            this.groupBoxAuth.Visible = false;
            // 
            // fbUsePolling
            // 
            this.fbUsePolling.Checked = false;
            this.fbUsePolling.Dock = System.Windows.Forms.DockStyle.Top;
            this.fbUsePolling.LabelName = "Use Polling Credentials:";
            this.fbUsePolling.Location = new System.Drawing.Point(2, 15);
            this.fbUsePolling.MinimumSize = new System.Drawing.Size(0, 24);
            this.fbUsePolling.Name = "fbUsePolling";
            this.fbUsePolling.Size = new System.Drawing.Size(359, 24);
            this.fbUsePolling.TabIndex = 32;
            // 
            // fbPassword
            // 
            this.fbPassword.Dock = System.Windows.Forms.DockStyle.Top;
            this.fbPassword.IsPassword = true;
            this.fbPassword.LabelName = "Password:";
            this.fbPassword.Location = new System.Drawing.Point(2, 63);
            this.fbPassword.Margin = new System.Windows.Forms.Padding(2);
            this.fbPassword.MinimumSize = new System.Drawing.Size(0, 24);
            this.fbPassword.Name = "fbPassword";
            this.fbPassword.Size = new System.Drawing.Size(359, 24);
            this.fbPassword.TabIndex = 6;
            this.fbPassword.Tag = "Your email sign in password. ";
            this.fbPassword.TextValue = "";
            this.fbPassword.ValidationRegEx = "^\\S+.*";
            // 
            // fbUserName
            // 
            this.fbUserName.Dock = System.Windows.Forms.DockStyle.Top;
            this.fbUserName.IsPassword = false;
            this.fbUserName.LabelName = "User name:";
            this.fbUserName.Location = new System.Drawing.Point(2, 39);
            this.fbUserName.Margin = new System.Windows.Forms.Padding(2);
            this.fbUserName.MinimumSize = new System.Drawing.Size(0, 24);
            this.fbUserName.Name = "fbUserName";
            this.fbUserName.Size = new System.Drawing.Size(359, 24);
            this.fbUserName.TabIndex = 5;
            this.fbUserName.Tag = "Your email sign in username (or email address).";
            this.fbUserName.TextValue = "";
            this.fbUserName.ValidationRegEx = "^\\S+.*";
            // 
            // SmtpConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxAuth);
            this.Controls.Add(this.groupBoxServer);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SmtpConfig";
            this.Size = new System.Drawing.Size(363, 266);
            this.groupBoxServer.ResumeLayout(false);
            this.groupBoxAuth.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxServer;
        private FormBlockText fbPort;
        private FormBlockText fbSmtpServer;
        private System.Windows.Forms.GroupBox groupBoxAuth;
        private FormBlockText fbPassword;
        private FormBlockText fbUserName;
        private FormBlockText fbEmailAddress;
        private FormBlockCombo fbSSLType;
        private FormBlockCheckBox fbAuthentication;
        private FormBlockCheckBox fbBccMyself;
        private FormBlockCheckBox fbUsePolling;

    }
}
