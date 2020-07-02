// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Online.SecMgmt.PowerShell.Commands
{
    using System;
    using System.Management.Automation;
    using System.Runtime.InteropServices;
    using Interop;

    /// <summary>
    /// Cmdlet that registers the device with the MDM service.
    /// </summary>
    [Cmdlet(VerbsLifecycle.Register, "SecMgmtDeviceWithMdm", SupportsShouldProcess = true)]
    [OutputType(typeof(string))]
    public class RegisterSecMgmtDeviceWithMdm : WindowsMgmtCmdlet
    {
        public string UserPrincipalName { get; set; }

        public SwitchParameter UseAadCredentials { get; set; }

        /// <summary>
        /// Performs the execution of the command.
        /// </summary>
        public override void ExecuteCmdlet()
        {
            if (!ShouldProcess("Registers the devices, invoking this cmdlet, with the MDM service."))
            {
                return;
            }

            if (UseAadCredentials.IsPresent && UseAadCredentials.ToBool())
            {
                // Function that should be invoked after a device is registered with Azure Active Directory
                MdmRegistration.RegisterDeviceWithManagementUsingAADCredentials(IntPtr.Zero);
            }
            else
            {
                /*
                 * Functions that should be invoked to register the device with MDM only 
                 */

                MdmRegistration.DiscoverManagementService(UserPrincipalName, out IntPtr pInfo);
                MdmRegistration.ManagementServiceInfo info = (MdmRegistration.ManagementServiceInfo)Marshal.PtrToStructure(pInfo, typeof(MdmRegistration.ManagementServiceInfo));

                MdmRegistration.RegisterDeviceWithManagement(UserPrincipalName, info.mdmServiceUri, "accessToken");
            }
        }
    }
}
