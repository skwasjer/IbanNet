$wc = New-Object 'System.Net.WebClient'

$files = Get-ChildItem test-result*.xml -Recurse

$uploadUri = "https://ci.appveyor.com/api/testresults/mstest/$($env:APPVEYOR_JOB_ID)"
Write-Host "Uploading test results to $($uploadUri)"
foreach ($file in $files)
{
    Write-Host "$($file)" -ForegroundColor Gray
    $wc.UploadFile($uploadUri, $file)
}