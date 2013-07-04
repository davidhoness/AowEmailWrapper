namespace AowEmailWrapper.Controls
{
    partial class AutoconfigPage3Select
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoconfigPage3Select));
            this.groupBoxOptions = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelRight = new System.Windows.Forms.Panel();
            this.lblAutoconfigPage3UserDecidesMessage = new System.Windows.Forms.Label();
            this.radioAutoconfigPage3Select2 = new System.Windows.Forms.RadioButton();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.lblAutoconfigPage3WrapperDecidesMessage = new System.Windows.Forms.Label();
            this.fbServerPreference = new AowEmailWrapper.Controls.FormBlockCombo();
            this.radioAutoconfigPage3Select1 = new System.Windows.Forms.RadioButton();
            this.lblAutoconfigPage3ServerMessage = new System.Windows.Forms.Label();
            this.groupBoxOptions.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxOptions
            // 
            this.groupBoxOptions.Controls.Add(this.panel1);
            this.groupBoxOptions.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxOptions.Location = new System.Drawing.Point(0, 20);
            this.groupBoxOptions.Name = "groupBoxOptions";
            this.groupBoxOptions.Size = new System.Drawing.Size(455, 223);
            this.groupBoxOptions.TabIndex = 0;
            this.groupBoxOptions.TabStop = false;
            this.groupBoxOptions.Text = "Options";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelRight);
            this.panel1.Controls.Add(this.panelLeft);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(449, 181);
            this.panel1.TabIndex = 0;
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.lblAutoconfigPage3UserDecidesMessage);
            this.panelRight.Controls.Add(this.radioAutoconfigPage3Select2);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRight.Location = new System.Drawing.Point(249, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(200, 181);
            this.panelRight.TabIndex = 3;
            // 
            // lblAutoconfigPage3UserDecidesMessage
            // 
            this.lblAutoconfigPage3UserDecidesMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAutoconfigPage3UserDecidesMessage.Location = new System.Drawing.Point(0, 38);
            this.lblAutoconfigPage3UserDecidesMessage.Name = "lblAutoconfigPage3UserDecidesMessage";
            this.lblAutoconfigPage3UserDecidesMessage.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.lblAutoconfigPage3UserDecidesMessage.Size = new System.Drawing.Size(200, 131);
            this.lblAutoconfigPage3UserDecidesMessage.TabIndex = 2;
            this.lblAutoconfigPage3UserDecidesMessage.Text = "Use this option if you want to have precise control over which auto detected sett" +
                "ings the Wrapper should use.";
            // 
            // radioAutoconfigPage3Select2
            // 
            this.radioAutoconfigPage3Select2.Dock = System.Windows.Forms.DockStyle.Top;
            this.radioAutoconfigPage3Select2.Location = new System.Drawing.Point(0, 0);
            this.radioAutoconfigPage3Select2.Name = "radioAutoconfigPage3Select2";
            this.radioAutoconfigPage3Select2.Size = new System.Drawing.Size(200, 38);
            this.radioAutoconfigPage3Select2.TabIndex = 6;
            this.radioAutoconfigPage3Select2.TabStop = true;
            this.radioAutoconfigPage3Select2.Text = "Manual override, show the settings so that I can choose";
            this.radioAutoconfigPage3Select2.UseVisualStyleBackColor = true;
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.lblAutoconfigPage3WrapperDecidesMessage);
            this.panelLeft.Controls.Add(this.fbServerPreference);
            this.panelLeft.Controls.Add(this.radioAutoconfigPage3Select1);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(200, 181);
            this.panelLeft.TabIndex = 2;
            // 
            // lblAutoconfigPage3WrapperDecidesMessage
            // 
            this.lblAutoconfigPage3WrapperDecidesMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAutoconfigPage3WrapperDecidesMessage.Location = new System.Drawing.Point(0, 62);
            this.lblAutoconfigPage3WrapperDecidesMessage.Name = "lblAutoconfigPage3WrapperDecidesMessage";
            this.lblAutoconfigPage3WrapperDecidesMessage.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.lblAutoconfigPage3WrapperDecidesMessage.Size = new System.Drawing.Size(200, 107);
            this.lblAutoconfigPage3WrapperDecidesMessage.TabIndex = 4;
            this.lblAutoconfigPage3WrapperDecidesMessage.Text = resources.GetString("lblAutoconfigPage3WrapperDecidesMessage.Text");
            // 
            // fbServerPreference
            // 
            this.fbServerPreference.Dock = System.Windows.Forms.DockStyle.Top;
            this.fbServerPreference.LabelName = "Preference:";
            this.fbServerPreference.Location = new System.Drawing.Point(0, 38);
            this.fbServerPreference.Margin = new System.Windows.Forms.Padding(2);
            this.fbServerPreference.MinimumSize = new System.Drawing.Size(0, 24);
            this.fbServerPreference.Name = "fbServerPreference";
            this.fbServerPreference.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.fbServerPreference.SelectedIndex = -1;
            this.fbServerPreference.SelectedValue = "";
            this.fbServerPreference.Size = new System.Drawing.Size(200, 24);
            this.fbServerPreference.TabIndex = 3;
            // 
            // radioAutoconfigPage3Select1
            // 
            this.radioAutoconfigPage3Select1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radioAutoconfigPage3Select1.Location = new System.Drawing.Point(0, 0);
            this.radioAutoconfigPage3Select1.Name = "radioAutoconfigPage3Select1";
            this.radioAutoconfigPage3Select1.Size = new System.Drawing.Size(200, 38);
            this.radioAutoconfigPage3Select1.TabIndex = 5;
            this.radioAutoconfigPage3Select1.Text = "Let the Wrapper decide (default)";
            this.radioAutoconfigPage3Select1.UseVisualStyleBackColor = true;
            // 
            // lblAutoconfigPage3ServerMessage
            // 
            this.lblAutoconfigPage3ServerMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAutoconfigPage3ServerMessage.Location = new System.Drawing.Point(0, 0);
            this.lblAutoconfigPage3ServerMessage.Name = "lblAutoconfigPage3ServerMessage";
            this.lblAutoconfigPage3ServerMessage.Size = new System.Drawing.Size(455, 20);
            this.lblAutoconfigPage3ServerMessage.TabIndex = 2;
            this.lblAutoconfigPage3ServerMessage.Text = "Please specify which server settings should be used.";
            // 
            // AutoconfigPage3Select
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.groupBoxOptions);
            this.Controls.Add(this.lblAutoconfigPage3ServerMessage);
            this.Name = "AutoconfigPage3Select";
            this.Size = new System.Drawing.Size(455, 396);
            this.groupBoxOptions.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxOptions;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Panel panelLeft;
        private FormBlockCombo fbServerPreference;
        private System.Windows.Forms.Label lblAutoconfigPage3UserDecidesMessage;
        private System.Windows.Forms.Label lblAutoconfigPage3WrapperDecidesMessage;
        private System.Windows.Forms.Label lblAutoconfigPage3ServerMessage;
        private System.Windows.Forms.RadioButton radioAutoconfigPage3Select2;
        private System.Windows.Forms.RadioButton radioAutoconfigPage3Select1;
    }
}
