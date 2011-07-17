using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AowEmailWrapper.ConfigFramework
{
    [XmlRoot("accounts")]
    public class AccountConfigValuesList
    {
        private List<AccountConfigValues> _accounts;
        private string _activeAccountName;

        [XmlElement("account")]
        public List<AccountConfigValues> Accounts
        {
            get { return _accounts; }
            set { _accounts = value; }
        }

        [XmlAttribute("active_account")]
        public string ActiveAccountName
        {
            get { return _activeAccountName; }
            set { _activeAccountName = value; }
        }

        [XmlIgnore]
        public AccountConfigValues ActiveAccount
        {
            get { return GetAccountByName(_activeAccountName); }
        }

        public AccountConfigValues GetAccountByName(string name)
        {
            return _accounts.Find(item => !string.IsNullOrEmpty(item.Name) && item.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }

        public bool CheckAccountExistsByName(string name)
        {
            return (GetAccountByName(name) != null);
        }

        public AccountConfigValuesList()
            : this(false)
        { }

        public AccountConfigValuesList(bool defaults)
        {
            if (defaults)
            {
                string defaultName = "Default";
                _activeAccountName = defaultName;
                _accounts = new List<AccountConfigValues>();
                _accounts.Add(new AccountConfigValues(true, defaultName));                
            }
        }
    }
}
