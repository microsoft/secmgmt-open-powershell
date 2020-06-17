// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Online.SecMgmt.PowerShell.Authenticators
{
    using System.Collections.Generic;
    using Models.Authentication;

    /// <summary>
    /// Represents the parameters used for authenticating using a refresh token.
    /// </summary>
    public class RefreshTokenParameters : AuthenticationParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RefreshTokenParameters" /> class.
        /// </summary>
        public RefreshTokenParameters(MgmtAccount account, MgmtEnvironment environment, IEnumerable<string> scopes)
            : base(account, environment, scopes)
        {
        }

        /// <summary>
        /// Gets the certificate thumbprint.
        /// </summary>
        public string CertificateThumbprint => Account.GetProperty(MgmtAccountPropertyType.CertificateThumbprint);

        /// <summary>
        /// Gets the refresh token.
        /// </summary>
        public string RefreshToken => Account.GetProperty(MgmtAccountPropertyType.RefreshToken);

        /// <summary>
        /// Gets the application secret.
        /// </summary>
        public string Secret => Account.GetProperty(MgmtAccountPropertyType.ServicePrincipalSecret);
    }
}