using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using EricDaugherty.CSES.SmtpServer;
using System.IO;
using System.Net.Mail;
using System.Net;
using AowEmailWrapper.ASG;
using AowEmailWrapper.Classes;
using AowEmailWrapper.Games;
using AowEmailWrapper.Helpers;
using Lesnikowski.Mail;

namespace AowEmailWrapper.CSES
{
    public class SmtpSpool : IMessageSpool
    {
        #region Private Members

        private const string EMAIL_APPEND_TEXT = "--------------------------------------------------------\r\nAutosent with the Age of Wonders Email Wrapper [{0}]";

        private IMail _message;

        #endregion

        #region Public Properties

        public IMail SpooledEmail
        {
            get { return _message; }
        }

        #endregion

        #region IMessageSpool Members

        public bool SpoolMessage(IMail message)
        {
            bool isValid = false;
            
            try
            {
                StringBuilder bodyBuilder = new StringBuilder(message.TextData.Text);

                foreach (MimeData attachment in message.Attachments)
                {
                    if (!string.IsNullOrEmpty(attachment.FileName) &&
                        ASGFileInfo.IsAsg(attachment.FileName))
                    {
                        isValid = true;

                        string turnLog = TurnLogger.LogTurn(attachment.FileName, message.From[0].Address, message.TextData.Text);

                        bodyBuilder.Append(StringHelper.CrLf);
                        bodyBuilder.Append(turnLog);
                    }
                }

                if (isValid)
                {
                    bodyBuilder.Append(StringHelper.CrLf);
                    bodyBuilder.Append(StringHelper.CrLf);
                    bodyBuilder.Append(string.Format(EMAIL_APPEND_TEXT, ConfigHelper.BuildVersion));

                    message.TextData.Text = bodyBuilder.ToString();

                    message.MessageID = Guid.NewGuid().ToString();

                    _message = message;
                }
                else
                {
                    _message = null;
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                Trace.Flush();
            }

            return isValid;
        }

        #endregion

        #region Constructors

        public SmtpSpool()
        {            
        }

        #endregion
    }
}
