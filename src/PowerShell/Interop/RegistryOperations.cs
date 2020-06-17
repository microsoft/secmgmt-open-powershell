using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Online.SecMgmt.PowerShell.Interop
{
    [FlagsAttribute]
    public enum RegOptions
    {
        Nonvolatile = 0x0,
        Volatile = 0x1,
        CreateLink = 0x2,
        BackupRestore = 0x4,
        OpenLink = 0x8
    }

    public enum RegSam
    {
        None = 0x0,
        Write = 0x00020006
    }

    public enum RegResult
    {
        None = 0x0,
        CreatedNewKey = 0x00000001,
        OpenedExistingKey = 0x00000002
    }

    public class SecurityAttributes
    {
    }

    public sealed class RegistryOperations
    {
        public const int RegistryDword = 4;

        [DllImport("Advapi32.dll",
           CharSet = CharSet.Unicode,
           EntryPoint = "RegCreateKeyEx",
           SetLastError = true)]
        static extern int RegCreateKeyEx(
                      IntPtr hKey,
                      string lpSubKey,
                      int Reserved,
                      string lpClass,
                      RegOptions dwOptions,
                      RegSam samDesired,
                      SecurityAttributes lpSecurityAttributes,
                      out IntPtr phkResult,
                      out RegResult lpdwDisposition);

        public static void RegistryCreateKey(
                      IntPtr key,
                      string subkey,
                      int reserved,
                      string classType,
                      RegOptions options,
                      RegSam samDesired,
                      SecurityAttributes securityAttributes,
                      out IntPtr result,
                      out RegResult disposition)
        {
            int error = 0;

            if ((error = RegCreateKeyEx(key, subkey, reserved, classType, options, samDesired, securityAttributes, out result, out disposition)) != 0)
            {
                throw new Win32Exception(error);
            }
        }

        [DllImport("Advapi32.dll",
           CharSet = CharSet.Unicode,
           EntryPoint = "RegSetValueEx",
           SetLastError = true)]
        static extern int RegSetValueEx(
                      IntPtr hKey,
                      [In] string lpValueName,
                      IntPtr lpReserved,
                      int lpType,
                      byte[] lpData,
                      int cbData);

        public static void RegistrySetValue(
                       IntPtr key,
                       [In] string valueName,
                       IntPtr reserved,
                       int type,
                       byte[] data,
                       int countData)
        {
            int error = 0;
            if ((error = RegSetValueEx(key, valueName, reserved, type, data, countData)) != 0)
            {
                throw new Win32Exception(error);
            }
        }

        [DllImport("Advapi32.dll")]
        static extern int RegCloseKey(IntPtr hKey);

        public static void RegistryCloseKey(ref IntPtr key)
        {
            int error = 0;
            if ((error = RegCloseKey(key)) != 0)
            {
                throw new Win32Exception(error);
            }
            key = System.IntPtr.Zero;
        }
    }
}
