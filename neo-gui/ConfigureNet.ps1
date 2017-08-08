param ([string] $configuration, [string] $targetDirectory)

$configuration = $configuration.Trim();
$targetDirectory = $targetDirectory.Trim();

<# .SYNOPSIS
     Automates the process of configuring the gui to use a specific net. 

.NOTES
     Author     : Darcy Thomas - dthomas@passageways.com
#>

Write-Host "Configuration: $configuration"
Write-Host "Target Directory: $targetDirectory"

$configFilePath = "$($targetDirectory)config.json"
$protocolFilePath = "$($targetDirectory)protocol.json"


if ($configuration -like '*MainNet*') { 
	Copy-Item "$($targetDirectory)protocol.mainnet.json"  "$($targetDirectory)protocol.json" -Force
	Copy-Item "$($targetDirectory)config.mainnet.json" "$($targetDirectory)config.json" -Force
}


elseif ($configuration -like '*TestNet*') { 
	Copy-Item "$($targetDirectory)protocol.testnet.json"  "$($targetDirectory)protocol.json" -Force
	Copy-Item "$($targetDirectory)config.testnet.json" "$($targetDirectory)config.json" -Force
}


