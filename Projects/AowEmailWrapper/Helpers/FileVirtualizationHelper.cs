using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AowEmailWrapper.Helpers
{
    public class FileVirtualizationHelper
    {
        #region Win32 API routines

        enum TOKEN_INFORMATION_CLASS
        {
            TokenUser = 1,
            TokenGroups,
            TokenPrivileges,
            TokenOwner,
            TokenPrimaryGroup,
            TokenDefaultDacl,
            TokenSource,
            TokenType,
            TokenImpersonationLevel,
            TokenStatistics,
            TokenRestrictedSids,
            TokenSessionId,
            TokenGroupsAndPrivileges,
            TokenSessionReference,
            TokenSandBoxInert,
            TokenAuditPolicy,
            TokenOrigin,
            MaxTokenInfoClass  // MaxTokenInfoClass should always be the last enum
        }

        const UInt32 MAXIMUM_ALLOWED = 0x2000000;

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern Boolean CloseHandle(IntPtr hSnapshot);

        [DllImport("advapi32", SetLastError = true), System.Security.SuppressUnmanagedCodeSecurityAttribute]
        static extern Boolean OpenProcessToken(IntPtr ProcessHandle, // handle to process
                                            UInt32 DesiredAccess, // desired access to process
                                            ref IntPtr TokenHandle); // handle to open access token

        [DllImport("advapi32.dll", SetLastError = true)]
        static extern Boolean SetTokenInformation(IntPtr TokenHandle, TOKEN_INFORMATION_CLASS TokenInformationClass, ref UInt32 TokenInformation, UInt32 TokenInformationLength);

        #endregion

        #region Public Methods

        public static bool Enable()
        {
            return SetVirtualization(true);
        }

        public static bool Disable()
        {
            return SetVirtualization(false);
        }

        #endregion

        #region Private Methods

        private static bool SetVirtualization(bool DoEnable)
        {
            bool returnVal = true;

            IntPtr Token = (IntPtr)0;
            UInt32 EnableValue = DoEnable ? (UInt32)1 : (UInt32)0;
            UInt32 EnableValueSize = sizeof(UInt32);

            Process thisProcess = Process.GetCurrentProcess();

            if (!OpenProcessToken(thisProcess.Handle, MAXIMUM_ALLOWED, ref Token))
            {
                returnVal = false;
            }
            if (!SetTokenInformation(Token, (TOKEN_INFORMATION_CLASS)24, ref EnableValue, EnableValueSize))
            {
                returnVal = false;
            }
            CloseHandle(Token);
            thisProcess.Dispose();

            return returnVal;
        }

        #endregion
    }
}
