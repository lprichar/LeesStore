using LeesStore.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace LeesStore.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(LeesStoreEntityFrameworkCoreDbMigrationsModule),
        typeof(LeesStoreApplicationContractsModule)
        )]
    public class LeesStoreDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
