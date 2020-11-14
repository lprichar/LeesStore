using LeesStore.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace LeesStore.Products
{
    [Authorize(LeesStorePermissions.ViewEditProducts)]
    public class ProductsAppService : CrudAppService<Product, ProductDto, int,
        PagedAndSortedResultRequestDto, CreateProductDto>, IProductAppService
    {
        private readonly IRepository<Product, int> _repository;

        public ProductsAppService(IRepository<Product, int> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<ProductDto> IncrementQuantity(int productId)
        {
            var product = await _repository.GetAsync(i => i.Id == productId);
            Logger.LogInformation($"Incrementing quantity of '{product.Name}'");
            if (product.ProductType == ProductType.Other)
            {
                throw new UserFriendlyException(L["CantUpdateQuantityForProductsOfTypeOther"]);
            }

            product.Quantity++;
            return ObjectMapper.Map<Product, ProductDto>(product);
        }
    }
}
