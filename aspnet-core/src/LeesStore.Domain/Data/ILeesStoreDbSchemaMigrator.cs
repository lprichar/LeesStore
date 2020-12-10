using System.Threading.Tasks;

namespace LeesStore.Data
{
    public interface ILeesStoreDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
