// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Online.SecMgmt.PowerShell.Interop
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// Provides methods to create and modify a GPO directly, without using the Group Policy Object Editor.
    /// </summary>
    [ComImport, Guid("7E37D5E7-263D-45CF-842B-96A95C63E46C"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IGroupPolicyObject2
    {
        /// <summary>
        /// Creates a new GPO in the Active Directory with the specified display name.
        /// </summary>
        /// <param name="domainName">Specifies the Active Directory path of the object to create. If the path specifies a domain controller, the GPO is created on that DC. Otherwise, the system will select a DC on the caller's behalf.</param>
        /// <param name="displayName">Specifies the display name of the object to create.</param>
        /// <param name="flags">Specifies whether or not the registry information should be loaded for the GPO.</param>
        void New([MarshalAs(UnmanagedType.LPWStr)] string domainName, [MarshalAs(UnmanagedType.LPWStr)] string displayName, uint flags);

        /// <summary>
        /// Opens the specified GPO and optionally loads the registry information.
        /// </summary>
        /// <param name="path">Specifies the Active Directory path of the object to open. If the path specifies a domain controller, the GPO is created on that DC. Otherwise, the system will select a DC on the caller's behalf.</param>
        /// <param name="flags">Specifies whether or not the registry information should be loaded for the GPO. This parameter can be one of the following values.</param>
        void OpenDsgpo([MarshalAs(UnmanagedType.LPWStr)] string path, uint flags);

        /// <summary>
        /// Opens the default GPO for the computer and optionally loads the registry information.
        /// </summary>
        /// <param name="flags">Specifies whether or not the registry information should be loaded for the GPO. This parameter can be one of the following values.</param>
        void OpenLocalMachineGpo(uint flags);

        /// <summary>
        /// Opens the default GPO for the specified remote computer and optionally loads the registry information.
        /// </summary>
        /// <param name="computerName">Specifies the name of the computer. The format of the name is \ComputerName.</param>
        /// <param name="flags">Specifies whether or not the registry information should be loaded for the GPO. This parameter can be one of the following values.</param>
        void OpenRemoteMachineGpo([MarshalAs(UnmanagedType.LPWStr)] string computerName, uint flags);

        /// <summary>
        /// Saves the specified registry policy settings to disk and updates the revision number of the GPO
        /// </summary>
        /// <param name="machine">Specifies the registry policy settings to be saved. If this parameter is true, the computer policy settings are saved. Otherwise, the user policy settings are saved.</param>
        /// <param name="add">Specifies whether this is an add or delete operation. If this parameter is false, the last policy setting for the specified extension pGuidExtension is removed. In all other cases, this parameter is true.</param>
        /// <param name="extension">Specifies the GUID or unique name of the snap-in extension that will process policy. If the GPO is to be processed by the snap-in that processes .pol files, you must specify the REGISTRY_EXTENSION_GUID value.</param>
        /// <param name="app">Specifies the GUID that identifies the MMC snap-in used to edit this policy. The snap-in can be a Microsoft snap-in or a third-party snap-in.</param>
        void Save(
            [MarshalAs(UnmanagedType.Bool)] bool machine,
            [MarshalAs(UnmanagedType.Bool)] bool add,
            [MarshalAs(UnmanagedType.LPStruct)] Guid extension,
            [MarshalAs(UnmanagedType.LPStruct)] Guid app);

        /// <summary>
        /// Delete the GPO.
        /// </summary>
        void Delete();

        /// <summary>
        /// Gets the unique GPO name.
        /// </summary>
        /// <param name="name">Pointer to a buffer that receives the GPO name.</param>
        /// <param name="maxLength">Specifies the size, in characters, of the pszName buffer.</param>
        void GetName([MarshalAs(UnmanagedType.LPWStr)] StringBuilder name, int maxLength);

        /// <summary>
        /// Gets the display name for the GPO.
        /// </summary>
        /// <param name="name">Pointer to a buffer that receives the display name.</param>
        /// <param name="maxLength">Specifies the size, in characters, of the pszName buffer.</param>
        void GetDisplayName([MarshalAs(UnmanagedType.LPWStr)] StringBuilder name, int maxLength);

        /// <summary>
        /// Sets the display name for the GPO.
        /// </summary>
        /// <param name="name">Specifies the new display name.</param>
        void SetDisplayName([MarshalAs(UnmanagedType.LPWStr)] string name);

        /// <summary>
        /// Gets the path to the GPO.
        /// </summary>
        /// <param name="path">Pointer to a buffer that receives the path. If the GPO is an Active Directory object, the path is in ADSI name format. If the GPO is a computer object, this parameter receives a file system path.</param>
        /// <param name="maxPath">Specifies the maximum number of characters that can be stored in the pszPath buffer.</param>
        void GetPath([MarshalAs(UnmanagedType.LPWStr)] StringBuilder path, int maxPath);

        /// <summary>
        /// Gets the Active Directory path to the root of the specified GPO section.
        /// </summary>
        /// <param name="section">Specifies the GPO section. This parameter can be one of the following values.</param>
        /// <param name="path">Pointer to a buffer that receives the path, in ADSI format (LDAP://cn=user, ou=users, dc=coname, dc=com).</param>
        /// <param name="maxPath">Specifies the maximum number of characters that can be stored in the pszPath buffer.</param>
        void GetDSPath(uint section, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder path, int maxPath);

        /// <summary>
        /// Gets the file system path to the root of the specified GPO section. The path is in UNC format.
        /// </summary>
        /// <param name="section">Specifies the GPO section. This parameter can be one of the following values.</param>
        /// <param name="path">Pointer to a buffer that receives the path.</param>
        /// <param name="maxPath">Specifies the maximum number of characters that can be stored in the pszPath buffer.</param>
        void GetFileSysPath(uint section, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder path, int maxPath);

        /// <summary>
        /// Gets a handle to the root of the registry key for the specified GPO section.
        /// </summary>
        /// <param name="section">Specifies the GPO section. This parameter can be one of the following values.</param>
        /// <returns>If the method succeeds, the return value is S_OK. Otherwise, the method returns one of the COM error codes defined in the Platform SDK header file WinError.h. If the registry information is not loaded, the method returns E_FAIL.</returns>
        IntPtr GetRegistryKey(uint section);

        /// <summary>
        /// Gets the options for the GPO.
        /// </summary>
        /// <returns>If the method succeeds, the return value is S_OK. Otherwise, the method returns one of the COM error codes defined in the Platform SDK header file WinError.h.</returns>
        uint GetOptions();

        /// <summary>
        /// Sets the options for the GPO.
        /// </summary>
        /// <param name="options">Specifies the new option values. This parameter can be one or more of the following options. For more information, see the following Remarks section.</param>
        /// <param name="mask">Specifies the options to change. This parameter can be one or more of the following options. For more information, see the following Remarks section.</param>
        void SetOptions(uint options, uint mask);

        /// <summary>
        /// Gets the computer name of the remote GPO
        /// </summary>
        /// <param name="name">Pointer to a buffer that receives the computer name.</param>
        /// <param name="maxLength">Specifies the size, in characters, of the pszName buffer.</param>
        void GetMachineName([MarshalAs(UnmanagedType.LPWStr)] StringBuilder name, int maxLength);

        /// <summary>
        /// Gets the property sheet pages associated with the GPO.
        /// </summary>
        /// <param name="pages">Address of the pointer to an array of property sheet pages.</param>
        /// <returns>If the method succeeds, the return value is S_OK. Otherwise, the method returns one of the COM error codes defined in the Platform SDK header file WinError.h.</returns>
        uint GetPropertySheetPages(out IntPtr pages);
    }
}