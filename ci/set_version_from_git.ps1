$ciPath = (Split-Path $MyInvocation.MyCommand.Path) + "\"

# Discover version(s)
$versions = & ($ciPath + "discover_version.ps1")

if ($versions)
{
    $prNumber = $env:APPVEYOR_PULL_REQUEST_NUMBER

    # Build new version string
    $newVersion = "$($versions.InfoVersion.Major).$($versions.InfoVersion.Minor).$($versions.InfoVersion.Patch)"

	$package_version = $newVersion
	$assembly_version = "$newVersion.0"
	$fullVersion = "$newVersion.$env:APPVEYOR_BUILD_NUMBER"
    $semVer1 = $fullVersion

    $branch = $env:APPVEYOR_REPO_BRANCH
    if ($branch)
    {
        $postfix = ""
        if ($branch -eq "master")
        {
            $postfix = "beta"
        } elseif ($branch -eq "develop")
        {
            $postfix = "alpha"
        } elseif ($branch.StartsWith("release")) {
        } else {
            $postfix = $branch -replace "[^0-9A-Za-z\-]","-"
        }

        if ($postfix)
        {
            $package_version += "-" + $postfix + ".build." + $env:APPVEYOR_BUILD_NUMBER
            $semVer1 += "-" + $postfix + ".build." + $env:APPVEYOR_BUILD_NUMBER
        }
    }

    if ($prNumber)
    {
        $package_version += "-PR" + $prNumber
        $semVer1 += "-PR" + $prNumber
    }

    if ($versions.InfoVersion.Suffix)
    {
		$package_version += "-" + $versions.InfoVersion.Suffix
        $semVer1 += "-" + $versions.InfoVersion.Suffix
    }

	$informational_version = $semVer1

    Write-Host "Build v$fullVersion will be building package version v$package_version"

    Try
    {
        Set-AppveyorBuildVariable "package_version" $package_version		
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
