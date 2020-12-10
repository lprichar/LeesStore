using LeesStore.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace LeesStore.Permissions
{
    public class LeesStorePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(LeesStorePermissions.GroupName);

            myGroup.AddPermission(
                LeesStorePermissions.ViewEditProducts,
                L("Permission:ViewEditProducts"),
                MultiTenancySides.Both);
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<LeesStoreResource>(name);
        }
    }
}
