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

$validator = New-Object IbanNet.IbanValidator

$markdown = "## IbanNet supports $($validator.SupportedRegions.Count) regions`r`n"

$markdown += "| ISO country code | Country | IBAN length | IBAN example |`r`n"
$markdown += "|---|---|---|---|`r`n"
ForEach($region in $validator.SupportedRegions)
{
    $iban = [IbanNet.Iban]::Parse($region.Example)
    $markdown += "| $($region.TwoLetterISORegionName) | $($region.Region.EnglishName) | $($region.Length) | ``$($iban.ToString("S"))`` |`r`n"
}

$markdown += "`r`nFor more info visit [Wikipedia](https://en.wikipedia.org/wiki/International_Bank_Account_Number)."

$markdown | Set-Content (Join-Path $repoPath "SupportedRegions.md")