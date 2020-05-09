# parmas
$location = "eastus"
# $azUsername = ""
# $azPassword = ""
# $myIpAddress = ""

# folders
$buildFolder = (Get-Item -Path "./" -Verbose).FullName
$rootDir = Join-Path $buildFolder "../.."
$outputFolder = Join-Path $buildFolder "outputs"

# log in
az login --username $azUsername `
    --password $azPassword
az account set --subscription "YOUR-SUBSCRIPTION-NAME-OR-ID"

# create resource group
$resourceGroup = "LeesStoreResourceGroup"
az group create -n $resourceGroup -l $location

# create a sql server
$sqlServerName = "leesstoredb" # lowercase letters, number, hyphens only
$sqlAdmin = "LeeAdmin"
az sql server create -g $resourceGroup -l $location -n $sqlServerName --admin-user $sqlAdmin --admin-password $azPassword
$dbFqdn = az sql server list -g $resourceGroup --query "[0].fullyQualifiedDomainName" --output tsv
az sql server firewall-rule create -g $resourceGroup --server $sqlServerName --name "LeeHome" --start-ip-address $myIpAddress --end-ip-address $myIpAddress
az sql server firewall-rule create -g $resourceGroup --server $sqlServerName --name "Azure" --start-ip-address "0.0.0.0" --end-ip-address "0.0.0.0"

# create a sql database
$dbName = "LeesStoreDb"
$edition = "Basic" # 5 DTUs, Max 2 Gigs at $5/month
az sql db create -g $resourceGroup -l $location -n $dbName -s $sqlServerName -e $edition

# run migrations against sql server
cd (Join-Path $rootDir "/aspnet-core/src/LeesStore.Migrator/")
$migratorOutput = (Join-Path $outputFolder "Migrator")
dotnet publish --output $migratorOutput
cd $migratorOutput
$connectionString = "Server=$dbFqdn; Database=$dbName; User Id=$sqlAdmin; Password=$azPassword"
$Env:ConnectionStrings__Default = $connectionString
dotnet LeesStore.Migrator.dll -q

# create an azure container registry for holding docker containers
$registryName = "LeesStoreRegistry"
az acr create -g $resourceGroup -n $registryName --sku Basic --admin-enabled true -l $location
$loginServer = az acr show -n $registryName --query loginServer --output tsv

# publish host docker image to azure container registry
docker image ls
docker tag abp/host:latest $loginServer/leesstorehost:v1
az acr login -n $registryName
docker push $loginServer/leesstorehost:v1
az acr repository list -n $registryName -o table

# create a container group
$containerGroupName = "lees-store-group"
$acrPassword = az acr credential show -n $registryName --query "passwords[0].value"
$acrName = az acr credential show -n $registryName --query "username"
$hostDnsNameLabel = "leesstorehost"
$hostFqdn = "http://$hostDnsNameLabel.$location.azurecontainer.io/"
az container create -g $resourceGroup -n $containerGroupName --image $loginServer/leesstorehost:v1 --cpu 1 --memory 1 `
    --registry-username $acrName --registry-password $acrPassword `
    -e ConnectionStrings__Default=$connectionString App__ServerRootAddress=$hostFqdn App__CorsOrigins=$hostFqdn `
    --ip-address public --dns-name-label $hostDnsNameLabel --ports 80
az container show -g $resourceGroup -n $containerGroupName --query ipAddress.fqdn
# az container logs -g $resourceGroup -n $containerGroupName
az container exec -g $resourceGroup -n $containerGroupName --exec-command "/bin/bash"
# az container delete -g $resourceGroup -n $containerGroupName
az container stop -g $resourceGroup -n $containerGroupName 