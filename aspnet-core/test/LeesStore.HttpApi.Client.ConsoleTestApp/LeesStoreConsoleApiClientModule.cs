using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace LeesStore.HttpApi.Client.ConsoleTestApp
{
    [DependsOn(
        typeof(LeesStoreHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class LeesStoreConsoleApiClientModule : AbpModule
    {
        
    }
}
