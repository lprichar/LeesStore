using LeesStore.Products;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace LeesStore
{
    public class LeesStoreDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Product, int> _productRepository;

        public LeesStoreDataSeedContributor(IRepository<Product, int> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            var products = await _productRepository.GetCountAsync();
            if (products == 0)
            {
                await _productRepository.InsertAsync(new Product
                {
                    Name = "Basic Siren",
                    Quantity = 10,
                    ProductType = ProductType.Siren
                });
            }
        }
    }
}
