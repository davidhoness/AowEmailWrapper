namespace AowEmailWrapper.Controls
{
    partial class ServerChoiceForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerChoiceForm));
            this.serverChoiceControl = new AowEmailWrapper.Controls.ServerChoiceControl();
            this.SuspendLayout();
            // 
            // serverChoiceControl
            // 
            this.serverChoiceControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serverChoiceControl.EmailProvider = null;
            this.serverChoiceControl.Location = new System.Drawing.Point(3, 3);
            this.serverChoiceControl.Name = "serverChoiceControl";
            this.serverChoiceControl.Size = new System.Drawing.Size(609, 478);
            this.serverChoiceControl.TabIndex = 0;
            // 
            // ServerChoiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 481);
            this.Controls.Add(this.serverChoiceControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServerChoiceForm";
            this.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manual override";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private ServerChoiceControl serverChoiceControl;
    }
}