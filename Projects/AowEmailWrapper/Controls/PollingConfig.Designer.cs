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
            this.chkPoll = new System.Windows.Forms.CheckBox();
            this.groupBoxServer = new System.Windows.Forms.GroupBox();
            this.groupBoxAuth = new System.Windows.Forms.GroupBox();
            this.groupBoxEmailSelection = new System.Windows.Forms.GroupBox();
            this.fbPollInterval = new AowEmailWrapper.Controls.FormBlockCombo();
            this.fbPassword = new AowEmailWrapper.Controls.FormBlockText();
            this.fbUserName = new AowEmailWrapper.Controls.FormBlockText();
            this.fbUseSSL = new AowEmailWrapper.Controls.FormBlockCheckBox();
            this.fbPort = new AowEmailWrapper.Controls.FormBlockText();
            this.fbServer = new AowEmailWrapper.Controls.FormBlockText();
            this.fbEmailType = new AowEmailWrapper.Controls.FormBlockCombo();
            this.groupBoxServer.SuspendLayout();
            this.groupBoxAuth.SuspendLayout();
            this.groupBoxEmailSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkPoll
            // 
            this.chkPoll.AutoSize = true;
            this.chkPoll.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkPoll.Location = new System.Drawing.Point(0, 0);
            this.chkPoll.Margin = new System.Windows.Forms.Padding(2);
            this.chkPoll.Name = "chkPoll";
            this.chkPoll.Size = new System.Drawing.Size(363, 17);
            this.chkPoll.TabIndex = 8;
            this.chkPoll.Text = "Use Email Polling (email checking at timed intervals)";
            this.chkPoll.UseVisualStyleBackColor = true;
            this.chkPoll.CheckedChanged += new System.EventHandler(this.chkPoll_CheckedChanged);
            // 
            // groupBoxServer
            // 
            this.groupBoxServer.AutoSize = true;
            this.groupBoxServer.Controls.Add(this.fbUseSSL);
            this.groupBoxServer.Controls.Add(this.fbPort);
            this.groupBoxServer.Controls.Add(this.fbServer);
            this.groupBoxServer.Controls.Add(this.fbEmailType);
            this.groupBoxServer.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxServer.Location = new System.Drawing.Point(0, 17);
            this.groupBoxServer.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxServer.Name = "groupBoxServer";
            this.groupBoxServer.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.groupBoxServer.Size = new System.Drawing.Size(363, 116);
            this.groupBoxServer.TabIndex = 11;
            this.groupBoxServer.TabStop = false;
            this.groupBoxServer.Text = "Server";
            this.groupBoxServer.Visible = false;
            // 
            // groupBoxAuth
            // 
            this.groupBoxAuth.AutoSize = true;
            this.groupBoxAuth.Controls.Add(this.fbPassword);
            this.groupBoxAuth.Controls.Add(this.fbUserName);
            this.groupBoxAuth.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxAuth.Location = new System.Drawing.Point(0, 133);
            this.groupBoxAuth.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxAuth.Name = "groupBoxAuth";
            this.groupBoxAuth.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.groupBoxAuth.Size = new System.Drawing.Size(363, 68);
            this.groupBoxAuth.TabIndex = 12;
            this.groupBoxAuth.TabStop = false;
            this.groupBoxAuth.Text = "Authentication Details";
            this.groupBoxAuth.Visible = false;
            // 
            // groupBoxEmailSelection
            // 
            this.groupBoxEmailSelection.AutoSize = true;
            this.groupBoxEmailSelection.Controls.Add(this.fbPollInterval);
            this.groupBoxEmailSelection.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxEmailSelection.Location = new System.Drawing.Point(0, 201);
            this.groupBoxEmailSelection.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxEmailSelection.Name = "groupBoxEmailSelection";
            this.groupBoxEmailSelection.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.groupBoxEmailSelection.Size = new System.Drawing.Size(363, 44);
            this.groupBoxEmailSelection.TabIndex = 13;
            this.groupBoxEmailSelection.TabStop = false;
            this.groupBoxEmailSelection.Text = "Email Attachment Downloading";
            this.groupBoxEmailSelection.Visible = false;
            // 
            // fbPollInterval
            // 
            this.fbPollInterval.Dock = System.Windows.Forms.DockStyle.Top;
            this.fbPollInterval.LabelName = "Poll Interval Mins:";
            this.fbPollInterval.Location = new System.Drawing.Point(2, 15);
            this.fbPollInterval.Margin = new System.Windows.Forms.Padding(2);
            this.fbPollInterval.MinimumSize = new System.Drawing.Size(0, 24);
            this.fbPollInterval.Name = "fbPollInterval";
            this.fbPollInterval.SelectedIndex = -1;
            this.fbPollInterval.SelectedValue = "";
            this.fbPollInterval.Size = new System.Drawing.Size(359, 24);
            this.fbPollInterval.TabIndex = 16;
            this.fbPollInterval.Tag = "The length of time in minutes between automatically checking for email.";
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
            this.fbPassword.Size = new System.Drawing.Size(359, 24);
            this.fbPassword.TabIndex = 6;
            this.fbPassword.Tag = "Your email sign in password.";
            this.fbPassword.TextValue = "";
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
            this.fbUserName.Size = new System.Drawing.Size(359, 24);
            this.fbUserName.TabIndex = 5;
            this.fbUserName.Tag = "Your email sign in username.";
            this.fbUserName.TextValue = "";
            // 
            // fbUseSSL
            // 
            this.fbUseSSL.Checked = false;
            this.fbUseSSL.Dock = System.Windows.Forms.DockStyle.Top;
            this.fbUseSSL.LabelName = "Use SSL:";
            this.fbUseSSL.Location = new System.Drawing.Point(2, 87);
            this.fbUseSSL.MinimumSize = new System.Drawing.Size(0, 24);
            this.fbUseSSL.Name = "fbUseSSL";
            this.fbUseSSL.Size = new System.Drawing.Size(359, 24);
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
            this.fbPort.Size = new System.Drawing.Size(359, 24);
            this.fbPort.TabIndex = 17;
            this.fbPort.Tag = "The port number to connect to the incoming email server on.";
            this.fbPort.TextValue = "";
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
            this.fbServer.Size = new System.Drawing.Size(359, 24);
            this.fbServer.TabIndex = 16;
            this.fbServer.Tag = "The DNS name of the incoming email server.";
            this.fbServer.TextValue = "";
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
            this.fbEmailType.Size = new System.Drawing.Size(359, 24);
            this.fbEmailType.TabIndex = 15;
            this.fbEmailType.Tag = "The email protocol to use.";
            // 
            // PollingConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxEmailSelection);
            this.Controls.Add(this.groupBoxAuth);
            this.Controls.Add(this.groupBoxServer);
            this.Controls.Add(this.chkPoll);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "PollingConfig";
            this.Size = new System.Drawing.Size(363, 316);
            this.groupBoxServer.ResumeLayout(false);
            this.groupBoxAuth.ResumeLayout(false);
            this.groupBoxEmailSelection.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkPoll;
        private System.Windows.Forms.GroupBox groupBoxServer;
        private FormBlockCombo fbEmailType;
        private FormBlockText fbPort;
        private FormBlockText fbServer;
        private System.Windows.Forms.GroupBox groupBoxAuth;
        private FormBlockText fbPassword;
        private FormBlockText fbUserName;
        private System.Windows.Forms.GroupBox groupBoxEmailSelection;
        private FormBlockCombo fbPollInterval;
        private FormBlockCheckBox fbUseSSL;

    }
}
