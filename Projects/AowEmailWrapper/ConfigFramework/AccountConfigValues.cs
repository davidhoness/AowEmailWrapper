using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AowEmailWrapper.ConfigFramework
{
    [XmlRoot("account")]
    public class AccountConfigValues
    {
        private PollingConfigValues _pollingConfigValues;
        private SmtpConfigValues _smtpConfigValues;
        private string _name;

        [XmlElement("polling_config")]
        public PollingConfigValues PollingConfig
        {
            get { return _pollingConfigValues; }
            set { _pollingConfigValues = value; }
        }

        [XmlElement("smtp_config")]
        public SmtpConfigValues SmtpConfig
        {
            get { return _smtpConfigValues; }
            set { _smtpConfigValues = value; }
        }

        [XmlAttribute("name")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public AccountConfigValues()
            : this(false, "Default")
        { }

        public AccountConfigValues(bool defaults, string name)
        {
            if (defaults)
            {
                _name = name;
                _pollingConfigValues = new PollingConfigValues(defaults);
                _smtpConfigValues = new SmtpConfigValues(defaults);
            }
        }
    }
}
