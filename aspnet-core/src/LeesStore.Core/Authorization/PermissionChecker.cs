using Abp.Authorization;
using LeesStore.Authorization.Roles;
using LeesStore.Authorization.Users;

namespace LeesStore.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
