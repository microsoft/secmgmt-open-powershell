// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Online.SecMgmt.PowerShell.Factories
{
    using Graph;

    /// <summary>
    /// Represents a factory that provides initialized clients used to interact with online services.
    /// </summary>
    public interface IClientFactory
    {
        /// <summary>
        /// Creates a new instance of the Microsoft Graph service client.
        /// </summary>
        /// <returns>An instance of the <see cref="GraphServiceClient"/> class.</returns>
        IGraphServiceClient CreateGraphServiceClient();
    }
}