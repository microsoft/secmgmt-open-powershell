// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Online.SecMgmt.PowerShell.Commands
{
    using System.Management.Automation;

    [Cmdlet(VerbsData.Initialize, "SecMgmtHybirdDeviceEnrollment", SupportsShouldProcess = true)]
    [OutputType(typeof(string))]
    public class InitializeSecMgmtHybirdDeviceEnrollment : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            // Step 1 - Verify that the system is running Windows and has Azure AD Connect installed
            // Step 2 - Make sure that MS Online is installed, if it is not then install it
            // Step 3 - Run the following command Initialize-ADSyncDomainJoinedComputerSync -AdConnectorAccount $aadConnectorAccount -AzureADCredentials $credential 
            // Step 4 - Create a Group Policy using the following script as a guide

            //$gpo = New - GPO - Name 'Device enrollment'
            //$gpo | New - GPLink - target "DC=contoso,DC=com" - LinkEnabled Yes
            //$gpo | Set - GPRegistryValue - key "HKLM\Software\Policies\Microsoft\Windows\CurrentVersion\MDM" - ValueName AutoEnrollMDM - Type DWORD - value 1
            //$gpo | Set - GPRegistryValue - key "HKLM\Software\Policies\Microsoft\Windows\CurrentVersion\MDM" - ValueName UseAADCredentialType - Type DWORD - value 2

            base.ProcessRecord();
        }
    }
}