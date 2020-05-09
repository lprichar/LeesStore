using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using LeesStore.Authorization;
using LeesStore.Products.Dto;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LeesStore.Products
{
    [AbpAuthorize(PermissionNames.Pages_Products)]
    public class ProductsAppService : AsyncCrudAppService<Product, ProductDto, int, PagedAndSortedResultRequestDto, ProductDto>
    {
        private readonly IRepository<Product, int> _repository;
        private readonly IAbpSession _session;

        public ProductsAppService(IRepository<Product, int> repository, IAbpSession session) : base(repository)
        {
            _repository = repository;
            _session = session;
        }

        public override async Task<PagedResultDto<ProductDto>> GetAllAsync(PagedAndSortedResultRequestDto input)
        {
            if (_session.TenantId == 2)
            {
                using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MustHaveTenant))
                {
                    CheckGetAllPermission();
                    var query = _repository.GetAll();
                    var totalCount = await query.CountAsync();
                    query = ApplySorting(query, input);
                    query = ApplyPaging(query, input);
                    var entities = await AsyncQueryableExecuter.ToListAsync(query);
                    var items = entities.Select(MapToEntityDto).ToList();
                    return new PagedResultDto<ProductDto>(totalCount, items);
                }
            }
            return await base.GetAllAsync(input);
        }
    }
}
