using _Scripts.StaticData;

namespace _Scripts.Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        ButtonsConfiguration Buttons { get; }
        void LoadButtons();
    }
}