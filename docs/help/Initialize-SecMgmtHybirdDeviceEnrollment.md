---
content_git_url: https://github.com/microsoft/secmgmt-open-powershell/blob/master/docs/help/Initialize-SecMgmtHybirdDeviceEnrollment.md
external help file: Microsoft.Online.SecMgmt.PowerShell.dll-Help.xml
Module Name: SecMgmt
online version: https://docs.microsoft.com/powershell/module/secmgmt/initialize-secmgmthybirddeviceenrollment
original_content_git_url: https://github.com/microsoft/secmgmt-open-powershell/blob/master/docs/help/Initialize-SecMgmtHybirdDeviceEnrollment.md
schema: 2.0.0
---

# Initialize-SecMgmtHybirdDeviceEnrollment

## SYNOPSIS
Performs the tasks to intialize Hybrid Azure AD join in the current forest to be managed by MDM.

## SYNTAX

```powershell
Initialize-SecMgmtHybirdDeviceEnrollment [-Domain <String>] -GroupPolicyDisplayName <String>
 [-TenantId <String>] [-WhatIf] [-Confirm] [<CommonParameters>]
```

## DESCRIPTION
Performs the tasks to intialize Hybrid Azure AD join in the current forest to be managed by MDM. Through the execution of this cmdlet a group policy and service connection point will be created in the Active Directory forest associated with the Windows device where it is invoked.

## EXAMPLES

### Example 1
```powershell
PS C:\> Initialize-SecMgmtHybirdDeviceEnrollment -GroupPolicyDisplayName 'Device Management'
```

Performs the tasks to intialize Hybrid Azure AD join in the current forest to be managed by MDM. Use this option if you are not utilizing federation for authentication.


### Example 2
```powershell
PS C:\> Initialize-SecMgmtHybirdDeviceEnrollment -Domain 'federate-domain-name.com' -GroupPolicyDisplayName 'Device Management'
```

Performs the tasks to intialize Hybrid Azure AD join in the current forest to be managed by MDM. Use this option if you are utilizing federation for authentication. Note you will need to replace the domain value with your federate domain value (e.g. contoso.com).

## PARAMETERS

### -Domain
Azure AD domain used for device authentication

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -GroupPolicyDisplayName
Display name for the group policy that will be created

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TenantId
Identifier for the Azure Active Directory tenant.

```yaml
Type: String
Parameter Sets: (All)
Aliases: Tenant

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

### System.String

## NOTES

## RELATED LINKS
