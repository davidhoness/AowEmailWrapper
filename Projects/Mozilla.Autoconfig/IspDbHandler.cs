using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Configuration;
using System.Reflection;

namespace Mozilla.Autoconfig
{
    public class IspDbHandler
    {
        private const char At = '@';        
        private const char Bk = '\\';
        private const char Fw = '/';

        private const string LocalDiskKey = "Mozilla.Autoconfig.LocalDiskTemplate";
        private const string LocalDiskTemplateDefault = "file:///{0}/isp/{{0}}.xml"; //Must use double curly bracket notation here

        private const string IspDbKey = "Mozilla.Autoconfig.IspDbTemplate";
        private const string IspDbTemplateDefault = "https://autoconfig-live.mozillamessaging.com/autoconfig/v1.1/{0}";

        private const string LocalDomainKey = "Mozilla.Autoconfig.LocalDomainTemplate";
        private const string LocalDomainTemplateDefault = "http://autoconfig.{0}/mail/config-v1.1.xml";

        private const string LocalWellKnownKey = "Mozilla.Autoconfig.LocalWellKnownTemplate";
        private const string LocalWellKnownTemplateDefault = "http://{0}/.well-known/autoconfig/mail/config-v1.1.xml";

        /// <summary>
        /// Gets the autoconfig.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <param name="guess">if set to <c>true</c> [guess the config if all other mechanisms fail].</param>
        /// <returns></returns>
        public static MechanismResponse GetAutoconfig(string emailAddress, RequestType requestType)
        {
            //Ignore SSL certificate errors
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            MechanismResponse returnVal = new MechanismResponse();

            if (!string.IsNullOrEmpty(emailAddress))
            {
                int atIndex = emailAddress.IndexOf(At);

                if (atIndex > 0)
                {
                    string domain = emailAddress.Substring(atIndex + 1);
                    returnVal = GetAutoconfigByDomain(domain, requestType);
                }
            }

            ServicePointManager.ServerCertificateValidationCallback = null;

            return returnVal;
        }

        private static MechanismResponse GetAutoconfigByDomain(string domain, RequestType requestType)
        {
            MechanismResponse returnVal = new MechanismResponse();

            if (!string.IsNullOrEmpty(domain))
            {
                List<Mechanism> mechanisms = GetMechanisms();

                switch (requestType)
                { 
                    case RequestType.Standard:
                        returnVal = AttemptAll(mechanisms, domain);
                        break;
                    case RequestType.MxLookup:
                        bool found = false;
                        string[] mxRecords = MxLookupHandler.GetMXRecordsTrimDistinct(domain);

                        if (mxRecords != null && mxRecords.Length > 0)
                        {
                            foreach (string mx in mxRecords)
                            {
                                returnVal = AttemptAll(mechanisms, mx);
                                found = returnVal != null && returnVal.IsSuccess;
                                if (found) break;
                            }

                            if (!found) //Guess from the mx record
                            {
                                foreach (string mx in mxRecords)
                                {
                                    returnVal = MxGuessHandler.GuessConfig(mx);
                                    found = returnVal != null && returnVal.IsSuccess;
                                    if (found) break;
                                }
                            }
                        }
                        break;
                    case RequestType.Guess:
                        returnVal = MxGuessHandler.GuessConfig(domain);
                        break;
              
                }
            }

            if (returnVal != null)
            {
                returnVal.RequestType = requestType;
            }

            return returnVal;
        }

        private static List<Mechanism> GetMechanisms()
        {
            List<Mechanism> mechanisms = new List<Mechanism>();
            string executingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Replace(Bk, Fw);
            mechanisms.Add(new Mechanism(string.Format(GetProperty<string>(LocalDiskKey, LocalDiskTemplateDefault), executingPath), MechanismOriginType.LocalDisk));
            mechanisms.Add(new Mechanism(GetProperty<string>(LocalDomainKey, LocalDomainTemplateDefault), MechanismOriginType.LocalDomain));
            mechanisms.Add(new Mechanism(GetProperty<string>(LocalWellKnownKey, LocalWellKnownTemplateDefault), MechanismOriginType.LocalWellKnown));
            mechanisms.Add(new Mechanism(GetProperty<string>(IspDbKey, IspDbTemplateDefault), MechanismOriginType.IspDb));
            return mechanisms;
        }

        private static MechanismResponse AttemptAll(List<Mechanism> mechanisms, string domain)
        {
            MechanismResponse returnVal = new MechanismResponse();

            foreach (Mechanism mechanism in mechanisms)
            {
                returnVal = mechanism.Attempt(domain);
                if (returnVal != null && returnVal.IsSuccess) break;
            }

            return returnVal;
        }

        private static T GetProperty<T>(string key, T defaultValue)
        {
            T value = defaultValue;
            string configValue = ConfigurationManager.AppSettings.Get(key);

            if (!string.IsNullOrEmpty(configValue))
            {
                value = (T)Convert.ChangeType(configValue, typeof(T));
            }

            return value;
        }
    }
}
