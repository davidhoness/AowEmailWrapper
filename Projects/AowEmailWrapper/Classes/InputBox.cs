﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AowEmailWrapper.Localization;

namespace AowEmailWrapper.Classes
{
    public class InputBox
    {
        private const string ButtonKeyOK = "buttonOK";
        private const string ButtonKeyCancel = "buttonCancel";

        public static DialogResult Show(string title, string promptText, ref string value)
        {
            return ShowInput(title, promptText, ref value, null);
        }

        public static DialogResult Show(string title, string promptText, ref string value, Image iconImage)
        {
            Icon theIcon = FlimFlan.IconEncoder.Converter.BitmapToIcon(iconImage as Bitmap);
            return ShowInput(title, promptText, ref value, theIcon);
        }

        public static DialogResult Show(string title, string promptText, ref string value, Icon icon)
        {
            return ShowInput(title, promptText, ref value, icon);
        }

        private static DialogResult ShowInput(string title, string promptText, ref string value, Icon icon)
        {
            DialogResult dialogResult;

            using (Form form = new Form())
            {
                if (icon != null)
                {
                    form.Icon = icon;
                }

                Label label = new Label();
                TextBox textBox = new TextBox();
                Button buttonOk = new Button();
                Button buttonCancel = new Button();
                
                form.Text = title;
                label.Text = promptText;
                textBox.Text = value;

                buttonOk.Text = Translator.Translate(ButtonKeyOK);
                buttonCancel.Text = Translator.Translate(ButtonKeyCancel);
                buttonOk.DialogResult = DialogResult.OK;
                buttonCancel.DialogResult = DialogResult.Cancel;

                label.SetBounds(9, 20, 372, 13);
                textBox.SetBounds(12, 36, 372, 20);
                buttonOk.SetBounds(228, 72, 75, 23);
                buttonCancel.SetBounds(309, 72, 75, 23);

                label.AutoSize = true;
                textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
                buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

                form.ShowInTaskbar = false;
                form.ClientSize = new Size(396, 107);
                form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
                form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.StartPosition = FormStartPosition.CenterParent;
                form.MinimizeBox = false;
                form.MaximizeBox = false;
                form.AcceptButton = buttonOk;
                form.CancelButton = buttonCancel;
                dialogResult = form.ShowDialog();

                if (dialogResult.Equals(DialogResult.OK))
                {
                    value = textBox.Text;
                }

                label.Dispose();
                textBox.Dispose();
                buttonOk.Dispose();
                buttonCancel.Dispose();
            }

            return dialogResult;
        }
    }
}
