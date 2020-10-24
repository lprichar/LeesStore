using Volo.Abp.Modularity;

namespace LeesStore
{
    [DependsOn(
        typeof(LeesStoreApplicationModule),
        typeof(LeesStoreDomainTestModule)
        )]
    public class LeesStoreApplicationTestModule : AbpModule
    {

    }
}