namespace Bitdiff.Utils.Config
{
    public class ConfiguredFilePathService : IConfiguredFilePathService
    {
        private readonly IConfigurationSettingService _configurationSettingService;
        private readonly IFilePathResolver _filePathResolver;

        public ConfiguredFilePathService(IConfigurationSettingService configurationSettingService,
                                         IFilePathResolver filePathResolver)
        {
            _configurationSettingService = configurationSettingService;
            _filePathResolver = filePathResolver;
        }

        public string GetResolvedFilePathFromConfiguration(string configurationSettingName)
        {
            return
                _filePathResolver.GetPath(_configurationSettingService.GetConfigurationSetting(configurationSettingName));
        }
    }
}