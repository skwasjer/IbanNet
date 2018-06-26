$ciPath = (Split-Path $MyInvocation.MyCommand.Path) + "\"

# Discover version(s)
$versions = & ($ciPath + "discover_version.ps1")

if ($versions)
{
    # Build new version string
    $newVersion = "$($versions.InfoVersion.Major).$($versions.InfoVersion.Minor).$($versions.InfoVersion.Patch)"
    $newAppVeyorVersion = "$newVersion.$env:APPVEYOR_BUILD_NUMBER"
    if ($versions.InfoVersion.Suffix)
    {
        $newVersion += "-" + $versions.InfoVersion.Suffix
        $newAppVeyorVersion += "-" + $versions.InfoVersion.Suffix
    }

    Write-Host "AppVeyor build v$newAppVeyorVersion will be building package version v$newVersion"

    Try
    {
        Set-AppveyorBuildVariable "nuget_package_version" $newVersion
        Update-AppveyorBuild -Version $newAppVeyorVersion
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
