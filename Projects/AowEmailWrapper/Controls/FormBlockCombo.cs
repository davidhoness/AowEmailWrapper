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
    public partial class FormBlockCombo : BaseFormBlock
    {
        private const string PleaseChooseTextKey = "enumChooser";

        public string LabelName
        {
            get { return lblName.Text; }
            set { lblName.Text = value; }
        }

        public void AddItem(ComboBoxItem theItem)
        {
            if (comboBox.Items.Count.Equals(0))
            {
                comboBox.Items.Add(new ComboBoxItem(string.Empty, Translator.Translate(PleaseChooseTextKey)));
            }
            comboBox.Items.Add(theItem);
        }

        public void AddItem(string value)
        {
            AddItem(value, value);
        }

        public void AddItem(string value, string displayText)
        {
            AddItem(new ComboBoxItem(value, displayText));
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

        public FormBlockCombo()
        {            
            InitializeComponent();
            comboBox.DisplayMember = "Text";
            base.SetControls(lblName, comboBox);
        }
    }
}
