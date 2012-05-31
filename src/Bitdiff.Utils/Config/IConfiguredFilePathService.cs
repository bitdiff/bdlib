namespace Bitdiff.Utils.Config
{
    public interface IConfiguredFilePathService
    {
        string GetResolvedFilePathFromConfiguration(string configurationSettingName);
    }
}