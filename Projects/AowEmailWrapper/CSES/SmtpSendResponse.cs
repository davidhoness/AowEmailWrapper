using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Mail;
using System.Net;
using AowEmailWrapper.Games;
using Lesnikowski.Mail;

namespace AowEmailWrapper.CSES
{
    public class SmtpSendResponse: IDisposable
    {
        #region Private Members

        private IMail _theGameEmail;
        private bool _isSuccess;
        private string _errorMessage;

        #endregion

        #region Public Properties

        public IMail GameEmail
        {
            get { return _theGameEmail; }
            set { _theGameEmail = value; }
        }

        public bool IsSuccess
        {
            get { return _isSuccess; }
            set { _isSuccess = value; }
        }

        public string Error
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        #endregion

        #region Constructors

        public SmtpSendResponse(IMail theGameEmail, bool isSuccess)
            : this(theGameEmail, isSuccess, null)
        { }

        public SmtpSendResponse(IMail theGameEmail, bool isSuccess, string errorMessage)
        {
            _theGameEmail = theGameEmail;
            _isSuccess = isSuccess;
            _errorMessage = errorMessage;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (_theGameEmail != null)
            {
                _theGameEmail = null;
            }
            _errorMessage = null;
        }

        #endregion
    }
}
