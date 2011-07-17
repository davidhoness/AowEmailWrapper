using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace AowEmailWrapper.Helpers
{
    public class FileHelper
    {
        #region XML Data

        public static void SaveXmlFile(string theFilePath, System.Object theObject)
        {
            try
            {
                string xml = XmlHelper.Serialize(theObject);
                File.WriteAllText(theFilePath, xml);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                Trace.Flush();
            }
        }

        public static T LoadXmlFile<T>(string theFilePath)
        {
            try
            {
                if (File.Exists(theFilePath))
                {
                    string xml = File.ReadAllText(theFilePath);
                    return (T)XmlHelper.Deserialize<T>(xml);
                }
                else
                {
                    return default(T);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                Trace.Flush();
                return default(T);
            }
        }

        #endregion

        #region Text Data

        public static void SaveTextFile(string theFilePath, string theText, bool encrypt)
        {
            try
            {
                string toSave = (encrypt) ? CryptographyHelper.Encrypt(theText) : theText;
                File.WriteAllText(theFilePath, toSave);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                Trace.Flush();
            }
        }

        public static string LoadTextFile(string theFilePath, bool decrypt)
        {
            try
            {
                if (File.Exists(theFilePath))
                {
                    string theText = File.ReadAllText(theFilePath);
                    return (decrypt) ? CryptographyHelper.Decrypt(theText) : theText;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                Trace.Flush();
                return null;
            }
        }

        public static bool DeleteTextFile(string theFilePath)
        {
            bool success = false;

            try
            {
                if (File.Exists(theFilePath))
                {
                    File.Delete(theFilePath);
                    success = true;
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                Trace.Flush();
            }

            return success;
        }

        #endregion
    }
}
