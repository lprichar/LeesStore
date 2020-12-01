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
            var controllerNamespace = controller?.ControllerType?.Namespace;
            if (controllerNamespace == null) return;
            var namespaceElements = controllerNamespace.Split('.');
            var nextToLastNamespace = namespaceElements.ElementAtOrDefault(namespaceElements.Length - 2)?.ToLowerInvariant();
            var isInClientNamespace = nextToLastNamespace == "client";
            controller.ApiExplorer.GroupName = isInClientNamespace ? "client-v1" : "v1";
        }
    }
}
