using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using AowEmailWrapper.Games;
using AowEmailWrapper.ASG;
using AowEmailWrapper.Helpers;
using AowEmailWrapper.ConfigFramework;
using Lesnikowski.Client;
using Lesnikowski.Mail;
using Lesnikowski.Mail.Headers;

namespace AowEmailWrapper.CSES
{
    public delegate void SmtpSenderSentEventHandler(object sender, SmtpSendResponse theResponse);

    public class SmtpSender : IDisposable
    {
        #region Private Members

        private Queue<IMail> _messageQueue;
        private List<string> _messageIDsBeingSent;
        private Dictionary<string, int> _messageSendAttemptCount;
        private string _host;
        private int _port;
        private SmtpSSLType _sslType;
        private string _username;
        private string _password;
        private bool _bccMyself;

        #endregion

        #region Public Properties

        public event SmtpSenderSentEventHandler OnEmailSent;

        public bool IsSending
        {
            get { return !_messageQueue.Count.Equals(0); }
        }

        #endregion

        #region Constructors

        public SmtpSender(string host, int port, SmtpSSLType sslType, bool bccMyself)
            : this(host, port, null, null, sslType, bccMyself)
        { }

        public SmtpSender(string host, int port, string username, string password, SmtpSSLType sslType, bool bccMyself)
        {
            _host = host;
            _port = port;
            _username = username;
            _password = password;
            _sslType = sslType;
            _bccMyself = bccMyself;

            _messageQueue = new Queue<IMail>();
            _messageIDsBeingSent = new List<string>();
            _messageSendAttemptCount = new Dictionary<string, int>();
        }

        #endregion

        #region Public Methods

        public void SendMessage(IMail theGameEmail)
        {
            _messageQueue.Enqueue(theGameEmail);
            //new System.Threading.Thread(new System.Threading.ThreadStart(this.ProcessMessageQueue)).Start();

            System.Threading.Thread newThread = new System.Threading.Thread(new System.Threading.ThreadStart(this.ProcessMessageQueue));
            newThread.SetApartmentState(System.Threading.ApartmentState.STA);
            newThread.Start();
        }

        #endregion

        #region Private Methods

        private void ProcessMessageQueue()
        {
            IMail theGameEmail = null;

            try
            {
                if (_messageIDsBeingSent.Count.Equals(0))
                {
                    if (_messageQueue.Count > 0)
                    {
                        theGameEmail = _messageQueue.Peek();
                        _messageIDsBeingSent.Add(theGameEmail.MessageID);
                        SendAowEmail(theGameEmail);
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("ProcessMessageQueue Error: {0}", ex.ToString()));
                if (theGameEmail != null)
                {
                    RaiseOnEmailSentEvent(new SmtpSendResponse(theGameEmail, false, ex));
                }
            }
        }

        private void SendAowEmail(IMail theGameEmail)
        {
            try
            {
                using (Smtp smtp = new Smtp())      
                {
                    switch (_sslType)
                    {
                        case SmtpSSLType.None:
                            smtp.Connect(_host, _port);
                            smtp.Ehlo();
                            break;
                        case SmtpSSLType.SSL:
                            smtp.ConnectSSL(_host, _port);
                            smtp.Ehlo();
                            break;
                        case SmtpSSLType.TLS:
                            smtp.Connect(_host, _port);
                            smtp.Ehlo();
                            smtp.StartTLS();
                            break;
                    }

                    if (!string.IsNullOrEmpty(_username) && 
                        !string.IsNullOrEmpty(_password))
                    {
                        smtp.UseBestLogin(_username, _password);
                    }

                    if (_bccMyself)
                    {
                        theGameEmail.Bcc.Add(theGameEmail.From[0]);
                    }

                    smtp.SendMessage(theGameEmail, true);

                    smtp.Close();
                }
                Trace.WriteLine(string.Format("EMAIL: [{0}] Message sent successfully.", theGameEmail.To[0].Address));
                RaiseOnEmailSentEvent(new SmtpSendResponse(theGameEmail, true));
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("EMAIL: [{0}] {1}", theGameEmail.To, ex.ToString()));

                if (IsRetrySend(theGameEmail.MessageID))
                {
                    //Try again
                    _messageIDsBeingSent.Remove(theGameEmail.MessageID);
                    Trace.WriteLine(string.Format("EMAIL: [{0}] Retry: {1}", theGameEmail.To, _messageSendAttemptCount[theGameEmail.MessageID]));
                }
                else
                {
                    //Send FAILED
                    RaiseOnEmailSentEvent(new SmtpSendResponse(theGameEmail, false, ex));
                }
            }
            ProcessMessageQueue();
        }

        private bool IsRetrySend(string theID)
        {
            if (!_messageSendAttemptCount.Keys.Contains<string>(theID))
            {
                _messageSendAttemptCount.Add(theID, 1);
            }
            else
            {
                _messageSendAttemptCount[theID]++;
            }

            return (_messageSendAttemptCount[theID] <= 3);
        }

        private void RetryClear(string theID)
        {
            if (_messageSendAttemptCount.Keys.Contains<string>(theID))
            {
                _messageSendAttemptCount.Remove(theID);
            }
        }

        private void RaiseOnEmailSentEvent(SmtpSendResponse theResponse)
        {
            if (_messageIDsBeingSent.Contains(theResponse.GameEmail.MessageID))
            {
                _messageIDsBeingSent.Remove(theResponse.GameEmail.MessageID);
            }
            if (_messageQueue.Count > 0 && _messageQueue.Peek().Equals(theResponse.GameEmail))
            {
                _messageQueue.Dequeue();
            }

            RetryClear(theResponse.GameEmail.MessageID);

            if (OnEmailSent != null)
            {
                OnEmailSent.Invoke(this, theResponse);
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            _messageQueue = null;
            _messageIDsBeingSent = null;
            _messageSendAttemptCount = null;
            _host = null;
            _username = null;
            _password = null;
        }

        #endregion
    }
}
