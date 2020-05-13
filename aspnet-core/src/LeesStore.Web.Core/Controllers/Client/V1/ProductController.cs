using Abp.Domain.Repositories;
using Abp.Web.Models;
using LeesStore.Products;
using LeesStore.Products.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LeesStore.Controllers.Client.V1
{
    [DontWrapResult(WrapOnError = false, WrapOnSuccess = false, LogError = true)]
    public class ProductController : LeesStoreControllerBase
    {
        private readonly IRepository<Product, int> _productRepository;

        public ProductController(IRepository<Product, int> productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("api/client/v1/product/{id}")]
        //[ProducesResponseType(typeof(List<Product>, 200))]
        public async Task<ProductDto> Get(int id)
        {
            var product = await _productRepository.GetAsync(id);
            var productDto = ObjectMapper.Map<ProductDto>(product);
            return productDto;
        }
    }
}
