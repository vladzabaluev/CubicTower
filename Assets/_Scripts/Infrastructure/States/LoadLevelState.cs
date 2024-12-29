using System.Linq;
using _Scripts.GameLogic;
using _Scripts.GameLogic.DragAndDrop;
using _Scripts.Infrastructure.Factory;
using _Scripts.Infrastructure.Services.PersistantProgress;
using _Scripts.Infrastructure.Services.StaticData;
using CodeBase.Infrastructure;
using UnityEngine;

namespace _Scripts.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistantProgressService _progressService;
        private readonly IStaticDataService _staticDataService;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IGameFactory gameFactory, IPersistantProgressService progressService, IStaticDataService staticDataService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _staticDataService = staticDataService;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _gameFactory.CleanUp();
            _sceneLoader.Load(sceneName, OnLoadComplete);
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }

        private void OnLoadComplete()
        {
            InitGameWorld();
            InformProgressReaders();
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (var progressReader in _gameFactory.ProgressReaders)
                progressReader.LoadProgress(_progressService.Progress);

            Debug.Log("Informing progress readers");
        }

        private void InitGameWorld()
        {
            _staticDataService.LoadButtons();
            GameObject buttonContainer = GameObject.FindGameObjectWithTag("ButtonContainer");

            var buttons = _gameFactory.CreateRectangleButtons(buttonContainer.transform);
            GameObject rectangleCreator = GameObject.FindGameObjectWithTag("RectangleCreator");

            rectangleCreator.GetComponent<RectangleCreator>()
                ?.Construct(buttons.Select(x => x.GetComponent<RectangleButton>()).ToList());

            //Тут буду создавать всю хуйню - кнопки и башню
        }
    }
}