$connectionString = "Server=leesstorequickdeploy.database.windows.net;Initial Catalog=LeesStoreQuickDeploy;User ID=lprichar;Password=Abcdefg1;"

# clean
Remove-Item ".\dist\Migrator\*.*"
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