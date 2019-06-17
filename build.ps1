$root = "../../"
$outputFolder = "artifacts"
$outputContents = Join-Path $outputFolder "*.*"
$libraryFolder =  Join-Path $outputFolder "SRISYS.Web"
$wwwrootFolder = Join-Path $libraryFolder "wwwroot"

Write-Output "Cleaning the artifacts folder..."
Remove-Item $outputContents -Recurse

Write-Output "Restoring nuget packages..."
dotnet restore

$outputPath = Join-Path $root $libraryFolder
Write-Output "Building and publishing the application..."
dotnet publish -f netcoreapp1.1 -c Release --force -o $outputPath

Write-Output "Copying web folders to artifacts..."
Copy-Item "src/SRISYS.Web/Views" $libraryFolder -Recurse
Copy-Item "src/SRISYS.Web/wwwroot" $libraryFolder -Recurse
Copy-Item "src/SRISYS.Web/appSettings.json" $libraryFolder

Write-Output "Copying the docker folder to artifacts..."
Copy-Item "docker/Dockerfile" $outputFolder