using Microsoft.EntityFrameworkCore;
using System.Linq;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace LeesStore.EntityFrameworkCore
{
    public static class LeesStoreDbContextModelCreatingExtensions
    {
        public static void ConfigureLeesStore(this ModelBuilder builder)
        {
            var allDbSets = typeof(LeesStoreDbContext).GetProperties()
                .Where(p => p.PropertyType.Name == "DbSet`1")
                .Select(p => new
                {
                    Type = p.PropertyType.GetGenericArguments()[0],
                    p.Name
                });

            foreach (var property in allDbSets)
            {
                var type = property.Type;
                builder.Entity(type, i =>
                {
                    i.ToTable(LeesStoreConsts.DbTablePrefix + property.Name, LeesStoreConsts.DbSchema);
                    i.ConfigureByConvention();
                });
            }

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(LeesStoreConsts.DbTablePrefix + "YourEntities", LeesStoreConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});
        }
    }
}