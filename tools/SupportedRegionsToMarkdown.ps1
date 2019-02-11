# Extract supported regions from assembly to update README.md

$scriptPath = (Split-Path $MyInvocation.MyCommand.Path)
$repoPath = Join-Path $scriptPath "..\"

function Load-IbanNet()
{
    param(
        [parameter(Mandatory=$true)]
        [string]
        $configuration
    )
    Add-Type -Path (Join-Path $repoPath "src\IbanNet\bin\$configuration\netstandard1.2\IbanNet.dll")
}

Try
{
    Load-IbanNet "Debug"
}
Catch
{
    Write-Host "Can't load IbanNet, make sure the solution is built in 'Debug' configuration." -ForegroundColor Red
    exit
}

$regions = New-Object IbanNet.Registry.IbanRegistry;

$markdown = "## IbanNet supports $($regions.Count) regions`r`n"

$markdown += "| ISO country code | Country | SEPA | Length | IBAN example |`r`n"
$markdown += "|---|---|---|---|---|`r`n"
ForEach($region in $regions)
{
    $iban = [IbanNet.Iban]::Parse($region.Iban.Example)
    $markdown += "| $($region.TwoLetterISORegionName) | $($region.EnglishName) | $(If ($region.Sepa.IsMember) { "Yes" } else { "No" }) | $($region.Iban.Length) | ``$($iban.ToString("S"))`` |`r`n"
}

$markdown += "`r`nFor more info visit [Wikipedia](https://en.wikipedia.org/wiki/International_Bank_Account_Number)."

[System.IO.File]::WriteAllLines((Join-Path $repoPath "SupportedRegions.md"), $markdown)