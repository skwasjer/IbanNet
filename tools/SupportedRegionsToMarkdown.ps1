# Extract supported countries from assembly to update README.md

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

$countries = New-Object IbanNet.Registry.SwiftRegistryProvider;
$supportedCount = $countries.Count

$markdown = "## IbanNet supports [SUPPORTED_COUNT] countries`r`n`r`n"

$markdown += "| ISO country code | Country | SEPA | Length | IBAN example |`r`n"
$markdown += "|---|---|---|---|---|`r`n"
ForEach($country in $countries)
{
    $iban = [IbanNet.Iban]::Parse($country.Iban.Example)
    $markdown += "| $($country.TwoLetterISORegionName) | $($country.EnglishName) | $(If ($country.Sepa.IsMember) { "Yes" } else { "No" }) | $($country.Iban.Length) | ``$($iban.ToString("S"))`` |`r`n"
}

$overrideCountryDescription = @{ "AX" = "Åland Islands"; "IM" = "Isle of Man"; "JE" = "Jersey"; "GG" = "Guernsey" }

ForEach($country in $countries)
{
    If ($country.IncludedCountries.Count -gt 0)
    {
        $supportedCount += $country.IncludedCountries.Count

        $markdown += "`r`n"
        $markdown += "### $($country.EnglishName) includes:`r`n`r`n"
        
        ForEach($ic in $country.IncludedCountries)
        {
            $regionInfo = New-Object System.Globalization.RegionInfo($ic)
            # Resolve english name
            $ccName = $overrideCountryDescription.Get_Item($regionInfo.TwoLetterISORegionName)
            if ($ccName -eq $null)
            {
                If ($regionInfo.ThreeLetterISORegionName -eq "ZZZ")
                {
                    $ccName = "Unknown"
                }
                Else
                {
                    $ccName = $regionInfo.EnglishName
                }
            }

            $markdown += "- $($ccName) ($($ic.Substring(3)))`r`n"
        }
    }
}

$markdown += "`r`nFor more info visit [Wikipedia](https://en.wikipedia.org/wiki/International_Bank_Account_Number)."

$markdown = $markdown.Replace("[SUPPORTED_COUNT]", $supportedCount)

[System.IO.File]::WriteAllLines((Join-Path $repoPath "SupportedCountries.md"), $markdown)