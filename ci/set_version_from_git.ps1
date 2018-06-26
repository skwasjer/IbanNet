$ciPath = (Split-Path $MyInvocation.MyCommand.Path) + "\"

# Discover version(s)
$versions = & ($ciPath + "discover_version.ps1")

if ($versions)
{
    # Build new version string
    $newVersion = "$($versions.InfoVersion.Major).$($versions.InfoVersion.Minor).$($versions.InfoVersion.Patch)"
	$assembly_version = "$newVersion.0"
	$fullVersion = "$newVersion.$env:APPVEYOR_BUILD_NUMBER"
    $semVer1 = $fullVersion

    if ($versions.InfoVersion.Suffix)
    {
        $semVer1 += "-" + $versions.InfoVersion.Suffix
    }

	$informational_version = $semVer1

    Write-Host "Build v$fullVersion will be building package version v$semVer1"

    Try
    {
        Set-AppveyorBuildVariable "package_version" $semVer1		
        Set-AppveyorBuildVariable "assembly_version" $assembly_version
        Set-AppveyorBuildVariable "file_version" $fullVersion
        Set-AppveyorBuildVariable "informational_version" $semVer1
        Update-AppveyorBuild -Version $semVer1
    }
    Catch
    {
        $host.SetShouldExit(2)
    }
}
else
{
    $host.SetShouldExit(1)
}
