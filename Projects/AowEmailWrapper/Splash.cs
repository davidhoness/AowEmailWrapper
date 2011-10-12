using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using AowEmailWrapper.Classes;

namespace AowEmailWrapper
{
    public partial class Splash : Form
    {
        private static Splash _frmSplash = null;
        private static Thread _frmThread = null;

        private double _opacityIncrement = .05;
        private double _opacityDecrement = .1;
        private const int TIMER_INTERVAL = 50;
        private System.Windows.Forms.Timer _timer;

        public Splash()
        {
            InitializeComponent();
            this.ClientSize = this.BackgroundImage.Size;
            this.Opacity = .5;

            _timer = new System.Windows.Forms.Timer(this.components);
            _timer.Tick += new System.EventHandler(this.timer_Tick);
            _timer.Interval = TIMER_INTERVAL;
            _timer.Start();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                // Turn on WS_EX_TOOLWINDOW style bit
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= ExtendedWindowStyles.WS_EX_TOOLWINDOW;
                return cp;
            }
        }

        private void timer_Tick(object sender, System.EventArgs e)
        {
            if (_opacityIncrement > 0)
            {
                if (this.Opacity < 1)
                    this.Opacity += _opacityIncrement;
            }
            else
            {
                if (this.Opacity > 0)
                    this.Opacity += _opacityIncrement;
                else
                    this.Close();
            }
        }

        private static void ShowForm()
        {
            _frmSplash = new Splash();
            Application.Run(_frmSplash);
        }

        public static  void ShowSplashScreen()
        {
            if (_frmSplash != null)
                return;
            _frmThread = new Thread(new ThreadStart(Splash.ShowForm));
            _frmThread.IsBackground = true;
            _frmThread.SetApartmentState(ApartmentState.STA);
            _frmThread.Start();
        }

        public static void CloseForm()
        {
            if (_frmSplash != null && _frmSplash.IsDisposed == false)
            {
                // Make it start going away.
                _frmSplash._opacityIncrement = -_frmSplash._opacityDecrement;
            }
            _frmThread = null;	// we don't need these any more.
            _frmSplash = null;
        }
    }
}
