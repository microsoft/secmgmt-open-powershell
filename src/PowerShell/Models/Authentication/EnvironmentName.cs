// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Online.SecMgmt.PowerShell.Models.Authentication
{
    /// <summary>
    /// Collection of names for the available instances of the Microsoft cloud.
    /// </summary>
    public enum EnvironmentName
    {
        /// <summary>
        /// The global instance of the Microsoft cloud.
        /// </summary>
        AzureCloud,

        /// <summary>
        /// The Chinese sovereign cloud instance of the Microsoft cloud.
        /// </summary>
        AzureChinaCloud,

        /// <summary>
        /// The German sovereign cloud instance of the Microsoft cloud.
        /// </summary>
        AzureGermanCloud,

        /// <summary>
        /// The pre-productions instance of the Microsoft cloud.
        /// </summary>
        AzurePPE,

        /// <summary>
        /// The US Government sovereign cloud instance of the Microsoft cloud.
        /// </summary>
        AzureUSGovernment
    }
}