using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Mozilla.Autoconfig
{
    internal class Mechanism
    {
        private string _format;
        private MechanismOriginType _type;

        public Mechanism(string format, MechanismOriginType type)
        {
            _format = format;
            _type = type;
        }

        public MechanismResponse Attempt(string domain)
        {
            MechanismResponse returnVal = new MechanismResponse();
            returnVal.Origin = _type;

            string xmlPath = string.Format(_format, domain);
            string xml = WebClientGetXml(xmlPath);

            if (!string.IsNullOrEmpty(xml))
            {
                try
                {
                    returnVal.ClientConfig = Deserialize<ClientConfig>(xml);
                    returnVal.ResponseType = MechanismResponseType.Success;
                }
                catch (Exception ex)
                {
                    returnVal.Exception = ex;
                    returnVal.ResponseType = MechanismResponseType.Exception;
                }
            }
            else
            {
                returnVal.ResponseType = MechanismResponseType.NotFound;
            }

            return returnVal;
        }

        private static string WebClientGetXml(string path)
        {
            string returnVal = null;

            using (WebClient fileReader = new WebClient())
            {
                try
                {
                    returnVal = fileReader.DownloadString(path);
                }
                catch { }
            }

            return returnVal;
        }

        private static T Deserialize<T>(string theXml)
        {
            T value = default(T);

            if (!string.IsNullOrEmpty(theXml))
            {
                XmlSerializer serial = new XmlSerializer(typeof(T));
                {
                    using (StringReader reader = new StringReader(theXml))
                    {
                        try
                        {
                            return (T)Convert.ChangeType(serial.Deserialize(reader), typeof(T));
                        }
                        finally
                        {
                            serial = null;
                        }
                    }
                }
            }

            return value;
        }
    }
}
