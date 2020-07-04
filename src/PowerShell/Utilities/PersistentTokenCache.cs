// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Online.SecMgmt.PowerShell.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using Identity.Client;
    using Identity.Client.Extensions.Msal;
    using Rest;

    /// <summary>
    /// Implements a token cache that leverages persistent storage.
    /// </summary>
    public class PersistentTokenCache : MgmtTokenCache
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
        /// Gets the data from the cache.
        /// </summary>
        /// <returns>The data from the token cache.</returns>
        public override byte[] GetCacheData()
        {
            return GetMsalCacheHelper().LoadUnencryptedTokenCache();
        }

        /// <summary>
        /// Notification that is triggered after token acquisition.
        /// </summary>
        /// <param name="args">Arguments related to the cache item impacted</param>
        public override void AfterAccessNotification(TokenCacheNotificationArgs args)
        {
            MsalCacheHelper cacheHelper = GetMsalCacheHelper();

            args.AssertNotNull(nameof(args));

            try
            {
                if (args.HasStateChanged)
                {
                    cacheHelper.SaveUnencryptedTokenCache(args.TokenCache.SerializeMsalV3());
                }
            }
            catch (Exception)
            {
                cacheHelper.Clear();
                throw;
            }
        }

        /// <summary>
        /// Notification that is triggered before token acquisition.
        /// </summary>
        /// <param name="args">Arguments related to the cache item impacted</param>
        public override void BeforeAccessNotification(TokenCacheNotificationArgs args)
        {
            MsalCacheHelper cacheStorage = GetMsalCacheHelper();

            args.AssertNotNull(nameof(args));

            try
            {
                args.TokenCache.DeserializeMsalV3(cacheStorage.LoadUnencryptedTokenCache());
            }
            catch (Exception)
            {
                cacheStorage.Clear();
                throw;
            }
        }

        /// <summary>
        /// Registers the token cache with client application.
        /// </summary>
        /// <param name="client">The client application to be used when registering the token cache.</param>
        public override void RegisterCache(IClientApplicationBase client)
        {
            ServiceClientTracing.Information("Registering the persistent token cache.");

            base.RegisterCache(client);
        }

        /// <summary>
        /// Gets an aptly configured instance of the <see cref="MsalCacheHelper" /> class.
        /// </summary>
        /// <returns>An aptly configured instance of the <see cref="MsalCacheStorage" /> class.</returns>
        private MsalCacheHelper GetMsalCacheHelper()
        {
            StorageCreationPropertiesBuilder builder = new StorageCreationPropertiesBuilder(Path.GetFileName(CacheFilePath), Path.GetDirectoryName(CacheFilePath), ClientId);

            builder = builder.WithMacKeyChain(serviceName: "Microsoft.Developer.IdentityService", accountName: "MSALCache");
            builder = builder.WithLinuxKeyring(
                schemaName: "msal.cache",
                collection: "default",
                secretLabel: "MSALCache",
                attribute1: new KeyValuePair<string, string>("MsalClientID", "Microsoft.Developer.IdentityService"),
                attribute2: new KeyValuePair<string, string>("MsalClientVersion", "1.0.0.0"));

            return MsalCacheHelper.CreateAsync(builder.Build(), new TraceSource("Security and Management Open PowerShell")).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}