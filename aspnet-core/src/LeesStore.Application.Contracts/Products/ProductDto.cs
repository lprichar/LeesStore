using Volo.Abp.Application.Dtos;

namespace LeesStore.Products
{
    public class ProductDto : AuditedEntityDto<int>
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public ProductType ProductType { get; set; }
    }
}
