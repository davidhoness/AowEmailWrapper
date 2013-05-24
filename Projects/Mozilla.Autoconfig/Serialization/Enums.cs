using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Mozilla.Autoconfig
{    
    public enum SocketType
    {
        [XmlEnum(Name = "")]
        Unknown = 0,
        [XmlEnum(Name = "plain")]
        Plain,
        [XmlEnum(Name = "SSL")]
        SSL,
        [XmlEnum(Name = "STARTTLS")]
        STARTTLS
    }

    public enum ServerType
    {
        [XmlEnum(Name = "")]
        Unknown = 0,
        [XmlEnum(Name = "imap")]
        IMAP,
        [XmlEnum(Name = "pop3")]
        POP3,
        [XmlEnum(Name = "smtp")]
        SMTP
    }

    public enum AuthenticationType
    {
        [XmlEnum(Name = "")]
        Unknown = 0,
        [XmlEnum(Name = "none")]
        None,
        [XmlEnum(Name = "plain")]
        Plain,
        [XmlEnum(Name = "password-cleartext")]
        PasswordClearText,
        [XmlEnum(Name = "password-encrypted")]
        PasswordEncrypted,
        [XmlEnum(Name = "client-IP-address")]
        ClientIpAddress
    }

    public enum MechanismResponseType
    {
        Success,
        Exception,
        NotFound
    }

    public enum MechanismOriginType
    {
        LocalDisk,
        IspDb,
        LocalDomain,
        LocalWellKnown,
        Guess
    }

    public enum RequestType
    {
        Standard,
        MxLookup,
        Guess
    }
}
