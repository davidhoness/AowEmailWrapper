namespace AowEmailWrapper.Controls
{
    partial class PollingConfig
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
            this.groupBoxServer = new System.Windows.Forms.GroupBox();
            this.fbUseSSL = new AowEmailWrapper.Controls.FormBlockCheckBox();
            this.fbPort = new AowEmailWrapper.Controls.FormBlockText();
            this.fbServer = new AowEmailWrapper.Controls.FormBlockText();
            this.fbEmailType = new AowEmailWrapper.Controls.FormBlockCombo();
            this.groupBoxAuth = new System.Windows.Forms.GroupBox();
            this.fbPassword = new AowEmailWrapper.Controls.FormBlockText();
            this.fbUserName = new AowEmailWrapper.Controls.FormBlockText();
            this.fbPollingSetup = new AowEmailWrapper.Controls.FormBlockPollingSetup();
            this.groupBoxServer.SuspendLayout();
            this.groupBoxAuth.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxServer
            // 
            this.groupBoxServer.AutoSize = true;
            this.groupBoxServer.Controls.Add(this.fbUseSSL);
            this.groupBoxServer.Controls.Add(this.fbPort);
            this.groupBoxServer.Controls.Add(this.fbServer);
            this.groupBoxServer.Controls.Add(this.fbEmailType);
            this.groupBoxServer.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxServer.Location = new System.Drawing.Point(0, 22);
            this.groupBoxServer.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxServer.Name = "groupBoxServer";
            this.groupBoxServer.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.groupBoxServer.Size = new System.Drawing.Size(484, 116);
            this.groupBoxServer.TabIndex = 25;
            this.groupBoxServer.TabStop = false;
            this.groupBoxServer.Text = "Server";
            this.groupBoxServer.Visible = false;
            // 
            // fbUseSSL
            // 
            this.fbUseSSL.Checked = false;
            this.fbUseSSL.Dock = System.Windows.Forms.DockStyle.Top;
            this.fbUseSSL.LabelName = "Use SSL:";
            this.fbUseSSL.Location = new System.Drawing.Point(2, 87);
            this.fbUseSSL.MinimumSize = new System.Drawing.Size(0, 24);
            this.fbUseSSL.Name = "fbUseSSL";
            this.fbUseSSL.Size = new System.Drawing.Size(480, 24);
            this.fbUseSSL.TabIndex = 18;
            // 
            // fbPort
            // 
            this.fbPort.Dock = System.Windows.Forms.DockStyle.Top;
            this.fbPort.IsPassword = false;
            this.fbPort.LabelName = "Port:";
            this.fbPort.Location = new System.Drawing.Point(2, 63);
            this.fbPort.Margin = new System.Windows.Forms.Padding(2);
            this.fbPort.MinimumSize = new System.Drawing.Size(0, 24);
            this.fbPort.Name = "fbPort";
            this.fbPort.Size = new System.Drawing.Size(480, 24);
            this.fbPort.TabIndex = 17;
            this.fbPort.Tag = "The port number to connect to the incoming email server on.";
            this.fbPort.TextValue = "";
            this.fbPort.ValidationRegEx = "^(0|[1-9][0-9]*)$";
            // 
            // fbServer
            // 
            this.fbServer.Dock = System.Windows.Forms.DockStyle.Top;
            this.fbServer.IsPassword = false;
            this.fbServer.LabelName = "Server:";
            this.fbServer.Location = new System.Drawing.Point(2, 39);
            this.fbServer.Margin = new System.Windows.Forms.Padding(2);
            this.fbServer.MinimumSize = new System.Drawing.Size(0, 24);
            this.fbServer.Name = "fbServer";
            this.fbServer.Size = new System.Drawing.Size(480, 24);
            this.fbServer.TabIndex = 16;
            this.fbServer.Tag = "The DNS name of the incoming email server.";
            this.fbServer.TextValue = "";
            this.fbServer.ValidationRegEx = "^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\\.){3}([0-9]|[1-9][0-9]|1[0-9]{" +
                "2}|2[0-4][0-9]|25[0-5])$|^(([\\w]|[\\w][\\w0-9\\-]*[\\w0-9])\\.)*([\\w]|[\\w][\\w0-9\\-]*[" +
                "\\w0-9])$";
            // 
            // fbEmailType
            // 
            this.fbEmailType.Dock = System.Windows.Forms.DockStyle.Top;
            this.fbEmailType.LabelName = "Email Type:";
            this.fbEmailType.Location = new System.Drawing.Point(2, 15);
            this.fbEmailType.Margin = new System.Windows.Forms.Padding(2);
            this.fbEmailType.MinimumSize = new System.Drawing.Size(0, 24);
            this.fbEmailType.Name = "fbEmailType";
            this.fbEmailType.SelectedIndex = -1;
            this.fbEmailType.SelectedValue = "";
            this.fbEmailType.Size = new System.Drawing.Size(480, 24);
            this.fbEmailType.TabIndex = 15;
            this.fbEmailType.Tag = "The email protocol to use.";
            // 
            // groupBoxAuth
            // 
            this.groupBoxAuth.AutoSize = true;
            this.groupBoxAuth.Controls.Add(this.fbPassword);
            this.groupBoxAuth.Controls.Add(this.fbUserName);
            this.groupBoxAuth.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxAuth.Location = new System.Drawing.Point(0, 138);
            this.groupBoxAuth.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxAuth.Name = "groupBoxAuth";
            this.groupBoxAuth.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.groupBoxAuth.Size = new System.Drawing.Size(484, 68);
            this.groupBoxAuth.TabIndex = 26;
            this.groupBoxAuth.TabStop = false;
            this.groupBoxAuth.Text = "Authentication Details";
            this.groupBoxAuth.Visible = false;
            // 
            // fbPassword
            // 
            this.fbPassword.Dock = System.Windows.Forms.DockStyle.Top;
            this.fbPassword.IsPassword = true;
            this.fbPassword.LabelName = "Password:";
            this.fbPassword.Location = new System.Drawing.Point(2, 39);
            this.fbPassword.Margin = new System.Windows.Forms.Padding(2);
            this.fbPassword.MinimumSize = new System.Drawing.Size(0, 24);
            this.fbPassword.Name = "fbPassword";
            this.fbPassword.Size = new System.Drawing.Size(480, 24);
            this.fbPassword.TabIndex = 6;
            this.fbPassword.Tag = "Your email sign in password.";
            this.fbPassword.TextValue = "";
            this.fbPassword.ValidationRegEx = "^\\S+.*";
            // 
            // fbUserName
            // 
            this.fbUserName.Dock = System.Windows.Forms.DockStyle.Top;
            this.fbUserName.IsPassword = false;
            this.fbUserName.LabelName = "User name:";
            this.fbUserName.Location = new System.Drawing.Point(2, 15);
            this.fbUserName.Margin = new System.Windows.Forms.Padding(2);
            this.fbUserName.MinimumSize = new System.Drawing.Size(0, 24);
            this.fbUserName.Name = "fbUserName";
            this.fbUserName.Size = new System.Drawing.Size(480, 24);
            this.fbUserName.TabIndex = 5;
            this.fbUserName.Tag = "Your email sign in username.";
            this.fbUserName.TextValue = "";
            this.fbUserName.ValidationRegEx = "^\\S+.*";
            // 
            // fbPollingSetup
            // 
            this.fbPollingSetup.CheckBoxText = "Check for email";
            this.fbPollingSetup.Checked = false;
            this.fbPollingSetup.Dock = System.Windows.Forms.DockStyle.Top;
            this.fbPollingSetup.EveryText = "every:";
            this.fbPollingSetup.Location = new System.Drawing.Point(0, 0);
            this.fbPollingSetup.MinimumSize = new System.Drawing.Size(0, 22);
            this.fbPollingSetup.Name = "fbPollingSetup";
            this.fbPollingSetup.SelectedIndex = -1;
            this.fbPollingSetup.SelectedValue = "";
            this.fbPollingSetup.Size = new System.Drawing.Size(484, 22);
            this.fbPollingSetup.TabIndex = 24;
            // 
            // PollingConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxAuth);
            this.Controls.Add(this.groupBoxServer);
            this.Controls.Add(this.fbPollingSetup);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "PollingConfig";
            this.Size = new System.Drawing.Size(484, 316);
            this.groupBoxServer.ResumeLayout(false);
            this.groupBoxAuth.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FormBlockPollingSetup fbPollingSetup;
        private System.Windows.Forms.GroupBox groupBoxServer;
        private FormBlockCheckBox fbUseSSL;
        private FormBlockText fbPort;
        private FormBlockText fbServer;
        private FormBlockCombo fbEmailType;
        private System.Windows.Forms.GroupBox groupBoxAuth;
        private FormBlockText fbPassword;
        private FormBlockText fbUserName;


    }
}
