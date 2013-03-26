using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Mozilla.Autoconfig
{
    public class ServerBase
    {
        private const string EMAILADDRESS = "%EMAILADDRESS%";
        private const string EMAILDOMAIN = "%EMAILDOMAIN%";
        private const string EMAILLOCALPART = "%EMAILLOCALPART%";
        private const char At = '@';
        
        private ServerType _type;
        private string _hostname;
        private int _port;
        private SocketType _socketType;
        private string _usernameFormat;
        private AuthenticationType _authentication;
        
        public ServerBase()
        { }

        [XmlAttribute("type")]
        public ServerType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        [XmlElement("hostname")]
        public string Hostname
        {
            get { return _hostname; }
            set { _hostname = value; }
        }

        [XmlElement("port")]
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        [XmlElement("socketType")]
        public SocketType SocketType
        {
            get { return _socketType; }
            set { _socketType = value; }
        }

        [XmlElement("username")]
        public string UsernameFormat
        {
            get { return _usernameFormat; }
            set { _usernameFormat = value; }
        }

        [XmlElement("authentication")]
        public AuthenticationType Authentication
        {
            get { return _authentication; }
            set { _authentication = value; }
        }

        public string GetUsernameFormatted(string emailAddress)
        {
            string returnVal = null;

            if (!string.IsNullOrEmpty(UsernameFormat) &&
                !string.IsNullOrEmpty(emailAddress))
            {
                string domain = string.Empty;
                string localPart = string.Empty;

                int atIndex = emailAddress.IndexOf(At);

                if (atIndex > 0)
                {
                    domain = emailAddress.Substring(atIndex + 1);
                    localPart = emailAddress.Substring(0, atIndex);
                }

                returnVal = UsernameFormat
                    .Replace(EMAILADDRESS, emailAddress)
                    .Replace(EMAILDOMAIN, domain)
                    .Replace(EMAILLOCALPART, localPart);
            }
            else if (Authentication.Equals(AuthenticationType.None))
            {
                returnVal = string.Empty;
            }
            else
            {
                returnVal = emailAddress;
            }

            return returnVal;
        }
    }
}
