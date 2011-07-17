using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Win32;
using AowEmailWrapper.Helpers;
using System.Xml.Serialization;

namespace AowEmailWrapper.Games
{
    public enum AowGameType
    {
        [XmlEnum(Name = "Aow1")]
        Aow1 = 1,
        [XmlEnum(Name = "Aow2")]
        Aow2,
        [XmlEnum(Name = "AowSm")]
        AowSm,
        [XmlEnum(Name = "Unknown")]
        Unknown
    }

    public class AowGame
    {
        #region String Constants

        private const string AowRegPathTemplate = "Software\\Triumph Studios\\{0}";
        private const string Aow1GameName = "Age of Wonders";
        private const string Aow2GameName = "Age of Wonders II";
        private const string AowSmGameName = "Age of Wonders Shadow Magic";

        private const string GeneralPath = "General";
        private const string RootDirKeyName = "Root Directory";
        private const string MostRecentlyUsedFileKeyName = "Most Recently Used File";

        private const string EmailPath = "Email";        
        private const string AttachmentDirKeyName = "Attachment Directory";
        private const string LocalEmailKeyName = "Local Email address";
        private const string SMTPServerKeyName = "SMTP Server";

        private const string Aow1ExeName = "AoW.exe";
        private const string Aow2ExeName = "AoW2.exe";
        private const string AowSmExeName = "AoWSM.exe";

        private const string SteamRegPath = "Software\\Valve\\Steam";
        private const string SteamDirKeyName = "SteamPath";
        private const string SteamAppsPathTemplate = "{0}\\SteamApps\\common";
        private const string DummyTestFileTemplate = "{0}.asg";
        
        private const string EmailInFolder = "EmailIn";
        private const string EmailOutFolder = "EmailOut";
        private const string SaveFolder = "Save";

        #endregion

        #region Private Members

        private AowGameType _gameType;
        private bool _isInstalled = false;
        private DirectoryInfo _root = null;
        private DirectoryInfo _emailIn = null;
        private DirectoryInfo _emailOut = null;
        private DirectoryInfo _save = null;
        private RegistryKey _rootRegKey = null;
        private string _exeFile = null;
        private string _gameName = null;
        private bool _writeAccess = false;

        #endregion

        #region Public Properties

        public AowGameType GameType
        {
            get { return _gameType; }
        }

        public bool IsInstalled
        {
            get { return _isInstalled; }
        }

        public DirectoryInfo Root
        {
            get { return _root; }
        }

        public DirectoryInfo EmailIn
        {
            get { return _emailIn; }
        }

        public DirectoryInfo EmailOut
        {
            get { return _emailOut; }
        }

        public DirectoryInfo Save
        {
            get { return _save; }
        }

        public string ExeFile
        {
            get { return _exeFile; }
        }

        public string GameName
        {
            get { return _gameName; }
        }

        public bool WriteAccess
        {
            get { return _writeAccess; }
        }

        #endregion

        #region Constructors

        public AowGame(AowGameType theGameType)
        {
            _gameType = theGameType;

            switch (_gameType)
            {
                case AowGameType.Aow1:
                    _exeFile = Aow1ExeName;
                    _gameName = Aow1GameName;
                    break;
                case AowGameType.Aow2:
                    _exeFile = Aow2ExeName;
                    _gameName = Aow2GameName;
                    break;
                case AowGameType.AowSm:                    
                    _exeFile = AowSmExeName;
                    _gameName = AowSmGameName;
                    break;
            }

            _rootRegKey = RegistryHelper.GetDeepestKey(Registry.CurrentUser, string.Format(AowRegPathTemplate, _gameName), false);

            if (_rootRegKey != null)
            {
                DetectGame();
            }
        }

        #endregion

        #region Public Methods

        public void SetEmailConfig(string attachmentDir, string localEmailAddress, string smtpServer)
        {
            if (_isInstalled && _rootRegKey != null)
            {
                if (!string.IsNullOrEmpty(attachmentDir))
                {
                    RegistryHelper.SetValue(_rootRegKey, EmailPath, AttachmentDirKeyName, attachmentDir);
                }
                if (!string.IsNullOrEmpty(localEmailAddress))
                {
                    RegistryHelper.SetValue(_rootRegKey, EmailPath, LocalEmailKeyName, localEmailAddress);
                }
                if (!string.IsNullOrEmpty(smtpServer))
                {
                    RegistryHelper.SetValue(_rootRegKey, EmailPath, SMTPServerKeyName, smtpServer);
                }
            }
        }

        #endregion

        #region Private Methods

        private void DetectGame()
        {
            string gameRootFolder = null;
            string steamAppsCommon = null;

            _isInstalled = IsGameInstalled(_rootRegKey, _exeFile, out gameRootFolder);

            //Try Steam
            if (!_isInstalled && IsSteamInstalled(out steamAppsCommon))
            {
                _isInstalled = IsGameInSteam(steamAppsCommon, _exeFile, _gameName, _rootRegKey, out gameRootFolder);
            }

            //Try Most Recently Used File path
            if (!_isInstalled)
            {
                _isInstalled = CheckMostRecentlyUsedFile(_rootRegKey, _exeFile, out gameRootFolder);
            }

            if (_isInstalled)
            {
                _root = new DirectoryInfo(gameRootFolder);

                string emailInFolder = Path.Combine(gameRootFolder, EmailInFolder);
                string emailOutFolder = Path.Combine(gameRootFolder, EmailOutFolder);
                string saveFolder = Path.Combine(gameRootFolder, SaveFolder);

                _emailIn = Directory.Exists(emailInFolder) ? new DirectoryInfo(emailInFolder) : _root;
                _emailOut = Directory.Exists(emailOutFolder) ? new DirectoryInfo(emailOutFolder) : _root;
                _save = Directory.Exists(saveFolder) ? new DirectoryInfo(saveFolder) : _emailIn;

                _writeAccess = WritePermission(_emailIn) && WritePermission(_save);

                if (!_writeAccess && Environment.OSVersion.Version.Major >= 6) //Vista or later
                {
                    //Windows User Account Control (UAC) is probably on
                    //See: C:\Users\*UserName*\AppData\Local\VirtualStore\Program Files (x86)\Age of Wonders\EmailIn
                    FileVirtualizationHelper.Enable();
                    _writeAccess = WritePermission(_emailIn) && WritePermission(_save);
                }
            }
        }

        private bool IsGameInstalled(RegistryKey regRoot, string exeFile, out string gameFolderOut)
        {
            bool returnVal = false;
            gameFolderOut = null;

            if (regRoot != null)
            {
                string root = RegistryHelper.GetValue(regRoot, GeneralPath, RootDirKeyName);

                if (!string.IsNullOrEmpty(root) && Directory.Exists(root))
                {
                    if (File.Exists(Path.Combine(root, exeFile)))
                    {
                        gameFolderOut = root;
                        returnVal = true;
                    }                    
                }
            }

            return returnVal;
        }

        private bool IsSteamInstalled(out string steamAppsCommonOut)
        {
            string steamPath = RegistryHelper.GetValue(Registry.CurrentUser, SteamRegPath, SteamDirKeyName);            
            string steamAppsCommon = null;
            bool returnVal = false;
            steamAppsCommonOut = null;

            if (!string.IsNullOrEmpty(steamPath))
            {
                steamAppsCommon = string.Format(SteamAppsPathTemplate, steamPath.Replace('/', '\\'));
                if (Directory.Exists(steamAppsCommon))
                {                    
                    steamAppsCommonOut = steamAppsCommon;
                    returnVal = true;
                }
            }

            return returnVal;
        }

        private bool IsGameInSteam(string steamDir, string exe, string gameName, RegistryKey root, out string gameFolderOut)
        {
            bool returnVal = false;
            gameFolderOut = null;

            if (root != null)
            {
                string steamGameFolder = Path.Combine(steamDir, gameName);
                if (Directory.Exists(steamGameFolder))
                {
                    if (File.Exists(Path.Combine(steamGameFolder, exe)))
                    {
                        RegistryHelper.SetValue(root, GeneralPath, RootDirKeyName, steamGameFolder);
                        gameFolderOut = steamGameFolder;
                        returnVal = true;
                    }
                }
            }

            return returnVal;
        }

        private bool CheckMostRecentlyUsedFile(RegistryKey regRoot, string exeFile, out string gameFolderOut)
        {
            bool returnVal = false;
            gameFolderOut = null;

            if (regRoot != null)
            {
                string mostRecentlyUsedFile = RegistryHelper.GetValue(regRoot, GeneralPath, MostRecentlyUsedFileKeyName);
                if (!string.IsNullOrEmpty(mostRecentlyUsedFile))
                {
                    string mostRecentlyUsedFileFolder = Path.GetDirectoryName(mostRecentlyUsedFile);
                    if (!string.IsNullOrEmpty(mostRecentlyUsedFileFolder) && Directory.Exists(mostRecentlyUsedFileFolder))
                    {
                        string root = Directory.GetParent(mostRecentlyUsedFileFolder).FullName;
                        if (File.Exists(Path.Combine(root, exeFile)))
                        {
                            RegistryHelper.SetValue(regRoot, GeneralPath, RootDirKeyName, root);
                            gameFolderOut = root;
                            returnVal = true;
                        }
                    }
                }
            }

            return returnVal;
        }

        private bool WritePermission(DirectoryInfo folder)
        {
            bool returnVal = false;
            try
            {
                string testFile = Path.Combine(folder.FullName, string.Format(DummyTestFileTemplate, Guid.NewGuid().ToString()));
                File.WriteAllBytes(testFile, new byte[] { 1, 2, 3 });
                returnVal = true;
                File.Delete(testFile);
            }
            catch
            { }

            return returnVal;
        }

        #endregion
    }
}
