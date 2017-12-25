$ciPath = (Split-Path $MyInvocation.MyCommand.Path) + "\"

# Discover version(s)
$versions = & ($ciPath + "discover_version.ps1")

# Build new version string
$newVersion = "$($versions.Version.Major).$($versions.Version.Minor).$($versions.Version.Build)"
if ($versions.Version.Suffix)
{
    $newVersion += "-" + $versions.Version.Suffix
}

Write-Host "Build $env:APPVEYOR_BUILD_NUMBER using version v$newVersion"

Update-AppveyorBuild -Version $newVersion