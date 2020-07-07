// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Online.SecMgmt.PowerShell.Commands
{
    using System;
    using System.Management.Automation;
    using System.Runtime.InteropServices;
    using System.Text;
    using Exceptions;
    using Interop;

    /// <summary>
    /// Cmdlet that registers the device with the management (MDM) service.
    /// </summary>
    [Cmdlet(VerbsLifecycle.Register, "SecMgmtDeviceWithManagement", DefaultParameterSetName = RegisterWithAadCredentialsParameterSet, SupportsShouldProcess = true)]
    [OutputType(typeof(string))]
    public class RegisterSecMgmtDeviceWithManagement : WindowsMgmtCmdlet
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
            if (!ShouldProcess("Registers the devices, invoking this cmdlet, with the management service."))
            {
                return;
            }

            StringBuilder stringBuilder = new StringBuilder();
            int error;
            uint maxBufferSize = 256;

            if (MdmRegistration.IsDeviceRegisteredWithManagement(out bool registered, maxBufferSize, stringBuilder) == 0)
            {
                if (registered)
                {
                    WriteObject($"Device is already registered for management by {stringBuilder}");
                    return;
                }
            }
            else
            {
                throw new MgmtPowerShellException("Failed to determine if the device is registered with management.");
            }

            if (ParameterSetName.Equals(RegisterWithAadCredentialsParameterSet, StringComparison.InvariantCultureIgnoreCase))
            {
                error = MdmRegistration.RegisterDeviceWithManagementUsingAADCredentials(IntPtr.Zero);
            }
            else if (ParameterSetName.Equals(RegisterWithAadDeviceCredentialsParameterSet, StringComparison.InvariantCultureIgnoreCase))
            {
                error = MdmRegistration.RegisterDeviceWithManagementUsingAADDeviceCredentials();
            }
            else
            {
                if (MdmRegistration.DiscoverManagementService(UserPrincipalName, out IntPtr pInfo) == 0)
                {
                    MdmRegistration.ManagementServiceInfo info = (MdmRegistration.ManagementServiceInfo)Marshal.PtrToStructure(pInfo, typeof(MdmRegistration.ManagementServiceInfo));
                    error = MdmRegistration.RegisterDeviceWithManagement(UserPrincipalName, info.mdmServiceUri, AccessToken);
                }
                else
                {
                    throw new MgmtPowerShellException($"Failed to discover the management service for {UserPrincipalName}");
                }
            }

            if (error != 0)
            {
                throw new MgmtPowerShellException($"Device registration with the management service failed with error {error}");
            }

            WriteObject("Device has been registered with the management service.");
        }
    }
}