// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Online.SecMgmt.PowerShell.Interop
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// Provides functions to manage MDM registration.
    /// </summary>
    internal static class MdmRegistration
    {
        /// <summary>
        /// Contains the endpoints and information about the management service.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct ManagementServiceInfo
        {
            /// <summary>
            /// The URI of the MDM service.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string mdmServiceUri;

            /// <summary>
            /// The URI of the authentication service.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string authenticationUri;
        }

        /// <summary>
        /// Discovers the MDM service.
        /// </summary>
        /// <param name="pszUPN">Address of a NULL-terminated Unicode string containing the user principal name (UPN) of the user requesting registration.</param>
        /// <param name="ppMgmtInfo">Address of a MANAGEMENT_SERVICE_INFO structure that contains pointers to the URIs of the management and authentication services.</param>
        /// <returns>If the function succeeds, the return value is ERROR_SUCCESS. If the function fails, the returned value describes the error.</returns>
        [DllImport("mdmregistration.dll")]
        public static extern int DiscoverManagementService([MarshalAs(UnmanagedType.LPWStr)] string pszUPN, out IntPtr ppMgmtInfo);

        /// <summary>
        /// Checks whether the device is registered with an MDM service. If the device is registered, it also returns the user principal name (UPN) of the registered user.
        /// </summary>
        /// <param name="pfIsDeviceRegisteredWithManagement">A flag indicating whether or not the device is registered.</param>
        /// <param name="cchUPN">Contains the maximum length that can be returned through the pszUPN parameter.</param>
        /// <param name="pszUPN">Optional address of a buffer that receives the NULL-terminated Unicode string containing the UPN of the user registered with the management service. If pszUPN is NULL then the BOOL pointed to by the pfIsDeviceRegisteredWithManagement parameter is updated to indicate whether the device is registered and the function returns ERROR_SUCCESS.</param>
        /// <returns>If the function succeeds, the return value is ERROR_SUCCESS and the BOOL pointed to by the pfIsDeviceRegisteredWithManagement parameter contains TRUE or FALSE. If TRUE, the Unicode string pointed to by the pszUPN parameter contains the UPN of the registered user.</returns>
        [DllImport("mdmregistration.dll")]
        public static extern int IsDeviceRegisteredWithManagement(out bool pfIsDeviceRegisteredWithManagement, uint cchUPN, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszUPN);

        /// <summary>
        /// Registers a device with a MDM service, using the Mobile Device Enrollment Protocol.
        /// </summary>
        /// <param name="pszUPN">Address of a NULL-terminated Unicode string containing the user principal name (UPN) of the user requesting the registration.</param>
        /// <param name="ppszMDMServiceUri">Address of a NULL-terminated Unicode string containing the URI of the MDM service.</param>
        /// <param name="ppzsAccessToken">Address of a NULL-terminated Unicode string containing a token acquired from a Secure Token Service which the management service will use to validate the user.</param>
        /// <returns>If the function succeeds, the return value is ERROR_SUCCESS. If the function fails, the returned value describes the error. </returns>
        [DllImport("mdmregistration.dll")]
        public static extern int RegisterDeviceWithManagement(
            [MarshalAs(UnmanagedType.LPWStr)] string pszUPN,
            [MarshalAs(UnmanagedType.LPWStr)] string ppszMDMServiceUri,
            [MarshalAs(UnmanagedType.LPWStr)] string ppzsAccessToken);

        /// <summary>
        /// Registers a device with a MDM service, using Azure Active Directory (AAD) credentials.
        /// </summary>
        /// <param name="hUserToken">The User to impersonate when attempting to get AAD token.</param>
        /// <returns>If the function succeeds, the return value is ERROR_SUCCESS. If the function fails, the returned value describes the error.</returns>
        /// <remarks>The caller of this function must be running as an elevated process.</remarks>
        [DllImport("mdmregistration.dll")]
        public static extern int RegisterDeviceWithManagementUsingAADCredentials(IntPtr hUserToken);

        /// <summary>
        /// Registers a device with a MDM service, using Azure Active Directory (AAD) device credentials.
        /// </summary>
        /// <returns>If the function succeeds, the return value is ERROR_SUCCESS. If the function fails, the returned value describes the error.</returns>
        [DllImport("mdmregistration.dll")]
        public static extern int RegisterDeviceWithManagementUsingAADDeviceCredentials();

        /// <summary>
        /// Unregisters a device with the MDM service.
        /// </summary>
        /// <param name="enrollmentID">The identifier for the enrollment.</param>
        /// <returns>If the function succeeds, the return value is ERROR_SUCCESS. If the function fails, the returned value describes the error.</returns>
        /// <remarks>The caller of this function must be running as an elevated process.</remarks>
        [DllImport("mdmregistration.dll")]
        public static extern int UnregisterDeviceWithManagement([MarshalAs(UnmanagedType.LPWStr)] string enrollmentID);
    }
}