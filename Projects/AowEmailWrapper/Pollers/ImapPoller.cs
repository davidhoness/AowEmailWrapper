using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using AowEmailWrapper.Classes;
using AowEmailWrapper.ASG;
using AowEmailWrapper.Games;
using AowEmailWrapper.ConfigFramework;
using AowEmailWrapper.Helpers;
using AowEmailWrapper.Pollers.MessageStore;
using Lesnikowski.Client.IMAP;
using Lesnikowski.Mail;
using Lesnikowski.Mail.Headers;
using Lesnikowski.Mail.Headers.Constants;

namespace AowEmailWrapper.Pollers
{
    public class ImapPoller : BasePoller
    {
        public ImapPoller(
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

                using (Imap imap = new Imap())
                {
                    if (_enableSsl)
                    {
                        imap.ConnectSSL(_host, _port);
                    }
                    else
                    {
                        imap.Connect(_host, _port);
                    }

                    imap.UseBestLogin(_username, _password);

                    imap.SelectInbox();

                    List<long> serverUids = imap.SearchFlag(Flag.Unseen);

                    MessageStoreCollection localMessageStore = MessageStoreManager.LoadLocalMessageStore(_username, _host);

                    if (localMessageStore != null)
                    {
                        MessageStoreManager.RemoveMessagesNoLongerOnServer(ref localMessageStore, serverUids);

                        List<long> uidsToCheck = MessageStoreManager.GetMessagesToCheck(localMessageStore, serverUids);

                        foreach (long uid in uidsToCheck)
                        {
                            //string fileName;

                            string eml = imap.GetMessageByUID(uid);
                            IMail email = new MailBuilder().CreateFromEml(eml); //SpoolEmlViaDisk(imap.GetMessageByUID(uid), out fileName);

                            if (ProcessEmailAttachments(email) > 0)
                            {
                                //We want the user to be able to go Mark as Unread > Poll > Redownload
                                //So just mark as read but don't add to local message store
                                emailDownloaded = true;
                                imap.MarkMessageSeenByUID(uid);
                            }
                            else
                            {
                                localMessageStore.Messages.Add(new MessageStoreMessage(uid));
                                imap.MarkMessageUnseenByUID(uid);
                            }

                            //ClearSpooledEml(fileName);
                        }
                    }
                    else
                    {
                        //New message store, add all unread currently on server
                        localMessageStore = new MessageStoreCollection(serverUids);
                    }

                    MessageStoreManager.SaveLocalMessageStore(_username, _host, localMessageStore);

                    localMessageStore.Dispose();
                    localMessageStore = null;

                    imap.Close();
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
    }
}
