namespace Syndicate.Core.Localization
{
    public interface ILocalizationService
    {
        void Reload();

        string GetLanguageValue(string key);
    }
}