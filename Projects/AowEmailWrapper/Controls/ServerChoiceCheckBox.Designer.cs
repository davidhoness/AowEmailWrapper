namespace AowEmailWrapper.Controls
{
    partial class ServerChoiceCheckBox
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
            this.panelLeft = new System.Windows.Forms.Panel();
            this.radioButton = new System.Windows.Forms.RadioButton();
            this.panelImage = new System.Windows.Forms.Panel();
            this.pictureBoxSmtp = new System.Windows.Forms.PictureBox();
            this.pictureBoxPop = new System.Windows.Forms.PictureBox();
            this.pictureBoxImap = new System.Windows.Forms.PictureBox();
            this.panelRight = new System.Windows.Forms.Panel();
            this.panelValues = new System.Windows.Forms.Panel();
            this.labelSocketValue = new System.Windows.Forms.Label();
            this.labelPortValue = new System.Windows.Forms.Label();
            this.labelHostValue = new System.Windows.Forms.Label();
            this.panelLabels = new System.Windows.Forms.Panel();
            this.labelSocket = new System.Windows.Forms.Label();
            this.labelPort = new System.Windows.Forms.Label();
            this.labelServer = new System.Windows.Forms.Label();
            this.panelLeft.SuspendLayout();
            this.panelImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSmtp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImap)).BeginInit();
            this.panelRight.SuspendLayout();
            this.panelValues.SuspendLayout();
            this.panelLabels.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelLeft
            // 
            this.panelLeft.BackColor = System.Drawing.Color.Transparent;
            this.panelLeft.Controls.Add(this.radioButton);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(30, 65);
            this.panelLeft.TabIndex = 0;
            // 
            // radioButton
            // 
            this.radioButton.AutoSize = true;
            this.radioButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioButton.Location = new System.Drawing.Point(0, 0);
            this.radioButton.Name = "radioButton";
            this.radioButton.Size = new System.Drawing.Size(30, 65);
            this.radioButton.TabIndex = 0;
            this.radioButton.TabStop = true;
            this.radioButton.Text = "  ";
            this.radioButton.UseVisualStyleBackColor = true;
            // 
            // panelImage
            // 
            this.panelImage.BackColor = System.Drawing.Color.Transparent;
            this.panelImage.Controls.Add(this.pictureBoxSmtp);
            this.panelImage.Controls.Add(this.pictureBoxPop);
            this.panelImage.Controls.Add(this.pictureBoxImap);
            this.panelImage.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelImage.Location = new System.Drawing.Point(30, 0);
            this.panelImage.Name = "panelImage";
            this.panelImage.Size = new System.Drawing.Size(70, 65);
            this.panelImage.TabIndex = 1;
            // 
            // pictureBoxSmtp
            // 
            this.pictureBoxSmtp.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxSmtp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxSmtp.Image = global::AowEmailWrapper.Properties.Resources.smtp_server;
            this.pictureBoxSmtp.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxSmtp.Name = "pictureBoxSmtp";
            this.pictureBoxSmtp.Size = new System.Drawing.Size(70, 65);
            this.pictureBoxSmtp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxSmtp.TabIndex = 2;
            this.pictureBoxSmtp.TabStop = false;
            // 
            // pictureBoxPop
            // 
            this.pictureBoxPop.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxPop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxPop.Image = global::AowEmailWrapper.Properties.Resources.pop3_server;
            this.pictureBoxPop.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxPop.Name = "pictureBoxPop";
            this.pictureBoxPop.Size = new System.Drawing.Size(70, 65);
            this.pictureBoxPop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxPop.TabIndex = 1;
            this.pictureBoxPop.TabStop = false;
            // 
            // pictureBoxImap
            // 
            this.pictureBoxImap.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxImap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxImap.Image = global::AowEmailWrapper.Properties.Resources.imap_server;
            this.pictureBoxImap.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxImap.Name = "pictureBoxImap";
            this.pictureBoxImap.Size = new System.Drawing.Size(70, 65);
            this.pictureBoxImap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxImap.TabIndex = 0;
            this.pictureBoxImap.TabStop = false;
            // 
            // panelRight
            // 
            this.panelRight.BackColor = System.Drawing.Color.Transparent;
            this.panelRight.Controls.Add(this.panelValues);
            this.panelRight.Controls.Add(this.panelLabels);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(100, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.panelRight.Size = new System.Drawing.Size(272, 65);
            this.panelRight.TabIndex = 2;
            // 
            // panelValues
            // 
            this.panelValues.Controls.Add(this.labelSocketValue);
            this.panelValues.Controls.Add(this.labelPortValue);
            this.panelValues.Controls.Add(this.labelHostValue);
            this.panelValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelValues.Location = new System.Drawing.Point(66, 6);
            this.panelValues.Name = "panelValues";
            this.panelValues.Size = new System.Drawing.Size(206, 59);
            this.panelValues.TabIndex = 1;
            // 
            // labelSocketValue
            // 
            this.labelSocketValue.BackColor = System.Drawing.Color.Transparent;
            this.labelSocketValue.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelSocketValue.Location = new System.Drawing.Point(0, 40);
            this.labelSocketValue.Name = "labelSocketValue";
            this.labelSocketValue.Size = new System.Drawing.Size(206, 20);
            this.labelSocketValue.TabIndex = 6;
            this.labelSocketValue.Text = "TLS";
            // 
            // labelPortValue
            // 
            this.labelPortValue.BackColor = System.Drawing.Color.Transparent;
            this.labelPortValue.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelPortValue.Location = new System.Drawing.Point(0, 20);
            this.labelPortValue.Name = "labelPortValue";
            this.labelPortValue.Size = new System.Drawing.Size(206, 20);
            this.labelPortValue.TabIndex = 5;
            this.labelPortValue.Text = "587";
            // 
            // labelHostValue
            // 
            this.labelHostValue.BackColor = System.Drawing.Color.Transparent;
            this.labelHostValue.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelHostValue.Location = new System.Drawing.Point(0, 0);
            this.labelHostValue.Name = "labelHostValue";
            this.labelHostValue.Size = new System.Drawing.Size(206, 20);
            this.labelHostValue.TabIndex = 4;
            this.labelHostValue.Text = "mail.myserver.com";
            // 
            // panelLabels
            // 
            this.panelLabels.BackColor = System.Drawing.Color.Transparent;
            this.panelLabels.Controls.Add(this.labelSocket);
            this.panelLabels.Controls.Add(this.labelPort);
            this.panelLabels.Controls.Add(this.labelServer);
            this.panelLabels.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLabels.Location = new System.Drawing.Point(0, 6);
            this.panelLabels.Name = "panelLabels";
            this.panelLabels.Size = new System.Drawing.Size(66, 59);
            this.panelLabels.TabIndex = 0;
            // 
            // labelSocket
            // 
            this.labelSocket.BackColor = System.Drawing.Color.Transparent;
            this.labelSocket.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelSocket.Location = new System.Drawing.Point(0, 40);
            this.labelSocket.Name = "labelSocket";
            this.labelSocket.Size = new System.Drawing.Size(66, 20);
            this.labelSocket.TabIndex = 2;
            this.labelSocket.Text = "Socket:";
            this.labelSocket.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelPort
            // 
            this.labelPort.BackColor = System.Drawing.Color.Transparent;
            this.labelPort.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelPort.Location = new System.Drawing.Point(0, 20);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(66, 20);
            this.labelPort.TabIndex = 1;
            this.labelPort.Text = "Port:";
            this.labelPort.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelServer
            // 
            this.labelServer.BackColor = System.Drawing.Color.Transparent;
            this.labelServer.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelServer.Location = new System.Drawing.Point(0, 0);
            this.labelServer.Name = "labelServer";
            this.labelServer.Size = new System.Drawing.Size(66, 20);
            this.labelServer.TabIndex = 0;
            this.labelServer.Text = "Server:";
            this.labelServer.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ServerChoiceCheckBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.panelImage);
            this.Controls.Add(this.panelLeft);
            this.Name = "ServerChoiceCheckBox";
            this.Size = new System.Drawing.Size(372, 65);
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            this.panelImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSmtp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImap)).EndInit();
            this.panelRight.ResumeLayout(false);
            this.panelValues.ResumeLayout(false);
            this.panelLabels.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelImage;
        private System.Windows.Forms.PictureBox pictureBoxImap;
        private System.Windows.Forms.PictureBox pictureBoxPop;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Panel panelLabels;
        private System.Windows.Forms.Label labelSocket;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.Label labelServer;
        private System.Windows.Forms.PictureBox pictureBoxSmtp;
        private System.Windows.Forms.RadioButton radioButton;
        private System.Windows.Forms.Panel panelValues;
        private System.Windows.Forms.Label labelSocketValue;
        private System.Windows.Forms.Label labelPortValue;
        private System.Windows.Forms.Label labelHostValue;
    }
}
