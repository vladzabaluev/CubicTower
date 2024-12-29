using System;
using _Scripts.Infrastructure.Services;
using _Scripts.Infrastructure.Services.Localization;

namespace _Scripts.Localization
{
    [Serializable]
    public class LocalizationVariant
    {
        public string RussianVersion;

        public string EnglishVersion;
        private readonly ILocalizationService _localizationService;

        public LocalizationVariant(string russianVersion, string englishVersion)
        {
            RussianVersion = russianVersion;
            EnglishVersion = englishVersion;
            _localizationService = (ILocalizationService) AllServices.Container.Single<ILocalizationService>();
        }

        public string GetCurrent()
        {
            return _localizationService.CurrentLanguage.Value switch
            {
                Language.Russian => RussianVersion,
                Language.English => EnglishVersion,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}