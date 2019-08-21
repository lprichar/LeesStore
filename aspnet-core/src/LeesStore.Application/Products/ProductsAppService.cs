using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using LeesStore.Authorization;
using LeesStore.Products.Dto;

namespace LeesStore.Products
{
    [AbpAuthorize(PermissionNames.Pages_Products)]
    public class ProductsAppService : AsyncCrudAppService<Product, ProductDto, int, PagedAndSortedResultRequestDto, ProductDto>
    {
        private readonly IProductRepository _repository;

        public ProductsAppService(IProductRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task Increment(int productId)
        {
            var product = _repository.Get(productId);
            var idealQuantity = _repository.GetIdealQuantity(productId);
            product.Quantity = idealQuantity;
            await _repository.UpdateAsync(product);
        }
    }
}
