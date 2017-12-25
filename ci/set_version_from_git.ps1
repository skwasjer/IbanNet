$ciPath = (Split-Path $MyInvocation.MyCommand.Path) + "\"

# Discover version(s)
$versions = & ($ciPath + "discover_version.ps1")

if ($versions)
{
    # Build new version string
    $newVersion = "$($versions.Version.Major).$($versions.Version.Minor).$($versions.Version.Patch)"
    $newAppVeyorVersion = "$newVersion.$env:APPVEYOR_BUILD_NUMBER"
    if ($versions.Version.Suffix)
    {
        $newVersion += "-" + $versions.Version.Suffix
        $newAppVeyorVersion += "-" + $versions.Version.Suffix
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
