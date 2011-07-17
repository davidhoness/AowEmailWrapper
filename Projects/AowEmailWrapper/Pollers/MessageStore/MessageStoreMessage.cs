using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AowEmailWrapper.Pollers.MessageStore
{
    [XmlRoot("msg")]
    public class MessageStoreMessage
    {
        private string _uid;
        private string _from;
        private string _subject;
        private string _date;
        private string _dateTicks;
        private string _fileName;

        public MessageStoreMessage()
        {
            _uid = string.Empty;
        }

        public MessageStoreMessage(string uid)
        {
            _uid = uid;
        }

        public MessageStoreMessage(long uid)
        {
            _uid = uid.ToString();
        }

        [XmlAttribute("uid")]
        public string UID
        {
            get { return _uid; }
            set { _uid = value; }
        }

        [XmlAttribute("from")]
        public string From
        {
            get { return _from; }
            set { _from = value; }
        }

        [XmlAttribute("subject")]
        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }

        [XmlAttribute("date")]
        public string Date
        {
            get { return _date; }
            set { _date = value; }
        }

        [XmlAttribute("ticks")]
        public string DateTicks
        {
            get { return _dateTicks; }
            set { _dateTicks = value; }
        }

        [XmlAttribute("fileName")]
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }
    }
}
