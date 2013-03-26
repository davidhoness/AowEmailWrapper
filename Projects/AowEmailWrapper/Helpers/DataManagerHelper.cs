using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using AowEmailWrapper.ConfigFramework;
using AowEmailWrapper.Pollers.MessageStore;
using AowEmailWrapper.Localization.Framework;

namespace AowEmailWrapper.Helpers
{
    public class DataManagerHelper
    {        
        private const string CONFIG_FILE_NAME = "config.xml";
        private const string ACTIVITY_FILE_NAME = "activity.xml";
        private const string LOCALIZATION_FILE_NAME = "Localization.xml";
        private const string ACCOUNT_TEMPLATES_FILE_NAME = "TemplateAccounts.xml";
        private const string MessageStoreFileTemplate = "{0}@{1}.xml";
        private const string TurnLogFilenameTemplate = "{0}.log";

        #region Account Templates
        /*
        public static AccountConfigValuesList LoadAccountTemplates()
        {
            AccountConfigValuesList returnVal = null;

            string templatesFilePath = Path.Combine(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), ACCOUNT_TEMPLATES_FILE_NAME);

            returnVal = FileHelper.LoadXmlFile<AccountConfigValuesList>(templatesFilePath);

            if (returnVal == null)
            {
                returnVal = new AccountConfigValuesList();
            }

            return returnVal;
        }
        */
        #endregion

        #region Localization

        public static Languages LoadLanguages()
        {
            Languages returnVal = null;

            string localizationFilePath = Path.Combine(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), LOCALIZATION_FILE_NAME);

            returnVal = FileHelper.LoadXmlFile<Languages>(localizationFilePath);

            if (returnVal == null)
            {
                returnVal = new Languages();
            }

            return returnVal;
        }

        #endregion

        #region Config

        public static Config LoadConfig()
        {
            bool isNew = false;
            return LoadConfig(out isNew);
        }

        public static Config LoadConfig(out bool isNewConfig)
        {
            Config returnVal = null;
            isNewConfig = false;

            string configFilePath = Path.Combine(AppDataHelper.Config.FullName, CONFIG_FILE_NAME);

            returnVal = FileHelper.LoadXmlFile<Config>(configFilePath);

            if (returnVal == null)
            {
                returnVal = new Config(true);
                isNewConfig = true;
            }

            return returnVal;
        }

        public static void SaveConfig(Config toSave)
        {
            string configFilePath = Path.Combine(AppDataHelper.Config.FullName, CONFIG_FILE_NAME);
            FileHelper.SaveXmlFile(configFilePath, toSave);
        }

        #endregion

        #region Activity Log

        public static ActivityList LoadActivityLog()
        {
            ActivityList returnVal = null;

            string activityFilePath = Path.Combine(AppDataHelper.ActivityLog.FullName, ACTIVITY_FILE_NAME);

            returnVal = FileHelper.LoadXmlFile<ActivityList>(activityFilePath);

            if (returnVal == null) returnVal = new ActivityList();

            return returnVal;
        }

        public static void SaveActivityLog(ActivityList toSave)
        {
            string activityFilePath = Path.Combine(AppDataHelper.ActivityLog.FullName, ACTIVITY_FILE_NAME);
            FileHelper.SaveXmlFile(activityFilePath, toSave);
        }

        #endregion

        #region Message Store

        public static MessageStoreCollection LoadLocalMessageStore(string username, string host)
        {
            string theFilePath = GetLocalMessageStoreFileName(username, host);
            return FileHelper.LoadXmlFile<MessageStoreCollection>(theFilePath);
        }

        public static void SaveLocalMessageStore(string username, string host, MessageStoreCollection localMessageStore)
        {
            string theFilePath = GetLocalMessageStoreFileName(username, host);
            FileHelper.SaveXmlFile(theFilePath, localMessageStore);
        }

        private static string GetLocalMessageStoreFileName(string username, string host)
        {
            string fileName = string.Format(MessageStoreFileTemplate, username, host);
            return Path.Combine(AppDataHelper.MessageStore.FullName, fileName);
        }

        #endregion

        #region Turn Logger

        public static void SaveTurnLog(string fileName, string theLog)
        {
            string theFilePath = GetTurnLogFilePath(fileName);
            DeleteTurnLog(theFilePath);
            FileHelper.SaveTextFile(theFilePath, theLog, true);
        }

        public static string LoadTurnLog(string fileName)
        {
            string theFilePath = GetTurnLogFilePath(fileName);
            return FileHelper.LoadTextFile(theFilePath, true);
        }

        public static bool DeleteTurnLog(string fileName)
        {
            return FileHelper.DeleteTextFile(GetTurnLogFilePath(fileName));
        }

        private static string GetTurnLogFilePath(string fileName)
        {
            return Path.Combine(AppDataHelper.TurnLogs.FullName, string.Format(TurnLogFilenameTemplate, StringHelper.RemoveInvalidPathChars(fileName)));
        }

        #endregion
    }
}
