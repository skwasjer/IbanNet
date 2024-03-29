﻿# Extract supported countries from assembly to update README.md

$scriptPath = (Split-Path $MyInvocation.MyCommand.Path)
$repoPath = Join-Path $scriptPath "..\"

function Load-IbanNet()
{
    param(
        [parameter(Mandatory=$true)]
        [string]
        $configuration
    )
    Add-Type -Path (Join-Path $repoPath "src\IbanNet\bin\$configuration\netstandard1.6\IbanNet.dll")
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
    $markdown = "| ISO country code | Country | Length | IBAN example | SEPA | Bank ID | Branch ID |`n"
    $markdown += "|---|---|---|---|---|---|---|`n"

    ForEach($country in $countries)
    {
        $exampleIban = "-"
        If (-Not [string]::IsNullOrEmpty($country.Iban.Example))
        {
            $exampleIban = "``$($parser.Parse($country.Iban.Example).ToString([IbanNet.IbanFormat]::Print))``"
        }

        $markdown += "| $($country.TwoLetterISORegionName) | $($country.EnglishName) "
        $markdown += "| $($country.Iban.Length) "
        $markdown += "| $($exampleIban) "
        $markdown += "| $(If (-Not $country.Sepa) { "-" } elseIf ($country.Sepa.IsMember) { "Yes" } else { "No" }) "
        $markdown += "| $(If ($country.Bank.Position -eq 0) { "-" } elseIf ($country.Bank.Length -gt 0) { "![Supported][supported]" } else { "No" }) "
        $markdown += "| $(If ($country.Branch.Position -eq 0) { "-" } elseIf ($country.Branch.Length -gt 0) { "![Supported][supported]" } else { "No" }) "
        $markdown += "|`n"
    }

    $markdown += "`n"

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

# Support Ukraine

$markdown = "[![Stand With Ukraine](https://raw.githubusercontent.com/vshymanskyy/StandWithUkraine/main/badges/StandWithUkraine.svg)](https://stand-with-ukraine.pp.ua)`n`n"
$markdown += "*In mid 2022, an IBAN pattern for the Russian Federation (RU) was registered with SWIFT. This project has not (yet) adopted support for this pattern due to the war in Ukraine.*`n`n"

$markdown += "# IbanNet supports [SUPPORTED_COUNT] countries`n`n"

# Render Swift

$markdown += "## SWIFT registry`n`n"
$markdown += "See the [SWIFT website](https://www.swift.com/standards/data-standards/iban-international-bank-account-number) for more information.`n`n"

$markdown += Render-Table($swiftProvider);

# Included countries

$overrideCountryDescription = @{ "TF" = "French Southern and Antarctic Lands" }

ForEach($country in $registry)
{
    If ($country.IncludedCountries.Count -gt 0)
    {
        $supportedCount += $country.IncludedCountries.Count

        $markdown += "### $($country.EnglishName) includes:`n`n"
        
        ForEach($ic in $country.IncludedCountries)
        {
            Try
            {
                $regionInfo = New-Object System.Globalization.RegionInfo($ic)
            }
            Catch
            {
                $regionInfo = $null
                $regionInfo = New-Object System.Globalization.RegionInfo($country.TwoLetterISORegionName + "-" + $ic)
            }


            # Resolve english name
            $ccName = $null
            $ccName = $overrideCountryDescription.Get_Item($ic)

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

            $markdown += "- $($ccName) ($($ic))`n"
        }

        $markdown += "`n"
    }
}

$markdown += "`n## Wikipedia`n`nExtra (unofficial) countries from [Wikipedia](https://en.wikipedia.org/wiki/International_Bank_Account_Number):`n`n"

$comparer = New-Object IbanNet.Registry.IbanCountryCodeComparer;
$filteredWikiCountries = [System.Linq.Enumerable]::Except($wikiProvider, $swiftProvider, $comparer) | Sort-Object -Property TwoLetterISORegionName
$markdown += Render-Table($filteredWikiCountries);

$markdown += "> The countries taken from *Wikipedia* are not enabled by default when using IbanNet. Check the documentation how to enable the ``WikipediaRegistryProvider``.`n`n"

$markdown = $markdown.Replace("[SUPPORTED_COUNT]", $supportedCount)

$markdown += "[supported]: ./docs/res/check.svg ""Supported""`n"
$markdown += "[not-supported]: ./doc/res/close.svg ""Not supported""`n"

[System.IO.File]::WriteAllLines((Join-Path $repoPath "SupportedCountries.md"), $markdown)
