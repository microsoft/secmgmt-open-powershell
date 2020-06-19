// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Online.SecMgmt.PowerShell.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Implementation of a custom assembly resolver.
    /// </summary>
    public static class CustomAssemblyResolver
    {
        /// <summary>
        /// Contains the assemblies from the preload directory.
        /// </summary>
        private static readonly IList<string> PreloadAssemblies = new List<string>();

        /// <summary>
        /// Gets or sets the path to the preload assembly folder.
        /// </summary>
        private static string PreloadAssemblyFolder { get; set; }

        /// <summary>
        /// Initializes the custom assembly resolver.
        /// </summary>
        public static void Initialize()
        {
            PreloadAssemblyFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "PreloadAssemblies");

            foreach (string file in Directory.GetFiles(PreloadAssemblyFolder, "*.dll"))
            {
                PreloadAssemblies.Add(Path.GetFileNameWithoutExtension(file));
            }

            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        /// <summary>
        /// Handles the <see cref="AppDomain.AssemblyResolve" />, <see cref="AppDomain.ResourceResolve" />, or <see cref="AppDomain.AssemblyResolve" /> events for app domain.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The arguments for the resolve event.</param>
        /// <returns></returns>
        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            try
            {
                AssemblyName assembly = new AssemblyName(args.Name);

                if (PreloadAssemblies.Contains(assembly.Name, StringComparer.InvariantCultureIgnoreCase))
                {
                    return Assembly.LoadFrom(Path.Combine(PreloadAssemblyFolder, $"{assembly.Name}.dll"));
                }
            }
            catch
            { }

            return null;
        }
    }
}