using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace LeesStore.Products.Dto
{
    [AutoMapFrom(typeof(Product))]
    public class ProductDto : EntityDto<int>
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
