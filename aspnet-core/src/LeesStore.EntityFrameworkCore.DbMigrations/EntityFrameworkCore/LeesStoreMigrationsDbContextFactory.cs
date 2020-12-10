using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace LeesStore.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class LeesStoreMigrationsDbContextFactory : IDesignTimeDbContextFactory<LeesStoreMigrationsDbContext>
    {
        public LeesStoreMigrationsDbContext CreateDbContext(string[] args)
        {
            LeesStoreEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<LeesStoreMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new LeesStoreMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../LeesStore.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
