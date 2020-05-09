using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Linq;

namespace LeesStore.Web.Host.Startup
{
    /// <summary>
    /// Tells the swashbuckle swagger generator which endpoints should go in which swagger files.
    /// Details: https://github.com/domaindrivendev/Swashbuckle.AspNetCore#generate-multiple-swagger-documents
    /// </summary>
    public class SwaggerFileMapperConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var controllerNamespace = controller.ControllerType.Namespace;
            var namespaceElements = controllerNamespace == null ? new string[0] : controllerNamespace.Split('.');
            var nextToLastNamespace = namespaceElements.ElementAtOrDefault(namespaceElements.Length - 2)?.ToLowerInvariant();
            var lastNamespace = namespaceElements.LastOrDefault()?.ToLowerInvariant();
            var isInClientNamespace = nextToLastNamespace == "client" || lastNamespace == "client";
            // when we get to a V2 this we will need a mapping from namespaces to swagger file versions here
            controller.ApiExplorer.GroupName = isInClientNamespace ?
                Startup.SwashbuckleClientV1ApiGroupName :
                Startup.SwashbuckleWebAppApiGroupName;
        }
    }
}
