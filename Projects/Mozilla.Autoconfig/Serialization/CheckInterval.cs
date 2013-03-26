using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Mozilla.Autoconfig
{
    [XmlRoot("checkInterval")]
    public class CheckInterval
    {
        private int _minutes;

        public CheckInterval()
        { }

        [XmlAttribute("minutes")]
        public int Minutes
        {
            get { return _minutes; }
            set { _minutes = value; }
        }
    }
}
