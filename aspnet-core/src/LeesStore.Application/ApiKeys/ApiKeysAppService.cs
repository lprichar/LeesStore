using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using LeesStore.ApiKeys.Dto;
using LeesStore.Authorization;
using LeesStore.Authorization.ApiKeys;
using LeesStore.Authorization.Roles;
using LeesStore.Authorization.Users;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace LeesStore.ApiKeys
{
    [AbpAuthorize(PermissionNames.Pages_ApiKeys)]
    public class ApiKeysAppService : AsyncCrudAppService<ApiKey, ApiKeyDto, long, PagedAndSortedResultRequestDto, CreateApiKeyDto>
    {
        private readonly IRepository<ApiKey, long> _repository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly UserManager _userManager;
        private readonly IRoleService _roleService;

        public ApiKeysAppService(IRepository<ApiKey, long> repository,
            IPasswordHasher<User> passwordHasher,
            UserManager userManager,
            IRoleService roleService
            ) : base(repository)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
            _userManager = userManager;
            _roleService = roleService;
        }

        protected override IQueryable<ApiKey> CreateFilteredQuery(PagedAndSortedResultRequestDto input)
        {
            return _repository.GetAll().Where(u => u.Discriminator == nameof(ApiKey));
        }

        public override async Task<ApiKeyDto> CreateAsync(CreateApiKeyDto input)
        {
            var apiKey = new ApiKey();
            apiKey.UserName = input.ApiKey;
            apiKey.EmailAddress = input.ApiKey + "@noreply.com";
            apiKey.NormalizedEmailAddress = apiKey.EmailAddress;
            apiKey.Name = "API Key";
            apiKey.Surname = "API Key";
            apiKey.IsEmailConfirmed = true;
            apiKey.Password = _passwordHasher.HashPassword(apiKey, input.Secret);
            await _userManager.CreateAsync(apiKey);

            var apiRole = await _roleService.EnsureApiRole();
            await _userManager.SetRolesAsync(apiKey, new[] { apiRole.Name });

            await CurrentUnitOfWork.SaveChangesAsync();

            return new ApiKeyDto
            {
                Id = apiKey.Id,
                ApiKey = apiKey.UserName
            };
        }

        public CreateApiKeyDto MakeApiKey()
        {
            return new CreateApiKeyDto
            {
                ApiKey = User.CreateRandomPassword(),
                Secret = User.CreateRandomPassword()
            };
        }
    }
}
