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
using System.Threading.Tasks;

namespace LeesStore.ApiKeys
{
    [AbpAuthorize(PermissionNames.Pages_ApiKeys)]
    public class ApiKeysAppService : AsyncCrudAppService<ApiKey, ApiKeyDto, long, PagedAndSortedResultRequestDto, CreateApiKeyDto>
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly UserManager _userManager;
        private readonly IRoleService _roleService;

        public ApiKeysAppService(IRepository<ApiKey, long> repository,
            IPasswordHasher<User> passwordHasher,
            UserManager userManager,
            IRoleService roleService
            ) : base(repository)
        {
            _passwordHasher = passwordHasher;
            _userManager = userManager;
            _roleService = roleService;
        }

        public override async Task<ApiKeyDto> CreateAsync(CreateApiKeyDto input)
        {
            var fakeUniqueEmail = input.ApiKey + "@noreply.com";

            var apiKey = new ApiKey
            {
                UserName = input.ApiKey,
                EmailAddress = fakeUniqueEmail,
                Name = "API Key",
                Surname = "API Key",
                IsEmailConfirmed = true,
                NormalizedEmailAddress = fakeUniqueEmail
            };

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
