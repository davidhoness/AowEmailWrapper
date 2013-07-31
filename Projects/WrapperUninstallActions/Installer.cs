using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;

namespace WrapperUninstallActions
{
    [RunInstaller(true)]
    public partial class Installer :  System.Configuration.Install.Installer
    {
        private const string APPDATA_ENVIRONMENT_VARIABLE = "APPDATA";
        private const string APPDATA_Wrapper_Root = "AowEmailWrapper";

        private const string WINDOWS_REG_STARTUP_LOCATION = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";
        private const string AowRegPathTemplate = "Software\\Triumph Studios\\{0}";
        private const string Aow1GameName = "Age of Wonders";
        private const string Aow2GameName = "Age of Wonders II";
        private const string AowSmGameName = "Age of Wonders Shadow Magic";
        private const string EmailPath = "Email";
        private const string AttachmentDirKeyName = "Attachment Directory";
        private const string SMTPServerKeyName = "SMTP Server";
        private const string StartupKeyName = "Age of Wonders Email Wrapper";
        private const string RemoveWrapperConfigMessage = "Remove the Wrapper config files?";

        public Installer()
        {
            InitializeComponent();
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Uninstall(IDictionary savedState)
        {
            //Remove auto start key
            RegistryHelper.DeleteValue(Registry.CurrentUser, WINDOWS_REG_STARTUP_LOCATION, StartupKeyName);

            //Blank out email settings
            foreach (string game in new string[] { Aow1GameName, Aow2GameName, AowSmGameName })
            {
                RegistryKey rootRegKey = RegistryHelper.GetDeepestKey(Registry.CurrentUser, string.Format(AowRegPathTemplate, game), false);

                if (rootRegKey != null)
                {
                    RegistryHelper.SetValue(rootRegKey, EmailPath, AttachmentDirKeyName, string.Empty);
                    RegistryHelper.SetValue(rootRegKey, EmailPath, SMTPServerKeyName, string.Empty);
                }
            }

            DialogResult removeConfig = MessageBox.Show(RemoveWrapperConfigMessage, StartupKeyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (removeConfig == DialogResult.Yes)
            {
                //Delete app data folder
                DirectoryInfo AppDataFolder = new DirectoryInfo(Path.Combine(Environment.GetEnvironmentVariable(APPDATA_ENVIRONMENT_VARIABLE), APPDATA_Wrapper_Root));
                if (AppDataFolder != null && AppDataFolder.Exists)
                {
                    AppDataFolder.Delete(true);
                }
            }

            base.Uninstall(savedState);
        }
    }
}
