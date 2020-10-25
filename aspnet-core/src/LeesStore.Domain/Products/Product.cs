using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace LeesStore.Products
{
    public class Product : AuditedAggregateRoot<int>
    {
        [Required, MaxLength(255)]
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public ProductType ProductType { get; set; }
    }
}
