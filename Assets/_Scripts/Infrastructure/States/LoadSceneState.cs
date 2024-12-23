using _Scripts.GameLogic;
using _Scripts.Infrastructure.Factory;
using CodeBase.Infrastructure;
using UnityEngine;

namespace _Scripts.Infrastructure.States
{
    public class LoadSceneState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;

        public LoadSceneState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(sceneName, OnLoadComplete);
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }

        private void OnLoadComplete()
        {
            _gameFactory.CreateRectangle(new Vector3(0, 0, 0), null);
            Debug.Log("Создание бяк");
            _gameStateMachine.Enter<GameLoopState>();
        }
    }
}