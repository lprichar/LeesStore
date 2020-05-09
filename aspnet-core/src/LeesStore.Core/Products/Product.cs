using Abp.Domain.Entities;

namespace LeesStore.Products
{
    public class Product : Entity<int>, IMustHaveTenant
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int TenantId { get; set; }
    }
}
