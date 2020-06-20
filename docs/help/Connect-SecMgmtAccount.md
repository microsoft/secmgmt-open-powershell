---
content_git_url: https://github.com/microsoft/secmgmt-open-powershell/blob/master/docs/help/Connect-SecMgmtAccount.md
external help file: Microsoft.Online.SecMgmt.PowerShell.dll-Help.xml
Module Name: SecMgmt
online version: https://docs.microsoft.com/powershell/module/secmgmt/connect-secmgmtaccount
original_content_git_url: https://github.com/microsoft/secmgmt-open-powershell/blob/master/docs/help/Connect-SecMgmtAccount.md
schema: 2.0.0
---

# Connect-SecMgmtAccount

## SYNOPSIS
Connect to the Microsoft cloud with an authenticated account for use with the cmdlets. 

## SYNTAX

### User (Default)
```powershell
Connect-SecMgmtAccount [-Environment <EnvironmentName>] [-Tenant <String>] [-UseDeviceAuthentication] [-WhatIf]
 [-Confirm] [<CommonParameters>]
```

### AccessToken
```powershell
Connect-SecMgmtAccount -AccessToken <String> [-Environment <EnvironmentName>] [-Tenant <String>] [-WhatIf]
 [-Confirm] [<CommonParameters>]
```

### RefreshToken
```powershell
Connect-SecMgmtAccount -ApplicationId <String> [-CertificateThumbprint <String>] [-Credential <PSCredential>]
 [-Environment <EnvironmentName>] -RefreshToken <String> [-Tenant <String>] [-WhatIf] [-Confirm]
 [<CommonParameters>]
```

### ServicePrincipalCertificate
```powershell
Connect-SecMgmtAccount -ApplicationId <String> -CertificateThumbprint <String> [-Environment <EnvironmentName>]
 [-ServicePrincipal] -Tenant <String> [-WhatIf] [-Confirm] [<CommonParameters>]
```

### ServicePrincipal
```powershell
Connect-SecMgmtAccount -Credential <PSCredential> [-Environment <EnvironmentName>] [-ServicePrincipal]
 -Tenant <String> [-WhatIf] [-Confirm] [<CommonParameters>]
```

## DESCRIPTION
Connect to the Microsoft cloud with an authenticated account for use with the cmdlets. 

## EXAMPLES

### Example 1
```powershell
PS C:\> Connect-SecMgmtAccount
```

This command connect to the Microsoft cloud.

### Example 2
```powershell
PS C:\> $credential = Get-Credential
PS C:\> Connect-SecMgmtAccount -Credential $credential -Tenant 'xxxx-xxxx-xxxx-xxxx' -ServicePrincipal
```

The first command gets the service principal credentials (application identifier and service principal secret), and then stores them in the $credential variable. The second command connects to the Microsoft cloud using the service principal credentials stored in $credential for the specified Tenant. The ServicePrincipal switch parameter indicates that the account authenticates as a service principal.

### Example 3
```powershell
PS C:\> $refreshToken = '<refreshToken>'
PS C:\> Connect-SecMgmtAccount -ApplicationId 'xxxx-xxxx-xxxx-xxxx' -RefreshToken $refreshToken
```

Connects to the Microsoft cloud using a refresh token that was generated using a [native application](https://docs.microsoft.com/azure/active-directory/develop/native-app).

### Example 4
```powershell
PS C:\> $appId = 'xxxx-xxxx-xxxx-xxxx'
PS C:\> $secret =  ConvertTo-SecureString 'app-secret-here' -AsPlainText -Force
PS C:\> $refreshToken = '<refreshToken>'
PC C:\> $tenantId = 'yyyy-yyyy-yyyy-yyyy'
PS C:\>
PS C:\> $credential = New-Object System.Management.Automation.PSCredential($appId, $secret)
PS C:\>
PS C:\> Connect-SecMgmtAccount -ApplicationId $appId -Credential $credential -RefreshToken $refreshToken
```

Connects to the Microsoft cloud using a refresh token that was generated using a [web application](https://docs.microsoft.com/azure/active-directory/develop/web-app).

## PARAMETERS

### -AccessToken
Access token used to connect.

```yaml
Type: String
Parameter Sets: AccessToken
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ApplicationId
Identifier of the application used to connect.

```yaml
Type: String
Parameter Sets: RefreshToken, ServicePrincipalCertificate
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CertificateThumbprint
Certificate thumbprint of a digital public key X.509 certificate.

```yaml
Type: String
Parameter Sets: RefreshToken
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

```yaml
Type: String
Parameter Sets: ServicePrincipalCertificate
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Credential
Provides the application identifier and secret for service principal credentials.

```yaml
Type: PSCredential
Parameter Sets: RefreshToken
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

```yaml
Type: PSCredential
Parameter Sets: ServicePrincipal
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Environment
Environment containing the account.

```yaml
Type: EnvironmentName
Parameter Sets: (All)
Aliases:
Accepted values: AzureCloud, AzureChinaCloud, AzureGermanCloud, AzurePPE, AzureUSGovernment

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RefreshToken
Refresh token used to connect.

```yaml
Type: String
Parameter Sets: RefreshToken
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ServicePrincipal
Indicates that this account authenticates by providing service principal credentials.

```yaml
Type: SwitchParameter
Parameter Sets: ServicePrincipalCertificate
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

```yaml
Type: SwitchParameter
Parameter Sets: ServicePrincipal
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Tenant
Identifier or name for the tenant.

```yaml
Type: String
Parameter Sets: User, AccessToken, RefreshToken
Aliases: Domain, TenantId

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

```yaml
Type: String
Parameter Sets: ServicePrincipalCertificate, ServicePrincipal
Aliases: Domain, TenantId

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UseDeviceAuthentication
Use device code authentication instead of a browser control.

```yaml
Type: SwitchParameter
Parameter Sets: User
Aliases: Device, DeviceAuth, DeviceCode

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: cf

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf
Shows what would happen if the cmdlet runs.
The cmdlet is not run.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### Microsoft.Online.SecMgmt.PowerShell.Models.Authentication.MgmtContext

## NOTES

## RELATED LINKS
