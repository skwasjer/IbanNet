
# http://blog.peterritchie.com/Resetting-Build-Number-in-Appveyor/

$apiUrl = 'https://ci.appveyor.com/api'
$token = $env:APPVEYOR_API_TOKEN
$headers = @{
  "Authorization" = "Bearer $token"
  "Content-type" = "application/json"
  "Accept" = "application/json"
}
$accountName = $env:APPVEYOR_ACCOUNT_NAME
$projectSlug = $env:APPVEYOR_PROJECT_SLUG
$buildNumber = $env:APPVEYOR_BUILD_NUMBER
$buildVersionText = $env:APPVEYOR_BUILD_VERSION
$buildVersion = New-Object -TypeName PSObject -Property (@{
    'MajorVersion'=$buildVersionText.Split('.')[0];
    'MinorVersion'=$buildVersionText.Split('.')[1];
    'BuildVersion'=$buildVersionText.Split('.')[2]
})

$response = Invoke-RestMethod -Method Get -Uri "$apiUrl/projects/$accountName/$projectSlug/history?recordsNumber=100" -Headers $headers
$lastBuildVersion = $response.builds | select -first 1 @{Label="MajorVersion"; Expression={$_.version.Split('.')[0]}}, @{Label="MinorVersion"; Expression={$_.version.Split('.')[1]}}, @{Label="BuildVersion"; Expression={$_.version.Split('.')[2]}}

if ($lastBuildVersion.MajorVersion -ne $buildVersion.MajorVersion -or ($lastBuildVersion.MajorVersion -eq $buildVersion.MajorVersion -and $lastBuildVersion.MinorVersion -ne $buildVersion.MinorVersion))
{
  $build = @{
      nextBuildNumber = 0
  }
  $json = $build | ConvertTo-Json

  Invoke-RestMethod -Method Put "$apiUrl/projects/$accountName/$projectSlug/settings/build-number" -Body $json -Headers $headers
}