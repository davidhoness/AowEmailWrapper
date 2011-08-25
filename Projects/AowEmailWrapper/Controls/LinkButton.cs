using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using AowEmailWrapper.Helpers;

namespace AowEmailWrapper.Controls
{
    public class LinkButton : Button
    {
        private string _url;

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (!string.IsNullOrEmpty(_url))
            {
                Process theBrowser = new Process();
                theBrowser.StartInfo.UseShellExecute = true;
                theBrowser.StartInfo.FileName = _url;
                theBrowser.Start();
            }
        }

        public LinkButton()
            : base()
        {
            this.Cursor = Cursors.Hand;
        }
    }
}
