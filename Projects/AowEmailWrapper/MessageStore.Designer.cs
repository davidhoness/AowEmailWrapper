namespace AowEmailWrapper
{
    partial class MessageStore
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageStore));
            this.pnlButton = new System.Windows.Forms.Panel();
            this.panelMessage = new System.Windows.Forms.Panel();
            this.lblLocalMessageStoreMessage = new System.Windows.Forms.Label();
            this.panelRedownload = new System.Windows.Forms.Panel();
            this.cmdRedownload = new System.Windows.Forms.Button();
            this.pnlClose = new System.Windows.Forms.Panel();
            this.cmdClose = new System.Windows.Forms.Button();
            this.messageStoreList = new AowEmailWrapper.Controls.MessageStoreList();
            this.pnlButton.SuspendLayout();
            this.panelMessage.SuspendLayout();
            this.panelRedownload.SuspendLayout();
            this.pnlClose.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlButton
            // 
            this.pnlButton.Controls.Add(this.panelMessage);
            this.pnlButton.Controls.Add(this.panelRedownload);
            this.pnlButton.Controls.Add(this.pnlClose);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButton.Location = new System.Drawing.Point(0, 504);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Padding = new System.Windows.Forms.Padding(5);
            this.pnlButton.Size = new System.Drawing.Size(736, 109);
            this.pnlButton.TabIndex = 2;
            // 
            // panelMessage
            // 
            this.panelMessage.AutoScroll = true;
            this.panelMessage.BackColor = System.Drawing.SystemColors.Info;
            this.panelMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMessage.Controls.Add(this.lblLocalMessageStoreMessage);
            this.panelMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMessage.Location = new System.Drawing.Point(5, 5);
            this.panelMessage.Name = "panelMessage";
            this.panelMessage.Size = new System.Drawing.Size(526, 99);
            this.panelMessage.TabIndex = 18;
            // 
            // lblLocalMessageStoreMessage
            // 
            this.lblLocalMessageStoreMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLocalMessageStoreMessage.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblLocalMessageStoreMessage.Location = new System.Drawing.Point(0, 0);
            this.lblLocalMessageStoreMessage.Name = "lblLocalMessageStoreMessage";
            this.lblLocalMessageStoreMessage.Padding = new System.Windows.Forms.Padding(2);
            this.lblLocalMessageStoreMessage.Size = new System.Drawing.Size(524, 97);
            this.lblLocalMessageStoreMessage.TabIndex = 0;
            this.lblLocalMessageStoreMessage.Text = resources.GetString("lblLocalMessageStoreMessage.Text");
            // 
            // panelRedownload
            // 
            this.panelRedownload.Controls.Add(this.cmdRedownload);
            this.panelRedownload.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRedownload.Location = new System.Drawing.Point(531, 5);
            this.panelRedownload.Name = "panelRedownload";
            this.panelRedownload.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.panelRedownload.Size = new System.Drawing.Size(100, 99);
            this.panelRedownload.TabIndex = 7;
            // 
            // cmdRedownload
            // 
            this.cmdRedownload.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmdRedownload.Location = new System.Drawing.Point(5, 0);
            this.cmdRedownload.Name = "cmdRedownload";
            this.cmdRedownload.Size = new System.Drawing.Size(95, 54);
            this.cmdRedownload.TabIndex = 7;
            this.cmdRedownload.Text = "Re-Download";
            this.cmdRedownload.UseVisualStyleBackColor = true;
            this.cmdRedownload.Click += new System.EventHandler(this.cmdRedownload_Click);
            // 
            // pnlClose
            // 
            this.pnlClose.Controls.Add(this.cmdClose);
            this.pnlClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlClose.Location = new System.Drawing.Point(631, 5);
            this.pnlClose.Name = "pnlClose";
            this.pnlClose.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.pnlClose.Size = new System.Drawing.Size(100, 99);
            this.pnlClose.TabIndex = 5;
            // 
            // cmdClose
            // 
            this.cmdClose.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmdClose.Location = new System.Drawing.Point(5, 0);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(95, 54);
            this.cmdClose.TabIndex = 4;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // messageStoreList
            // 
            this.messageStoreList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageStoreList.ItemsRemoved = false;
            this.messageStoreList.Location = new System.Drawing.Point(0, 0);
            this.messageStoreList.Messages = null;
            this.messageStoreList.Name = "messageStoreList";
            this.messageStoreList.Padding = new System.Windows.Forms.Padding(5, 5, 5, 1);
            this.messageStoreList.Size = new System.Drawing.Size(736, 504);
            this.messageStoreList.TabIndex = 3;
            // 
            // MessageStore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 613);
            this.Controls.Add(this.messageStoreList);
            this.Controls.Add(this.pnlButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MessageStore";
            this.Text = "Local Message Store";
            this.pnlButton.ResumeLayout(false);
            this.panelMessage.ResumeLayout(false);
            this.panelRedownload.ResumeLayout(false);
            this.pnlClose.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlButton;
        private AowEmailWrapper.Controls.MessageStoreList messageStoreList;
        private System.Windows.Forms.Panel pnlClose;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Panel panelRedownload;
        private System.Windows.Forms.Button cmdRedownload;
        private System.Windows.Forms.Panel panelMessage;
        private System.Windows.Forms.Label lblLocalMessageStoreMessage;


    }
}