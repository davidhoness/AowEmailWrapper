using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace AowEmailWrapper.Localization.Framework
{
    public class Language
    {
        private List<Lookup> _lookupList;
        private string _displayName;
        private string _code;

        [XmlAttribute("display")]
        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }

        [XmlAttribute("code")]
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        [XmlElement("lookup")]
        public List<Lookup> LoopupList
        {
            get { return _lookupList; }
            set { _lookupList = value; }
        }

        public Language()
        { }

        public Lookup FindLookup(string key)
        {
            Lookup returnVal = null;

            if (_lookupList != null &&
                _lookupList.Count > 0)
            {
                returnVal = _lookupList.Find(lookup => lookup.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase));
            }

            return returnVal;
        }
    }
}
