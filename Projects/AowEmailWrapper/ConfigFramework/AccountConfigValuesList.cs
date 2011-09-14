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

        [XmlIgnore]
        public AccountConfigValues StartUpAccount
        {
            get 
            {
                AccountConfigValues startUpAccount = _accounts.Find(account => account.IsStartUpAccount);
                if (startUpAccount == null)
                {
                    //To handle old config data from previous versions of the Wrapper
                    startUpAccount = ActiveAccount;
                    if (startUpAccount != null)
                    {
                        startUpAccount.IsStartUpAccount = true;
                    }
                }
                return startUpAccount;
            }
            set 
            {
                if (value != null)
                {
                    _accounts.ForEach(account => account.IsStartUpAccount = false);
                    value.IsStartUpAccount = true;
                }
            }
        }

        public AccountConfigValues GetAccountByName(string name)
        {
            return _accounts.Find(item => !string.IsNullOrEmpty(item.Name) && item.Name.Equals(name, StringComparison.InvariantCulture));
        }

        public bool CheckAccountExistsByName(string name)
        {
            return (GetAccountByName(name) != null);
        }

        public AccountConfigValuesList()
        { }
    }
}
