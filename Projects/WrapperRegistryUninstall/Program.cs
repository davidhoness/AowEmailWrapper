using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace WrapperRegistryUninstall
{
    class Program
    {
        private const string WINDOWS_REG_STARTUP_LOCATION = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";
        private const string AowRegPathTemplate = "Software\\Triumph Studios\\{0}";
        private const string Aow1GameName = "Age of Wonders";
        private const string Aow2GameName = "Age of Wonders II";
        private const string AowSmGameName = "Age of Wonders Shadow Magic";
        private const string EmailPath = "Email";
        private const string AttachmentDirKeyName = "Attachment Directory";
        private const string SMTPServerKeyName = "SMTP Server";
        private const string StartupKeyName = "Age of Wonders Email Wrapper";

        static void Main(string[] args)
        {
            RegistryHelper.DeleteValue(Registry.CurrentUser, WINDOWS_REG_STARTUP_LOCATION, StartupKeyName);

            foreach (string game in new string[] { Aow1GameName, Aow2GameName, AowSmGameName })
            {
                RegistryKey rootRegKey = RegistryHelper.GetDeepestKey(Registry.CurrentUser, string.Format(AowRegPathTemplate, game), false);

                if (rootRegKey != null)
                {
                    RegistryHelper.SetValue(rootRegKey, EmailPath, AttachmentDirKeyName, string.Empty);
                    RegistryHelper.SetValue(rootRegKey, EmailPath, SMTPServerKeyName, string.Empty);
                }
            }
        }
    }
}
