using Abp.Domain.Repositories;

namespace LeesStore.Products
{
    public interface IProductRepository : IRepository<Product, int>
    {
        int GetIdealQuantity(int productId);
    }
}
