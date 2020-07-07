---
content_git_url: https://github.com/microsoft/secmgmt-open-powershell/blob/master/docs/help/New-SecMgmtAccessToken.md
external help file: Microsoft.Online.SecMgmt.PowerShell.dll-Help.xml
Module Name: SecMgmt
online version: https://docs.microsoft.com/powershell/module/secmgmt/new-secmgmtaccesstoken
original_content_git_url: https://github.com/microsoft/secmgmt-open-powershell/blob/master/docs/help/New-SecMgmtAccessToken.md
schema: 2.0.0
---

# New-SecMgmtAccessToken

## SYNOPSIS
Acquires an access token from Azure Active Directory.

## SYNTAX

### RefreshToken
```powershell
New-SecMgmtAccessToken [-ApplicationId <String>] [-CertificateThumbprint <String>] [-Credential <PSCredential>]
 [-Environment <EnvironmentName>] -RefreshToken <String> -Scopes <String[]> [-ServicePrincipal]
 [-Tenant <String>] [<CommonParameters>]
```

### ServicePrincipal
```powershell
New-SecMgmtAccessToken -ApplicationId <String> -Credential <PSCredential> [-Environment <EnvironmentName>]
 -Scopes <String[]> [-ServicePrincipal] -Tenant <String> [-UseAuthorizationCode] [<CommonParameters>]
```

### ServicePrincipalCertificate
```powershell
New-SecMgmtAccessToken -ApplicationId <String> -CertificateThumbprint <String> [-Environment <EnvironmentName>]
 -Scopes <String[]> [-ServicePrincipal] -Tenant <String> [-UseAuthorizationCode] [<CommonParameters>]
```

### User
```powershell
New-SecMgmtAccessToken -ApplicationId <String> [-Environment <EnvironmentName>] -Scopes <String[]>
 [-Tenant <String>] [-UseAuthorizationCode] [-UseDeviceAuthentication] [<CommonParameters>]
```

## DESCRIPTION
Acquires an access token from Azure Active Directory.

## EXAMPLES

### Example 1
```powershell
PS C:\> $credential = Get-Credential
PS C:\> New-PartnerAccessToken -ApplicationId 'xxxx-xxxx-xxxx-xxxx' -Scopes 'https://graph.microsoft.com/.default' -ServicePrincipal -Credential $credential -Tenant 'xxxx-xxxx-xxxx-xxxx' -UseAuthorizationCode
```

The first command gets the service principal credentials (application identifier and service principal secret), and then stores them in the $credential variable. The second command will request a new access token from Azure Active Directory. When using the UseAuthorizationCode parameter you will be prompted to authentication interactively using the authorization code flow. The redirect URI value will generated dynamically. This generation process will attempt to find a port between 8400 and 8999 that is not in use. Once an available port has been found, the redirect URL value will be constructed (e.g. <http://localhost:8400>). So, it is important that you have configured the redirect URI value for your Azure Active Directory application accordingly.

### Example 2
```powershell
PS C:\> $credential = Get-Credential
PS C:\> $refreshToken = '<refreshToken>'
PS C:\> New-PartnerAccessToken -ApplicationId 'xxxx-xxxx-xxxx-xxxx' -Credential $credential -RefreshToken $refreshToken -Scopes 'https://graph.microsoft.com/.default' -ServicePrincipal -Tenant 'yyyy-yyyy-yyyy-yyyy'
```

The first command gets the service principal credentials (application identifier and service principal secret), and then stores them in the $credential variable. The third command will generate a new access token using the service principal credentials stored in the $credential variable and the refresh token stored in the $refreshToken variable for authentication.

## PARAMETERS

### -ApplicationId
The application identifier to be used during authentication.

```yaml
Type: String
Parameter Sets: RefreshToken
Aliases: ClientId

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

```yaml
Type: String
Parameter Sets: ServicePrincipal, ServicePrincipalCertificate, User
Aliases: ClientId

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CertificateThumbprint
Certificate Hash (Thumbprint)

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
Credentials that represents the service principal.

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
The environment use for authentication.

```yaml
Type: EnvironmentName
Parameter Sets: (All)
Aliases: EnvironmentName
Accepted values: AzureCloud, AzureChinaCloud, AzureGermanCloud, AzurePPE, AzureUSGovernment

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RefreshToken
The refresh token to use during authentication.

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

### -Scopes
Scopes requested to access a protected API.

```yaml
Type: String[]
Parameter Sets: (All)
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
Parameter Sets: RefreshToken, ServicePrincipalCertificate
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
Parameter Sets: RefreshToken, User
Aliases: Domain, TenantId

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

```yaml
Type: String
Parameter Sets: ServicePrincipal, ServicePrincipalCertificate
Aliases: Domain, TenantId

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UseAuthorizationCode
Use the authorization code flow during authentication.

```yaml
Type: SwitchParameter
Parameter Sets: ServicePrincipal, ServicePrincipalCertificate, User
Aliases: AuthCode

Required: False
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
Aliases: DeviceCode, DeviceAuth, Device

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

### Microsoft.Online.SecMgmt.PowerShell.Models.Authentication.AuthResult

## NOTES

## RELATED LINKS
