using LeesStore.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace LeesStore.Products
{
    [Authorize(LeesStorePermissions.ViewEditProducts)]
    public class ProductsAppService : CrudAppService<Product, ProductDto, int, PagedAndSortedResultRequestDto, CreateProductDto>, IProductAppService
    {
        public ProductsAppService(IRepository<Product, int> repository) : base(repository)
        {
        }
    }
}
