using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AowEmailWrapper.ConfigFramework
{
    [XmlRoot("aowemailwrapper_config")]
    public class Config
    {
        private PreferencesConfigValues _preferencesConfigValues;
        private AccountConfigValuesList _accountsList;

        [XmlElement("preferences_config")]
        public PreferencesConfigValues PreferencesConfig
        {
            get { return _preferencesConfigValues; }
            set { _preferencesConfigValues = value; }
        }

        [XmlElement("accounts")]
        public AccountConfigValuesList AccountsList
        {
            get { return _accountsList; }
            set { _accountsList = value; }
        }

        public Config()
            : this(false)
        { }

        public Config(bool defaults)
        {
            if (defaults)
            {
                _preferencesConfigValues = new PreferencesConfigValues(true);
                _accountsList = new AccountConfigValuesList();
                _accountsList.Accounts = new List<AccountConfigValues>();
            }
        }
    }
}
