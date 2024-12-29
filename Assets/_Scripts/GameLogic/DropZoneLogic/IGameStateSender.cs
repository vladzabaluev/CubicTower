using _Scripts.Infrastructure.Reactive;

namespace _Scripts.GameLogic.DropZoneLogic
{
    public interface IGameStateSender
    {
        ReactiveProperty<string> OnGameStateChange { get; } 
    }
}