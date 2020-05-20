using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using LeesStore.Authorization.ApiKeys;
using LeesStore.Authorization.Roles;
using LeesStore.Authorization.Users;
using LeesStore.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LeesStore.Authorization
{
    public interface IApiKeyAuthenticationService
    {
        Task<ApiKeyAuthenticationResult> Authenticate(string apiKey, string secret);
    }

    public class ApiKeyAuthenticationService : DomainService, IApiKeyAuthenticationService
    {
        private readonly IRepository<ApiKey, long> _apiKeysRepository;
        private readonly TenantManager _tenantManager;
        private readonly AbpLogInManager<Tenant, Role, User> _loginManager;

        public ApiKeyAuthenticationService(IRepository<ApiKey, long> apiKeysRepository, TenantManager tenantManager, AbpLogInManager<Tenant, Role, User> loginManager)
        {
            _apiKeysRepository = apiKeysRepository;
            _tenantManager = tenantManager;
            _loginManager = loginManager;
        }

        public async Task<ApiKeyAuthenticationResult> Authenticate(string apiKey, string secret)
        {
            var user = await GetApiKeyByApiKeyId(apiKey);
            var noMatchingApiKeyFound = user == null;
            if (noMatchingApiKeyFound)
            {
                Logger.Info($"Attempted login with API Key: '{apiKey}', but no matching API key exists");
                return new ApiKeyAuthenticationResult(false);
            }

            var apiKeyExistsButHasNoTenant = !user.TenantId.HasValue;
            if (apiKeyExistsButHasNoTenant)
            {
                Logger.Error($"Api Key '{apiKey}' has a null TenantId and someone tried to log in as it");
                return new ApiKeyAuthenticationResult(false);
            }

            var tenant = await _tenantManager.GetByIdAsync(user.TenantId.Value);
            var loginResult = await _loginManager.LoginAsync(apiKey, secret, tenant.TenancyName, shouldLockout: true);
            if (loginResult.Result == AbpLoginResultType.Success)
            {
                Logger.Info($"Successful login with Api Key '{apiKey}'");
                return new ApiKeyAuthenticationResult(true, user.TenantId.Value);
            }
            Logger.Warn($"Failed login attempt with API Key '{apiKey}'.  Result: " + loginResult.Result);
            return new ApiKeyAuthenticationResult(false);
        }

        private async Task<ApiKey> GetApiKeyByApiKeyId(string apiKeyId)
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var user = await _apiKeysRepository.GetAll().SingleOrDefaultAsync(i => i.UserName == apiKeyId);
                return user;
            }
        }
    }
}
