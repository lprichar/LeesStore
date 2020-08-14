$resourceGroupName = "LeesStoreQuickDeploy"
$location = "eastus"

az login
# create a resource group to hold everything
az group create -l $location -n $resourceGroupName

# create a sql server
$sqlUsername = "lprichar"
$sqlPassword = "[password]"
$sqlName = "leesstorequickdeploy"
az sql server create -g $resourceGroupName -n $sqlName `
    -u $sqlUsername -p $sqlPassword -l $location

# add firewall rule for access from Azure services
az sql server firewall-rule create -g $resourceGroupName -s $sqlName -n Azure --start-ip-address 0.0.0.0 --end-ip-address 0.0.0.0

# create a sql database
$sqlDbName = "LeesStoreQuickDeploy"
az sql db create -g $resourceGroupName -s $sqlName -n $sqlDbName `
    --compute-model "Serverless" -e "GeneralPurpose" -f "Gen5" -c 1 --max-size "1GB"

# create an app service
$appServicePlan = "LeesStoreServicePlan"
$webAppName = "leesstore2"
az appservice plan create -n $appServicePlan -g $resourceGroupName --sku Free -l $location
az webapp create -n $webAppName -g $resourceGroupName -p $appServicePlan
