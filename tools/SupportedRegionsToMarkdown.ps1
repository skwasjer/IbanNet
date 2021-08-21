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

function Render-Table($countries)
{
    $markdown = "| ISO country code | Country | SEPA | Length | IBAN example |`r`n"
    $markdown += "|---|---|---|---|---|`r`n"

    ForEach($country in $countries)
    {
        $iban = ""
        If (-Not [string]::IsNullOrEmpty($country.Iban.Example))
        {
            $iban = "``$($parser.Parse($country.Iban.Example).ToString([IbanNet.IbanFormat]::Print))``"
        }
        $markdown += "| $($country.TwoLetterISORegionName) | $($country.EnglishName) | $(If (-Not $country.Sepa) { "-" } elseIf ($country.Sepa.IsMember) { "Yes" } else { "No" }) | $($country.Iban.Length) | $($iban) |`r`n"
    }

    $markdown += "`r`n"

    return $markdown;
}

$validator = New-Object IbanNet.IbanValidator;
$parser = New-Object IbanNet.IbanParser($validator);

$registry = New-Object IbanNet.Registry.IbanRegistry;

$swiftProvider = New-Object IbanNet.Registry.Swift.SwiftRegistryProvider;
$wikiProvider = New-Object IbanNet.Registry.Wikipedia.WikipediaRegistryProvider;

$registry.Providers.Add($swiftProvider);
$registry.Providers.Add($wikiProvider);

$supportedCount = $registry.Count

$markdown = "## IbanNet supports [SUPPORTED_COUNT] countries`r`n`r`n"

# Render Swift

$markdown += "### SWIFT registry`r`n`r`n"
$markdown += "See the [SWIFT website](https://www.swift.com/standards/data-standards/iban-international-bank-account-number) for more information.`r`n`r`n"

$markdown += Render-Table($swiftProvider);

# Included countries

$overrideCountryDescription = @{ "AX" = "Åland Islands"; "IM" = "Isle of Man"; "JE" = "Jersey"; "GG" = "Guernsey" }

ForEach($country in $registry)
{
    If ($country.IncludedCountries.Count -gt 0)
    {
        $supportedCount += $country.IncludedCountries.Count

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

        $markdown += "`r`n"
    }
}

$markdown += "`r`n### Wikipedia`r`n`r`nExtra (unofficial) countries from [Wikipedia](https://en.wikipedia.org/wiki/International_Bank_Account_Number):`r`n`r`n"

$comparer = New-Object IbanNet.Registry.IbanCountryCodeComparer;
$filteredWikiCountries = [System.Linq.Enumerable]::Except($wikiProvider, $swiftProvider, $comparer) | Sort-Object -Property TwoLetterISORegionName
$markdown += Render-Table($filteredWikiCountries);

$markdown += "> The countries taken from *Wikipedia* are not enabled by default when using IbanNet. Check the documentation how to enable the ``WikipediaRegistryProvider``.`r`n"

$markdown = $markdown.Replace("[SUPPORTED_COUNT]", $supportedCount)

[System.IO.File]::WriteAllLines((Join-Path $repoPath "SupportedCountries.md"), $markdown)
