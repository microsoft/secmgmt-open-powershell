---
content_git_url: https://github.com/microsoft/secmgmt-open-powershell/blob/master/docs/help/Register-SecMgmtDeviceWithManagement.md
external help file: Microsoft.Online.SecMgmt.PowerShell.dll-Help.xml
Module Name: SecMgmt
online version: https://docs.microsoft.com/powershell/module/secmgmt/register-SecMgmtDeviceWithManagement
original_content_git_url: https://github.com/microsoft/secmgmt-open-powershell/blob/master/docs/help/Register-SecMgmtDeviceWithManagement.md
schema: 2.0.0
---

# Register-SecMgmtDeviceWithManagement

## SYNOPSIS
Registers the device, that is invoking this cmdlet, with the MDM service.

## SYNTAX

### RegisterWithAadCredentials (Default)
```powershell
Register-SecMgmtDeviceWithManagement [-UseAzureAdCredentials] [-WhatIf] [-Confirm] [<CommonParameters>]
```

### RegisterWithCredentials
```powershell
Register-SecMgmtDeviceWithManagement -AccessToken <String> -UserPrincipalName <String> [-WhatIf] [-Confirm]
 [<CommonParameters>]
```

### RegisterWithAadDeviceCredentials
```powershell
Register-SecMgmtDeviceWithManagement [-UseAzureAdDeviceCredentials] [-WhatIf] [-Confirm] [<CommonParameters>]
```

## DESCRIPTION
Registers the device, that is invoking this cmdlet, with the MDM service.

## EXAMPLES

### Example 1
```powershell
PS C:\> Register-SecMgmtDeviceWithManagement -UseAzureAdCredentials
```

Registers the device, that is invoking this cmdlet, with the MDM service leveraging the Azure Active Directory credentials the user used to authenticate into Windows.

### Example 2
```powershell
PS C:\> Register-SecMgmtDeviceWithManagement -UseAzureAdDeviceCredentials
```

Registers the device, that is invoking this cmdlet, with the MDM service leveraging the Azure Active Directory device credentials.

### Example 3
```powershell
PS C:\> $accessToken = 'JSON-web-token-here'
PS C:\> $upn = 'user@contoso.onmicrosoft.com'
PS C:\> Register-SecMgmtDeviceWithManagement -AccessToken $accessToken -UserPrincipalName $upn
```

Registers the device, that is invoking this cmdlet, with the MDM service leveraging the specified access information.

## PARAMETERS

### -AccessToken
Access token used by the management service will use to validate the user.

```yaml
Type: String
Parameter Sets: RegisterWithCredentials
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UseAzureAdCredentials
A flag indicating the registration should utilize Azure Active Directory user credentials.

```yaml
Type: SwitchParameter
Parameter Sets: RegisterWithAadCredentials
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UseAzureAdDeviceCredentials
A flag indicating the registration should utilize Azure Active Directory device credentials.

```yaml
Type: SwitchParameter
Parameter Sets: RegisterWithAadDeviceCredentials
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UserPrincipalName
User principal name (UPN) of the user requesting the registration.

```yaml
Type: String
Parameter Sets: RegisterWithCredentials
Aliases: UPN

Required: True
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

### System.String

## NOTES

## RELATED LINKS
