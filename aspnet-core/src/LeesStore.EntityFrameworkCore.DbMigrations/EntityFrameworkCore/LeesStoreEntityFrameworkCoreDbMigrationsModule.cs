using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace LeesStore.EntityFrameworkCore
{
    [DependsOn(
        typeof(LeesStoreEntityFrameworkCoreModule)
        )]
    public class LeesStoreEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<LeesStoreMigrationsDbContext>();
        }
    }
}
