﻿#
# Module manifest for module 'SecMgmt'
#
# Generated by: Microsoft Corporation
#
# Generated on: 07/10/2020
#

@{
    # Script module or binary module file associated with this manifest.
    RootModule = 'SecMgmt.psm1'

    # Version number of this module.
    ModuleVersion = '0.1.0'

    # Supported PSEditions
    CompatiblePSEditions = 'Core', 'Desktop'

    # ID used to uniquely identify this module
    GUID = '3e8c2cc1-4be1-43e6-a017-81e59feac6c6'

    # Author of this module
    Author = 'Microsoft Corporation'

    # Company or vendor of this module
    CompanyName = 'Microsoft Corporation'

    # Copyright statement for this module
    Copyright = 'Microsoft Corporation. All rights reserved.'

    # Description of the functionality provided by this module
    Description = 'Security and Management - cmdlets for managing Microsoft 365 resources.'

    # Minimum version of the Windows PowerShell engine required by this module
    PowerShellVersion = '5.1'

    # Name of the Windows PowerShell host required by this module
    # PowerShellHostName = ''

    # Minimum version of the Windows PowerShell host required by this module
    # PowerShellHostVersion = ''

    # Minimum version of Microsoft .NET Framework required by this module. This prerequisite is valid for the PowerShell Desktop edition only.
    DotNetFrameworkVersion = '4.7.2'

    # Minimum version of the common language runtime (CLR) required by this module. This prerequisite is valid for the PowerShell Desktop edition only.
    # CLRVersion = '4.0'

    # Processor architecture (None, X86, Amd64) required by this module
    # ProcessorArchitecture = ''

    # Modules that must be imported into the global environment prior to importing this module
    #RequiredModules = @()

    # Assemblies that must be loaded prior to importing this module
    RequiredAssemblies = '.\Microsoft.Online.SecMgmt.PowerShell.dll',
                         '.\Microsoft.Graph.Beta.dll',
                         '.\Microsoft.Graph.Core.dll',
                         '.\Microsoft.Extensions.Caching.Abstractions.dll',
                         '.\Microsoft.Extensions.Caching.Memory.dll',
                         '.\Microsoft.Extensions.DependencyInjection.Abstractions.dll',
                         '.\Microsoft.Extensions.Options.dll',
                         '.\Microsoft.Extensions.Primitives.dll',
                         '.\Microsoft.Identity.Client.dll', 
                         '.\Microsoft.Identity.Client.Extensions.Msal.dll',
                         '.\Microsoft.IdentityModel.JsonWebTokens.dll',
                         '.\Microsoft.IdentityModel.Logging.dll',
                         '.\Microsoft.IdentityModel.Tokens.dll',
                         '.\Microsoft.Rest.ClientRuntime.dll', 
                         '.\System.Runtime.CompilerServices.Unsafe.dll'

    # Script files (.ps1) that are run in the caller's environment prior to importing this module.
    # ScriptsToProcess = @()

    # Type files (.ps1xml) to be loaded when importing this module
    # TypesToProcess = @()

    # Format files (.ps1xml) to be loaded when importing this module
    FormatsToProcess = 'Microsoft.Online.SecMgmt.PowerShell.format.ps1xml'

    # Modules to import as nested modules of the module specified in RootModule/ModuleToProcess
    NestedModules = @()

    # Functions to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no functions to export.
    FunctionsToExport = @()

    # Cmdlets to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no cmdlets to export.
    CmdletsToExport = 'Connect-SecMgmtAccount',
                      'Disconnect-SecMgmtAccount',
                      'Initialize-SecMgmtHybirdDeviceEnrollment',
                      'Install-SecMgmtInsightsConnector',
                      'New-SecMgmtAccessToken',
                      'Register-SecMgmtDeviceWithManagement',
                      'Resolve-SecMgmtError'

    # Variables to export from this module
    # VariablesToExport = @()

    # Aliases to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no aliases to export.
    AliasesToExport = @()

    # DSC resources to export from this module
    # DscResourcesToExport = @()

    # List of all modules packaged with this module
    # ModuleList = @()

    # List of all files packaged with this module
    # FileList = @()

    # Private data to pass to the module specified in RootModule/ModuleToProcess. This may also contain a PSData hashtable with additional module metadata used by PowerShell.
    PrivateData = @{
        PSData = @{
            # Tags applied to this module. These help with module discovery in online galleries.
            Tags = 'Microsoft365','MicrosoftIntune','Office365'

            # A URL to the license for this module.
            LicenseUri = 'https://raw.githubusercontent.com/microsoft/secmgmt-open-powershell/master/LICENSE'

            # A URL to the main website for this project.
            ProjectUri = 'https://github.com/microsoft/secmgmt-open-powershell'

            # A URL to an icon representing this module.
            # IconUri = ''

            # ReleaseNotes of this module
            ReleaseNotes = ''

            # Prerelease string of this module
            # Prerelease = 'preview'

            # Flag to indicate whether the module requires explicit user acceptance for install/update
            # RequireLicenseAcceptance = $false

            # External dependent modules of this module
            # ExternalModuleDependencies = @()

        } # End of PSData hashtable 
    } # End of PrivateData hashtable

    # HelpInfo URI of this module
    # HelpInfoURI = ''

    # Default prefix for commands exported from this module. Override the default prefix using Import-Module -Prefix.
    # DefaultCommandPrefix = ''
}