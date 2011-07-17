using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AowEmailWrapper.Classes;
using AowEmailWrapper.Pollers.MessageStore;

namespace AowEmailWrapper.Controls
{
    public partial class MessageStoreList : UserControl
    {
        private MessageStoreCollection _messageStoreCollection;
        private ListViewColumnSorter _lvwColumnSorter;
        private bool _itemsRemoved;        

        public bool ItemsRemoved
        {
            get { return _itemsRemoved; }
            set { _itemsRemoved = value; }
        }

        public MessageStoreCollection Messages
        {
            get 
            {
                RemoveCheckedItems();
                return _messageStoreCollection; 
            }
            set
            { 
                _messageStoreCollection = value;                
                _itemsRemoved = false;
                Populate();
            }
        }

        public MessageStoreList()
        {
            InitializeComponent();
            _lvwColumnSorter = new ListViewColumnSorter();
            listView.ListViewItemSorter = _lvwColumnSorter;
            listView.ColumnClick += new ColumnClickEventHandler(listView_ColumnClick);
            listView.HeaderStyle = ColumnHeaderStyle.Clickable;
        }

        private void Populate()
        {
            if (_messageStoreCollection != null)
            {
                listView.Items.Clear();
                listView.SuspendLayout();

                List<MessageStoreMessage> gameMessages = _messageStoreCollection.Messages.FindAll(msg => !string.IsNullOrEmpty(msg.From));
                if (gameMessages.Count > 0)
                {
                    foreach (MessageStoreMessage msg in gameMessages)
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = msg.From;
                        item.SubItems.Add(new ListViewItem.ListViewSubItem(item, msg.Subject));
                        item.SubItems.Add(new ListViewItem.ListViewSubItem(item, msg.Date));
                        item.SubItems.Add(new ListViewItem.ListViewSubItem(item, msg.FileName));
                        item.SubItems.Add(new ListViewItem.ListViewSubItem(item, msg.DateTicks));
                        item.Tag = msg.UID;

                        item.ImageIndex = 0;

                        listView.Items.Add(item);
                    }

                    _lvwColumnSorter.SortColumn = 4;
                    _lvwColumnSorter.Order = SortOrder.Descending;
                    listView.Sort();

                    listView.Columns[0].Width = -1;
                    listView.Columns[1].Width = -1;
                    listView.Columns[2].Width = -1;
                    listView.Columns[3].Width = -1;
                    listView.Columns[4].Width = 0;
                }
                else
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = "No game emails";
                    item.Tag = "None";
                    listView.Items.Add(item);
                    listView.Columns[0].Width = -1;
                }

                listView.ResumeLayout();
            }
        }

        private void RemoveCheckedItems()
        {
            if (_messageStoreCollection != null && _messageStoreCollection.Messages != null && listView.CheckedItems.Count > 0)
            {
                int count = _messageStoreCollection.Messages.Count;

                foreach (ListViewItem item in listView.CheckedItems)
                {
                    _messageStoreCollection.Messages.RemoveAll(msg => msg.UID.Equals(item.Tag.ToString(), StringComparison.InvariantCultureIgnoreCase));
                }

                _itemsRemoved = _messageStoreCollection.Messages.Count < count;
            }
        }

        private void listView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == _lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (_lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    _lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    _lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                _lvwColumnSorter.SortColumn = e.Column;
                _lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.listView.Sort();
        }
    }
}
