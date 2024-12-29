using System.Collections.Generic;
using _Scripts.Infrastructure.Reactive;
using _Scripts.Localization;

namespace _Scripts.GameLogic.DropZoneLogic
{
    public interface IGameStateSender
    {
        public Dictionary<string, LocalizationVariant> Localization { get; }
        ReactiveProperty<string> OnGameStateChange { get; }
    }
}