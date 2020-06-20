// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


namespace Microsoft.Online.SecMgmt.PowerShell.Authenticators
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading;
    using System.Threading.Tasks;
    using Identity.Client;
    using Identity.Client.Extensions.Msal;
    using Models.Authentication;
    using System.IO;
    using Microsoft.Rest;

    /// <summary>
    /// Provides a chain of responsibility pattern for authenticators.
    /// </summary>
    internal abstract class DelegatingAuthenticator : IAuthenticator
    {
        /// <summary>
        /// The file name for the token cache.
        /// </summary>
        private const string CacheFileName = "msal.cache";

        /// <summary>
        /// The file path for the token cache file.
        /// </summary>
        private static readonly string CacheFilePath =
            Path.Combine(SharedUtilities.GetUserRootDirectory(), ".IdentityService", CacheFileName);

        /// <summary>
        /// Gets or sets the next authenticator in the chain.
        /// </summary>
        public IAuthenticator NextAuthenticator { get; set; }

        /// <summary>
        /// Apply this authenticator to the given authentication parameters.
        /// </summary>
        /// <param name="parameters">The complex object containing authentication specific information.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// An instance of <see cref="AuthenticationResult" /> that represents the access token generated as result of a successful authenication. 
        /// </returns>
        public abstract Task<AuthenticationResult> AuthenticateAsync(AuthenticationParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Determine if this authenticator can apply to the given authentication parameters.
        /// </summary>
        /// <param name="parameters">The complex object containing authentication specific information.</param>
        /// <returns><c>true</c> if this authenticator can apply; otherwise <c>false</c>.</returns>
        public abstract bool CanAuthenticate(AuthenticationParameters parameters);

        /// <summary>
        /// Gets an aptly configured client.
        /// </summary>
        /// <param name="account">The account information to be used when generating the client.</param>
        /// <param name="environment">The environment where the client is connecting.</param>
        /// <param name="redirectUri">The redirect URI for the client.</param>
        /// <returns>An aptly configured client.</returns>
        public IClientApplicationBase GetClient(MgmtAccount account, MgmtEnvironment environment, string redirectUri = null)
        {
            IClientApplicationBase app;

            if (account.IsPropertySet(MgmtAccountPropertyType.CertificateThumbprint) || account.IsPropertySet(MgmtAccountPropertyType.ServicePrincipalSecret))
            {
                app = CreateConfidentialClient(
                    GetAzureCloudInstance(environment),
                    account.GetProperty(MgmtAccountPropertyType.ApplicationId),
                    account.GetProperty(MgmtAccountPropertyType.ServicePrincipalSecret),
                    GetCertificate(account.GetProperty(MgmtAccountPropertyType.CertificateThumbprint)),
                    redirectUri,
                    account.Tenant);
            }
            else
            {
                app = CreatePublicClient(
                    GetAzureCloudInstance(environment),
                    account.GetProperty(MgmtAccountPropertyType.ApplicationId),
                    redirectUri,
                    account.Tenant);
            }

            return app;
        }

        /// <summary>
        /// Determine if this request can be authenticated using the given authenticator, and authenticate if it can.
        /// </summary>
        /// <param name="parameters">The complex object containing authentication specific information.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><c>true</c> if the request can be authenticated; otherwise <c>false</c>.</returns>
        public async Task<AuthenticationResult> TryAuthenticateAsync(AuthenticationParameters parameters, CancellationToken cancellationToken = default)
        {
            if (CanAuthenticate(parameters))
            {
                return await AuthenticateAsync(parameters, cancellationToken).ConfigureAwait(false);
            }

            if (NextAuthenticator != null)
            {
                return await NextAuthenticator.TryAuthenticateAsync(parameters, cancellationToken).ConfigureAwait(false);
            }

            return null;
        }

        /// <summary>
        /// Creates a confidential client used for generating tokens.
        /// </summary>
        /// <param name="cloudInstance">The cloud instance used for authentication.</param>
        /// <param name="clientId">Identifier of the client requesting the token.</param>
        /// <param name="certificate">Certificate used by the client requesting the token.</param>
        /// <param name="clientSecret">Secret of the client requesting the token.</param>
        /// <param name="redirectUri">The redirect URI for the client.</param>
        /// <param name="tenantId">Identifier of the tenant requesting the token.</param>
        /// <returns>An aptly configured confidential client.</returns>
        private static IConfidentialClientApplication CreateConfidentialClient(
            AzureCloudInstance cloudInstance,
            string clientId = null,
            string clientSecret = null,
            X509Certificate2 certificate = null,
            string redirectUri = null,
            string tenantId = null)
        {
            ConfidentialClientApplicationBuilder builder = ConfidentialClientApplicationBuilder.Create(clientId);

            builder = builder.WithAuthority(cloudInstance, tenantId);

            if (!string.IsNullOrEmpty(clientSecret))
            {
                builder = builder.WithClientSecret(clientSecret);
            }

            if (certificate != null)
            {
                builder = builder.WithCertificate(certificate);
            }

            if (!string.IsNullOrEmpty(redirectUri))
            {
                builder = builder.WithRedirectUri(redirectUri);
            }

            if (!string.IsNullOrEmpty(tenantId))
            {
                builder = builder.WithTenantId(tenantId);
            }

            IConfidentialClientApplication client = builder.WithLogging((level, message, pii) =>
            {
                MgmtSession.Instance.DebugMessages.Enqueue($"[MSAL] {level} {message}");
            }).Build();

            GetMsalCacheStorage(clientId).RegisterCache(client.UserTokenCache);

            return client;
        }

        /// <summary>
        /// Creates a public client used for generating tokens.
        /// </summary>
        /// <param name="cloudInstance">The cloud instance used for authentication.</param>
        /// <param name="clientId">Identifier of the client requesting the token.</param>
        /// <param name="redirectUri">The redirect URI for the client.</param>
        /// <param name="tenantId">Identifier of the tenant requesting the token.</param>
        /// <returns>An aptly configured public client.</returns>
        private static IPublicClientApplication CreatePublicClient(
            AzureCloudInstance cloudInstance,
            string clientId = null,
            string redirectUri = null,
            string tenantId = null)
        {
            PublicClientApplicationBuilder builder = PublicClientApplicationBuilder.Create(clientId);

            builder = builder.WithAuthority(cloudInstance, tenantId);

            if (!string.IsNullOrEmpty(redirectUri))
            {
                builder = builder.WithRedirectUri(redirectUri);
            }

            if (!string.IsNullOrEmpty(tenantId))
            {
                builder = builder.WithTenantId(tenantId);
            }

            IPublicClientApplication client = builder.WithLogging((level, message, pii) =>
            {
                MgmtSession.Instance.DebugMessages.Enqueue($"[MSAL] {level} {message}");
            }).Build();

            ServiceClientTracing.Information($"[MSAL] Registering the token cache for client {clientId}");
            GetMsalCacheStorage(clientId).RegisterCache(client.UserTokenCache);

            return client;
        }

        /// <summary>
        /// Gets the Azure cloud instance based an instance of the <see cref="MgmtEnvironment" /> class.
        /// </summary>
        /// <param name="environment">The environment information used to be generate the Azure Cloud instance.</param>
        /// <returns>The Azure cloud instance based an instance of the <see cref="MgmtEnvironment" /> class.</returns>
        private static AzureCloudInstance GetAzureCloudInstance(MgmtEnvironment environment)
        {
            environment.AssertNotNull(nameof(environment));

            if (environment.EnvironmentName == EnvironmentName.AzureChinaCloud)
            {
                return AzureCloudInstance.AzureChina;
            }
            else if (environment.EnvironmentName == EnvironmentName.AzureGermanCloud)
            {
                return AzureCloudInstance.AzureGermany;
            }
            else if (environment.EnvironmentName == EnvironmentName.AzureCloud)
            {
                return AzureCloudInstance.AzurePublic;
            }
            else if (environment.EnvironmentName == EnvironmentName.AzureUSGovernment)
            {
                return AzureCloudInstance.AzureUsGovernment;
            }

            return AzureCloudInstance.None;
        }

        /// <summary>
        /// Gets the specified certificate.
        /// </summary>
        /// <param name="thumbprint">Thumbprint of the certificate to be located.</param>
        /// <returns>An instance of the <see cref="X509Certificate2"/> class that represents the certificate.</returns>
        private static X509Certificate2 GetCertificate(string thumbprint)
        {
            if (string.IsNullOrEmpty(thumbprint))
            {
                return null;
            }

            if (FindCertificateByThumbprint(thumbprint, StoreLocation.CurrentUser, out X509Certificate2 certificate) ||
                    FindCertificateByThumbprint(thumbprint, StoreLocation.LocalMachine, out certificate))
            {
                return certificate;
            }

            return null;
        }

        /// <summary>
        /// Gets an aptly configured instance of the <see cref="MsalCacheStorage" /> class.
        /// </summary>
        /// <param name="clientId">The client identifier for the calling application.</param>
        /// <returns>An aptly configured instance of the <see cref="MsalCacheStorage" /> class.</returns>
        private static MsalCacheHelper GetMsalCacheStorage(string clientId)
        {
            StorageCreationPropertiesBuilder builder = new StorageCreationPropertiesBuilder(Path.GetFileName(CacheFilePath), Path.GetDirectoryName(CacheFilePath), clientId);

            builder = builder.WithMacKeyChain(serviceName: "Microsoft.Developer.IdentityService", accountName: "MSALCache");
            builder = builder.WithLinuxKeyring(
                schemaName: "msal.cache",
                collection: "default",
                secretLabel: "MSALCache",
                attribute1: new KeyValuePair<string, string>("MsalClientID", "Microsoft.Developer.IdentityService"),
                attribute2: new KeyValuePair<string, string>("MsalClientVersion", "1.0.0.0"));

            return MsalCacheHelper.CreateAsync(builder.Build(), new TraceSource("Security and Management Open PowerShell")).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Locates a certificate by thumbprint.
        /// </summary>
        /// <param name="thumbprint">Thumbprint of the certificate to be located.</param>
        /// <param name="storeLocation">The location of the X.509 certifcate store.</param>
        /// <returns><c>true</c> if the certificate was found; otherwise <c>false</c>.</returns>
        private static bool FindCertificateByThumbprint(string thumbprint, StoreLocation storeLocation, out X509Certificate2 certificate)
        {
            X509Store store = null;
            X509Certificate2Collection col;

            thumbprint.AssertNotNull(nameof(thumbprint));

            try
            {
                store = new X509Store(StoreName.My, storeLocation);
                store.Open(OpenFlags.ReadOnly);

                col = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);

                certificate = col.Count == 0 ? null : col[0];

                return col.Count > 0;
            }
            finally
            {
                store?.Close();
            }
        }
    }
}