using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AowEmailWrapper.Classes
{
    public delegate void StartedTaskCompleteEventHandler(StartedTaskWatcher sender, Process theProcess);

    public class StartedTaskWatcher
    {
        private Process _process;
        private StartedTaskCompleteEventHandler _taskCompleteEventHandler;
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

        public StartedTaskWatcher(Process process, StartedTaskCompleteEventHandler taskCompleteEventHandler)
        {
            _process = process;
            _taskCompleteEventHandler = taskCompleteEventHandler;
        }

        public void Start()
        {
            if (_taskCompleteEventHandler != null)
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
                    _taskCompleteEventHandler(this, _process);
                }

                _process.Dispose();
            }
        }
    }
}
