using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace LeesStore.Authorization
{
    public class LeesStoreAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Products, L("Products"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Pages_ApiKeys, L("ApiKeys"), multiTenancySides: MultiTenancySides.Tenant);
            context.CreatePermission(PermissionNames.Api, L("Api"), multiTenancySides: MultiTenancySides.Tenant);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, LeesStoreConsts.LocalizationSourceName);
        }
    }
}
