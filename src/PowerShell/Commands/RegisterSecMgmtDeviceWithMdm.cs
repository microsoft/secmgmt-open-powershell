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
    [Cmdlet(VerbsLifecycle.Register, "SecMgmtDeviceWithMdm", DefaultParameterSetName = RegisterWithAadCredentialsParameterSet, SupportsShouldProcess = true)]
    [OutputType(typeof(string))]
    public class RegisterSecMgmtDeviceWithMdm : WindowsMgmtCmdlet
    {
        /// <summary>
        /// Name of the register with Azure Active Directory credentials parameter set.
        /// </summary>
        private const string RegisterWithAadCredentialsParameterSet = "RegisterWithAadCredentials";

        /// <summary>
        /// Name of the register with Azure Active Directory credentials parameter set.
        /// </summary>
        private const string RegisterWithAadDeviceCredentialsParameterSet = "RegisterWithAadDeviceCredentials";

        /// <summary>
        /// Name fo the register with credentials parameter set.
        /// </summary>
        private const string RegisterWithCredentialsParameterSet = "RegisterWithCredentials";

        /// <summary>
        /// Gets or sets the access token used by the management service will use to validate the user.
        /// </summary>
        [Parameter(HelpMessage = "Access token used by the management service will use to validate the user.", Mandatory = true, ParameterSetName = RegisterWithCredentialsParameterSet)]
        [ValidateNotNull]
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating the registration should utilize Azure Active Directory user credentials.
        /// </summary>
        [Parameter(HelpMessage = "A flag indicating the registration should utilize Azure Active Directory user credentials.", Mandatory = true, ParameterSetName = RegisterWithAadCredentialsParameterSet)]
        public SwitchParameter UseAzureAdCredentials { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating the registration should utilize Azure Active Directory device credentials.
        /// </summary>
        [Parameter(HelpMessage = "A flag indicating the registration should utilize Azure Active Directory device credentials.", Mandatory = true, ParameterSetName = RegisterWithAadDeviceCredentialsParameterSet)]
        public SwitchParameter UseAzureAdDeviceCredentials { get; set; }

        /// <summary>
        /// Gets or sets the user principal name of the user requesting the registration.
        /// </summary>
        [Alias("UPN")]
        [Parameter(HelpMessage = "User principal name (UPN) of the user requesting the registration.", Mandatory = true, ParameterSetName = RegisterWithCredentialsParameterSet)]
        [ValidateNotNull]
        public string UserPrincipalName { get; set; }

        /// <summary>
        /// Performs the execution of the command.
        /// </summary>
        public override void ExecuteCmdlet()
        {
            if (!ShouldProcess("Registers the devices, invoking this cmdlet, with the MDM service."))
            {
                return;
            }

            if (ParameterSetName.Equals(RegisterWithAadCredentialsParameterSet, StringComparison.InvariantCultureIgnoreCase))
            {
                MdmRegistration.RegisterDeviceWithManagementUsingAADCredentials(IntPtr.Zero);
            }
            else if (ParameterSetName.Equals(RegisterWithAadDeviceCredentialsParameterSet, StringComparison.InvariantCultureIgnoreCase))
            {
                MdmRegistration.RegisterDeviceWithManagementUsingAADDeviceCredentials();
            }
            else
            {
                MdmRegistration.DiscoverManagementService(UserPrincipalName, out IntPtr pInfo);
                MdmRegistration.ManagementServiceInfo info = (MdmRegistration.ManagementServiceInfo)Marshal.PtrToStructure(pInfo, typeof(MdmRegistration.ManagementServiceInfo));

                MdmRegistration.RegisterDeviceWithManagement(UserPrincipalName, info.mdmServiceUri, AccessToken);
            }
        }
    }
}