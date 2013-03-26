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
            this.panelAuthentication = new System.Windows.Forms.Panel();
            this.panelCreateButton = new System.Windows.Forms.Panel();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.groupBoxAuth = new System.Windows.Forms.GroupBox();
            this.fbServerPreference = new AowEmailWrapper.Controls.FormBlockCombo();
            this.fbPassword = new AowEmailWrapper.Controls.FormBlockText();
            this.fbEmailAddress = new AowEmailWrapper.Controls.FormBlockText();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.pictureBoxMoz = new System.Windows.Forms.PictureBox();
            this.panelAuthentication.SuspendLayout();
            this.panelCreateButton.SuspendLayout();
            this.groupBoxAuth.SuspendLayout();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMoz)).BeginInit();
            this.SuspendLayout();
            // 
            // panelAuthentication
            // 
            this.panelAuthentication.Controls.Add(this.pictureBoxMoz);
            this.panelAuthentication.Controls.Add(this.panelCreateButton);
            this.panelAuthentication.Controls.Add(this.groupBoxAuth);
            this.panelAuthentication.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelAuthentication.Location = new System.Drawing.Point(0, 25);
            this.panelAuthentication.Name = "panelAuthentication";
            this.panelAuthentication.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.panelAuthentication.Size = new System.Drawing.Size(437, 144);
            this.panelAuthentication.TabIndex = 23;
            // 
            // panelCreateButton
            // 
            this.panelCreateButton.Controls.Add(this.buttonCreate);
            this.panelCreateButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelCreateButton.Location = new System.Drawing.Point(322, 97);
            this.panelCreateButton.Name = "panelCreateButton";
            this.panelCreateButton.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.panelCreateButton.Size = new System.Drawing.Size(115, 47);
            this.panelCreateButton.TabIndex = 14;
            // 
            // buttonCreate
            // 
            this.buttonCreate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonCreate.Enabled = false;
            this.buttonCreate.Location = new System.Drawing.Point(0, 5);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(115, 42);
            this.buttonCreate.TabIndex = 14;
            this.buttonCreate.Text = "Create";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // groupBoxAuth
            // 
            this.groupBoxAuth.Controls.Add(this.fbServerPreference);
            this.groupBoxAuth.Controls.Add(this.fbPassword);
            this.groupBoxAuth.Controls.Add(this.fbEmailAddress);
            this.groupBoxAuth.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxAuth.Location = new System.Drawing.Point(0, 5);
            this.groupBoxAuth.Name = "groupBoxAuth";
            this.groupBoxAuth.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.groupBoxAuth.Size = new System.Drawing.Size(437, 92);
            this.groupBoxAuth.TabIndex = 12;
            this.groupBoxAuth.TabStop = false;
            this.groupBoxAuth.Text = "Authentication Details";
            // 
            // fbServerPreference
            // 
            this.fbServerPreference.Dock = System.Windows.Forms.DockStyle.Top;
            this.fbServerPreference.LabelName = "Incoming Server Preference:";
            this.fbServerPreference.Location = new System.Drawing.Point(3, 61);
            this.fbServerPreference.Margin = new System.Windows.Forms.Padding(2);
            this.fbServerPreference.MinimumSize = new System.Drawing.Size(0, 24);
            this.fbServerPreference.Name = "fbServerPreference";
            this.fbServerPreference.SelectedIndex = -1;
            this.fbServerPreference.SelectedValue = "";
            this.fbServerPreference.Size = new System.Drawing.Size(431, 24);
            this.fbServerPreference.TabIndex = 2;
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
            this.fbPassword.ValidationRegEx = "^\\S+.*";
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
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.progressBar);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(437, 25);
            this.panelHeader.TabIndex = 24;
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressBar.Location = new System.Drawing.Point(0, 0);
            this.progressBar.MarqueeAnimationSpeed = 150;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(437, 22);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 0;
            this.progressBar.Visible = false;
            // 
            // pictureBoxMoz
            // 
            this.pictureBoxMoz.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBoxMoz.Image = global::AowEmailWrapper.Properties.Resources.thunder;
            this.pictureBoxMoz.Location = new System.Drawing.Point(0, 97);
            this.pictureBoxMoz.Name = "pictureBoxMoz";
            this.pictureBoxMoz.Size = new System.Drawing.Size(56, 47);
            this.pictureBoxMoz.TabIndex = 15;
            this.pictureBoxMoz.TabStop = false;
            // 
            // AccountsCreationWizzard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelAuthentication);
            this.Controls.Add(this.panelHeader);
            this.Name = "AccountsCreationWizzard";
            this.Size = new System.Drawing.Size(437, 189);
            this.panelAuthentication.ResumeLayout(false);
            this.panelCreateButton.ResumeLayout(false);
            this.groupBoxAuth.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMoz)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelAuthentication;
        private System.Windows.Forms.GroupBox groupBoxAuth;
        private FormBlockText fbPassword;
        private FormBlockText fbEmailAddress;
        private System.Windows.Forms.Panel panelCreateButton;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.ProgressBar progressBar;
        private FormBlockCombo fbServerPreference;
        private System.Windows.Forms.PictureBox pictureBoxMoz;
    }
}
