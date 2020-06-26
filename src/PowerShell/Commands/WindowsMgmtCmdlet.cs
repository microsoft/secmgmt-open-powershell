﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Online.SecMgmt.PowerShell.Commands
{
    using System;
    using Microsoft.Online.SecMgmt.PowerShell.Exceptions;

    /// <summary>
    /// Base for cmdlets that will only work on devices running Windows.
    /// </summary>
    public abstract class WindowsMgmtCmdlet : MgmtPSCmdlet
    {
        /// <summary>
        /// Operations that happen before the cmdlet is invoked.
        /// </summary>
        protected override void BeginProcessing()
        {
            if (Environment.OSVersion.Platform != PlatformID.Win32NT)
            {
                throw new MgmtPowerShellException("This cmdlet can only be invoked on a device running Windows");
            }
        }
    }
}