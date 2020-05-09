using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using LeesStore.Configuration;

namespace LeesStore.Web.Host.Startup
{
    [DependsOn(
       typeof(LeesStoreWebCoreModule))]
    public class LeesStoreWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public LeesStoreWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(LeesStoreWebHostModule).GetAssembly());
        }
    }
}
