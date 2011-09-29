using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using AowEmailWrapper.Classes;
using AowEmailWrapper.ASG;
using AowEmailWrapper.Games;
using AowEmailWrapper.Helpers;
using AowEmailWrapper.ConfigFramework;
using AowEmailWrapper.Pollers.MessageStore;
using Lesnikowski.Client;
using Lesnikowski.Mail;
using Lesnikowski.Mail.Headers;
using Lesnikowski.Mail.Headers.Constants;

namespace AowEmailWrapper.Pollers
{
    public class Pop3Poller : BasePoller
    {
        public Pop3Poller(
            string host,
            int port,
            bool enableSsl,
            string username,
            string password,
            int pollInterval,
            EmailSaveFolder saveFolder,
            AowGameManager gameManager)
            : base(
            host,
            port,
            enableSsl,
            username,
            password,
            pollInterval,
            saveFolder,
            gameManager)
        { }

        protected override void Poll()
        {
            bool emailDownloaded = false;
            Exception error = null;

            try
            {
                PollBegin();

                using (Pop3 pop3 = new Pop3())
                {
                    if (_enableSsl)
                    {
                        pop3.ConnectSSL(_host, _port);
                    }
                    else
                    {
                        pop3.Connect(_host, _port);
                    }

                    pop3.UseBestLogin(_username, _password);

                    List<string> serverUids = pop3.GetAll();

                    MessageStoreCollection localMessageStore = MessageStoreManager.LoadLocalMessageStore(_username, _host);

                    if (localMessageStore != null)
                    {
                        MessageStoreManager.RemoveMessagesNoLongerOnServer(ref localMessageStore, serverUids);

                        List<string> uidsToCheck = MessageStoreManager.GetMessagesToCheck(localMessageStore, serverUids);

                        foreach (string uid in uidsToCheck)
                        {
                            //string fileName;

                            string eml = pop3.GetMessageByUID(uid);
                            IMail email = new MailBuilder().CreateFromEml(eml); //SpoolEmlViaDisk(pop3.GetMessageByUID(uid), out fileName);

                            MessageStoreMessage theMessage = new MessageStoreMessage(uid);

                            if (ProcessEmailAttachments(email) > 0)
                            {
                                emailDownloaded = true;

                                //Only populate the extra data for game emails
                                if (email.From.Count > 0)
                                {
                                    theMessage.From = email.From[0].Address;
                                }
                                
                                theMessage.Subject = email.Subject;

                                if (email.Date.HasValue)
                                {
                                    theMessage.Date = email.Date.Value.ToString();
                                    theMessage.DateTicks = email.Date.Value.Ticks.ToString();
                                }
                                //In case the email doesn't come down with a good date
                                if (string.IsNullOrEmpty(theMessage.Date))
                                { 
                                    DateTime stamp = DateTime.Now;
                                    theMessage.Date = stamp.ToString();
                                    theMessage.DateTicks = stamp.Ticks.ToString();
                                }

                                theMessage.FileName = GetAttachmentsString(email);
                            }

                            localMessageStore.Messages.Add(theMessage);

                            //ClearSpooledEml(fileName);
                        }
                    }
                    else
                    {
                        //New message store, add all currently on server
                        localMessageStore = new MessageStoreCollection(serverUids);
                    }

                    MessageStoreManager.SaveLocalMessageStore(_username, _host, localMessageStore);

                    localMessageStore.Dispose();
                    localMessageStore = null;

                    pop3.Close();
                }
            }
            catch (Exception ex)
            {
                error = ex;
                Trace.TraceError(ex.ToString());
                Trace.Flush();
            }
            finally
            {
                PollEnd(emailDownloaded, error);
            }
        }

        private string GetAttachmentsString(IMail email)
        {
            string returnVal = string.Empty;

            if (email != null && email.Attachments != null && email.Attachments.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                foreach (MimeData attachment in email.Attachments)
                {
                    sb.Append(attachment.FileName);
                    sb.Append(Environment.NewLine);
                }

                returnVal = sb.ToString().Trim().Replace(Environment.NewLine, ", ");                
            }

            return returnVal;
        }
    }
}
