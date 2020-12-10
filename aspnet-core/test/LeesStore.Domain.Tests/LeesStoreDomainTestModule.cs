using LeesStore.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace LeesStore
{
    [DependsOn(
        typeof(LeesStoreEntityFrameworkCoreTestModule)
        )]
    public class LeesStoreDomainTestModule : AbpModule
    {

    }
}