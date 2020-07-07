---
content_git_url: https://github.com/microsoft/secmgmt-open-powershell/blob/master/docs/help/Resolve-SecMgmtError.md
external help file: Microsoft.Online.SecMgmt.PowerShell.dll-Help.xml
Module Name: SecMgmt
online version: https://docs.microsoft.com/powershell/module/secmgmt/resolve-secmgmterror
original_content_git_url: https://github.com/microsoft/secmgmt-open-powershell/blob/master/docs/help/Resolve-SecMgmtError.md
schema: 2.0.0
---

# Resolve-SecMgmtError

## SYNOPSIS
Display detailed information about PowerShell errors, with extended details for cmdlet errors.

## SYNTAX

### AnyErrorParameterSet
```powershell
Resolve-SecMgmtError [[-Error] <ErrorRecord[]>] [<CommonParameters>]
```

### LastErrorParameterSet
```powershell
Resolve-SecMgmtError [-Last] [<CommonParameters>]
```

## DESCRIPTION
Resolves and displays detailed information about errors in the current PowerShell session, including where the error occurred in script, stack trace, and all inner and aggregate exceptions. 

## EXAMPLES

### Example 1
```powershell
PS C:\> Resolve-PartnerError -Last
```

Get the details of the last error.

### Example 2
```powershell
PS C:\> Resolve-PartnerError
```

Get details of all errors that have occurred in the current session.

### Example 3
```powershell
PS C:\> Resolve-PartnerError $Error[0]
```

Get the details of the specified error.

## PARAMETERS

### -Error
The error records to resolve

```yaml
Type: ErrorRecord[]
Parameter Sets: AnyErrorParameterSet
Aliases:

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Last
The last error

```yaml
Type: SwitchParameter
Parameter Sets: LastErrorParameterSet
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.Management.Automation.ErrorRecord[]

## OUTPUTS

### Microsoft.Online.SecMgmt.PowerShell.Models.Errors.MgmtErrorRecord

### Microsoft.Online.SecMgmt.PowerShell.Models.Errors.MgmtExceptionRecord

## NOTES

## RELATED LINKS
