using Volo.Abp.Settings;

namespace LeesStore.Settings
{
    public class LeesStoreSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(LeesStoreSettings.MySetting1));
        }
    }
}
