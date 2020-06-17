using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Online.SecMgmt.PowerShell.Interop
{
    internal enum GROUP_POLICY_OBJECT_TYPE : uint
    {
        GPOTypeLocal = 0,                       // Default GPO on the local machine
        GPOTypeRemote,                          // GPO on a remote machine
        GPOTypeDS,                              // GPO in the Active Directory
        GPOTypeLocalUser,                       // User-specific GPO on the local machine 
        GPOTypeLocalGroup                       // Group-specific GPO on the local machine 
    }

    /// <summary>
    /// 
    /// </summary>
    internal enum GPO_OPEN : int
    {
        LOAD_REGISTRY = 1,
        READ_ONLY = 2,
    }

    /// <summary>
    /// 
    /// </summary>
    internal enum GPO_SECTION : int
    {
        ROOT = 0,
        USER = 1,
        MACHINE = 2,
    }

    /// <summary>
    /// 
    /// </summary>
    [ComImport, Guid("EA502723-A23D-11d1-A7D3-0000F87571E3"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IGroupPolicyObject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords")]
        void New(
            [MarshalAs(UnmanagedType.LPWStr)] string domainName,
            [MarshalAs(UnmanagedType.LPWStr)] string displayName,
            uint flags);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        void OpenDsgpo(
            [MarshalAs(UnmanagedType.LPWStr)] string path,
            uint flags);

        void OpenLocalMachineGpo(
            uint flags);

        void OpenRemoteMachineGpo(
            [MarshalAs(UnmanagedType.LPWStr)] string computerName,
            uint flags);

        void Save(
            [MarshalAs(UnmanagedType.Bool)] bool machine,
            [MarshalAs(UnmanagedType.Bool)] bool add,
            [MarshalAs(UnmanagedType.LPStruct)] Guid extension,
            [MarshalAs(UnmanagedType.LPStruct)] Guid app);

        void Delete();

        void GetName(
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder name,
            int maxLength);

        void GetDisplayName(
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder name,
            int maxLength);

        void SetDisplayName(
            [MarshalAs(UnmanagedType.LPWStr)] string name);

        void GetPath(
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder path,
            int maxPath);

        void GetDSPath(
            uint section,
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder path,
            int maxPath);

        void GetFileSysPath(
            uint section,
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder path,
            int maxPath);

        IntPtr GetRegistryKey(uint section);

        uint GetOptions();

        void SetOptions(
            uint options,
            uint mask);

        void GetMachineName(
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder name,
            int maxLength);

        uint GetPropertySheetPages(
            out IntPtr pages);
    }
}