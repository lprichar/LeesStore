using System.Threading.Tasks;
using LeesStore.Configuration.Dto;

namespace LeesStore.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
