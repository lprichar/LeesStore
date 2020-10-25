using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace LeesStore.Products
{
    public interface IProductAppService : ICrudAppService<ProductDto, int, PagedAndSortedResultRequestDto, ProductDto>
    {
    }
}
