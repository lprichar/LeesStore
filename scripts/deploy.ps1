$webAppName = "leesstore2"
$resourceGroupName = "LeesStoreQuickDeploy"
$connectionString = "Server=leesstorequickdeploy.database.windows.net;Initial Catalog=LeesStoreQuickDeploy;UID=lee;Password=[password];"

az login

# clean
Remove-Item ".\dist" -Recurse -Force

# publish (single file, windows specific)
dotnet publish -c Release -o ".\dist\Migrator" `
    -r "win-x64" /p:PublishSingleFile=true `
    .\aspnet-core\src\LeesStore.Migrator
Copy-Item ".\aspnet-core\src\LeesStore.Migrator\log4net.config" ".\dist\Migrator"

# run migrator with customized connection string
Push-Location
Set-Location ".\dist\Migrator"
$env:ConnectionStrings__Default = $connectionString
.\LeesStore.Migrator.exe -q
Pop-Location

# set connection string
az webapp config appsettings set -n $webappname -g $resourceGroupName --settings ConnectionStrings__Default=$connectionString
# set url's
$url = "https://leesstore2.azurewebsites.net/"
az webapp config appsettings set -n $webappname -g $resourceGroupName --settings App__ServerRootAddress=$url App__ClientRootAddress=$url App__CorsOrigins=$url

# Compiler angular
Push-Location
Set-Location ".\angular"
ng build --prod --aot --output-path "../dist/ng"
Pop-Location
Move-Item "dist/ng" "aspnet-core/src/LeesStore.Web.Host/wwwroot"

# compile Host
dotnet publish -c Release -o ".\dist\Host" .\aspnet-core\src\LeesStore.Web.Host
# zip Host
Compress-Archive -PassThru -Path ".\dist\Host\*" -DestinationPath ".\dist\Host.zip"
# upload Host
az webapp deployment source config-zip -n $webappname -g $resourceGroupName --src ".\dist\Host.zip"

