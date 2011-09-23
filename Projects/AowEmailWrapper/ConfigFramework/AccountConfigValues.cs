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
        private string _emailProviderType; //Template property
        private string _shortUserName; //Template property
        private List<string> _templateDomains; //Template property

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
        public string EmailProvider
        {
            get { return _emailProviderType; }
            set { _emailProviderType = value; }
        }

        //Template property
        [XmlElement("domain")]
        public List<string> TemplateDomains
        {
            get { return _templateDomains; }
            set { _templateDomains = value; }
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

        public bool IsDomainMatch(string emailAddress)
        {
            bool returnVal = false;

            if (_templateDomains != null && _templateDomains.Count > 0 & !string.IsNullOrEmpty(emailAddress))
            {
                string emailAddressTrimmed = emailAddress.ToLower().Trim();
                returnVal = _templateDomains.Find(domain => emailAddressTrimmed.Contains(domain)) != null;
            }

            return returnVal;
        }

    }
}
