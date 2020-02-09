$buildFolder = (Get-Item -Path "./" -Verbose).FullName
$slnFolder = Join-Path $buildFolder "../"
$outputFolder = Join-Path $buildFolder "outputs"
$webHostFolder = Join-Path $slnFolder "src/LeesStore.Web.Host"
$ngFolder = Join-Path $buildFolder "../../angular"

Set-Location $webHostFolder
dotnet publish --output (Join-Path $outputFolder "Host")

Set-Location (Join-Path $outputFolder "Host")

docker rmi abp/host -f
docker build -t abp/host .

Set-Location $buildFolder
