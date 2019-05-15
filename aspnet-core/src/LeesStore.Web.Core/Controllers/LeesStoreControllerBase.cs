using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace LeesStore.Controllers
{
    public abstract class LeesStoreControllerBase: AbpController
    {
        protected LeesStoreControllerBase()
        {
            LocalizationSourceName = LeesStoreConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
