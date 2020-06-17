// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Online.SecMgmt.PowerShell.Commands
{
    using System.Management.Automation;
    using System.Collections.ObjectModel;
    using System.DirectoryServices;

    [Cmdlet(VerbsData.Initialize, "SecMgmtHybirdDeviceEnrollment", SupportsShouldProcess = true)]
    [OutputType(typeof(string))]
    public class InitializeSecMgmtHybirdDeviceEnrollment : PSCmdlet
    {
        [Parameter(HelpMessage = "Azure AD domain used for device authentication", Mandatory = true)]
        [ValidateNotNull]
        public string Domain { get; set; }
        
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

                    foreach (var value in deSCP.Properties["keywords"])
                    {
                        deSCP.Properties["keywords"].Remove(value);
                    }

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

                WriteObject("Configuration complete!");
            }
        }
    }
}