using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using Mozilla.Autoconfig;

namespace AowEmailWrapper.Classes
{
    public class TimeOutServerTest : IDisposable
    {
        #region Private Members

        private ManualResetEvent _timeoutObject;

        private bool _isSuccess = false;
        private bool _isDisposed = false;
        private bool _incoming;
        private int _timeoutMs;

        private IncomingServer _incomingServer;
        private OutgoingServer _outgoingServer;

        #endregion

        #region Public Properties

        public bool IsSuccess
        {
            get { return _isSuccess; }
        }

        public IncomingServer IncomingServer
        {
            get { return _incomingServer; }
        }

        public OutgoingServer OutgoingServer
        {
            get { return _outgoingServer; }
        }

        #endregion

        #region Constructors

        public TimeOutServerTest(IncomingServer incomingServer)
        {
            _incomingServer = incomingServer;
            _timeoutObject = new ManualResetEvent(false);
            _incoming = true;
        }

        public TimeOutServerTest(OutgoingServer outgoingServer)
        {
            _outgoingServer = outgoingServer;
            _timeoutObject = new ManualResetEvent(false);
            _incoming = false;
        }

        #endregion

        #region Public Methods

        public void Test(int timeoutMs)
        {
            _timeoutMs = timeoutMs;

            Thread beginThread = new Thread(new ThreadStart(this.BeginTest));
            beginThread.Start();
        }

        #endregion

        #region Private Methods

        private void BeginTest()
        {
            Thread testThread = null;

            _timeoutObject.Reset();

            if (_incoming)
            {
                switch (_incomingServer.Type)
                {
                    case ServerType.IMAP:
                        testThread = new Thread(new ThreadStart(this.TestIMAP));
                        break;
                    case ServerType.POP3:
                        testThread = new Thread(new ThreadStart(this.TestPOP));                        
                        break;
                }
            }
            else
            {
                testThread = new Thread(new ThreadStart(this.TestSMTP));
            }

            if (testThread != null)
            {
                testThread.Start();

                if (!_timeoutObject.WaitOne(_timeoutMs, false))
                {
                    if (testThread != null) testThread.Abort();
                }
            }
        }

        #endregion

        #region Testing Functions

        private void TestIMAP()
        {
            try
            {
                using (Lesnikowski.Client.IMAP.Imap imap = new Lesnikowski.Client.IMAP.Imap())
                {
                    switch (_incomingServer.SocketType)
                    {
                        case SocketType.SSL:
                            imap.ConnectSSL(_incomingServer.Hostname, _incomingServer.Port);
                            break;
                        case SocketType.Unknown:
                        case SocketType.Plain:
                        case SocketType.STARTTLS:
                            imap.Connect(_incomingServer.Hostname, _incomingServer.Port);
                            _incomingServer.SocketType = SocketType.Plain;
                            imap.StartTLS();
                            _incomingServer.SocketType = SocketType.STARTTLS;
                            break;
                    }

                    _isSuccess = true;
                }
            }
            catch (Lesnikowski.Client.ServerException ex)
            {
                _isSuccess = ex.InnerException == null;
            }
            catch { }
            finally
            {
                if (!_isDisposed) _timeoutObject.Set();
            }
        }

        private void TestPOP()
        {
            try
            {
                using (Lesnikowski.Client.Pop3 pop3 = new Lesnikowski.Client.Pop3())
                {
                    switch (_incomingServer.SocketType)
                    {
                        case SocketType.SSL:
                            pop3.ConnectSSL(_incomingServer.Hostname, _incomingServer.Port);
                            break;
                        case SocketType.Unknown:
                        case SocketType.Plain:
                        case SocketType.STARTTLS:
                            pop3.Connect(_incomingServer.Hostname, _incomingServer.Port);
                            _incomingServer.SocketType = SocketType.Plain;
                            pop3.STLS();
                            _incomingServer.SocketType = SocketType.STARTTLS;
                            break;
                    }

                    _isSuccess = true;
                }
            }
            catch (Lesnikowski.Client.ServerException ex)
            {
                _isSuccess = ex.InnerException == null;
            }
            catch { }
            finally
            {
                if (!_isDisposed) _timeoutObject.Set();
            }
        }

        private void TestSMTP()
        {
            try
            {
                using (Lesnikowski.Client.Smtp smtp = new Lesnikowski.Client.Smtp())
                {
                    switch (_outgoingServer.SocketType)
                    {
                        case SocketType.SSL:
                            smtp.ConnectSSL(_outgoingServer.Hostname, _outgoingServer.Port);
                            smtp.Ehlo();
                            break;
                        case SocketType.Unknown:
                        case SocketType.Plain:
                        case SocketType.STARTTLS:
                            smtp.Connect(_outgoingServer.Hostname, _outgoingServer.Port);
                            smtp.Ehlo();
                            _outgoingServer.SocketType = SocketType.Plain;
                            smtp.StartTLS();
                            _outgoingServer.SocketType = SocketType.STARTTLS;
                            break;
                    }

                    _isSuccess = true;
                }
            }
            catch (Lesnikowski.Client.ServerException ex)
            {
                _isSuccess = ex.InnerException == null;
            }
            catch { }
            finally
            {
                if (!_isDisposed) _timeoutObject.Set();
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            _timeoutObject.Close();
            _isDisposed = true;
        }

        #endregion
    }
}
