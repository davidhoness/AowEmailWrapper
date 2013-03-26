using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Mozilla.Autoconfig
{
    [XmlRoot("outgoingServer")]
    public class OutgoingServer : ServerBase
    {
        public OutgoingServer()
            : base()
        { }
    }
}
