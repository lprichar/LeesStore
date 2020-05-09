$resourceGroup = "LeesStoreResourceGroup"
$containerGroupName = "lees-store-group"
$registryName = "LeesStoreRegistry"
$loginServer = az acr show -n $registryName --query loginServer --output tsv

# publish host docker image to azure container registry
docker image ls
docker tag abp/host:latest $loginServer/leesstorehost:v1
az acr login -n $registryName
docker push $loginServer/leesstorehost:v1
az acr repository list -n $registryName -o table

az container restart -g $resourceGroup -n $containerGroupName