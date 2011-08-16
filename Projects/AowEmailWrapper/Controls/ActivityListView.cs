﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AowEmailWrapper.ConfigFramework;
using AowEmailWrapper.Games;
using AowEmailWrapper.Classes;
using AowEmailWrapper.Localization;

namespace AowEmailWrapper.Controls
{
    public delegate void ActivityListViewEventHandler(object sender, List<Activity> list);

    public partial class ActivityListView : UserControl
    {
        #region Private Members

        private ActivityList _activityLog;
        private ListViewColumnSorter _lvwColumnSorter;
        private ContextMenu _contextMenu;

        private const string Menu_Remove_Tag = "menuItemRemove";
        private const string Menu_MarkEnded_Tag = "menuItemMarkEnded";
        private const string Menu_MarkSent_Tag = "menuItemMarkSent";

        private bool _populating = false;

        #endregion

        #region Public Properties

        new public ActivityListViewEventHandler OnDoubleClick;
        public ActivityListViewEventHandler OnMarkAsEnded;
        public EventHandler OnListChanged;

        public ActivityList ActivityLog
        {
            get 
            { 
                return _activityLog; 
            }
            set 
            { 
                _activityLog = value;
                Populate();
            }
        }

        public ImageList SmallImageList
        {
            get { return listView.SmallImageList; }
            set { listView.SmallImageList = value; }
        }

        #endregion

        #region Constructors

        public ActivityListView()
        {
            InitializeComponent();
            _lvwColumnSorter = new ListViewColumnSorter();
            _lvwColumnSorter.Order = SortOrder.Descending;
            listView.Resize += new EventHandler(listView_Resize);
            listView.ListViewItemSorter = _lvwColumnSorter;
            
            CreateContextMenu();
        }

        #endregion

        #region Public Methods

        public override void Refresh()
        {            
            Populate();
            base.Refresh();
        }

        #endregion

        #region Private Methods

        private void Populate()
        {
            listView.Items.Clear();

            if (_activityLog != null && _activityLog.Activities != null && _activityLog.Activities.Count > 0)
            {
                listView.SuspendLayout();
                _populating = true;

                foreach (Activity activity in _activityLog.Activities)
                {
                    ListViewItem item = new ListViewItem();
                    int age = GetAgeInDays(activity.DateTicks);                    
                    SetItemColour(item, activity, age);

                    item.Text = activity.FileName;
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, activity.MapTitle));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, activity.TurnNumber));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, (age > 0) ? age.ToString() : string.Empty));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, activity.Status.Equals(ActivityState.None) ? string.Empty : Translator.TranslateEnum(activity.Status)));                    
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, activity.DateTicks));

                    item.Tag = activity;

                    switch (activity.GameType)
                    {
                        case AowGameType.Aow1:
                            item.ImageIndex = 3;
                            break;
                        case AowGameType.Aow2:
                            item.ImageIndex = 4;
                            break;
                        case AowGameType.AowSm:
                            item.ImageIndex = 5;
                            break;
                        case AowGameType.Unknown:
                            item.ImageIndex = 6;
                            break;
                    }

                    listView.Items.Add(item);
                }

                _lvwColumnSorter.SortColumn = 5;
                listView.Sort();

                listView.Columns[1].Width = 120; //Map
                listView.Columns[2].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize); //Turn
                listView.Columns[3].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize); //Age               
                listView.Columns[4].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent); //Status
                listView.Columns[5].Width = 0; //Ticks

                ResizeColumns();

                _populating = false;
                listView.ResumeLayout();
            }
            else
            {
                listView.Columns[0].Width = 290;
                listView.Columns[1].Width = 120;
                listView.Columns[2].Width = 25;
                listView.Columns[3].Width = 43;
                listView.Columns[4].Width = 25;
                listView.Columns[5].Width = 0;
            }
        }

        private void listView_Resize(object sender, EventArgs e)
        {
            if (!_populating)
            {
                listView.SuspendLayout();
                ResizeColumns();
                listView.ResumeLayout();
            }
        }

        private void ResizeColumns()
        {
            int listViewRightMargin =
                listView.Columns[1].Width +
                listView.Columns[2].Width +
                listView.Columns[3].Width +
                listView.Columns[4].Width + 25;

            listView.Columns[0].Width = listView.Width - listViewRightMargin; //File Name
        }

        private void RaiseListChanged()
        {
            if (OnListChanged != null)
            {
                OnListChanged(this, new EventArgs());
            }
        }

        private List<Activity> GetSelectedActivities()
        {
            List<Activity> returnVal = new List<Activity>();

            if (listView.SelectedItems.Count > 0)
            {
                foreach (ListViewItem selected in listView.SelectedItems)
                {
                    returnVal.Add((Activity)selected.Tag);
                }
            }

            return returnVal;
        }

        private void RemoveSelected()
        {
            List<Activity> theActivities = GetSelectedActivities();
            if (theActivities != null && theActivities.Count > 0)
            {
                foreach (Activity activity in theActivities)
                {
                    _activityLog.Activities.Remove(activity);
                }                
                Refresh();
                RaiseListChanged();
            }
        }

        private void MarkState(ActivityState state, List<Activity> theActivities)
        {
            if (theActivities != null && theActivities.Count > 0)
            {
                foreach (Activity activity in theActivities)
                {
                    activity.Status = state;
                }
                Refresh();
                RaiseListChanged();
            }
        }

        private void listView_DoubleClick(object sender, System.EventArgs e)
        {
            if (OnDoubleClick != null)
            {
                List<Activity> theList = GetSelectedActivities();

                if (theList != null && theList.Count == 1)
                {
                    OnDoubleClick(this, theList);
                }
            }
        }

        private void SetItemColour(ListViewItem listItem, Activity activity, int age)
        {
            if (activity.Status.Equals(ActivityState.Received))
            {
                listItem.BackColor = SystemColors.Info;
            }
            else if (activity.Status.Equals(ActivityState.Sent))
            {
                if (age >= 14 && age < 28)
                {
                    listItem.BackColor = Color.PeachPuff;
                }
                else if (age >= 28)
                {
                    listItem.BackColor = Color.MistyRose;
                }
            }
            else if (activity.Status.Equals(ActivityState.Ended))
            {
                listItem.ForeColor = Color.Gray;
            }
        }

        private int GetAgeInDays(string theTicks)
        {
            int returnVal = 0;
            long ticks = 0;
            if (long.TryParse(theTicks, out ticks))
            {
                DateTime timeStamp = new DateTime(ticks);

                TimeSpan age = DateTime.Now.Subtract(timeStamp);
                returnVal = age.Days;
            }
            return returnVal;
        }

        #endregion

        #region Context Menu

        private void CreateContextMenu()
        {
            _contextMenu = new ContextMenu();

            int indexCount = 0;

            EventHandler menuItemClickEvent = new EventHandler(ContextMenu_Click);
            _contextMenu = new ContextMenu();

            MenuItem remove = new MenuItem();
            MenuItem markEnded = new MenuItem();
            MenuItem markSent = new MenuItem();

            _contextMenu.MenuItems.AddRange(new MenuItem[] { remove, markEnded, markSent });

            _contextMenu.Popup += new EventHandler(ContextMenu_Popup);

            remove.Index = indexCount;
            remove.Text = Translator.Translate(Menu_Remove_Tag);
            remove.Tag = Menu_Remove_Tag;
            remove.Click += menuItemClickEvent;

            indexCount++;
            markEnded.Index = indexCount;
            markEnded.Text = Translator.Translate(Menu_MarkEnded_Tag);
            markEnded.Tag = Menu_MarkEnded_Tag;
            markEnded.Click += menuItemClickEvent;

            indexCount++;
            markSent.Index = indexCount;
            markSent.Text = Translator.Translate(Menu_MarkSent_Tag);
            markSent.Tag = Menu_MarkSent_Tag;
            markSent.Click += menuItemClickEvent;

            listView.ContextMenu = _contextMenu;
        }

        private void ContextMenu_Click(object sender, EventArgs e)
        {
            GetSelectedActivities();
            switch (((MenuItem)sender).Tag.ToString())
            {
                case Menu_Remove_Tag:
                    RemoveSelected();
                    break;
                case Menu_MarkEnded_Tag:
                    List<Activity> selected = GetSelectedActivities();
                    MarkState(ActivityState.Ended, selected);
                    if (selected != null && selected.Count > 0 && OnMarkAsEnded != null)
                    {
                        OnMarkAsEnded(this, selected);
                    }
                    break;
                case Menu_MarkSent_Tag:
                    MarkState(ActivityState.Sent, GetSelectedActivities());
                    break;
            }
        }

        private void ContextMenu_Popup(object sender, EventArgs e)
        {
            bool enabled = listView.SelectedItems.Count > 0;
            foreach (MenuItem menu in _contextMenu.MenuItems)
            {
                menu.Enabled = enabled;
            }
        }

        #endregion
    }
}
