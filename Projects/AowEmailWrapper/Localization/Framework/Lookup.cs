using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AowEmailWrapper.Localization.Framework
{
    [XmlRoot("lookup")]
    public class Lookup
    {
        private string _key;
        private string _value;

        [XmlAttribute("key")]
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        [XmlAttribute("value")]
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public Lookup()
        { }
    }
}
