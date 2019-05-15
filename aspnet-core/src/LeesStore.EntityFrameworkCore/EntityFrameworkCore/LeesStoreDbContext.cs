using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using LeesStore.Authorization.Roles;
using LeesStore.Authorization.Users;
using LeesStore.MultiTenancy;
using LeesStore.Products;

namespace LeesStore.EntityFrameworkCore
{
    public class LeesStoreDbContext : AbpZeroDbContext<Tenant, Role, User, LeesStoreDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public LeesStoreDbContext(DbContextOptions<LeesStoreDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
