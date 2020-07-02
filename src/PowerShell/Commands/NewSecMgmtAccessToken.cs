// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Online.SecMgmt.PowerShell.Commands
{
    using System.Management.Automation;

    [Cmdlet(VerbsCommon.New, "SecMgmtAccessToken")]
    public class NewSecMgmtAccessToken : MgmtAsyncCmdlet
    {
        /// <summary>
        /// The name of the by module parameter set.
        /// </summary>
        private const string ByModuleParameterSet = "ByModule";

        /// <summary>
        /// The message written to the console.
        /// </summary>
        private const string Message = "We have launched a browser for you to login. For the old experience with device code flow, please run 'New-PartnerAccessToken -UseDeviceAuthentication'.";

        /// <summary>
        /// The name of the refresh token parameter set.
        /// </summary>
        private const string RefreshTokenParameterSet = "RefreshToken";

        /// <summary>
        /// The name of the service principal parameter set.
        /// </summary>
        private const string ServicePrincipalParameterSet = "ServicePrincipal";

        /// <summary>
        /// The name of the service principal certificate parameter set.
        /// </summary>
        private const string ServicePrincipalCertificateParameterSet = "ServicePrincipalCertificate";

        /// <summary>
        /// The name of the user parameter set.
        /// </summary>
        private const string UserParameterSet = "User";

    }
}