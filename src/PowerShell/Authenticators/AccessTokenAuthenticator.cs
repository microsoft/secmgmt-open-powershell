﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Online.SecMgmt.PowerShell.Authenticators
{
    using System;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;
    using Exceptions;
    using Identity.Client;
    using IdentityModel.JsonWebTokens;
    using Models.Authentication;
    using Rest;

    /// <summary>
    /// Provides the ability to authenticate using an access token.
    /// </summary>
    internal class AccessTokenAuthenticator : DelegatingAuthenticator
    {
        /// <summary>
        /// Apply this authenticator to the given authentication parameters.
        /// </summary>
        /// <param name="parameters">The complex object containing authentication specific information.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// An instance of <see cref="AuthenticationResult" /> that represents the access token generated as result of a successful authenication. 
        /// </returns>
        public override async Task<AuthenticationResult> AuthenticateAsync(AuthenticationParameters parameters, CancellationToken cancellationToken = default)
        {
            JsonWebToken token;
            string value = parameters.Account.GetProperty(MgmtAccountPropertyType.AccessToken);

            token = new JsonWebToken(value);

            ServiceClientTracing.Information($"[AccessTokenAuthenticator] The specified access token expires at {token.ValidTo}");

            if (DateTimeOffset.UtcNow > token.ValidTo)
            {
                throw new MgmtPowerShellException("The access token has expired. Generate a new one and try again.", MgmtPowerShellErrorCategory.TokenExpired);
            }

            await Task.CompletedTask.ConfigureAwait(false);

            ServiceClientTracing.Information("[AccessTokenAuthenticator] Constructing the authentication result based on the specified access token");

            return new AuthenticationResult(
                value,
                false,
                null,
                token.ValidTo,
                token.ValidTo,
                token.GetClaim("tid").Value,
                GetAccount(token),
                null,
                parameters.Scopes,
                Guid.Empty,
                null);
        }

        /// <summary>
        /// Determine if this authenticator can apply to the given authentication parameters.
        /// </summary>
        /// <param name="parameters">The complex object containing authentication specific information.</param>
        /// <returns><c>true</c> if this authenticator can apply; otherwise <c>false</c>.</returns>
        public override bool CanAuthenticate(AuthenticationParameters parameters)
        {
            return parameters is AccessTokenParameters;
        }

        private IAccount GetAccount(JsonWebToken token)
        {
            token.AssertNotNull(nameof(token));

            token.TryGetClaim("upn", out Claim claim);

            if (claim != null)
            {
                ServiceClientTracing.Information($"[AccessTokenAuthenticator] The UPN claim value is {claim.Value}");
                ServiceClientTracing.Information($"[AccessTokenAuthenticator] Constructing the resource account value based on specified access token");

                return new ResourceAccount(
                    "login.microsoftonline.com",
                    $"{token.GetClaim("oid").Value}.{token.GetClaim("tid")}",
                    token.GetClaim("oid").Value,
                    token.GetClaim("tid").Value,
                    claim.Value);
            }

            ServiceClientTracing.Information("[AccessTokenAuthenticator] The UPN claim is not present in the access token.");
            return null;
        }
    }
}
