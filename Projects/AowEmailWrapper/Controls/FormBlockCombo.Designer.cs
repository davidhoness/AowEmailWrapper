namespace AowEmailWrapper.Controls
{
    partial class FormBlockCombo
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
            this.lblName = new System.Windows.Forms.Label();
            this.panelLabel.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox
            // 
            this.comboBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Location = new System.Drawing.Point(135, 0);
            this.comboBox.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(224, 21);
            this.comboBox.TabIndex = 0;
            // 
            // panelLabel
            // 
            this.panelLabel.Controls.Add(this.lblName);
            this.panelLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLabel.Location = new System.Drawing.Point(0, 0);
            this.panelLabel.Margin = new System.Windows.Forms.Padding(2);
            this.panelLabel.Name = "panelLabel";
            this.panelLabel.Size = new System.Drawing.Size(135, 24);
            this.panelLabel.TabIndex = 3;
            // 
            // lblName
            // 
            this.lblName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblName.Location = new System.Drawing.Point(0, 0);
            this.lblName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblName.MinimumSize = new System.Drawing.Size(0, 20);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(135, 20);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Name";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FormBlockCombo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelLabel);
            this.Controls.Add(this.comboBox);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormBlockCombo";
            this.Size = new System.Drawing.Size(359, 24);
            this.panelLabel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox;
        private System.Windows.Forms.Panel panelLabel;
        private System.Windows.Forms.Label lblName;
    }
}
