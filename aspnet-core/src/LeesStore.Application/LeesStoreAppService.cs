using System;
using System.Collections.Generic;
using System.Text;
using LeesStore.Localization;
using Volo.Abp.Application.Services;

namespace LeesStore
{
    /* Inherit your application services from this class.
     */
    public abstract class LeesStoreAppService : ApplicationService
    {
        protected LeesStoreAppService()
        {
            LocalizationResource = typeof(LeesStoreResource);
        }
    }
}
