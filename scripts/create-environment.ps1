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

# create a sql database
$sqlDbName = "LeesStoreQuickDeploy"
az sql db create -g $resourceGroupName -s $sqlName -n $sqlDbName `
    --compute-model "Serverless" -e "GeneralPurpose" -f "Gen5" -c 1 --max-size "1GB"