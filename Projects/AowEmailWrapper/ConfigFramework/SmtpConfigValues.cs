using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using AowEmailWrapper.Helpers;

namespace AowEmailWrapper.ConfigFramework
{
    public enum SmtpSSLType
    {
        [XmlEnum(Name = "None")]
        None = 0,
        [XmlEnum(Name = "SSL")]
        SSL,
        [XmlEnum(Name = "TLS")]
        TLS
    }

    [XmlRoot("smtp_config")]
    public class SmtpConfigValues
    {
        private string _smtpServer;
        private int _port;
        private string _emailAddress;
        private bool _authentication;
        private string _username;
        private string _password;
        private bool _usePollingCredentials;
        private SmtpSSLType _smtpSSLType;
        private bool _bccMyself;

        [XmlAttribute("smtpserver")]
        public string SmtpServer
        {
            get { return _smtpServer; }
            set { _smtpServer = value; }
        }

        [XmlAttribute("port")]
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        [XmlAttribute("emailaddress")]
        public string EmailAddress
        {
            get { return _emailAddress; }
            set { _emailAddress = value; }
        }

        [XmlAttribute("smtpssltype")]
        public SmtpSSLType SmtpSSLType
        {
            get { return _smtpSSLType; }
            set { _smtpSSLType = value; }
        }

        [XmlAttribute("authentication")]
        public bool Authentication
        {
            get { return _authentication; }
            set { _authentication = value; }
        }

        [XmlAttribute("username")]
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        [XmlAttribute("password")]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        [XmlIgnore]
        public string PasswordTrue
        {
            get { return CryptographyHelper.Decrypt(_password); }
            set { _password = CryptographyHelper.Encrypt(value); }
        }

        [XmlAttribute("usepollingcredentials")]
        public bool UsePollingCredentials
        {
            get { return _usePollingCredentials; }
            set { _usePollingCredentials = value; }
        }

        [XmlAttribute("bccmyself")]
        public bool BCCMyself
        {
            get { return _bccMyself; }
            set { _bccMyself = value; }
        }

        public SmtpConfigValues()
            : this(false)
        { }

        public SmtpConfigValues(bool defaults)
        {
            if (defaults)
            {
                _smtpServer = "smtp.gmail.com";
                _port = 587;
                _emailAddress = "joe.bloggs@gmail.com";
                _authentication = true;
                _username = string.Empty;
                _password = string.Empty;
                _usePollingCredentials = true;
                _smtpSSLType = SmtpSSLType.TLS;
                _bccMyself = false;
            }
        }
    }
}
