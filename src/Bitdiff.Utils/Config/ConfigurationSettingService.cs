using System.Configuration;

namespace Bitdiff.Utils.Config
{
    public class ConfigurationSettingService : IConfigurationSettingService
    {
        public string GetConfigurationSetting(string name)
        {
            return ConfigurationManager.AppSettings[name];
        }
    }
}