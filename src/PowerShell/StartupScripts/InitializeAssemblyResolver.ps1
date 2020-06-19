if ($PSEdition -eq 'Desktop') {
	try {
	    [Microsoft.Online.SecMgmt.PowerShell.Utilities.CustomAssemblyResolver]::Initialize()
	} catch {}
}