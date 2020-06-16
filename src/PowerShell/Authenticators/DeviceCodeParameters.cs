// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Online.SecMgmt.PowerShell.Authenticators
{
    using System.Collections.Generic;
    using Models.Authentication;

    /// <summary>
    /// Represents the parameters used for authenticating using the device code flow.
    /// </summary>
    public class DeviceCodeParameters : AuthenticationParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceCodeParameters" /> class.
        /// </summary>
        public DeviceCodeParameters(MgmtAccount account, MgmtEnvironment environment, IEnumerable<string> scopes)
            : base(account, environment, scopes)
        {
        }
    }
}