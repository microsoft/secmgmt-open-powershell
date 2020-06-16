// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Online.SecMgmt.PowerShell.Models.Authentication
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    /// <summary>
    /// A record of metadata necessary to manage assets in a specific cloud, including necessary endpoints,
    /// location of service-specific endpoints, and information for bootstrapping authentication
    /// </summary>
    public class MgmtEnvironment
    {
        /// <summary>
        /// Gets or sets the authentication endpoint.
        /// </summary>
        public string ActiveDirectoryAuthority { get; set; }

        /// <summary>
        /// Gets or sets the Azure AD Graph endpoint.
        /// </summary>
        public string AzureAdGraphEndpoint { get; set; }

        /// <summary>
        /// Gets or sets the Azure endpoint.
        /// </summary>
        public string AzureEndpoint { get; set; }

        /// <summary>
        /// Gets or sets the Microsoft Graph endpoint.
        /// </summary>
        public string GraphEndpoint { get; set; }

        /// <summary>
        /// Gets or sets the partner environment name.
        /// </summary>
        public EnvironmentName EnvironmentName { get; set; }

        /// <summary>
        /// Gets the Partner Center endpoint.
        /// </summary>
        public string PartnerCenterEndpoint { get; set; }

        /// <summary>
        /// Gets the defined Microsoft Partner Center environments.
        /// </summary>
        public static IDictionary<EnvironmentName, MgmtEnvironment> PublicEnvironments { get; } = InitializeEnvironments();

        /// <summary>
        /// Initializes a list of known environments.
        /// </summary>
        /// <returns>A dictionary containing the known environments.</returns>
        private static IDictionary<EnvironmentName, MgmtEnvironment> InitializeEnvironments()
        {
            return new ConcurrentDictionary<EnvironmentName, MgmtEnvironment>
            {
                [EnvironmentName.AzureCloud] = new MgmtEnvironment
                {
                    ActiveDirectoryAuthority = EnvironmentConstants.AzureActiveDirectoryEndpoint,
                    AzureAdGraphEndpoint = EnvironmentConstants.AzureAdGraphEndpoint,
                    AzureEndpoint = EnvironmentConstants.AzureEndpoint,
                    GraphEndpoint = EnvironmentConstants.GraphEndpoint,
                    EnvironmentName = EnvironmentName.AzureCloud
                },

                [EnvironmentName.AzureChinaCloud] = new MgmtEnvironment
                {
                    ActiveDirectoryAuthority = EnvironmentConstants.ChinaActiveDirectoryEndpoint,
                    AzureAdGraphEndpoint = EnvironmentConstants.ChinaAzureAdGraphEndpoint,
                    AzureEndpoint = EnvironmentConstants.ChinaAzureEndpoint,
                    GraphEndpoint = EnvironmentConstants.ChinaGraphEndpoint,
                    EnvironmentName = EnvironmentName.AzureChinaCloud
                },

                [EnvironmentName.AzureGermanCloud] = new MgmtEnvironment
                {
                    ActiveDirectoryAuthority = EnvironmentConstants.GermanActiveDirectoryEndpoint,
                    AzureAdGraphEndpoint = EnvironmentConstants.GermanAzureAdGraphEndpoint,
                    AzureEndpoint = EnvironmentConstants.GermanAzureEndpoint,
                    GraphEndpoint = EnvironmentConstants.GermanGraphEndpoint,
                    EnvironmentName = EnvironmentName.AzureGermanCloud
                },

                [EnvironmentName.AzurePPE] = new MgmtEnvironment
                {
                    ActiveDirectoryAuthority = EnvironmentConstants.AzureActiveDirectoryPpeEndpoint,
                    AzureAdGraphEndpoint = EnvironmentConstants.AzureAdGraphPpeEndpoint,
                    AzureEndpoint = EnvironmentConstants.PpeAzureEndpoint,
                    GraphEndpoint = EnvironmentConstants.PpeGraphEndpoint,
                    EnvironmentName = EnvironmentName.AzurePPE
                },

                [EnvironmentName.AzureUSGovernment] = new MgmtEnvironment
                {
                    ActiveDirectoryAuthority = EnvironmentConstants.USGovernmentActiveDirectoryEndpoint,
                    AzureAdGraphEndpoint = EnvironmentConstants.USGovernmentAzureAdGraphEndpoint,
                    AzureEndpoint = EnvironmentConstants.USGovernmentAzureEndpoint,
                    GraphEndpoint = EnvironmentConstants.USGovernmentGraphEndpoint,
                    EnvironmentName = EnvironmentName.AzureUSGovernment
                }
            };
        }
    }
}