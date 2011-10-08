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
                    string[] parameters = new string[] { theEmail.Attachments[0].FileName, theEmail.RenderEml() };

                    //Starting new thread so that the main ProcessSMTPRequest thread can return to the game asap
                    System.Threading.Thread saveThread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(SaveEmail));
                    saveThread.Start(parameters);
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

        private static void SaveEmail(object obj)
        {
            try
            {
                string[] parameters = (string[])obj;
                if (parameters != null && parameters.Length.Equals(2))
                {
                    File.WriteAllText(GetEmlFilePath(parameters[0]), parameters[1]);
                    parameters = null;
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                Trace.Flush();
            }
            finally
            {
                obj = null;
            }
        }

        private static string GetEmlFilePath(string fileName)
        {
            return Path.Combine(AppDataHelper.Resend.FullName, string.Format(ResendFileNameTemplate, fileName));
        }

        #endregion
    }
}
