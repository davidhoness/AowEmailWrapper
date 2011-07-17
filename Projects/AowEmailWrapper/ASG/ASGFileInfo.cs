using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using AowEmailWrapper.Helpers;
using Lesnikowski.Mail;

namespace AowEmailWrapper.ASG
{
    public enum ASGFileType
    {
        Unknown = 1,
        Aow1,
        Aow2Sm
    }

    public class ASGFileInfo : IDisposable
    {
        #region Private Members

        //Hex: 43 46 53 00 02 78 9C EC (CFS..xœì)
        private byte[] Aow1_Pattern = new byte[] { 67, 70, 83, 0, 2, 120, 156 };

        //Hex: 18 00 00 00 48 4D 00 00 00 00 00 00 (....HM.....)
        private byte[] Aow2_Sm_Pattern = new byte[] { 24, 0, 0, 0, 72, 77, 0, 0, 0, 0, 0 };

        private ASGFileType _fileType = ASGFileType.Unknown;
        private MimeData _theAttachment;

        private const string ASG_REGEX = ".[Aa][Ss][Gg]";

        #endregion

        #region Public Properties

        public string FileName
        {
            get { return _theAttachment.FileName; }
        }

        public ASGFileType FileType
        {
            get { return _fileType; }
        }

        public byte[] DataBytes
        {
            get { return _theAttachment.Data; }
        }

        public int Length
        {
            get { return _theAttachment.Data.Length; }
        }

        #endregion

        #region Constructors

        public ASGFileInfo(MimeData theAttachment)
        {
            _theAttachment = theAttachment;
            DetectFileVersion();
        }

        #endregion

        #region Private Methods

        private void DetectFileVersion()
        {
            if (BytePatternMatch(Aow1_Pattern, _theAttachment.Data))
            {
                _fileType = ASGFileType.Aow1;
            }
            else if (BytePatternMatch(Aow2_Sm_Pattern, _theAttachment.Data))
            {
                _fileType = ASGFileType.Aow2Sm;
            }
            else
            {
                _fileType = ASGFileType.Unknown;
            }
        }

        private bool BytePatternMatch(byte[] pattern, byte[] candidate)
        {
            bool success = false;

            if (candidate.Length >= pattern.Length)
            {
                for (int i = 0; i <= pattern.GetUpperBound(0); i++)
                {
                    success = (candidate[i].Equals(pattern[i]));
                    if (!success) break;
                }
            }

            return success;
        }

        #endregion

        #region Public Methods
       
        public static bool IsAsg(string path)
        {
            return Regex.IsMatch(path, ASG_REGEX);
        }

        //==== Game Bug Compensation ====
        //The games have a bug in the save dialogue screen where an extra '.' in the file name will cause it to be truncated.
        //Example: "Midwinter (Dave, Fred, Rob) Upatch 1.4.asg" > press Save
        //       = "Midwinter (Dave, Fred, Rob) Upatch 1.asg"
        //This can cause the file saved to disk and the file name recorded for the email to differ.
        //This search pattern will compensate though.
        public static string SafeSearchFileName(string fileName)
        {
            string returnVal = string.Empty;

            string fileNameTrim = fileName.Trim();
            if (fileNameTrim.Length > 0)
            {
                int firstDotIndex = fileNameTrim.IndexOf('.');
                returnVal = fileNameTrim.Substring(0, firstDotIndex);
            }

            return returnVal;
        }

        public bool SaveToFolder(string folderPath)
        {
            bool success = false;

            if (Directory.Exists(folderPath))
            {
                _theAttachment.Save(Path.Combine(folderPath, _theAttachment.FileName));
                success = true;
            }

            return success;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            this._theAttachment = null;
            this.Aow1_Pattern = null;
            this.Aow2_Sm_Pattern = null;
        }

        #endregion
    }
}
