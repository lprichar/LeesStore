using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using LeesStore.Products.Dto;

namespace LeesStore.Products
{
    public class ProductsAppService : AsyncCrudAppService<Product, ProductDto, int, PagedAndSortedResultRequestDto, ProductDto>
    {
        public ProductsAppService(IRepository<Product, int> repository) : base(repository)
        {
        }
    }
}
