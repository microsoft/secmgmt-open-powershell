---
content_git_url: https://github.com/microsoft/secmgmt-open-powershell/blob/master/docs/help/Unregister-SecMgmtDeviceWithManagement.md
external help file: Microsoft.Online.SecMgmt.PowerShell.dll-Help.xml
Module Name: SecMgmt
online version: https://docs.microsoft.com/powershell/module/secmgmt/unregister-secmgmtdevicewithmanagement
original_content_git_url: https://github.com/microsoft/secmgmt-open-powershell/blob/master/docs/help/Unregister-SecMgmtDeviceWithManagement.md
schema: 2.0.0
---

# Unregister-SecMgmtDeviceWithManagement

## SYNOPSIS
Unregisters the device, that is invoking this cmdlet, with the MDM service.

## SYNTAX

```powershell
Unregister-SecMgmtDeviceWithManagement [-WhatIf] [-Confirm] [<CommonParameters>]
```

## DESCRIPTION
Unregisters the device, that is invoking this cmdlet, with the MDM service.

## EXAMPLES

### Example 1
```powershell
PS C:\> Unregister-SecMgmtDeviceWithManagement
```

Unregisters the device, that is invoking this cmdlet, with the MDM service.

## PARAMETERS

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
