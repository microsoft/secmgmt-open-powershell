// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Online.SecMgmt.PowerShell.Models.Authentication
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    /// <summary>
    /// Context information used for the execution of various tasks.
    /// </summary>
    public sealed class MgmtContext : IExtensibleModel
    {
        /// <summary>
        /// Gets or sets the account.
        /// </summary>
        public MgmtAccount Account { get; set; }

        /// <summary>
        /// Gets the environment used for authentication.
        /// </summary>
        public MgmtEnvironment Environment { get; set; }

        /// <summary>
        /// Gets the extended properties.
        /// </summary>
        public IDictionary<string, string> ExtendedProperties { get; } = new ConcurrentDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    }
}