using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using AowEmailWrapper.Helpers;

namespace AowEmailWrapper.ConfigFramework
{
    public enum EmailType
    {
        [XmlEnum(Name = "IMAP")]
        IMAP,
        [XmlEnum(Name = "POP3")]
        POP3
    }

    [XmlRoot("polling_config")]
    public class PollingConfigValues
    {
        private bool _usePolling;
        private EmailType _emailType;
        private string _server;
        private int _port;
        private bool _useSSL;
        private string _username;
        private string _password;
        private int _pollInterval;

        [XmlAttribute("usepolling")]
        public bool UsePolling
        {
            get { return _usePolling; }
            set { _usePolling = value; }
        }

        [XmlAttribute("emailtype")]
        public EmailType EmailType
        {
            get { return _emailType; }
            set { _emailType = value; }
        }

        [XmlAttribute("server")]
        public string Server
        {
            get { return _server; }
            set { _server = value; }
        }

        [XmlAttribute("port")]
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        [XmlAttribute("usessl")]
        public bool UseSSL
        {
            get { return _useSSL; }
            set { _useSSL = value; }
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

        [XmlAttribute("pollinterval")]
        public int PollInterval
        {
            get { return _pollInterval; }
            set { _pollInterval = value; }
        }

        public PollingConfigValues()
            : this(false)
        { }

        public PollingConfigValues(bool defaults)
        {
            if (defaults)
            {
                _usePolling = true;
                _emailType = EmailType.IMAP;
                _server = "imap.gmail.com";
                _port = 993;
                _useSSL = true;
                _username = "joe.bloggs";
                _password = string.Empty;
                _pollInterval = 10;
            }
        }
    }
}
