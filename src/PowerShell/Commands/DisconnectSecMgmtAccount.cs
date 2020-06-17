// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Online.SecMgmt.PowerShell.Commands
{
    using System.Management.Automation;
    using Models.Authentication;

    [Cmdlet(VerbsCommunications.Disconnect, "SecMgmtAccount", SupportsShouldProcess = true)]
    public class DisconnectSecMgmtAccount : MgmtPSCmdlet
    {
        /// <summary>
        /// Executes the operations associated with the cmdlet.
        /// </summary>
        protected override void ProcessRecord()
        {
            MgmtSession.Instance.Context = null;
        }
    }
}