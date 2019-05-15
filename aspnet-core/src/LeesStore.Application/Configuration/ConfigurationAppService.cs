using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using LeesStore.Configuration.Dto;

namespace LeesStore.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : LeesStoreAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
