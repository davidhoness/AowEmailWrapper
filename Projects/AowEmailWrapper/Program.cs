using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using AowEmailWrapper.Helpers;

namespace AowEmailWrapper
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //Is this a Restart?
            if (args.Length.Equals(2) &&
                args[0].Equals(ConfigHelper.RESTART_CMD_PARAM))
            {
                int oldProcessId = 0;

                //The old process Id will be on the command line
                if (int.TryParse(args[1], out oldProcessId))
                {
                    try
                    {
                        //Wait for it to exit
                        Process oldProcess = Process.GetProcessById(oldProcessId);
                        oldProcess.WaitForExit();
                        oldProcess.Dispose();
                    }
                    catch { }
                }
            }

            //To avoid two process' running at once
            Process thisProcess = Process.GetCurrentProcess();
            Process[] matchingNames = Process.GetProcessesByName(thisProcess.ProcessName);

            if (matchingNames.Length == 1)
            {
                thisProcess.Dispose();
                thisProcess = null;
                matchingNames[0].Dispose();
                matchingNames[0] = null;

                if (Array.Exists<string>(args, s => s.Equals(ConfigHelper.AUTOSTART_CMD_PARAM, StringComparison.InvariantCultureIgnoreCase)))
                {
                    int pauseMilliseconds = ConfigHelper.AutostartPauseSeconds * 1000;

                    if (pauseMilliseconds > 0)
                    {
                        System.Threading.Thread.Sleep(pauseMilliseconds);
                    }
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Main mainForm = new Main(args.Length.Equals(0));
                Application.Run(mainForm);
            }
        }
    }
}
