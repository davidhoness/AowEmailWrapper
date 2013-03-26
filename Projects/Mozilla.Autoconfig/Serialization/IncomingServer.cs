using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Mozilla.Autoconfig
{
    [XmlRoot("incomingServer")]
    public class IncomingServer : ServerBase
    {
        private Pop3 _pop3;

        public IncomingServer()
            : base()
        { }

        [XmlElement("pop3")]
        public Pop3 Pop3
        {
            get { return _pop3; }
            set { _pop3 = value; }
        }
    }
}
