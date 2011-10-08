namespace AowEmailWrapper.Controls
{
    partial class AccountsCreationForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountsCreationForm));
            this.accountsCreationWizzard = new AowEmailWrapper.Controls.AccountsCreationWizzard();
            this.SuspendLayout();
            // 
            // accountsCreationWizzard
            // 
            this.accountsCreationWizzard.AccountTemplates = null;
            this.accountsCreationWizzard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.accountsCreationWizzard.Location = new System.Drawing.Point(0, 0);
            this.accountsCreationWizzard.Name = "accountsCreationWizzard";
            this.accountsCreationWizzard.Size = new System.Drawing.Size(421, 237);
            this.accountsCreationWizzard.TabIndex = 0;
            this.accountsCreationWizzard.TemplateIcons = null;
            // 
            // AccountsCreationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 237);
            this.Controls.Add(this.accountsCreationWizzard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AccountsCreationForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create new account";
            this.ResumeLayout(false);

        }

        #endregion

        private AccountsCreationWizzard accountsCreationWizzard;



    }
}