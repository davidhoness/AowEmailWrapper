using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mozilla.Autoconfig;
using AowEmailWrapper.Helpers;
using AowEmailWrapper.ConfigFramework;
using AowEmailWrapper.Localization;

namespace AowEmailWrapper.Controls
{
    public partial class ServerChoiceControl : UserControl
    {
        public EventHandler Cancelled;
        public EventHandler ConfigChosen;

        private EmailProvider _emailProvider;

        public ServerChoiceControl()
        {
            InitializeComponent();

            cmdFinish.Click += new EventHandler(cmdFinish_Click);
            cmdCancel.Click += new EventHandler(cmdCancel_Click);
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            if (Cancelled != null)
            {
                Cancelled(this, e);
            }
        }

        private void cmdFinish_Click(object sender, EventArgs e)
        {
            if (ConfigChosen != null)
            {
                ConfigChosen(this, e);
            }
        }

        public EmailProvider EmailProvider
        {
            get { return Scrape(); }
            set { _emailProvider = value; Configure(); }
        }

        private void Configure()
        {
            this.SuspendLayout();

            panelIncomingContent.Controls.Clear();
            panelOutgoingContent.Controls.Clear();

            if (_emailProvider != null)
            {
                int tabCount = 0;

                if (_emailProvider.IncomingServers != null &&
                    _emailProvider.IncomingServers.Count > 0)
                {
                    foreach (IncomingServer incoming in _emailProvider.IncomingServers)
                    {
                        ServerChoiceCheckBox checkBox = new ServerChoiceCheckBox();
                        checkBox.IncomingServer = incoming;
                        checkBox.CheckedChanged += new EventHandler(checkBox_CheckedChanged_IncomingServer);
                        panelIncomingContent.Controls.Add(checkBox);
                        checkBox.Dock = DockStyle.Top;
                        checkBox.BringToFront();
                        checkBox.TabStop = true;
                        checkBox.TabIndex = tabCount;
                        tabCount++;
                    }
                }

                if (_emailProvider.OutgoingServers != null &&
                    _emailProvider.OutgoingServers.Count > 0)
                {
                    foreach (OutgoingServer outgoing in _emailProvider.OutgoingServers)
                    {
                        ServerChoiceCheckBox checkBox = new ServerChoiceCheckBox();
                        checkBox.OutgoingServer = outgoing;
                        checkBox.CheckedChanged += new EventHandler(checkBox_CheckedChanged_OutgoingServer);
                        panelOutgoingContent.Controls.Add(checkBox);
                        checkBox.Dock = DockStyle.Top;
                        checkBox.BringToFront();
                        checkBox.TabStop = true;
                        checkBox.TabIndex = tabCount;
                        tabCount++;
                    }
                }
            }

            this.ResumeLayout();
        }

        private EmailProvider Scrape()
        {
            EmailProvider returnVal = null;

            if (_emailProvider != null)
            {
                returnVal = XmlHelper.Deserialize<EmailProvider>(XmlHelper.Serialize(_emailProvider));

                if (returnVal.IncomingServers == null)
                {
                    returnVal.IncomingServers = new List<IncomingServer>();
                }
                returnVal.IncomingServers.Clear();
                ServerChoiceCheckBox incomingChoice = GetChecked(panelIncomingContent);
                if (incomingChoice != null && incomingChoice.IncomingServer != null)
                {
                    returnVal.IncomingServers.Add(incomingChoice.IncomingServer);
                }

                if (returnVal.OutgoingServers == null)
                {
                    returnVal.OutgoingServers = new List<OutgoingServer>();
                }
                returnVal.OutgoingServers.Clear();
                ServerChoiceCheckBox outgoingChoice = GetChecked(panelOutgoingContent);
                if (outgoingChoice != null && outgoingChoice.OutgoingServer != null)
                {
                    returnVal.OutgoingServers.Add(outgoingChoice.OutgoingServer);
                }
            }

            return returnVal;
        }

        private void checkBox_CheckedChanged_IncomingServer(object sender, EventArgs e)
        {
            ServerChoiceCheckBox senderCheckBox = sender as ServerChoiceCheckBox;
            Toggle(panelIncomingContent, senderCheckBox);
            IsValid();
        }

        private void checkBox_CheckedChanged_OutgoingServer(object sender, EventArgs e)
        {
            ServerChoiceCheckBox senderCheckBox = sender as ServerChoiceCheckBox;
            Toggle(panelOutgoingContent, senderCheckBox);
            IsValid();
        }

        private void Toggle(Panel thePanel, ServerChoiceCheckBox senderCheckBox)
        {           
            foreach (Control control in thePanel.Controls)
            {
                ServerChoiceCheckBox checkBox = control as ServerChoiceCheckBox;

                if (checkBox != null && !senderCheckBox.Equals(checkBox))
                {
                    checkBox.SuspendChecked = true;
                    checkBox.Checked = false;
                    checkBox.SuspendChecked = false;
                }
            }
        }

        private ServerChoiceCheckBox GetChecked(Panel thePanel)
        {
            ServerChoiceCheckBox returnVal = null;

            foreach (Control control in thePanel.Controls)
            {
                ServerChoiceCheckBox checkBox = control as ServerChoiceCheckBox;

                if (checkBox != null && checkBox.Checked)
                {
                    returnVal = checkBox;
                    break;
                }
            }

            return returnVal;
        }

        private void IsValid()
        {
            cmdFinish.Enabled = 
                GetChecked(panelIncomingContent) != null && 
                GetChecked(panelOutgoingContent) != null;
        }
    }
}

