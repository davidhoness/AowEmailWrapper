namespace AowEmailWrapper.Controls
{
    partial class FormBlockPollingSetup
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
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.panelLabel = new System.Windows.Forms.Panel();
            this.checkBox = new System.Windows.Forms.CheckBox();
            this.panelEvery = new System.Windows.Forms.Panel();
            this.labelEvery = new System.Windows.Forms.Label();
            this.panelLabel.SuspendLayout();
            this.panelEvery.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox
            // 
            this.comboBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Location = new System.Drawing.Point(191, 0);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(216, 21);
            this.comboBox.TabIndex = 9;
            // 
            // panelLabel
            // 
            this.panelLabel.AutoSize = true;
            this.panelLabel.Controls.Add(this.checkBox);
            this.panelLabel.Controls.Add(this.panelEvery);
            this.panelLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLabel.Location = new System.Drawing.Point(0, 0);
            this.panelLabel.Margin = new System.Windows.Forms.Padding(2);
            this.panelLabel.Name = "panelLabel";
            this.panelLabel.Size = new System.Drawing.Size(191, 24);
            this.panelLabel.TabIndex = 10;
            // 
            // checkBox
            // 
            this.checkBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBox.Location = new System.Drawing.Point(0, 0);
            this.checkBox.MinimumSize = new System.Drawing.Size(0, 23);
            this.checkBox.Name = "checkBox";
            this.checkBox.Size = new System.Drawing.Size(131, 23);
            this.checkBox.TabIndex = 2;
            this.checkBox.Text = "Check for email";
            this.checkBox.UseVisualStyleBackColor = false;
            // 
            // panelEvery
            // 
            this.panelEvery.Controls.Add(this.labelEvery);
            this.panelEvery.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelEvery.Location = new System.Drawing.Point(131, 0);
            this.panelEvery.Name = "panelEvery";
            this.panelEvery.Size = new System.Drawing.Size(60, 24);
            this.panelEvery.TabIndex = 3;
            // 
            // labelEvery
            // 
            this.labelEvery.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelEvery.Location = new System.Drawing.Point(0, 0);
            this.labelEvery.MinimumSize = new System.Drawing.Size(0, 20);
            this.labelEvery.Name = "labelEvery";
            this.labelEvery.Size = new System.Drawing.Size(60, 20);
            this.labelEvery.TabIndex = 4;
            this.labelEvery.Text = "every:";
            this.labelEvery.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FormBlockPollingSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelLabel);
            this.Controls.Add(this.comboBox);
            this.Name = "FormBlockPollingSetup";
            this.Size = new System.Drawing.Size(407, 24);
            this.panelLabel.ResumeLayout(false);
            this.panelEvery.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox;
        private System.Windows.Forms.Panel panelLabel;
        private System.Windows.Forms.CheckBox checkBox;
        private System.Windows.Forms.Panel panelEvery;
        private System.Windows.Forms.Label labelEvery;

    }
}
