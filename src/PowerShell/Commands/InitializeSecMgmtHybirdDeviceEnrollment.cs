// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Online.SecMgmt.PowerShell.Commands
{
    using System;
    using System.DirectoryServices;
    using System.Management.Automation;
    using System.Runtime.InteropServices;
    using Interop;
    using Models.Authentication;
    using Win32;

    /// <summary>
    /// Cmdlet that creates the group policy and service connection point required to have domain joined devices automatically enroll into MDM.
    /// </summary>
    [Cmdlet(VerbsData.Initialize, "SecMgmtHybirdDeviceEnrollment", SupportsShouldProcess = true)]
    [OutputType(typeof(string))]
    public class InitializeSecMgmtHybirdDeviceEnrollment : WindowsMgmtCmdlet
    {
        /// <summary>
        /// Gets or sets the Azure Active Directory domain used for device authentication.
        /// </summary>
        [Parameter(HelpMessage = "Azure Active Directory domain used for device authentication", Mandatory = true)]
        [ValidateNotNull]
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the display name for the Group Policy that will be created.
        /// </summary>
        [Parameter(HelpMessage = "Display name for the group policy that will be created", Mandatory = true)]
        [ValidateNotNull]
        public string GroupPolicyDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the tenant identifier.
        /// </summary>
        [Alias("Tenant")]
        [Parameter(HelpMessage = "Identifier for the Azure Active Directory tenant.", Mandatory = false)]
        [ValidateNotNull]
        public string TenantId { get; set; }

        /// <summary>
        /// Performs the execution of the command.
        /// </summary>
        public override void ExecuteCmdlet()
        {
            if (!ShouldProcess("Creates the group policy and  service connection point required to have domain joined devices automatically enroll into MDM."))
            {
                return;
            }

            string tenantId = string.IsNullOrEmpty(TenantId) ? MgmtSession.Instance.Context.Account.Tenant : TenantId;

            using (DirectoryEntry rootDSE = new DirectoryEntry("LDAP://RootDSE"))
            {
                DirectoryEntry deDRC;
                DirectoryEntry deSCP;
                int size = Marshal.SizeOf(typeof(int));

                string azureADId = $"azureADId:{tenantId}";
                string azureADName = $"azureADName:{Domain}";
                string configCN = rootDSE.Properties["configurationNamingContext"][0].ToString();
                string servicesCN = $"CN=Services,{configCN}";
                string drcCN = $"CN=Device Registration Configuration,{servicesCN}";
                string scpCN = $"CN=62a0ff2e-97b9-4513-943f-0d221bd30080,{drcCN}";

                if (DirectoryEntry.Exists($"LDAP://{drcCN}"))
                {
                    WriteDebug($"Device registration configuration container already exists at LDAP://{drcCN}");
                    deDRC = new DirectoryEntry($"LDAP://{drcCN}");
                }
                else
                {
                    WriteDebug($"Creating the device registration configuration container in LDAP://{servicesCN}");
                    DirectoryEntry entry = new DirectoryEntry($"LDAP://{servicesCN}");
                    deDRC = entry.Children.Add("CN=Device Registration Configuration", "container");
                    deDRC.CommitChanges();
                }

                if (DirectoryEntry.Exists($"LDAP://{scpCN}"))
                {
                    deSCP = new DirectoryEntry($"LDAP://{scpCN}");

                    WriteDebug($"Service connection point LDAP://{scpCN} already exists, so clearing the keywords property");
                    deSCP.Properties["keywords"].Clear();

                    WriteDebug($"Updating the keywords propoerty on the service connection point LDAP://{scpCN}");
                    deSCP.Properties["keywords"].Add(azureADName);
                    deSCP.Properties["keywords"].Add(azureADId);
                    deSCP.CommitChanges();
                }
                else
                {
                    WriteDebug($"The service connection point LDAP://{scpCN} does not exists, so it will be created");
                    deSCP = deDRC.Children.Add("CN=62a0ff2e-97b9-4513-943f-0d221bd30080", "serviceConnectionPoint");
                    deSCP.Properties["keywords"].Add(azureADName);
                    deSCP.Properties["keywords"].Add(azureADId);
                    deSCP.CommitChanges();
                }

                IGroupPolicyObject2 groupPolicyObject = new GroupPolicyObject() as IGroupPolicyObject2;

                IntPtr sectionKeyHandle;
                string domainName = $"LDAP://{rootDSE.Properties["defaultNamingContext"].Value}";

                WriteDebug($"Creating {GroupPolicyDisplayName} group policy");

                groupPolicyObject.New(domainName, GroupPolicyDisplayName, 0x1);
                sectionKeyHandle = groupPolicyObject.GetRegistryKey(0x2);

                RegistryOperations.RegistryCreateKey(
                    sectionKeyHandle,
                    @"Software\Policies\Microsoft\Windows\CurrentVersion\MDM",
                    0,
                    null,
                    0,
                    RegSAM.Write,
                    null,
                    out IntPtr key,
                    out RegResult desposition);

                SetRegistryDWordValue(key, "AutoEnrollMDM", 1);
                SetRegistryDWordValue(key, "UseAADCredentialType", 1);

                groupPolicyObject.Save(true, true, new Guid("7909AD9E-09EE-4247-BAB9-7029D5F0A278"), new Guid("D02B1F72-3407-48AE-BA88-E8213C6761F1"));
                groupPolicyObject.Save(true, true, new Guid("35378EAC-683F-11D2-A89A-00C04FBBCFA2"), new Guid("D02B1F72-3407-48AE-BA88-E8213C6761F1"));

                RegistryOperations.RegistryCloseKey(ref key);
                RegistryOperations.RegistryCloseKey(ref sectionKeyHandle);

                WriteObject($"Domain has been prepared and the {GroupPolicyDisplayName} group policy has been created. You will need to link the group policy for the settings to apply.");
            }
        }

        private void SetRegistryDWordValue(IntPtr key, string valueName, int value)
        {
            byte[] data = { (byte)value, (byte)(value >> 8), (byte)(value >> 16), (byte)(value >> 24) };

            RegistryOperations.RegistrySetValue(key, valueName, IntPtr.Zero, RegistryValueKind.DWord, data, 4);
        }
    }
}