#addin nuget:?package=Cake.CodeGen.NSwag&version=1.2.0&loaddependencies=true
// #addin nuget:?package=Cake.NSwag

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
   // NSwag.FromSwaggerSpecification(filePath)
   //      .ToCSharpClient("./aspnet-core/src/LeesStore.Cmd/ClientProxy/ClientApiProxy.cs", "LeesStore.Cmd.ClientProxy.ClientProxy.ClientApiProxy");

    var filePath = DownloadFile("http://localhost:21021/swagger/client-v1/swagger.json");
    Information("client swagger file downloaded to: " + filePath);
    var settings = new CSharpClientGeneratorSettings
    {
       ClassName = "ClientApiProxy",
       CSharpGeneratorSettings = 
       {
          Namespace = "LeesStore.Cmd.ClientProxy"
       }
    };

    NSwag.FromJsonSpecification(filePath)
        .GenerateCSharpClient("./aspnet-core/src/LeesStore.Cmd/ClientProxy/ClientApiProxy.cs", settings);

});

Task("Default")
.Does(() => {
   Information("Hello Cake!");
});

RunTarget(target);