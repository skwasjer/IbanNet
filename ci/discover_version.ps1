# -- Init
$ciPath = (Split-Path $MyInvocation.MyCommand.Path) + "\"
$repoPath = $ciPath + "..\" # TODO: set this to AppVeyor env var
$packagePath = $repoPath + "packages\"
$debug = $false

# -- Unzip helper
Add-Type -AssemblyName System.IO.Compression.FileSystem
function Unzip
{
    param([string]$zipfile, [string]$outpath)

    [System.IO.Compression.ZipFile]::ExtractToDirectory($zipfile, $outpath)
}

# -- Downloads and unpacks a Nuget package
function DownloadAndUnpackNugetPackage
{
    param([string]$packageName, [string]$version, [string]$outpath)

    $nugetUri = "https://www.nuget.org/api/v2/package/$packageName/$version"
    $packagePath = "$outpath\$packageName.$version.nupkg"
    $unpackPath = "$outpath\$packageName.$version"

    Write-Host "Downloading $nugetUri" -ForegroundColor Yellow
    if (Test-Path $unpackPath)
    {
        Write-Host "  Skipping, package already found."
    }
    else {
        Try
        {
            $webClient = New-Object System.Net.WebClient
            $webClient.DownloadFile($nugetUri, $packagePath)
        }
        Catch [System.Net.WebException]
        {
            Write-Host "Failed to download $packageName.$version.nupkg" -ForegroundColor Red
            Write-Host $_.Exception.InnerException.Message
            exit
        }
    
        Unzip $packagePath $unpackPath
        Write-Host "$packageName.$version unpacked"
    }

    if (Test-Path $packagePath) { Remove-Item $packagePath }

    Write-Host
    return $unpackPath
}

$unpackZoltuFolder = DownloadAndUnpackNugetPackage "Zoltu.Versioning" "1.2.34" $packagePath

# Add reference to Zoltu DLL's
Add-Type -Path ($unpackZoltuFolder + "\build\LibGit2Sharp.dll")
Add-Type -Path ($unpackZoltuFolder + "\build\Zoltu.Versioning.dll")

# Get repo path (.git folder)
$gitRepositoryPath = [LibGit2Sharp.Repository]::Discover($repoPath)
if ($gitRepositoryPath -eq $null)
{
    Write-Host "Git repository not found, aborting." -ForegroundColor Red
    # TODO: fail build job
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

$versions = [Zoltu.Versioning.VersionFinder]::GetVersions($commits, $tags);

return $versions