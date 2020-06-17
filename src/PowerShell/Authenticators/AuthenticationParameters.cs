// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Online.SecMgmt.PowerShell.Authenticators
{
    using System.Collections.Generic;
    using Models.Authentication;

    /// <summary>
    /// Represents the parameters used for authentication.
    /// </summary>
    public abstract class AuthenticationParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationParameters" /> class.
        /// </summary>
        protected AuthenticationParameters(MgmtAccount account, MgmtEnvironment environment, IEnumerable<string> scopes)
        {
            account.AssertNotNull(nameof(account));
            environment.AssertNotNull(nameof(environment));
            scopes.AssertNotNull(nameof(scopes));

            Account = account;
            Environment = environment;
            Scopes = scopes;
        }

        /// <summary>
        /// Gets the account information.
        /// </summary>
        public MgmtAccount Account { get; }

        /// <summary>
        /// Gets the Microsoft cloud environment information.
        /// </summary>
        public MgmtEnvironment Environment { get; }

        /// <summary>
        /// Gets the scopes.
        /// </summary>
        public IEnumerable<string> Scopes { get; }
    }
}