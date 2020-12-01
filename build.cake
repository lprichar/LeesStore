#addin nuget:?package=Cake.CodeGen.NSwag&version=1.2.0&loaddependencies=true

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("CreateProxy")
   .Description("Uses nswag to re-generate a c# proxy to the client api.")
   .Does(() =>
{
    var filePath = DownloadFile("http://localhost:21021/swagger/client-v1/swagger.json");

    Information("client swagger file downloaded to: " + filePath);
    var proxyClass = "ClientApiProxy";
    var proxyNamespace = "LeesStore.Cmd.ClientProxy";
    var destinationFile = File("./aspnet-core/src/LeesStore.Cmd/ClientProxy/ClientApiProxy.cs");
    
    var settings = new CSharpClientGeneratorSettings
    {
       ClassName = proxyClass,
       CSharpGeneratorSettings = 
       {
          Namespace = proxyNamespace
       }
    };

    NSwag.FromJsonSpecification(filePath)
        .GenerateCSharpClient(destinationFile, settings);

});

Task("Default")
.Does(() => {
   Information("Hello Cake!");
});

RunTarget(target);