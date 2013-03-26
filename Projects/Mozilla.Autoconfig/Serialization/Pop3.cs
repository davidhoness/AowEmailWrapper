using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Mozilla.Autoconfig
{
    [XmlRoot("pop3")]
    public class Pop3
    {
        private CheckInterval _checkInterval;

        public Pop3()
        { }

        [XmlElement("checkInterval")]
        public CheckInterval CheckInterval
        {
            get { return _checkInterval; }
            set { _checkInterval = value; }
        }
    }
}
