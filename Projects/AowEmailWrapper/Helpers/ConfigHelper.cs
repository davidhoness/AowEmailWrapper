using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using AowEmailWrapper.ConfigFramework;

namespace AowEmailWrapper.Helpers
{
    public class ConfigHelper
    {
        private const string NotifySoundKey = "NotifySound";
        private const string SentSoundKey = "SentSound";
        private const string AutostartPauseSecondsKey = "AutostartPauseSeconds";
        private const string EndedFolderKey = "EndedFolder";
        private const string EndedFolderDefault = "Ended";
        private const string WrapperListenPortKey = "WrapperListenPort";
        private const int WrapperListenPortDefault = 49252;

        public const string AUTOSTART_CMD_PARAM = "/autostart";

        public static int WrapperListenPort
        {
            get
            {
                int returnVal = GetProperty<int>(WrapperListenPortKey, WrapperListenPortDefault);
                return (returnVal > 0) ? returnVal : WrapperListenPortDefault;
            }
        }

        public static string BuildVersion
        {
            get { return System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString(); }
        }

        public static string NotifySound
        {
            get { return GetProperty<string>(NotifySoundKey, null); }
        }

        public static string SentSound
        {
            get { return GetProperty<string>(SentSoundKey, null); }
        }

        public static int AutostartPauseSeconds
        {
            get { return GetProperty<int>(AutostartPauseSecondsKey, 5); }
        }

        public static string EndedFolder
        {
            get { return GetProperty<string>(EndedFolderKey, EndedFolderDefault); }
        }

        public static T GetProperty<T>(string key, T defaultValue)
        {
            T value = defaultValue;
            string configValue = ConfigurationManager.AppSettings.Get(key);

            if (!string.IsNullOrEmpty(configValue))
            {
                value = (T)Convert.ChangeType(configValue, typeof(T));
            }

            return value;
        }

        public static T ParseEnumString<T>(string theText)
        {
            if (!string.IsNullOrEmpty(theText))
            {
                return (T)Enum.Parse(typeof(T), theText, true);
            }
            else
            {
                return (T)default(T);
            }
        }

    }
}
