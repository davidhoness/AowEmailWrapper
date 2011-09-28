using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using AowEmailWrapper.Games;
using AowEmailWrapper.Helpers;
using AowEmailWrapper.Classes;
using AowEmailWrapper.ConfigFramework;
using AowEmailWrapper.ASG;
using Lesnikowski.Mail;
using Lesnikowski.Mail.Headers;

namespace AowEmailWrapper.Helpers
{
    public class ResendHelper
    {
        #region Private Members
        
        private const string ResendFileNameTemplate = "{0}_resend.eml";

        #endregion

        #region Public Methods

        public static void Save(IMail theEmail)
        {
            try
            {
                if (theEmail.Attachments.Count > 0)
                {
                    theEmail.Save(GetEmlFilePath(theEmail.Attachments[0].FileName));
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                Trace.Flush();
            }
        }

        public static IMail Load(string asgFileName)
        {
            IMail returnVal = null;

            try
            {
                returnVal = new MailBuilder().CreateFromEmlFile(GetEmlFilePath(asgFileName));
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                Trace.Flush();
            }

            return returnVal;
        }

        public static void Delete(string asgFileName)
        {
            try
            {
                string filePath = GetEmlFilePath(asgFileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                Trace.Flush();
            }
        }

        public static bool CanResend(string asgFileName)
        { 
            string filePath = GetEmlFilePath(asgFileName);
            return File.Exists(filePath);
        }

        #endregion

        #region Private Methods

        private static string GetEmlFilePath(string fileName)
        {
            return Path.Combine(AppDataHelper.Resend.FullName, string.Format(ResendFileNameTemplate, fileName));
        }

        #endregion
    }
}
