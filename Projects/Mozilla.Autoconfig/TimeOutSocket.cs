using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace Mozilla.Autoconfig
{
    internal class TimeOutSocket : IDisposable
    {
        private int TimeoutDefaultMs = 4000;

        private ManualResetEvent _timeoutObject;

        private bool _isSuccess = false;
        private bool _isDisposed = false;

        private Exception _exception;

        private string _host;
        private int _port;

        public TimeOutSocket(string host, int port)
        {
            _host = host;
            _port = port;
            _timeoutObject = new ManualResetEvent(false);
            _isDisposed = false;
        }

        public bool IsSuccess
        {
            get { return _isSuccess; }
        }

        public string Host
        {
            get { return _host; }
        }

        public int Port
        {
            get { return _port; }
        }

        public void Test()
        {
            Test(TimeoutDefaultMs);
        }

        public void Test(int timeoutMs)
        {
            Thread testThread = new Thread(new ParameterizedThreadStart(this.BeginConnect));
            testThread.Start(timeoutMs);
        }

        private void BeginConnect(object obj)
        {
            int timeoutMs = (int)obj;
            _exception = null;
            _isSuccess = false;

            TcpClient tcpClient = new TcpClient();            
            _timeoutObject.Reset();

            tcpClient.BeginConnect(_host, _port, new AsyncCallback(this.CallBackMethod), tcpClient);
            if (_timeoutObject.WaitOne(timeoutMs, false))
            {
                tcpClient.Close();
            }
            else
            {
                tcpClient.Close();
                _isSuccess = false;
            }
        }

        private void CallBackMethod(IAsyncResult asyncResult)
        {
            try
            {
                TcpClient tcpClient = asyncResult.AsyncState as TcpClient;
                if (tcpClient != null)
                {
                    tcpClient.EndConnect(asyncResult);
                    _isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
            finally
            {
                if (!_isDisposed) _timeoutObject.Set();
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            _timeoutObject.Close();
            _isDisposed = true;
        }

        #endregion
    }
}