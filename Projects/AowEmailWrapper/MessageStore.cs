using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AowEmailWrapper.Pollers.MessageStore;
using AowEmailWrapper.Helpers;
using AowEmailWrapper.Localization;

namespace AowEmailWrapper
{
    public partial class MessageStore : Form
    {
        public EventHandler OnReDownload;
        private string _username;
        private string _host;

        public MessageStore(string username, string host)
        {
            InitializeComponent();

            Translator.TranslateForm(this);

            _username = username;
            _host = host;

            messageStoreList.Messages = MessageStoreManager.LoadLocalMessageStore(_username, _host);
            this.Text = Translator.Translate(this.Name, _username, _host);
        }

        private void cmdRedownload_Click(object sender, EventArgs e)
        {
            MessageStoreCollection theMessageStore = messageStoreList.Messages;

            if (messageStoreList.ItemsRemoved)
            {
                MessageStoreManager.SaveLocalMessageStore(_username, _host, theMessageStore);

                if (OnReDownload != null)
                {
                    this.Invoke(OnReDownload);
                }
            }

            this.Close();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
