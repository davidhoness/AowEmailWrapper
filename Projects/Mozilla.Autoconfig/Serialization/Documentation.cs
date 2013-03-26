using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Mozilla.Autoconfig
{
    [XmlRoot("documentation")]
    public class Documentation
    {
        private string _url;
        private string _descr;

        public Documentation()
        { }

        [XmlAttribute("url")]
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        [XmlElement("descr")]
        public string Descr
        {
            get { return _descr; }
            set { _descr = value; }
        }
    }
}
