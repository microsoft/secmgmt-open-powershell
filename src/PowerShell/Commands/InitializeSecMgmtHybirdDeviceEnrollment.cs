// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Online.SecMgmt.PowerShell.Commands
{
    using System;
    using System.ComponentModel.Design;
    using System.DirectoryServices;
    using System.Management.Automation;
    using System.Text;
    using Microsoft.Online.SecMgmt.PowerShell.Interop;

    [Cmdlet(VerbsData.Initialize, "SecMgmtHybirdDeviceEnrollment", SupportsShouldProcess = true)]
    [OutputType(typeof(string))]
    public class InitializeSecMgmtHybirdDeviceEnrollment : PSCmdlet
    {
        [Parameter(HelpMessage = "Azure AD domain used for device authentication", Mandatory = true)]
        [ValidateNotNull]
        public string Domain { get; set; }

        [Parameter(HelpMessage = "Display name for the group policy that will be created", Mandatory = true)]
        [ValidateNotNull]
        public string GroupPolicyDisplayName { get; set; }

        const uint GPO_OPEN_LOAD_REGISTRY = 0x00000001;    // Load the registry files
        const uint GPO_OPEN_READ_ONLY = 0x00000002;        // Open the GPO as read only

        protected override void ProcessRecord()
        {
            using (DirectoryEntry rootDSE = new DirectoryEntry("LDAP://RootDSE"))
            {
                DirectoryEntry deDRC;
                DirectoryEntry deSCP;
                string azureADId = "azureADId:851f90cd-614e-4523-acc1-cef7aaf00638";
                string azureADName = $"azureADName:{Domain}";
                string configCN = rootDSE.Properties["configurationNamingContext"][0].ToString();
                string servicesCN = $"CN=Services,{configCN}";
                string drcCN = $"CN=Device Registration Configuration,{servicesCN}";
                string scpCN = $"CN=62a0ff2e-97b9-4513-943f-0d221bd30080,{drcCN}";

                if (DirectoryEntry.Exists($"LDAP://{drcCN}"))
                {
                    deDRC = new DirectoryEntry($"LDAP://{drcCN}");
                }
                else
                {
                    DirectoryEntry entry = new DirectoryEntry($"LDAP://{servicesCN}");
                    deDRC = entry.Children.Add("CN=Device Registration Configuration", "container");
                    deDRC.CommitChanges();
                }

                if (DirectoryEntry.Exists($"LDAP://{scpCN}"))
                {
                    deSCP = new DirectoryEntry($"LDAP://{scpCN}");

                    deSCP.Properties["keywords"].Clear();

                    deSCP.Properties["keywords"].Add(azureADName);
                    deSCP.Properties["keywords"].Add(azureADId);
                    deSCP.CommitChanges();
                }
                else
                {
                    deSCP = deDRC.Children.Add("CN=62a0ff2e-97b9-4513-943f-0d221bd30080", "serviceConnectionPoint");
                    deSCP.Properties["keywords"].Add(azureADName);
                    deSCP.Properties["keywords"].Add(azureADId);
                    deSCP.CommitChanges();
                }

                IGroupPolicyObject gpo = new GroupPolicyObject() as IGroupPolicyObject;

                gpo.New("LDAP://DC=ninjacatdevices,DC=com", GroupPolicyDisplayName, 0x1);
                
                IntPtr sectionKeyHandle = gpo.GetRegistryKey(0x2);
                IntPtr key;
                IntPtr reserved = IntPtr.Zero;

                RegistryOperations.RegistryCreateKey(
                   sectionKeyHandle,
                   @"Software\Policies\Microsoft\Windows\CurrentVersion\MDM",
                   0,
                   null,
                   0,
                   RegSam.Write,
                   null,
                   out key,
                   out RegResult desposition);

                byte[] data = { (byte)((int)1),
                                    (byte)((int)1 >> 8),
                                    (byte)((int)1 >> 16),
                                    (byte)((int)1 >> 24) };

                RegistryOperations.RegistrySetValue(key,
                   "AutoEnrollMDM",
                   reserved,
                   RegistryOperations.RegistryDword,
                   data,
                   4);

                byte[] data2 = {
                    (byte)((int)2),
                    (byte)((int)2 >> 8),
                    (byte)((int)2 >> 16),
                    (byte)((int)2 >> 24) };

                RegistryOperations.RegistrySetValue(key,
                    "UseAADCredentialType",
                    reserved,
                    RegistryOperations.RegistryDword,
                    data2,
                    4);

                RegistryOperations.RegistryCloseKey(ref key);

                gpo.Save(true, true, new Guid("35378EAC-683F-11D2-A89A-00C04FBBCFA2"), new Guid("8FC0B734-A0E1-11d1-A7D3-0000F87571E3"));

                RegistryOperations.RegistryCloseKey(ref sectionKeyHandle);

                StringBuilder sb = new StringBuilder(1000);
                gpo.GetPath(sb, 1000);

                string gpoDistinguishedName = sb.ToString();

                WriteObject("Configuration complete!");
            }
        }
    }
}