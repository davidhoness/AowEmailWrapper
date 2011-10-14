using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using AowEmailWrapper.Games;

namespace AowEmailWrapper.Classes
{
    public delegate void StartedTaskCompleteEventHandler(object sender, AowGameType gameType);

    public class StartedTaskWatcher
    {
        private Process _process;
        private StartedTaskCompleteEventHandler _callBack;
        private AowGame _theGame;
        private bool _stop = false;

        public Process Process
        {
            get { return _process; }
            set { _process = value; }
        }

        public void Stop()
        {
            _stop = true;
        }

        public StartedTaskWatcher(AowGame theGame, StartedTaskCompleteEventHandler callBack)
        {
            _theGame = theGame;
            _callBack = callBack;
        }

        public void Start()
        {
            _process = new Process();
            _process.StartInfo.FileName = _theGame.ExeFile;
            _process.StartInfo.WorkingDirectory = _theGame.Root.FullName;

            _process.Start();
            
            new Thread(new ThreadStart(this.Watch)).Start();
        }

        private void Watch()
        {
            if (_callBack != null)
            {
                do
                {
                    if (!_process.HasExited)
                    {
                        _process.Refresh();
                    }
                }
                while (!_process.WaitForExit(1000) && !_stop);

                if (!_stop)
                {
                    _callBack(this, _theGame.GameType);
                }

                _process.Dispose();
            }
        }
    }
}
