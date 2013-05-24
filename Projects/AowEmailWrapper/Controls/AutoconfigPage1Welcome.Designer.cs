namespace AowEmailWrapper.Controls
{
    partial class AutoconfigPage1Welcome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoconfigPage1Welcome));
            this.labelWrapperMessage = new System.Windows.Forms.Label();
            this.labelAuthMessage = new System.Windows.Forms.Label();
            this.groupBoxAuth = new System.Windows.Forms.GroupBox();
            this.fbPassword = new AowEmailWrapper.Controls.FormBlockText();
            this.fbEmailAddress = new AowEmailWrapper.Controls.FormBlockText();
            this.groupBoxAuth.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelWrapperMessage
            // 
            this.labelWrapperMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelWrapperMessage.Location = new System.Drawing.Point(0, 0);
            this.labelWrapperMessage.Name = "labelWrapperMessage";
            this.labelWrapperMessage.Size = new System.Drawing.Size(431, 100);
            this.labelWrapperMessage.TabIndex = 0;
            this.labelWrapperMessage.Text = resources.GetString("labelWrapperMessage.Text");
            // 
            // labelAuthMessage
            // 
            this.labelAuthMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelAuthMessage.Location = new System.Drawing.Point(0, 100);
            this.labelAuthMessage.Name = "labelAuthMessage";
            this.labelAuthMessage.Size = new System.Drawing.Size(431, 20);
            this.labelAuthMessage.TabIndex = 1;
            this.labelAuthMessage.Text = "Please provide the authentication details for your email account.";
            // 
            // groupBoxAuth
            // 
            this.groupBoxAuth.Controls.Add(this.fbPassword);
            this.groupBoxAuth.Controls.Add(this.fbEmailAddress);
            this.groupBoxAuth.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxAuth.Location = new System.Drawing.Point(0, 120);
            this.groupBoxAuth.Name = "groupBoxAuth";
            this.groupBoxAuth.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.groupBoxAuth.Size = new System.Drawing.Size(431, 74);
            this.groupBoxAuth.TabIndex = 13;
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
            this.fbPassword.Size = new System.Drawing.Size(425, 24);
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
            this.fbEmailAddress.Size = new System.Drawing.Size(425, 24);
            this.fbEmailAddress.TabIndex = 0;
            this.fbEmailAddress.TextValue = "";
            this.fbEmailAddress.ValidationRegEx = resources.GetString("fbEmailAddress.ValidationRegEx");
            // 
            // AutoconfigPage1Welcome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.groupBoxAuth);
            this.Controls.Add(this.labelAuthMessage);
            this.Controls.Add(this.labelWrapperMessage);
            this.Name = "AutoconfigPage1Welcome";
            this.Size = new System.Drawing.Size(431, 253);
            this.groupBoxAuth.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelWrapperMessage;
        private System.Windows.Forms.Label labelAuthMessage;
        private System.Windows.Forms.GroupBox groupBoxAuth;
        private FormBlockText fbPassword;
        private FormBlockText fbEmailAddress;
    }
}
