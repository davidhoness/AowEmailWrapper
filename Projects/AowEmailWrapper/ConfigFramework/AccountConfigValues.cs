using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AowEmailWrapper.ConfigFramework
{
    public enum EmailProviderType
    {
        [XmlEnum(Name = "None")]
        None = 0,
        [XmlEnum(Name = "Google")]
        Google,
        [XmlEnum(Name = "WindowsLive")]
        WindowsLive,
        [XmlEnum(Name = "Yahoo")]
        Yahoo,
        [XmlEnum(Name = "Other")]
        Other
    }

    [XmlRoot("account")]
    public class AccountConfigValues
    {
        private PollingConfigValues _pollingConfigValues;
        private SmtpConfigValues _smtpConfigValues;
        private string _name;
        private bool _isStartUpAccount;
        private EmailProviderType _emailProviderType; //Template property
        private string _domains; //Template property
        private string _shortUserName; //Template property

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

        //Template property
        [XmlAttribute("emailprovidertype")]
        public EmailProviderType EmailProvider
        {
            get { return _emailProviderType; }
            set { _emailProviderType = value; }
        }

        //Template property
        [XmlAttribute("domains")]
        public string Domains
        {
            get { return _domains; }
            set { _domains = value; }
        }

        //Template property
        [XmlAttribute("shortusername")]
        public string ShortUserName
        {
            get { return _shortUserName; }
            set { _shortUserName = value; }
        }

        public AccountConfigValues()
        { }
    }
}
