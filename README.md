# Security and Management Open PowerShell Module

[![Build Status](https://dev.azure.com/isaiahwilliams/public/_apis/build/status/secmgmt-open-powershell?branchName=master)](https://dev.azure.com/isaiahwilliams/public/_build/latest?definitionId=53&branchName=master)

[![SecMgmt](https://img.shields.io/powershellgallery/v/SecMgmt.svg?style=flat-square&label=SecMgmt)](https://www.powershellgallery.com/packages/SecMgmt/) [![GitHub issues](https://img.shields.io/github/issues/microsoft/secmgmt-open-powershell.svg)](https://github.com/microsoft/secmgmt-open-powershell/issues/) [![GitHub pull-requests](https://img.shields.io/github/issues-pr/microsoft/secmgmt-open-powershell.svg)](https://gitHub.com/microsoft/secmgmt-open-powershell/pull/)

## Requirements

Security and Management Open PowerShell works with PowerShell 5.1 or higher on Windows, or PowerShell Core 6.x and later on
all platforms. If you aren't sure if you have PowerShell, or are on macOS or Linux,
[install the latest version of PowerShell Core](https://docs.microsoft.com/powershell/scripting/install/installing-powershell#powershell-core).

To check your PowerShell version, run the command:

```powershell
$PSVersionTable.PSVersion
```

To run Security and Management Open PowerShell in PowerShell 5.1 on Windows:

1. Update to [Windows PowerShell 5.1](https://docs.microsoft.com/powershell/scripting/install/installing-windows-powershell#upgrading-existing-windows-powershell) if needed. If you're on Windows 10, you already
  have PowerShell 5.1 installed.
2. Install [.NET Framework 4.7.2 or later](https://docs.microsoft.com/dotnet/framework/install).

There are no additional requirements for Security and Management Open PowerShell when using PowerShell Core.

## Install the Security and Management Open PowerShell module

The recommended install method is to only install for the active user:

```powershell
Install-Module -Name SecMgmt -AllowClobber -Scope CurrentUser
```

If you want to install for all users on a system, this requires administrator privileges. From an elevated PowerShell session either
run as administrator or with the `sudo` command on macOS or Linux:

```powershell
Install-Module -Name SecMgmt -AllowClobber -Scope AllUsers
```

By default, the PowerShell gallery isn't configured as a trusted repository for PowerShellGet. The first time you use the PSGallery you see the following prompt:

```output
Untrusted repository

You are installing the modules from an untrusted repository. If you trust this repository, change
its InstallationPolicy value by running the Set-PSRepository cmdlet.

Are you sure you want to install the modules from 'PSGallery'?
[Y] Yes  [A] Yes to All  [N] No  [L] No to All  [S] Suspend  [?] Help (default is "N"):
```

Answer `Yes` or `Yes to All` to continue with the installation.

### Discovering cmdlets

Use the `Get-Command` cmdlet to discover cmdlets within a specific module, or cmdlets that follow a specific search pattern:

```powershell
# List all cmdlets in the SecMgmt module
Get-Command -Module SecMgmt

# List all cmdlets that contain Hybrid
Get-Command -Name '*Hybrid*'

# List all cmdlets that contain Hybrid in the SecMgmt module
Get-Command -Module SecMgmt -Name '*Hybrid*'
```

### Cmdlet help and examples

To view the help content for a cmdlet, use the `Get-Help` cmdlet:

```powershell
# View the basic help content for Initialize-SecMgmtHybirdDeviceEnrollment
Get-Help -Name Initialize-SecMgmtHybirdDeviceEnrollment

# View the examples for Initialize-SecMgmtHybirdDeviceEnrollment
Get-Help -Name Initialize-SecMgmtHybirdDeviceEnrollment -Examples

# View the full help content for Initialize-SecMgmtHybirdDeviceEnrollment
Get-Help -Name Initialize-SecMgmtHybirdDeviceEnrollment -Full

# View the help content for Initialize-SecMgmtHybirdDeviceEnrollment on https://docs.microsoft.com
Get-Help -Name Initialize-SecMgmtHybirdDeviceEnrollment -Online
```
