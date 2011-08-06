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
            this.labelEvery = new System.Windows.Forms.Label();
            this.checkBox = new System.Windows.Forms.CheckBox();
            this.panelLabel.SuspendLayout();
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
            this.panelLabel.Controls.Add(this.labelEvery);
            this.panelLabel.Controls.Add(this.checkBox);
            this.panelLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLabel.Location = new System.Drawing.Point(0, 0);
            this.panelLabel.Margin = new System.Windows.Forms.Padding(2);
            this.panelLabel.Name = "panelLabel";
            this.panelLabel.Size = new System.Drawing.Size(191, 29);
            this.panelLabel.TabIndex = 10;
            // 
            // labelEvery
            // 
            this.labelEvery.BackColor = System.Drawing.SystemColors.Control;
            this.labelEvery.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelEvery.Location = new System.Drawing.Point(141, 0);
            this.labelEvery.Name = "labelEvery";
            this.labelEvery.Size = new System.Drawing.Size(50, 29);
            this.labelEvery.TabIndex = 3;
            this.labelEvery.Text = "every:";
            this.labelEvery.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkBox
            // 
            this.checkBox.BackColor = System.Drawing.SystemColors.Control;
            this.checkBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox.Location = new System.Drawing.Point(0, 0);
            this.checkBox.Name = "checkBox";
            this.checkBox.Size = new System.Drawing.Size(109, 29);
            this.checkBox.TabIndex = 2;
            this.checkBox.Text = "Check for email";
            this.checkBox.UseVisualStyleBackColor = false;
            // 
            // FormBlockPollingSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelLabel);
            this.Controls.Add(this.comboBox);
            this.Name = "FormBlockPollingSetup";
            this.Size = new System.Drawing.Size(407, 29);
            this.panelLabel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox;
        private System.Windows.Forms.Panel panelLabel;
        private System.Windows.Forms.CheckBox checkBox;
        private System.Windows.Forms.Label labelEvery;

    }
}
