using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AowEmailWrapper.Helpers;
using AowEmailWrapper.ConfigFramework;

namespace AowEmailWrapper.Controls
{
    public partial class AccountsCreationWizzard : UserControl
    {
        private EmailProviderType _selectedEmailType;
        private AccountConfigValuesList _accountTemplates;

        public EventHandler CreateClicked;

        public AccountConfigValuesList AccountTemplates
        {
            get { return _accountTemplates; }
            set { _accountTemplates = value; }
        }

        public ImageList RadioImages
        {
            get { return null; }
            set
            {
                SetIcons(value);
            }
        }

        public AccountsCreationWizzard()
        {
            InitializeComponent();
            fbEmailAddress.InnerTextBox.KeyUp += new KeyEventHandler(textBox_Changed);
            fbPassword.InnerTextBox.KeyUp += new KeyEventHandler(textBox_Changed);
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {            
            RadioButton theButton = (RadioButton)sender;
            string buttonArg = theButton.Tag.ToString();

            _selectedEmailType = ConfigHelper.ParseEnumString<EmailProviderType>(buttonArg);

            switch (_selectedEmailType)
            {
                case EmailProviderType.Google:
                    labelMessage.Text = "@gmail, @googlemail";
                    break;
                case EmailProviderType.WindowsLive:
                    labelMessage.Text = "@hotmail, @msn, @live";
                    break;
                case EmailProviderType.Yahoo:
                    labelMessage.Text = "@yahoo, @ymail, @rocketmail";
                    break;
                default:
                    labelMessage.Text = "You will imput the email settings manually.";
                    fbEmailAddress.TextValue = string.Empty;
                    fbPassword.TextValue = string.Empty;
                    break;
            }

            CheckCreateEnabled();
        }

        private void CheckCreateEnabled()
        {
            switch (_selectedEmailType)
            {
                case EmailProviderType.Google:                    
                case EmailProviderType.WindowsLive:
                case EmailProviderType.Yahoo:
                    buttonCreate.Enabled = fbEmailAddress.TextValue.Length > 0 && fbPassword.TextValue.Length > 0;
                    groupBoxAuthentication.Enabled = true;
                    break;
                default:
                    buttonCreate.Enabled = true;
                    groupBoxAuthentication.Enabled = false;
                    break;
            }
        }

        private void textBox_Changed(object sender, EventArgs e)
        {
            CheckCreateEnabled();
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            if (CreateClicked != null)
            {
                CreateClicked(sender, e);
            }
        }

        private void SetIcons(ImageList images)
        {
            if (images != null)
            {
                foreach (Control control in panelInnerRadio.Controls)
                {
                    if (control is RadioButton)
                    {                        
                        RadioButton radioButton = (RadioButton)control;
                        int imageIndex = images.Images.IndexOfKey(radioButton.Tag.ToString());
                        if (imageIndex >= 0)
                        {
                            radioButton.Image = images.Images[imageIndex];
                        }
                    }
                }
            }
        }
    }
}
