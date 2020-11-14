using Volo.Abp.Domain.Entities.Auditing;

namespace LeesStore.Products
{
    public class Product : FullAuditedAggregateRoot<int>
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public ProductType ProductType { get; set; }
    }
}
