using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using LeesStore.EntityFrameworkCore;
using LeesStore.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace LeesStore.Web.Tests
{
    [DependsOn(
        typeof(LeesStoreWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class LeesStoreWebTestModule : AbpModule
    {
        public LeesStoreWebTestModule(LeesStoreEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(LeesStoreWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(LeesStoreWebMvcModule).Assembly);
        }
    }
}