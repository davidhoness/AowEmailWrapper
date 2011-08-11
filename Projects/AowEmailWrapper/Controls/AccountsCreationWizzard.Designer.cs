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
            this.panelWhite = new System.Windows.Forms.Panel();
            this.panelInnerRadio = new System.Windows.Forms.Panel();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.panelMessagePadder = new System.Windows.Forms.Panel();
            this.panelMessage = new System.Windows.Forms.Panel();
            this.labelMessage = new System.Windows.Forms.Label();
            this.panelAuthentication = new System.Windows.Forms.Panel();
            this.groupBoxAuthentication = new System.Windows.Forms.GroupBox();
            this.panelCreateButton = new System.Windows.Forms.Panel();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.fbPassword = new AowEmailWrapper.Controls.FormBlockText();
            this.fbEmailAddress = new AowEmailWrapper.Controls.FormBlockText();
            this.panelWhite.SuspendLayout();
            this.panelInnerRadio.SuspendLayout();
            this.panelMessagePadder.SuspendLayout();
            this.panelMessage.SuspendLayout();
            this.panelAuthentication.SuspendLayout();
            this.groupBoxAuthentication.SuspendLayout();
            this.panelCreateButton.SuspendLayout();
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
            this.panelInnerRadio.Controls.Add(this.radioButton4);
            this.panelInnerRadio.Controls.Add(this.radioButton3);
            this.panelInnerRadio.Controls.Add(this.radioButton2);
            this.panelInnerRadio.Controls.Add(this.radioButton1);
            this.panelInnerRadio.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelInnerRadio.Location = new System.Drawing.Point(10, 10);
            this.panelInnerRadio.Name = "panelInnerRadio";
            this.panelInnerRadio.Size = new System.Drawing.Size(386, 72);
            this.panelInnerRadio.TabIndex = 0;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Dock = System.Windows.Forms.DockStyle.Left;
            this.radioButton4.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.radioButton4.Location = new System.Drawing.Point(270, 0);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.radioButton4.Size = new System.Drawing.Size(71, 72);
            this.radioButton4.TabIndex = 7;
            this.radioButton4.Tag = "Other";
            this.radioButton4.Text = "Other";
            this.radioButton4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Dock = System.Windows.Forms.DockStyle.Left;
            this.radioButton3.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.radioButton3.Location = new System.Drawing.Point(172, 0);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.radioButton3.Size = new System.Drawing.Size(98, 72);
            this.radioButton3.TabIndex = 6;
            this.radioButton3.Tag = "Yahoo";
            this.radioButton3.Text = "Yahoo Mail";
            this.radioButton3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.Dock = System.Windows.Forms.DockStyle.Left;
            this.radioButton2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.radioButton2.Location = new System.Drawing.Point(72, 0);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.radioButton2.Size = new System.Drawing.Size(100, 72);
            this.radioButton2.TabIndex = 5;
            this.radioButton2.Tag = "WindowsLive";
            this.radioButton2.Text = "Hotmail";
            this.radioButton2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Dock = System.Windows.Forms.DockStyle.Left;
            this.radioButton1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.radioButton1.Location = new System.Drawing.Point(0, 0);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.radioButton1.Size = new System.Drawing.Size(72, 72);
            this.radioButton1.TabIndex = 4;
            this.radioButton1.Tag = "Google";
            this.radioButton1.Text = "GMail";
            this.radioButton1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
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
            this.panelMessage.Controls.Add(this.labelMessage);
            this.panelMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMessage.Location = new System.Drawing.Point(0, 5);
            this.panelMessage.Name = "panelMessage";
            this.panelMessage.Size = new System.Drawing.Size(437, 25);
            this.panelMessage.TabIndex = 21;
            // 
            // labelMessage
            // 
            this.labelMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelMessage.Location = new System.Drawing.Point(0, 0);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(433, 21);
            this.labelMessage.TabIndex = 0;
            this.labelMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelAuthentication
            // 
            this.panelAuthentication.Controls.Add(this.panelCreateButton);
            this.panelAuthentication.Controls.Add(this.groupBoxAuthentication);
            this.panelAuthentication.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelAuthentication.Location = new System.Drawing.Point(0, 124);
            this.panelAuthentication.Name = "panelAuthentication";
            this.panelAuthentication.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.panelAuthentication.Size = new System.Drawing.Size(437, 109);
            this.panelAuthentication.TabIndex = 23;
            // 
            // groupBoxAuthentication
            // 
            this.groupBoxAuthentication.Controls.Add(this.fbPassword);
            this.groupBoxAuthentication.Controls.Add(this.fbEmailAddress);
            this.groupBoxAuthentication.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxAuthentication.Enabled = false;
            this.groupBoxAuthentication.Location = new System.Drawing.Point(0, 5);
            this.groupBoxAuthentication.Name = "groupBoxAuthentication";
            this.groupBoxAuthentication.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.groupBoxAuthentication.Size = new System.Drawing.Size(437, 69);
            this.groupBoxAuthentication.TabIndex = 12;
            this.groupBoxAuthentication.TabStop = false;
            this.groupBoxAuthentication.Text = "Authentication Details";
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
            this.panelInnerRadio.ResumeLayout(false);
            this.panelInnerRadio.PerformLayout();
            this.panelMessagePadder.ResumeLayout(false);
            this.panelMessage.ResumeLayout(false);
            this.panelAuthentication.ResumeLayout(false);
            this.groupBoxAuthentication.ResumeLayout(false);
            this.panelCreateButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelWhite;
        private System.Windows.Forms.Panel panelInnerRadio;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Panel panelMessagePadder;
        private System.Windows.Forms.Panel panelMessage;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.Panel panelAuthentication;
        private System.Windows.Forms.GroupBox groupBoxAuthentication;
        private FormBlockText fbPassword;
        private FormBlockText fbEmailAddress;
        private System.Windows.Forms.Panel panelCreateButton;
        private System.Windows.Forms.Button buttonCreate;
    }
}
