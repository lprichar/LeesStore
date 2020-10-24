using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using LeesStore.Authorization;
using LeesStore.Products.Dto;

namespace LeesStore.Products
{
    [AbpAuthorize(PermissionNames.Pages_Products)]
    public class ProductsAppService : AsyncCrudAppService<Product, ProductDto, int>
    {
        public ProductsAppService(IRepository<Product, int> repository) : base(repository)
        {
        }
    }
}
