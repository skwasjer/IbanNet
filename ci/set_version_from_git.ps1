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

    Write-Host "AppVeyor build $env:APPVEYOR_BUILD_NUMBER will be using version v$newVersion"

    Try
    {
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