using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using LeesStore.Configuration;
using LeesStore.Web;

namespace LeesStore.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class LeesStoreDbContextFactory : IDesignTimeDbContextFactory<LeesStoreDbContext>
    {
        public LeesStoreDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<LeesStoreDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            LeesStoreDbContextConfigurer.Configure(builder, configuration.GetConnectionString(LeesStoreConsts.ConnectionStringName));

            return new LeesStoreDbContext(builder.Options);
        }
    }
}
