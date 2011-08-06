using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AowEmailWrapper.Classes;
using AowEmailWrapper.Localization;

namespace AowEmailWrapper.Controls
{
    public partial class FormBlockPollingSetup : BaseFormBlock
    {
        private const string PleaseChooseTextKey = "enumChooser";
        private const string MinutesTextKey = "enumMinutes";
        private const string DisplayTextTemplate = "{0} {1}";
        private const int CheckBoxTickWidth = 24;

        public FormBlockPollingSetup()
        {
            InitializeComponent();
            comboBox.DisplayMember = "Text";
            base.SetControls(panelLabel, comboBox);
        }

        public string CheckBoxText
        {
            get { return checkBox.Text; }
            set { checkBox.Text = value; }
        }

        public string EveryText
        {
            get { return labelEvery.Text; }
            set { labelEvery.Text = value; }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            //This must always be on one line
            Size szEvery = new Size(int.MaxValue, int.MaxValue);
            szEvery = TextRenderer.MeasureText(labelEvery.Text, labelEvery.Font, szEvery, TextFormatFlags.SingleLine);
            labelEvery.Size = szEvery;

            int spareWidth = panelLabel.Width - labelEvery.Width;

            Size szCheckBoxText = new Size(spareWidth - CheckBoxTickWidth, int.MaxValue);
            szCheckBoxText = TextRenderer.MeasureText(checkBox.Text, checkBox.Font, szCheckBoxText, TextFormatFlags.WordBreak);

            checkBox.Width = szCheckBoxText.Width + CheckBoxTickWidth;
            this.Height = szCheckBoxText.Height + 4;
        }

        public void AddItem(int value)
        {
            AddItem(new ComboBoxItem(value.ToString(), string.Format(DisplayTextTemplate, value, Translator.Translate(MinutesTextKey))));
        }

        private void AddItem(ComboBoxItem theItem)
        {
            if (comboBox.Items.Count.Equals(0))
            {
                comboBox.Items.Add(new ComboBoxItem(string.Empty, Translator.Translate(PleaseChooseTextKey)));
            }
            comboBox.Items.Add(theItem);
        }

        public string SelectedValue
        {
            get
            {
                if (comboBox.SelectedIndex > 0)
                {
                    ComboBoxItem theSelectedItem = (ComboBoxItem)comboBox.SelectedItem;
                    return theSelectedItem.Value;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                foreach (ComboBoxItem item in comboBox.Items)
                {
                    if (item.Value.Equals(value, StringComparison.InvariantCultureIgnoreCase))
                    {
                        comboBox.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        public int SelectedIndex
        {
            get { return comboBox.SelectedIndex; }
            set { comboBox.SelectedIndex = value; }
        }

        public ComboBox.ObjectCollection Items
        {
            get { return comboBox.Items; }
        }

        public ComboBox InnerComboBox
        {
            get { return comboBox; }
        }

        public bool Checked
        {
            get { return checkBox.Checked; }
            set { checkBox.Checked = value; }
        }

        public CheckBox InnerCheckBox
        {
            get { return checkBox; }
        }
    }
}
