using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AowEmailWrapper.Helpers
{
    public class AppDataHelper
    {
        private const string APPDATA_ENVIRONMENT_VARIABLE = "APPDATA";
        private const string APPDATA_Wrapper_Root = "AowEmailWrapper";
        private const string APPDATA_Wrapper_MessageStore = "MessageStore";
        private const string APPDATA_Wrapper_CheckEmail = "CheckEmail";
        private const string APPDATA_Wrapper_TurnLogs = "TurnLogs";
        private const string APPDATA_Wrapper_Config = "Config";
        private const string APPDATA_Wrapper_ActivityLog = "ActivityLog";
        private const string APPDATA_Wrapper_Resend = "Resend";

        private static DirectoryInfo _wrapperRoot;
        private static DirectoryInfo _wrapperMessageStore;
        private static DirectoryInfo _wrapperCheckEmail;
        private static DirectoryInfo _wrapperTurnLogs;
        private static DirectoryInfo _wrapperConfig;
        private static DirectoryInfo _wrapperActivityLog;
        private static DirectoryInfo _wrapperResend;

        public static DirectoryInfo AppDataFolder
        {
            get { return new DirectoryInfo(Environment.GetEnvironmentVariable(APPDATA_ENVIRONMENT_VARIABLE)); }
        }
        
        public static DirectoryInfo Root
        {
            get
            {
                if (_wrapperRoot == null)
                {
                    _wrapperRoot = GetFolder(AppDataFolder, APPDATA_Wrapper_Root);
                }
                return _wrapperRoot; 
            }
        }

        public static DirectoryInfo MessageStore
        {
            get
            {
                if (_wrapperMessageStore == null)
                {
                    _wrapperMessageStore = GetFolder(Root, APPDATA_Wrapper_MessageStore);
                }
                return _wrapperMessageStore;
            }
        }

        public static DirectoryInfo CheckEmail
        {
            get
            {
                if (_wrapperCheckEmail == null)
                {
                    _wrapperCheckEmail = GetFolder(Root, APPDATA_Wrapper_CheckEmail);
                }
                return _wrapperCheckEmail;
            }
        }

        public static DirectoryInfo TurnLogs
        {
            get
            {
                if (_wrapperTurnLogs == null)
                {
                    _wrapperTurnLogs = GetFolder(Root, APPDATA_Wrapper_TurnLogs);
                }
                return _wrapperTurnLogs;
            }
        }

        public static DirectoryInfo Config
        {
            get
            {
                if (_wrapperConfig == null)
                {
                    _wrapperConfig = GetFolder(Root, APPDATA_Wrapper_Config);
                }
                return _wrapperConfig;
            }
        }

        public static DirectoryInfo ActivityLog
        {
            get
            {
                if (_wrapperActivityLog == null)
                {
                    _wrapperActivityLog = GetFolder(Root, APPDATA_Wrapper_ActivityLog);
                }
                return _wrapperActivityLog;
            }
        }

        public static DirectoryInfo Resend
        {
            get
            {
                if (_wrapperResend == null)
                {
                    _wrapperResend = GetFolder(Root, APPDATA_Wrapper_Resend);
                }
                return _wrapperResend;
            }
        }

        private static DirectoryInfo GetFolder(DirectoryInfo root, string folderName)
        {
            DirectoryInfo returnVal = null;
            if (Directory.Exists(root.FullName))
            {
                string newFolder = Path.Combine(root.FullName, folderName);
                if (!Directory.Exists(newFolder))
                {
                    Directory.CreateDirectory(newFolder);
                }

                returnVal = new DirectoryInfo(newFolder);
            }
            return returnVal;
        }
    }
}
