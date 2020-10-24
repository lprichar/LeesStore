using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LeesStore.Data;
using Volo.Abp.DependencyInjection;

namespace LeesStore.EntityFrameworkCore
{
    public class EntityFrameworkCoreLeesStoreDbSchemaMigrator
        : ILeesStoreDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreLeesStoreDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the LeesStoreMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<LeesStoreMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}