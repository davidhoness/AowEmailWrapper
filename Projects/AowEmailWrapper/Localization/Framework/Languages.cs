using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace AowEmailWrapper.Localization.Framework
{
    [XmlRoot("languages")]
    public class Languages
    {
        private List<Language> _languageList;

        [XmlElement("language")]
        public List<Language> LanguageList
        {
            get { return _languageList; }
            set { _languageList = value; }
        }

        public Languages()
        { }
    }
}
