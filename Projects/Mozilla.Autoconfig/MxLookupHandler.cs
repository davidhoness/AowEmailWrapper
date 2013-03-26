using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Microsoft.Win32;

namespace Mozilla.Autoconfig
{
    internal class MxLookupHandler
    {
        private const char Dot = '.';

        public MxLookupHandler()
        {
        }
        [DllImport("dnsapi", EntryPoint = "DnsQuery_W", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
        private static extern int DnsQuery([MarshalAs(UnmanagedType.VBByRefStr)]ref string pszName, QueryTypes wType, QueryOptions options, int aipServers, ref IntPtr ppQueryResults, int pReserved);

        [DllImport("dnsapi", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern void DnsRecordListFree(IntPtr pRecordList, int FreeType);

        public static string[] GetMXRecords(string domain)
        {
            IntPtr ptr1 = IntPtr.Zero;
            IntPtr ptr2 = IntPtr.Zero;
            MXRecord recMx;
            if (Environment.OSVersion.Platform != PlatformID.Win32NT)
            {
                throw new NotSupportedException();
            }
            ArrayList list1 = new ArrayList();
            int num1 = MxLookupHandler.DnsQuery(ref domain, QueryTypes.DNS_TYPE_MX, QueryOptions.DNS_QUERY_BYPASS_CACHE, 0, ref ptr1, 0);
            if (num1 != 0)
            {
                throw new Win32Exception(num1);
            }
            for (ptr2 = ptr1; !ptr2.Equals(IntPtr.Zero); ptr2 = recMx.pNext)
            {
                recMx = (MXRecord)Marshal.PtrToStructure(ptr2, typeof(MXRecord));
                if (recMx.wType == 15)
                {
                    string text1 = Marshal.PtrToStringAuto(recMx.pNameExchange);
                    list1.Add(text1);
                }
            }
            MxLookupHandler.DnsRecordListFree(ptr2, 0);
            return (string[])list1.ToArray(typeof(string));
        }

        public static string[] GetMXRecordsTrimDistinct(string domain)
        {
            string[] returnVal = new string[] { };
            try
            {
                string[] mxRecords = GetMXRecords(domain);

                if (mxRecords != null && mxRecords.Length > 0)
                {
                    for (int i = 0; i < mxRecords.Length; i++)
                        mxRecords[i] = MxTrim(mxRecords[i]);

                    returnVal = mxRecords.Distinct<string>().ToArray<string>();
                }
            }
            catch { }

            return returnVal;
        }

        private static string MxTrim(string mxRecord)
        {
            string returnVal = null;

            string[] split = mxRecord.Trim().Split(Dot);
            if (split.Length >= 2)
            {
                int upperBound = split.GetUpperBound(0);
                returnVal = string.Concat(split[upperBound - 1], Dot, split[upperBound]).ToLower();
            }
            else
            {
                returnVal = mxRecord;
            }

            return returnVal;
        }

        private enum QueryOptions
        {
            DNS_QUERY_ACCEPT_TRUNCATED_RESPONSE = 1,
            DNS_QUERY_BYPASS_CACHE = 8,
            DNS_QUERY_DONT_RESET_TTL_VALUES = 0x100000,
            DNS_QUERY_NO_HOSTS_FILE = 0x40,
            DNS_QUERY_NO_LOCAL_NAME = 0x20,
            DNS_QUERY_NO_NETBT = 0x80,
            DNS_QUERY_NO_RECURSION = 4,
            DNS_QUERY_NO_WIRE_QUERY = 0x10,
            DNS_QUERY_RESERVED = -16777216,
            DNS_QUERY_RETURN_MESSAGE = 0x200,
            DNS_QUERY_STANDARD = 0,
            DNS_QUERY_TREAT_AS_FQDN = 0x1000,
            DNS_QUERY_USE_TCP_ONLY = 2,
            DNS_QUERY_WIRE_ONLY = 0x100
        }

        private enum QueryTypes
        {
            DNS_TYPE_MX = 15
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MXRecord
        {
            public IntPtr pNext;
            public string pName;
            public short wType;
            public short wDataLength;
            public int flags;
            public int dwTtl;
            public int dwReserved;
            public IntPtr pNameExchange;
            public short wPreference;
            public short Pad;
        }
    }
}
