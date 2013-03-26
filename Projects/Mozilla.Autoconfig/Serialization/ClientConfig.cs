using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Mozilla.Autoconfig
{
    [XmlRoot("clientConfig")]
    public class ClientConfig
    {
        private string _version;
        private EmailProvider _emailProvider;
        private List<Documentation> _documentations;

        public ClientConfig()
        { }

        [XmlAttribute("version")]
        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        [XmlElement("emailProvider")]
        public EmailProvider EmailProvider
        {
            get { return _emailProvider; }
            set { _emailProvider = value; }
        }

        [XmlElement("documentation")]
        public List<Documentation> Documentations
        {
            get { return _documentations; }
            set { _documentations = value; }
        }
    }
}
