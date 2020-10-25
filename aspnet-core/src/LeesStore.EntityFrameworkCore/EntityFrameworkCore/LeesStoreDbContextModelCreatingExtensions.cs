using LeesStore.Products;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace LeesStore.EntityFrameworkCore
{
    public static class LeesStoreDbContextModelCreatingExtensions
    {
        public static void ConfigureLeesStore(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            builder.Entity<Product>(i =>
            {
                i.ToTable(LeesStoreConsts.DbTablePrefix + "Products", LeesStoreConsts.DbSchema);
                i.ConfigureByConvention();
            });

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(LeesStoreConsts.DbTablePrefix + "YourEntities", LeesStoreConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});
        }
    }
}