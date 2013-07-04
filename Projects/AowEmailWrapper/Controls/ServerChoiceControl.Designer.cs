namespace AowEmailWrapper.Controls
{
    partial class ServerChoiceControl
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
            this.panelBottom = new System.Windows.Forms.Panel();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.cmdFinish = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.groupBoxOutgoing = new System.Windows.Forms.GroupBox();
            this.panelOutgoingContent = new System.Windows.Forms.Panel();
            this.groupBoxIncoming = new System.Windows.Forms.GroupBox();
            this.panelIncomingContent = new System.Windows.Forms.Panel();
            this.panelBottom.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.groupBoxOutgoing.SuspendLayout();
            this.groupBoxIncoming.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBottom
            // 
            this.panelBottom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelBottom.Controls.Add(this.panelButtons);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 502);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(797, 47);
            this.panelBottom.TabIndex = 2;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.cmdFinish);
            this.panelButtons.Controls.Add(this.cmdCancel);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtons.Location = new System.Drawing.Point(425, 0);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(368, 43);
            this.panelButtons.TabIndex = 3;
            // 
            // cmdFinish
            // 
            this.cmdFinish.Enabled = false;
            this.cmdFinish.Location = new System.Drawing.Point(139, 7);
            this.cmdFinish.Name = "cmdFinish";
            this.cmdFinish.Size = new System.Drawing.Size(107, 30);
            this.cmdFinish.TabIndex = 0;
            this.cmdFinish.Text = "Finish";
            this.cmdFinish.UseVisualStyleBackColor = true;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(252, 7);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(107, 30);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.groupBoxOutgoing);
            this.panelMain.Controls.Add(this.groupBoxIncoming);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.panelMain.Size = new System.Drawing.Size(797, 502);
            this.panelMain.TabIndex = 3;
            // 
            // groupBoxOutgoing
            // 
            this.groupBoxOutgoing.Controls.Add(this.panelOutgoingContent);
            this.groupBoxOutgoing.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBoxOutgoing.Location = new System.Drawing.Point(447, 3);
            this.groupBoxOutgoing.Name = "groupBoxOutgoing";
            this.groupBoxOutgoing.Size = new System.Drawing.Size(350, 496);
            this.groupBoxOutgoing.TabIndex = 3;
            this.groupBoxOutgoing.TabStop = false;
            this.groupBoxOutgoing.Text = "Outgoing Email";
            // 
            // panelOutgoingContent
            // 
            this.panelOutgoingContent.AutoScroll = true;
            this.panelOutgoingContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOutgoingContent.Location = new System.Drawing.Point(3, 16);
            this.panelOutgoingContent.Name = "panelOutgoingContent";
            this.panelOutgoingContent.Size = new System.Drawing.Size(344, 477);
            this.panelOutgoingContent.TabIndex = 1;
            // 
            // groupBoxIncoming
            // 
            this.groupBoxIncoming.Controls.Add(this.panelIncomingContent);
            this.groupBoxIncoming.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBoxIncoming.Location = new System.Drawing.Point(0, 3);
            this.groupBoxIncoming.Name = "groupBoxIncoming";
            this.groupBoxIncoming.Size = new System.Drawing.Size(350, 496);
            this.groupBoxIncoming.TabIndex = 2;
            this.groupBoxIncoming.TabStop = false;
            this.groupBoxIncoming.Text = "Incoming Email";
            // 
            // panelIncomingContent
            // 
            this.panelIncomingContent.AutoScroll = true;
            this.panelIncomingContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelIncomingContent.Location = new System.Drawing.Point(3, 16);
            this.panelIncomingContent.Name = "panelIncomingContent";
            this.panelIncomingContent.Size = new System.Drawing.Size(344, 477);
            this.panelIncomingContent.TabIndex = 0;
            // 
            // ServerChoiceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelBottom);
            this.Name = "ServerChoiceControl";
            this.Size = new System.Drawing.Size(797, 549);
            this.panelBottom.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.groupBoxOutgoing.ResumeLayout(false);
            this.groupBoxIncoming.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button cmdFinish;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.GroupBox groupBoxOutgoing;
        private System.Windows.Forms.Panel panelOutgoingContent;
        private System.Windows.Forms.GroupBox groupBoxIncoming;
        private System.Windows.Forms.Panel panelIncomingContent;

    }
}
