using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Mozilla.Autoconfig
{
    [XmlRoot("emailProvider")]
    public class EmailProvider
    {
        private string _id;
        private List<string> _domains;
        private string _displayName;
        private string _displayShortName;
        private List<IncomingServer> _incomingServers;
        private List<OutgoingServer> _outgoingServers;
        private List<Documentation> _documentations;

        public EmailProvider()
        { }

        [XmlAttribute("id")]
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        [XmlElement("domain")]
        public List<string> Domains
        {
            get { return _domains; }
            set { _domains = value; }
        }

        [XmlElement("displayName")]
        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }

        [XmlElement("displayShortName")]
        public string DisplayShortName
        {
            get { return _displayShortName; }
            set { _displayShortName = value; }
        }

        [XmlElement("incomingServer")]
        public List<IncomingServer> IncomingServers
        {
            get { return _incomingServers; }
            set { _incomingServers = value; }
        }

        [XmlElement("outgoingServer")]
        public List<OutgoingServer> OutgoingServers
        {
            get { return _outgoingServers; }
            set { _outgoingServers = value; }
        }

        [XmlElement("documentation")]
        public List<Documentation> Documentations
        {
            get { return _documentations; }
            set { _documentations = value; }
        }
    }
}
