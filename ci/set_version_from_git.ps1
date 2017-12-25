$ciPath = (Split-Path $MyInvocation.MyCommand.Path) + "\"

# Discover version(s)
$versions = & ($ciPath + "discover_version.ps1")

# Build new version string
$newVersion = "$($versions.Version.Major).$($versions.Version.Minor).$($versions.Version.Patch)"
if ($versions.Version.Suffix)
{
    $newVersion += "-" + $versions.Version.Suffix
}

Write-Host "AppVeyor build $env:APPVEYOR_BUILD_NUMBER will be using version v$newVersion"

Update-AppveyorBuild -Version $newVersion