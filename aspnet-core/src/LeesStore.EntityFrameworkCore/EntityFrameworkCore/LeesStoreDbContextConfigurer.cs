using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace LeesStore.EntityFrameworkCore
{
    public static class LeesStoreDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<LeesStoreDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<LeesStoreDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
