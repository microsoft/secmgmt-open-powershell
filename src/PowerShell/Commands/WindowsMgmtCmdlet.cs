// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Online.SecMgmt.PowerShell.Commands
{
    using System;
    using System.Management.Automation;
    using Exceptions;
    using Properties;
    using Models.Authentication;

    /// <summary>
    /// Base for cmdlets that will only work on devices running Windows.
    /// </summary>
    public abstract class WindowsMgmtCmdlet : MgmtPSCmdlet
    {
        /// <summary>
        /// Operations that happen before the cmdlet is invoked.
        /// </summary>
        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            if (MgmtSession.Instance.Context == null)
            {
                throw new PSInvalidOperationException(Resources.RunConnectSecMgmtAccount);
            }

            if (Environment.OSVersion.Platform != PlatformID.Win32NT)
            {
                throw new MgmtPowerShellException("This cmdlet can only be invoked on a device running Windows");
            }
        }
    }
}