using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace WrapperRegistryUninstall
{
    public class RegistryHelper
    {
        public static string GetValue(RegistryKey root, string path, string valueName)
        {
            string returnVal = null;

            RegistryKey regKey = GetDeepestKey(root, path, false);

            if (regKey != null)
            {
                object theValue = regKey.GetValue(valueName);

                if (theValue != null)
                {
                    returnVal = theValue.ToString();                    
                }

                regKey.Close();
            }

            return returnVal;
        }

        public static bool SetValue(RegistryKey root, string path, string valueName, object value)
        {
            bool success = false;

            RegistryKey regKey = GetDeepestKey(root, path, true);

            if (regKey != null)
            {
                regKey.SetValue(valueName, value);
                success = true;
                regKey.Close();
            }

            return success;
        }

        public static bool DeleteValue(RegistryKey root, string path, string valueName)
        {
            bool success = false;

            RegistryKey regKey = GetDeepestKey(root, path, false);

            if (regKey != null && regKey.GetValue(valueName) != null)
            {
                regKey.DeleteValue(valueName);
                success = true;
                regKey.Close();
            }

            return success;
        }

        public static RegistryKey GetDeepestKey(RegistryKey root, string path, bool create)
        {
            string[] split = path.Split(Convert.ToChar("\\"));
            RegistryKey regKey = root;

            foreach (string str in split)
            {
                if (regKey != null)
                {
                    RegistryKey regKeyNew = regKey.OpenSubKey(str, true);
                    if (regKeyNew == null && create)
                    {
                        regKey = regKey.CreateSubKey(str);
                    }
                    else
                    {
                        regKey = regKeyNew;
                    }
                }
                else
                {
                    break;
                }
            }

            return regKey;
        }
    }
}
