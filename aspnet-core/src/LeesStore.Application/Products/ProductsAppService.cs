using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace LeesStore.Products
{
    public class ProductsAppService : CrudAppService<Product, ProductDto, int, PagedAndSortedResultRequestDto, ProductDto>, IProductAppService
    {
        public ProductsAppService(IRepository<Product, int> repository) : base(repository)
        {
        }
    }
}
