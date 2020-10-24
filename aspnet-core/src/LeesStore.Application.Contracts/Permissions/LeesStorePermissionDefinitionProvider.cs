using LeesStore.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace LeesStore.Permissions
{
    public class LeesStorePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(LeesStorePermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(LeesStorePermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<LeesStoreResource>(name);
        }
    }
}
