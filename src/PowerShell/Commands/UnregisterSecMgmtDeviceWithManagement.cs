// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Online.SecMgmt.PowerShell.Commands
{
    using System.Management.Automation;
    using System.Text;
    using Exceptions;
    using Interop;

    /// <summary>
    /// Cmdlet that unregisters the device with the management (MDM) service.
    /// </summary>
    [Cmdlet(VerbsLifecycle.Unregister, "SecMgmtDeviceWithManagement", SupportsShouldProcess = true)]
    [OutputType(typeof(string))]
    public class UnregisterSecMgmtDeviceWithManagement : WindowsMgmtCmdlet
    {
        /// <summary>
        /// Performs the execution of the command.
        /// </summary>
        public override void ExecuteCmdlet()
        {
            if (ShouldProcess("Unregisters the devices from the management service."))
            {
                return;
            }

            StringBuilder stringBuilder = new StringBuilder();
            int error;
            uint maxBufferSize = 256;

            if (MdmRegistration.IsDeviceRegisteredWithManagement(out bool registered, maxBufferSize, stringBuilder) == 0)
            {
                if (!registered)
                {
                    throw new MgmtPowerShellException("Device is not registered with management.");
                }
            }

            error = MdmRegistration.UnregisterDeviceWithManagement(null);

            if (error != 0)
            {
                throw new MgmtPowerShellException($"Failed to unregister the device from the management service with the error {error}");
            }

            WriteObject("Success");
        }
    }
}