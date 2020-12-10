using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace LeesStore.Data
{
    /* This is used if database provider does't define
     * ILeesStoreDbSchemaMigrator implementation.
     */
    public class NullLeesStoreDbSchemaMigrator : ILeesStoreDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}