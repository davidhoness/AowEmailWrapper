using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using AowEmailWrapper.Games;
using AowEmailWrapper.Helpers;
using System.Xml;

namespace AowEmailWrapper.Pollers.MessageStore
{
    public class MessageStoreManager
    {
        #region Public Methods

        public static MessageStoreCollection LoadLocalMessageStore(string username, string host)
        {
            return DataManagerHelper.LoadLocalMessageStore(username, host);
        }

        public static void SaveLocalMessageStore(string username, string host, MessageStoreCollection localMessageStore)
        {
            DataManagerHelper.SaveLocalMessageStore(username, host, localMessageStore);
        }

        public static void RemoveMessagesNoLongerOnServer(ref MessageStoreCollection localMessageStore, List<long> remoteMessageStore)
        {
            RemoveMessagesNoLongerOnServer(ref localMessageStore, LongToStringList(remoteMessageStore));
        }

        public static void RemoveMessagesNoLongerOnServer(ref MessageStoreCollection localMessageStore, List<string> remoteMessageStore)
        {
            localMessageStore.Messages.RemoveAll(msg => remoteMessageStore.Find(uid => uid.Equals(msg.UID)) == null);
        }

        public static List<long> GetMessagesToCheck(MessageStoreCollection localMessageStore, List<long> remoteMessageStore)
        {
            return remoteMessageStore.FindAll(uid => localMessageStore.Messages.Find(msg => msg.UID.Equals(uid.ToString())) == null);
        }

        public static List<string> GetMessagesToCheck(MessageStoreCollection localMessageStore, List<string> remoteMessageStore)
        {
            return remoteMessageStore.FindAll(uid => localMessageStore.Messages.Find(msg => msg.UID.Equals(uid)) == null);
        }

        #endregion

        #region Private Methods

        public static List<string> LongToStringList(List<long> input)
        {
            return input.ConvertAll(new Converter<long, string>(LongToString));
        }

        private static string LongToString(long value)
        {
            return value.ToString();
        }

        #endregion
    }
}
