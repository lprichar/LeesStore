using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LeesStore.Authorization.Roles
{
    public static class RoleNames
    {
        public const string Api = "API";
    }

    public interface IRoleService
    {
        /// <summary>
        /// ApiKeys should have the API permission.  Users/ApiKeys must get permissions by association with a Role.
        /// This code finds and returns a role called API or if it doesn't exist it creates and returns a role
        /// called API that has the API permission.  This code is called when an API Role is created.  Thus, the API
        /// role is created the 1st time a user creates an API Key for a tenant.
        /// </summary>
        Task<Role> EnsureApiRole();
    }

    public class RoleService : DomainService, IRoleService
    {
        private readonly RoleManager _roleManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly IPermissionManager _permissionManager;

        public RoleService(RoleManager roleManager, IRepository<Role> roleRepository, IPermissionManager permissionManager)
        {
            _roleManager = roleManager;
            _roleRepository = roleRepository;
            _permissionManager = permissionManager;
        }

        /// <summary>
        /// ApiKeys should have the API permission.  Users/ApiKeys must get permissions by association with a Role.
        /// This code finds and returns a role called API or if it doesn't exist it creates and returns a role
        /// called API that has the API permission.  This code is called when an API Role is created.  Thus, the API
        /// role is created the 1st time a user creates an API Key for a tenant.
        /// </summary>
        public async Task<Role> EnsureApiRole()
        {
            var apiRole = await _roleRepository.GetAll().FirstOrDefaultAsync(i => i.Name == RoleNames.Api);
            if (apiRole != null) return apiRole;

            var permissions = _permissionManager.GetAllPermissions().Where(i => i.Name == PermissionNames.Api);
            apiRole = new Role
            {
                TenantId = CurrentUnitOfWork.GetTenantId(),
                Name = RoleNames.Api,
                DisplayName = "API"
            };
            await _roleManager.CreateAsync(apiRole);
            await _roleManager.SetGrantedPermissionsAsync(apiRole, permissions);
            return apiRole;
        }
    }
}
