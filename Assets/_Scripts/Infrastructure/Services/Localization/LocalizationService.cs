using _Scripts.Infrastructure.Reactive;

namespace _Scripts.Infrastructure.Services.Localization
{
    public class LocalizationService : ILocalizationService
    {
        public ReactiveProperty<Language> CurrentLanguage { get; private set; } = new ReactiveProperty<Language>();

        public void ChangeLanguage(Language language)
        {
            if(CurrentLanguage.Value != language)
                CurrentLanguage.Value = language;
        }
    }

    public enum Language
    {
        Russian = 0,
        English = 1
    }
}