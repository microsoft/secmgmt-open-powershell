// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Online.SecMgmt.PowerShell.Authenticators
{
    using System.Collections.Generic;
    using Extensions;
    using Models.Authentication;

    /// <summary>
    /// Represents the parameters used for authenticating using app only authentication.
    /// </summary>
    public class ServicePrincipalParameters : AuthenticationParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServicePrincipalParameters" /> class.
        /// </summary>
        public ServicePrincipalParameters(MgmtAccount account, MgmtEnvironment environment, IEnumerable<string> scopes)
            : base(account, environment, scopes)
        {
        }

        /// <summary>
        /// Gets the certificate thumbprint.
        /// </summary>
        public string CertificateThumbprint => Account.GetProperty(MgmtAccountPropertyType.CertificateThumbprint);

        /// <summary>
        /// Gets the application secret.
        /// </summary>
        public string Secret => Account.GetProperty(MgmtAccountPropertyType.ServicePrincipalSecret);
    }
}