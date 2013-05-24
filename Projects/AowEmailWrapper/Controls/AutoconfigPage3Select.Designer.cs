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
            this.label2 = new System.Windows.Forms.Label();
            this.cmdUserDecides = new System.Windows.Forms.Button();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.fbServerPreference = new AowEmailWrapper.Controls.FormBlockCombo();
            this.cmdWrapperDecides = new System.Windows.Forms.Button();
            this.labelServerMessage = new System.Windows.Forms.Label();
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
            this.panel1.Size = new System.Drawing.Size(449, 169);
            this.panel1.TabIndex = 0;
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.label2);
            this.panelRight.Controls.Add(this.cmdUserDecides);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRight.Location = new System.Drawing.Point(249, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(200, 169);
            this.panelRight.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 38);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label2.Size = new System.Drawing.Size(200, 68);
            this.label2.TabIndex = 2;
            this.label2.Text = "Use this option if you want to have precise control over which auto detected sett" +
                "ings the Wrapper should use.";
            // 
            // cmdUserDecides
            // 
            this.cmdUserDecides.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmdUserDecides.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdUserDecides.Location = new System.Drawing.Point(0, 0);
            this.cmdUserDecides.Name = "cmdUserDecides";
            this.cmdUserDecides.Size = new System.Drawing.Size(200, 38);
            this.cmdUserDecides.TabIndex = 1;
            this.cmdUserDecides.Text = "Manual override, show the settings so that I can choose";
            this.cmdUserDecides.UseVisualStyleBackColor = true;
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.label1);
            this.panelLeft.Controls.Add(this.fbServerPreference);
            this.panelLeft.Controls.Add(this.cmdWrapperDecides);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(200, 169);
            this.panelLeft.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 62);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label1.Size = new System.Drawing.Size(200, 83);
            this.label1.TabIndex = 4;
            this.label1.Text = resources.GetString("label1.Text");
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
            // cmdWrapperDecides
            // 
            this.cmdWrapperDecides.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmdWrapperDecides.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdWrapperDecides.Location = new System.Drawing.Point(0, 0);
            this.cmdWrapperDecides.Name = "cmdWrapperDecides";
            this.cmdWrapperDecides.Size = new System.Drawing.Size(200, 38);
            this.cmdWrapperDecides.TabIndex = 0;
            this.cmdWrapperDecides.Text = "Let the Wrapper decide (default)";
            this.cmdWrapperDecides.UseVisualStyleBackColor = true;
            // 
            // labelServerMessage
            // 
            this.labelServerMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelServerMessage.Location = new System.Drawing.Point(0, 0);
            this.labelServerMessage.Name = "labelServerMessage";
            this.labelServerMessage.Size = new System.Drawing.Size(455, 20);
            this.labelServerMessage.TabIndex = 2;
            this.labelServerMessage.Text = "Please specify which server settings should be used.";
            // 
            // AutoconfigPage3Select
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.groupBoxOptions);
            this.Controls.Add(this.labelServerMessage);
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
        private System.Windows.Forms.Button cmdUserDecides;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Button cmdWrapperDecides;
        private FormBlockCombo fbServerPreference;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelServerMessage;
    }
}
