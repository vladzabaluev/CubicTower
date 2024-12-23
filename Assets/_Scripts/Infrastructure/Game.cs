using _Scripts.GameLogic;
using _Scripts.Infrastructure.Services;
using _Scripts.Infrastructure.States;
using CodeBase.Infrastructure;

namespace _Scripts.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;
        private SceneLoader _sceneLoader;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain, AllServices.Container);
        }
    }
}