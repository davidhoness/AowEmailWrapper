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
    public partial class ServerChoiceCheckBox : UserControl
    {
        public EventHandler CheckedChanged;
        private IncomingServer _incomingServer;
        private OutgoingServer _outgoingServer;
        private Color _defaultBackColor;
        private List<Label> _labels;
        private bool _suspendChecked;

        public ServerChoiceCheckBox()
        {
            InitializeComponent();
            _defaultBackColor = this.BackColor;

            radioButton.CheckedChanged += new EventHandler(radioButton_CheckedChanged);

            EventHandler setCheckedClick = new EventHandler(setChecked_Click);

            pictureBoxImap.Click += setCheckedClick;
            pictureBoxPop.Click += setCheckedClick;
            pictureBoxSmtp.Click += setCheckedClick;

            _labels = new List<Label>() { lblServerChoiceCheckBoxServer, lblServerChoiceCheckBoxPort, lblServerChoiceCheckBoxSocket, labelHostValue, labelPortValue, labelSocketValue };
            _labels.ForEach(label => label.Click += setCheckedClick);

            _labels.ForEach(label => label.Text = Translator.Translate(label.Name));
        }

        private void setChecked_Click(object sender, EventArgs e)
        {
            this.Checked = true;
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckedChanged != null && !SuspendChecked)
            {
                CheckedChanged(this, e);
            }

            if (this.Checked)
            {
                this.BackColor = SystemColors.Highlight;
                SetLabelColor(SystemColors.HighlightText);
            }
            else
            {
                this.BackColor = _defaultBackColor;
                SetLabelColor(SystemColors.ControlText);
            }
        }

        public bool Checked
        {
            get { return radioButton.Checked; }
            set { radioButton.Checked = value; }
        }

        public bool SuspendChecked
        {
            get { return _suspendChecked; }
            set { _suspendChecked = value; }
        }

        public IncomingServer IncomingServer
        {
            get { return _incomingServer; }
            set { _incomingServer = value; ConfigureIncoming(); }
        }

        public OutgoingServer OutgoingServer
        {
            get { return _outgoingServer; }
            set { _outgoingServer = value; ConfigureOutgoing(); }
        }

        private void ConfigureIncoming()
        {
            if (IncomingServer != null)
            {
                switch (IncomingServer.Type)
                { 
                    case ServerType.IMAP:
                        pictureBoxImap.BringToFront();
                        break;
                    case ServerType.POP3:
                        pictureBoxPop.BringToFront();
                        break;
                }

                labelHostValue.Text = IncomingServer.Hostname;
                labelPortValue.Text = IncomingServer.Port.ToString();
                labelSocketValue.Text = IncomingServer.SocketType.ToString();
            }
        }

        private void ConfigureOutgoing()
        {
            if (OutgoingServer != null)
            {
                pictureBoxSmtp.BringToFront();

                labelHostValue.Text = OutgoingServer.Hostname;
                labelPortValue.Text = OutgoingServer.Port.ToString();
                labelSocketValue.Text = OutgoingServer.SocketType.ToString();
            }
        }

        private void SetLabelColor(Color theColor)
        {
            _labels.ForEach(label => label.ForeColor = theColor);
        }
    }
}
