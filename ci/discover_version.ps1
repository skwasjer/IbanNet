# -- Init
$ciPath = (Split-Path $MyInvocation.MyCommand.Path)
$repoPath = Join-Path $ciPath "\.." # TODO: set this to AppVeyor env var
$packagePath = Join-Path $repoPath "\packages"
$debug = $false

# -- Unzip helper
Add-Type -AssemblyName System.IO.Compression.FileSystem
function Unzip
{
    param(
        [parameter(Mandatory=$true)]
        [string]
        $zipfile,
    
        [parameter(Mandatory=$true)]    
        [string]
        $outpath
    )

    [System.IO.Compression.ZipFile]::ExtractToDirectory($zipfile, $outpath)
}

# -- Downloads and unpacks a Nuget package
function Download-And-Unpack-Nuget-Package
{
    param(
        [parameter(Mandatory=$true)]
        [string]
        $packageName, 
        
        [parameter(Mandatory=$true)]
        [string]
        $version,

        [parameter(Mandatory=$true)]
        [string]
        $outpath
    )

    $nugetUri = "https://www.nuget.org/api/v2/package/$packageName/$version"
    $packagePath = Join-Path $outpath "$packageName.$version.nupkg"
    $unpackPath = Join-Path $outpath "$packageName.$version"

    # Skip if package path already exists
    if (-Not (Test-Path $unpackPath))
    {
        if (-Not (Test-Path $outpath))
        {
            New-Item $outpath -type directory | Out-Null
        }

        Try
        {
            Write-Host "Downloading $nugetUri" -ForegroundColor Yellow
            $webClient = New-Object System.Net.WebClient
            $webClient.DownloadFile($nugetUri, $packagePath) | Out-Null
        }
        Catch [System.Net.WebException]
        {
            Write-Host "Failed to download $packageName.$version.nupkg" -ForegroundColor Red
            Write-Host $_.Exception.InnerException.Message
            exit
        }
    
        Unzip $packagePath $unpackPath
        if ($debug) { Write-Host "$packageName.$version unpacked" }
    }

    if (Test-Path $packagePath) { Remove-Item $packagePath }
    
    Write-Host
    return $unpackPath
}

$unpackZoltuFolder = Download-And-Unpack-Nuget-Package "Zoltu.Versioning" "1.2.34" $packagePath

# Add reference to Zoltu DLL's
Add-Type -Path (Join-Path $unpackZoltuFolder "build\LibGit2Sharp.dll")
Add-Type -Path (Join-Path $unpackZoltuFolder "build\Zoltu.Versioning.dll")

# Get repo path (.git folder)
$gitRepositoryPath = [LibGit2Sharp.Repository]::Discover($repoPath)
if ($gitRepositoryPath -eq $null)
{
    Write-Host "Git repository not found, aborting" -ForegroundColor Red
    exit
}
else
{
    Write-Host "Git repository $gitRepositoryPath"
    Write-Host
}

# Using Zoltu, get the new build number
$gitRepository = New-Object -TypeName LibGit2Sharp.Repository -ArgumentList $gitRepositoryPath

$commits = [Zoltu.Versioning.GitVersion]::GetHeadCommitsFromRepository($gitRepository)
$tags = [Zoltu.Versioning.GitVersion]::GetTagsFromRepository($gitRepository)

if ($debug)
{
    Write-Host "Tags:"
    ForEach ($tag in $tags)
    {
        Write-Host "  $($tag.Name)"
    }

    Write-Host "Commits:"
    ForEach ($commit in $commits)
    {
        Write-Host "  Commit $($commit.Sha)"
    }
}

return [Zoltu.Versioning.VersionFinder]::GetVersions($commits, $tags);