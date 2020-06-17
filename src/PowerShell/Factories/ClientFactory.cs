// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Online.SecMgmt.PowerShell.Factories
{
    using System.Net;
    using System.Net.Http;
    using Graph;
    using Network;

    /// <summary>
    /// Factory that provides initialized clients used to interact with online services.
    /// </summary>
    internal class ClientFactory : IClientFactory
    {
        /// <summary>
        /// The service client used to communicate with Microsoft Graph.
        /// </summary>
        private static readonly IGraphServiceClient GraphServiceClient = new GraphServiceClient(
            null,
            new HttpProvider(
                GraphClientFactory.CreatePipeline(
                    GraphClientFactory.CreateDefaultHandlers(null),
                    new ClientTracingHandler
                    {
                        InnerHandler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip }
                    }), false, null));

        /// <summary>
        /// Creates a new instance of the Microsoft Graph service client.
        /// </summary>
        /// <returns>An instance of the <see cref="Graph.GraphServiceClient"/> class.</returns>
        public IGraphServiceClient CreateGraphServiceClient()
        {
            return GraphServiceClient;
        }
    }
}