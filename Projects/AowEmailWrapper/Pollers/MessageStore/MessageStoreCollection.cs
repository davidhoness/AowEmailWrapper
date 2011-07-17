using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AowEmailWrapper.Pollers.MessageStore
{
    [XmlRoot("messages")]
    public class MessageStoreCollection : IDisposable
    {
        private List<MessageStoreMessage> _messages;

        public MessageStoreCollection()
        {
            _messages = new List<MessageStoreMessage>();
        }

        public MessageStoreCollection(List<long> uids)
            : this(MessageStoreManager.LongToStringList(uids))
        { }

        public MessageStoreCollection(List<string> uids)
        {
            _messages = new List<MessageStoreMessage>();

            foreach (string uid in uids)
            {
                _messages.Add(new MessageStoreMessage(uid));
            }
        }

        [XmlElement("msg")]
        public List<MessageStoreMessage> Messages
        {
            get { return _messages; }
            set { _messages = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            _messages = null;
        }

        #endregion
    }
}
