using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

using AowEmailWrapper.ASG;
using AowEmailWrapper.Helpers;
using AowEmailWrapper.ConfigFramework;

namespace AowEmailWrapper.Games
{
    public delegate void AowGameSavedEventHandler(object sender, AowGameSavedEventArgs e);

    public class AowGameManager
    {
        #region String Constants

        private const string FileSearchTemplate = "*{0}*.asg";

        #endregion

        #region Private Members

        private List<AowGame> _games;
        private string _checkEmailFolder;

        #endregion

        #region Constructors

        public AowGameManager()
            : this(AppDataHelper.CheckEmail.FullName)
        { 
        }

        public AowGameManager(string checkEmailFolder)
        {
            _checkEmailFolder = checkEmailFolder;

            _games = new List<AowGame>();

            _games.Add(new AowGame(AowGameType.Aow1));
            _games.Add(new AowGame(AowGameType.Aow2));
            _games.Add(new AowGame(AowGameType.AowSm));
        }

        #endregion

        #region Public Properties

        public AowGameSavedEventHandler OnGameSaved;

        public List<AowGame> Games
        {
            get { return _games; }
            set { _games = value; }
        }

        #endregion

        #region Public Methods

        public bool IsInstalled(AowGameType theGameType)
        {
            bool returnVal = false;

            AowGame theGame = GetGameByType(theGameType);

            if (theGame != null)
            {
                returnVal = theGame.IsInstalled;
            }

            return returnVal;
        }

        public void StoreDownloadFile(ASGFileInfo theAsgFile)
        {
            StoreDownloadFile(theAsgFile, EmailSaveFolder.EmailIn);
        }

        public void StoreDownloadFile(ASGFileInfo theAsgFile, EmailSaveFolder saveFolder)
        {
            AowGame theGame = null;

            switch (theAsgFile.FileType)
            {
                case ASGFileType.Aow1:
                    if (IsInstalled(AowGameType.Aow1))
                    {
                        theGame = GetGameByType(AowGameType.Aow1);
                        theAsgFile.SaveToFolder(GetSaveFolder(theGame, saveFolder).FullName);
                    }
                    break;
                case ASGFileType.Aow2Sm:
                    bool aow2 = IsInstalled(AowGameType.Aow2);
                    bool aowSm = IsInstalled(AowGameType.AowSm);

                    if (aow2 && !aowSm)
                    {
                        theGame = GetGameByType(AowGameType.Aow2);
                        theAsgFile.SaveToFolder(GetSaveFolder(theGame, saveFolder).FullName);
                    }
                    else if (!aow2 && aowSm)
                    {
                        theGame = GetGameByType(AowGameType.AowSm);
                        theAsgFile.SaveToFolder(GetSaveFolder(theGame, saveFolder).FullName);
                    }
                    else if (aow2 && aowSm)
                    {
                        theGame = GetGameByFile(theAsgFile.FileName);

                        if (theGame != null)
                        {
                            theAsgFile.SaveToFolder(GetSaveFolder(theGame, saveFolder).FullName);
                        }
                        else
                        {
                            theAsgFile.SaveToFolder(_checkEmailFolder);
                        }
                    }
                    break;
            }

            if (theGame != null)
            {
                RaiseOnGameSaved(theGame.GameType, theAsgFile.FileName);
            }
            else
            {
                RaiseOnGameSaved(AowGameType.Unknown, theAsgFile.FileName);
            }
        }

        public void SetEmailConfigAll(string attachmentDir, string localEmailAddress, string smtpServer)
        {
            foreach (AowGame game in _games)
            {
                if (game.IsInstalled)
                {
                    game.SetEmailConfig(attachmentDir, localEmailAddress, smtpServer);
                }
            }
        }

        public AowGame GetGameByType(AowGameType theGameType)
        {
            return _games.Find(game => game.GameType.Equals(theGameType));
        }

        public AowGame GetGameByFile(string fileName)
        {
            AowGame returnVal = null;
            string searchPattern = GetSearchPattern(fileName);

            foreach (AowGame game in _games)
            {
                if (game.IsInstalled)
                {
                    int emailInCount = GetAllGameFiles(fileName, game.EmailIn, searchPattern).Length;
                    int emailOutCount = GetAllGameFiles(fileName, game.EmailOut, searchPattern).Length;
                    int saveCount = GetAllGameFiles(fileName, game.Save, searchPattern).Length;

                    if (emailInCount > 0 || emailOutCount > 0 || saveCount > 0)
                    {
                        if (returnVal == null)
                        {
                            returnVal = game;
                        }
                        else
                        {
                            //Two games with the same file have been found
                            returnVal = null;
                            break;
                        }
                    }
                }
            }

            return returnVal;
        }

        public bool CheckWriteAccess()
        {
            bool returnVal = true;

            foreach (AowGame game in _games)
            {
                if (game.IsInstalled)
                {
                    if (!game.WriteAccess)
                    {
                        returnVal = false;
                        break;
                    }
                }
            }

            return returnVal;
        }

        public string GetEmailInFolderList()
        {
            StringBuilder sb = new StringBuilder();

            foreach (AowGame game in _games)
            {
                if (game.IsInstalled && !game.WriteAccess)
                {
                    sb.Append(game.EmailIn.FullName);
                    sb.Append(Environment.NewLine);
                    sb.Append(game.Save.FullName);
                    sb.Append(Environment.NewLine);
                }
            }

            return sb.ToString().Trim();
        }

        public void ArchiveEndedGame(AowGameType theGameType, string fileName, string endedFolderName)
        {
            if (!theGameType.Equals(AowGameType.Unknown))
            {
                AowGame theGame = GetGameByType(theGameType);

                if (theGame != null && theGame.IsInstalled)
                {
                    DirectoryInfo[] theFolders = new DirectoryInfo[] { theGame.EmailIn, theGame.EmailOut, theGame.Save };
                    string searchPattern = GetSearchPattern(fileName);

                    foreach (DirectoryInfo folder in theFolders)
                    {
                        FileInfo[] matchingFiles = GetAllGameFiles(fileName, folder, searchPattern);
                        if (matchingFiles.Length > 0)
                        {
                            string endedFolderPath = Path.Combine(folder.FullName, endedFolderName);

                            if (!Directory.Exists(endedFolderPath))
                            {
                                Directory.CreateDirectory(endedFolderPath);
                            }

                            DirectoryInfo theEndedFolder = new DirectoryInfo(endedFolderPath);

                            foreach (FileInfo file in matchingFiles)
                            {
                                string newFileName = Path.Combine(theEndedFolder.FullName, file.Name);
                                try
                                {
                                    if (File.Exists(newFileName))
                                    {
                                        File.Delete(newFileName);
                                    }
                                    //This is in a Try Catch incase it tries to move a non virtualized file in virtualization mode (UAC on)
                                    File.Move(file.FullName, newFileName);
                                }
                                catch
                                { }
                            }
                        }
                    }
                }
            }
            else
            {
                //If it's an unknown game just delete
                string filePath = Path.Combine(_checkEmailFolder, fileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }

        #endregion

        #region Private Methods

        private string GetSearchPattern(string fileName)
        {
            return string.Format(FileSearchTemplate, ASGFileInfo.SafeSearchFileName(fileName));
        }

        private FileInfo[] GetAllGameFiles(string fileName, DirectoryInfo folder)
        {
            return GetAllGameFiles(fileName, folder, GetSearchPattern(fileName));
        }

        private FileInfo[] GetAllGameFiles(string fileName, DirectoryInfo folder, string searchPattern)
        {
            return folder.GetFiles(searchPattern);
        }

        private void RaiseOnGameSaved(AowGameType type, string fileName)
        {
            if (OnGameSaved != null)
            {
                OnGameSaved(this, new AowGameSavedEventArgs(type, fileName));
            }
        }

        private DirectoryInfo GetSaveFolder(AowGame theGame, EmailSaveFolder saveFolder)
        {
            DirectoryInfo returnVal = theGame.EmailIn;

            if (saveFolder.Equals(EmailSaveFolder.Save))
            {
                returnVal = theGame.Save;
            }

            return returnVal;
        }

        #endregion
    }
}