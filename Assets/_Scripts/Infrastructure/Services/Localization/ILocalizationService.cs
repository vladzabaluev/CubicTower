using _Scripts.Infrastructure.Reactive;

namespace _Scripts.Infrastructure.Services.Localization
{
    public interface ILocalizationService : IService
    {
        ReactiveProperty<Language> CurrentLanguage { get; }
        void ChangeLanguage(Language language);
    }
}