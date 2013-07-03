namespace AowEmailWrapper.Controls
{
    partial class AutoconfigPage2Search
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoconfigPage2Search));
            this.labelSearchMessage = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.groupBoxResult = new System.Windows.Forms.GroupBox();
            this.labelResultMessage = new System.Windows.Forms.Label();
            this.pictureBoxFailed = new System.Windows.Forms.PictureBox();
            this.pictureBoxSuccess = new System.Windows.Forms.PictureBox();
            this.groupBoxNext = new System.Windows.Forms.GroupBox();
            this.panelRight = new System.Windows.Forms.Panel();
            this.labelGuess = new System.Windows.Forms.Label();
            this.radioAutoconfigPage2Search2 = new System.Windows.Forms.RadioButton();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.labelMxLookup = new System.Windows.Forms.Label();
            this.radioAutoconfigPage2Search1 = new System.Windows.Forms.RadioButton();
            this.panelManual = new System.Windows.Forms.Panel();
            this.radioAutoconfigPage2Search3 = new System.Windows.Forms.RadioButton();
            this.groupBoxResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFailed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSuccess)).BeginInit();
            this.groupBoxNext.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelManual.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelSearchMessage
            // 
            this.labelSearchMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelSearchMessage.Location = new System.Drawing.Point(0, 0);
            this.labelSearchMessage.Name = "labelSearchMessage";
            this.labelSearchMessage.Size = new System.Drawing.Size(413, 20);
            this.labelSearchMessage.TabIndex = 0;
            this.labelSearchMessage.Text = "Searching for your email settings online…";
            // 
            // progressBar
            // 
            this.progressBar.BackColor = System.Drawing.Color.White;
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressBar.Location = new System.Drawing.Point(0, 20);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(413, 22);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 1;
            // 
            // groupBoxResult
            // 
            this.groupBoxResult.Controls.Add(this.labelResultMessage);
            this.groupBoxResult.Controls.Add(this.pictureBoxFailed);
            this.groupBoxResult.Controls.Add(this.pictureBoxSuccess);
            this.groupBoxResult.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxResult.Location = new System.Drawing.Point(0, 42);
            this.groupBoxResult.Name = "groupBoxResult";
            this.groupBoxResult.Size = new System.Drawing.Size(413, 63);
            this.groupBoxResult.TabIndex = 2;
            this.groupBoxResult.TabStop = false;
            this.groupBoxResult.Text = "Result";
            this.groupBoxResult.Visible = false;
            // 
            // labelResultMessage
            // 
            this.labelResultMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelResultMessage.Location = new System.Drawing.Point(87, 16);
            this.labelResultMessage.Name = "labelResultMessage";
            this.labelResultMessage.Size = new System.Drawing.Size(323, 44);
            this.labelResultMessage.TabIndex = 6;
            // 
            // pictureBoxFailed
            // 
            this.pictureBoxFailed.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBoxFailed.Image = global::AowEmailWrapper.Properties.Resources.cross;
            this.pictureBoxFailed.Location = new System.Drawing.Point(45, 16);
            this.pictureBoxFailed.Name = "pictureBoxFailed";
            this.pictureBoxFailed.Size = new System.Drawing.Size(42, 44);
            this.pictureBoxFailed.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxFailed.TabIndex = 5;
            this.pictureBoxFailed.TabStop = false;
            // 
            // pictureBoxSuccess
            // 
            this.pictureBoxSuccess.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBoxSuccess.Image = global::AowEmailWrapper.Properties.Resources.tick;
            this.pictureBoxSuccess.Location = new System.Drawing.Point(3, 16);
            this.pictureBoxSuccess.Name = "pictureBoxSuccess";
            this.pictureBoxSuccess.Size = new System.Drawing.Size(42, 44);
            this.pictureBoxSuccess.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxSuccess.TabIndex = 0;
            this.pictureBoxSuccess.TabStop = false;
            // 
            // groupBoxNext
            // 
            this.groupBoxNext.Controls.Add(this.panelRight);
            this.groupBoxNext.Controls.Add(this.panelLeft);
            this.groupBoxNext.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxNext.Location = new System.Drawing.Point(0, 105);
            this.groupBoxNext.Name = "groupBoxNext";
            this.groupBoxNext.Size = new System.Drawing.Size(413, 164);
            this.groupBoxNext.TabIndex = 3;
            this.groupBoxNext.TabStop = false;
            this.groupBoxNext.Text = "What next?";
            this.groupBoxNext.Visible = false;
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.labelGuess);
            this.panelRight.Controls.Add(this.radioAutoconfigPage2Search2);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRight.Location = new System.Drawing.Point(210, 16);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(200, 145);
            this.panelRight.TabIndex = 1;
            // 
            // labelGuess
            // 
            this.labelGuess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelGuess.Location = new System.Drawing.Point(0, 38);
            this.labelGuess.Name = "labelGuess";
            this.labelGuess.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.labelGuess.Size = new System.Drawing.Size(200, 107);
            this.labelGuess.TabIndex = 1;
            this.labelGuess.Text = resources.GetString("labelGuess.Text");
            // 
            // radioAutoconfigPage2Search2
            // 
            this.radioAutoconfigPage2Search2.Dock = System.Windows.Forms.DockStyle.Top;
            this.radioAutoconfigPage2Search2.Location = new System.Drawing.Point(0, 0);
            this.radioAutoconfigPage2Search2.Name = "radioAutoconfigPage2Search2";
            this.radioAutoconfigPage2Search2.Size = new System.Drawing.Size(200, 38);
            this.radioAutoconfigPage2Search2.TabIndex = 3;
            this.radioAutoconfigPage2Search2.TabStop = true;
            this.radioAutoconfigPage2Search2.Text = "Try to guess";
            this.radioAutoconfigPage2Search2.UseVisualStyleBackColor = true;
            this.radioAutoconfigPage2Search2.CheckedChanged += new System.EventHandler(this.radioAutoconfigPage2Search2_CheckedChanged);
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.labelMxLookup);
            this.panelLeft.Controls.Add(this.radioAutoconfigPage2Search1);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(3, 16);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(200, 145);
            this.panelLeft.TabIndex = 0;
            // 
            // labelMxLookup
            // 
            this.labelMxLookup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelMxLookup.Location = new System.Drawing.Point(0, 38);
            this.labelMxLookup.Name = "labelMxLookup";
            this.labelMxLookup.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.labelMxLookup.Size = new System.Drawing.Size(200, 107);
            this.labelMxLookup.TabIndex = 1;
            this.labelMxLookup.Text = resources.GetString("labelMxLookup.Text");
            // 
            // radioAutoconfigPage2Search1
            // 
            this.radioAutoconfigPage2Search1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radioAutoconfigPage2Search1.Location = new System.Drawing.Point(0, 0);
            this.radioAutoconfigPage2Search1.Name = "radioAutoconfigPage2Search1";
            this.radioAutoconfigPage2Search1.Size = new System.Drawing.Size(200, 38);
            this.radioAutoconfigPage2Search1.TabIndex = 2;
            this.radioAutoconfigPage2Search1.Text = "Try Mx Lookup";
            this.radioAutoconfigPage2Search1.UseVisualStyleBackColor = true;
            this.radioAutoconfigPage2Search1.CheckedChanged += new System.EventHandler(this.radioAutoconfigPage2Search1_CheckedChanged);
            // 
            // panelManual
            // 
            this.panelManual.Controls.Add(this.radioAutoconfigPage2Search3);
            this.panelManual.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelManual.Location = new System.Drawing.Point(0, 269);
            this.panelManual.Name = "panelManual";
            this.panelManual.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.panelManual.Size = new System.Drawing.Size(413, 44);
            this.panelManual.TabIndex = 5;
            // 
            // radioAutoconfigPage2Search3
            // 
            this.radioAutoconfigPage2Search3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioAutoconfigPage2Search3.Location = new System.Drawing.Point(0, 3);
            this.radioAutoconfigPage2Search3.Name = "radioAutoconfigPage2Search3";
            this.radioAutoconfigPage2Search3.Size = new System.Drawing.Size(413, 38);
            this.radioAutoconfigPage2Search3.TabIndex = 0;
            this.radioAutoconfigPage2Search3.TabStop = true;
            this.radioAutoconfigPage2Search3.Text = "Manual override, let me enter the settings myself";
            this.radioAutoconfigPage2Search3.UseVisualStyleBackColor = true;
            this.radioAutoconfigPage2Search3.CheckedChanged += new System.EventHandler(this.radioAutoconfigPage2Search3_CheckedChanged);
            // 
            // AutoconfigPage2Search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panelManual);
            this.Controls.Add(this.groupBoxNext);
            this.Controls.Add(this.groupBoxResult);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.labelSearchMessage);
            this.Name = "AutoconfigPage2Search";
            this.Size = new System.Drawing.Size(413, 343);
            this.groupBoxResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFailed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSuccess)).EndInit();
            this.groupBoxNext.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.panelManual.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelSearchMessage;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.GroupBox groupBoxResult;
        private System.Windows.Forms.GroupBox groupBoxNext;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Label labelGuess;
        private System.Windows.Forms.Label labelMxLookup;
        private System.Windows.Forms.PictureBox pictureBoxSuccess;
        private System.Windows.Forms.Label labelResultMessage;
        private System.Windows.Forms.PictureBox pictureBoxFailed;
        private System.Windows.Forms.Panel panelManual;
        private System.Windows.Forms.RadioButton radioAutoconfigPage2Search3;
        private System.Windows.Forms.RadioButton radioAutoconfigPage2Search2;
        private System.Windows.Forms.RadioButton radioAutoconfigPage2Search1;
    }
}
