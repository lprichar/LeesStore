using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using LeesStore.Authorization;

namespace LeesStore
{
    [DependsOn(
        typeof(LeesStoreCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class LeesStoreApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<LeesStoreAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(LeesStoreApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
