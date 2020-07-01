// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Online.SecMgmt.PowerShell.Interop
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using Microsoft.Win32;

    [Flags]
    public enum RegOption
    {
        NonVolatile = 0x0,
        Volatile = 0x1,
        CreateLink = 0x2,
        BackupRestore = 0x4,
        OpenLink = 0x8
    }

    [Flags]
    public enum RegSAM
    {
        QueryValue = 0x0001,
        SetValue = 0x0002,
        CreateSubKey = 0x0004,
        EnumerateSubKeys = 0x0008,
        Notify = 0x0010,
        CreateLink = 0x0020,
        WOW64_32Key = 0x0200,
        WOW64_64Key = 0x0100,
        WOW64_Res = 0x0300,
        Read = 0x00020019,
        Write = 0x00020006,
        Execute = 0x00020019,
        AllAccess = 0x000f003f
    }

    public enum RegResult
    {
        CreatedNewKey = 0x00000001,
        OpenedExistingKey = 0x00000002
    }

    public class SecurityAttributes
    {
    }

    /// <summary>
    /// Provides the ability to perform registry operations.
    /// </summary>
    internal sealed class RegistryOperations
    {
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern int RegCloseKey(IntPtr hKey);

        [DllImport("Advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegCreateKeyEx", SetLastError = true)]
        private static extern int RegCreateKeyEx(
            IntPtr hKey,
            string lpSubKey,
            int Reserved,
            string lpClass,
            RegOption dwOptions,
            RegSAM samDesired,
            SecurityAttributes lpSecurityAttributes,
            out IntPtr phkResult,
            out RegResult lpdwDisposition);

        [DllImport("Advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegSetValueEx", SetLastError = true)]
        private static extern int RegSetValueEx(
            IntPtr hKey,
            [MarshalAs(UnmanagedType.LPStr)] string lpValueName,
            int lpReserved,
            RegistryValueKind lpType,
            IntPtr lpData,
            int cbData);

        public static void RegistryCloseKey(ref IntPtr key)
        {
            int error;

            if ((error = RegCloseKey(key)) != 0)
            {
                throw new Win32Exception(error);
            }

            key = IntPtr.Zero;
        }

        public static void RegistryCreateKey(
            IntPtr hKey,
            string lpSubKey,
            int reserved,
            string lpClass,
            RegOption dwOptions,
            RegSAM samDesired,
            SecurityAttributes lpSecurityAttributes,
            out IntPtr phkResult,
            out RegResult lpdwDisposition)
        {
            int error;

            if ((error = RegCreateKeyEx(hKey, lpSubKey, reserved, lpClass, dwOptions, samDesired, lpSecurityAttributes, out phkResult, out lpdwDisposition)) != 0)
            {
                throw new Win32Exception(error);
            }
        }

        public static void RegistrySetValue(
            IntPtr hKey,
            [MarshalAs(UnmanagedType.LPStr)] string lpValueName,
            int reserved,
            RegistryValueKind dwType,
            IntPtr lpData,
            int cbData)
        {
            int error;

            if ((error = RegSetValueEx(hKey, lpValueName, reserved, dwType, lpData, cbData)) != 0)
            {
                throw new Win32Exception(error);
            }
        }
    }
}