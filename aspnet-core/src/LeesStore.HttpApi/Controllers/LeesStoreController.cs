using LeesStore.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace LeesStore.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class LeesStoreController : AbpController
    {
        protected LeesStoreController()
        {
            LocalizationResource = typeof(LeesStoreResource);
        }
    }
}