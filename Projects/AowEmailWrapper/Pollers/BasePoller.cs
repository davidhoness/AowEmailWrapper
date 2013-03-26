using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Diagnostics;
using System.IO;
using AowEmailWrapper.Games;
using AowEmailWrapper.Helpers;
using AowEmailWrapper.Classes;
using AowEmailWrapper.ConfigFramework;
using AowEmailWrapper.ASG;
using Lesnikowski.Mail;
using Lesnikowski.Mail.Headers;

namespace AowEmailWrapper.Pollers
{
    public delegate void PollerEmailEventHandler(object sender, PollerEventArgs e);

    public abstract class BasePoller
    {
        private const int ONE_MIN_MILLISECONDS = 60000;
        protected const string FileSpoolTemplate = "{0}.eml";

        protected AowGameManager _gameManager;
        protected Timer _timer;
        protected string _host;
        protected int _port;
        protected SSLType _sslType;
        protected string _username;
        protected string _password;
        protected string _outputPath;
        protected int _pollInterval;
        public event PollerEmailEventHandler OnEmailEvent;
        private Queue<string> _pollQueue;
        protected EmailSaveFolder _saveFolder;

        public bool IsPolling
        {
            get { return !_pollQueue.Count.Equals(0); }
        }

        protected BasePoller(
            string host, 
            int port,
            SSLType sslType, 
            string username, 
            string password, 
            int pollInterval,            
            EmailSaveFolder saveFolder,
            AowGameManager gameManager)
        {
            _host = host;
            _port = port;
            _sslType = sslType;
            _username = username;
            _password = password;
            _pollInterval = pollInterval;
            _saveFolder = saveFolder;
            _gameManager = gameManager;
            _pollQueue = new Queue<string>();            
        }

        public void Start()
        {
            if (_timer == null)
            {
                _timer = new Timer();
                _timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                _timer.Interval = _pollInterval * ONE_MIN_MILLISECONDS;
                _timer.Enabled = true;
                _timer.Start();
                PollNow();
            }
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Poll();
        }

        public void Stop()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
                _timer = null;
            }
        }

        protected virtual void Poll()
        { }

        public void PollNow()
        {
            new System.Threading.Thread(new System.Threading.ThreadStart(this.Poll)).Start();
        }

        protected void PollBegin()
        {
            _pollQueue.Enqueue(Guid.NewGuid().ToString());
            if (OnEmailEvent != null)
            {
                OnEmailEvent(this, new PollerEventArgs(PollState.Begin, false));
            }
        }

        protected void PollEnd(bool emailDownloaded, Exception ex)
        {
            _pollQueue.Dequeue();
            if (OnEmailEvent != null)
            {
                OnEmailEvent(this, new PollerEventArgs(PollState.End, emailDownloaded, ex));
            }
        }

        protected int ProcessEmailAttachments(IMail email)
        {
            int count = 0;

            try
            {
                if (email != null &&
                    email.Attachments != null &&
                    email.Attachments.Count > 0)
                {
                    foreach (MimeData attachment in email.Attachments)
                    {
                        if (!string.IsNullOrEmpty(attachment.FileName) &&
                            ASGFileInfo.IsAsg(attachment.FileName))
                        {
                            count++;

                            using (ASGFileInfo theASG = new ASGFileInfo(attachment))
                            {
                                if (theASG.Length > 0)
                                {
                                    _gameManager.StoreDownloadFile(theASG, _saveFolder);

                                    TurnLogger.SaveLog(theASG.FileNameTrue, email.TextData.Text);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                Trace.Flush();
            }

            return count;
        }

        /*
        protected IMail SpoolEmlViaDisk(string eml, out string fileName)
        {
            fileName = Path.Combine(AppDataHelper.Root.FullName, string.Format(FileSpoolTemplate, Guid.NewGuid().ToString()));
            File.WriteAllText(fileName, eml);
            return new MailBuilder().CreateFromEmlFile(fileName);
        }
        */

        protected void ClearSpooledEml(string fileName)
        {
            File.Delete(fileName);
        }
    }
}
