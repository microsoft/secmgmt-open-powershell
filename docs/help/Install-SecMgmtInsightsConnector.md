---
content_git_url: https://github.com/microsoft/secmgmt-open-powershell/blob/master/docs/help/Install-SecMgmtInsightsConnector.md
external help file: Microsoft.Online.SecMgmt.PowerShell.dll-Help.xml
Module Name: SecMgmt
online version: https://docs.microsoft.com/powershell/module/secmgmt/install-secmgmtinsightsconnector
original_content_git_url: https://github.com/microsoft/secmgmt-open-powershell/blob/master/docs/help/Install-SecMgmtInsightsConnector.md
schema: 2.0.0
---

# Install-SecMgmtInsightsConnector

## SYNOPSIS
Installs the latest version of the Security and Management Insights Power BI connector 

## SYNTAX

### CreateApp (Default)
```powershell
Install-SecMgmtInsightsConnector -ApplicationDisplayName <String> [-ConfigurePreconsent] [-WhatIf] [-Confirm]
 [<CommonParameters>]
```

### UseExisting
```powershell
Install-SecMgmtInsightsConnector -ApplicationId <String> [-ConfigurePreconsent] [-WhatIf] [-Confirm]
 [<CommonParameters>]
```

## DESCRIPTION
Installs the latest version of the Security and Management Insights Power BI connector.

## EXAMPLES

### Example 1
```powershell
PS C:\> Install-SecMgmtInsightsConnector -ApplicationDisplayName 'Security and Management Insights'
```

Creates a new Azure Active Directory application, for use with the Security and Management Insights Power BI connector. Then the latest version of the connector is downloaded, configured, and installed on the local device.

### Example 2
```powershell
PS C:\> Install-SecMgmtInsightsConnector -ApplicationId 'xxxx-xxxx-xxxx-xxxx'
```

Donwloads the latest version of the Security and Management Insights Power BI connector. Then it is configured using the specified application identifer and installed on the local device.

## PARAMETERS

### -ApplicationDisplayName
Display name for the Azure Active Directory application that will be created.

```yaml
Type: String
Parameter Sets: CreateApp
Aliases: AppDisplayName

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ApplicationId
Identifier for the Azure Active Directory application configured for the SecMgmtInsights connector.

```yaml
Type: String
Parameter Sets: UseExisting
Aliases: AppId

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ConfigurePreconsent
Flag indicating whether or not the Azure Active Directory application should be configured for pre-consent.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

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
