// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Online.SecMgmt.PowerShell.Models.Authentication
{
    using System;
    using System.Collections.Generic;
    using Identity.Client;

    /// <summary>
    /// Represents the results of one token acquisition operation.
    /// </summary>
    public class AuthResult : AuthenticationResult
    {
        /// <summary>
        /// Intializes a new instance of the <see cref="AuthResult" /> class.
        /// </summary>
        public AuthResult(string accessToken, bool isExtendedLifeTimeToken, string uniqueId, DateTimeOffset expiresOn, DateTimeOffset extendedExpiresOn, string tenantId, IAccount account, string idToken, IEnumerable<string> scopes, Guid correlationId)
            : base(accessToken, isExtendedLifeTimeToken, uniqueId, expiresOn, extendedExpiresOn, tenantId, account, idToken, scopes, correlationId, null)
        {
        }

        /// <summary>
        /// Gets or sets the refresh token.
        /// </summary>
        public string RefreshToken { get; set; }
    }
}