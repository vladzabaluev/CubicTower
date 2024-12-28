using System;
using _Scripts.Infrastructure.AssetManager;
using _Scripts.Infrastructure.Factory;
using _Scripts.Infrastructure.Services;
using _Scripts.Infrastructure.Services.PersistantProgress;
using _Scripts.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure;

namespace _Scripts.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";

        private readonly GameStateMachine _gameStateMachine;
        private SceneLoader _sceneLoader;
        private readonly AllServices _allServices;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices allServices)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _allServices = allServices;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(Initial, EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            _gameStateMachine.Enter<LoadProgressState>();
        }

        private void RegisterServices()
        {
            _allServices.RegisterSingle<IAssetProvider>(new AssetProvider());
            _allServices.RegisterSingle<IPersistantProgressService>(new PersistantProgressService());

            _allServices.RegisterSingle<IGameFactory>(
                new GameFactory((IAssetProvider) _allServices.Single<IAssetProvider>()));

            _allServices.RegisterSingle<ISaveLoadService>(new SaveLoadService(
                (PersistantProgressService) _allServices.Single<IPersistantProgressService>(),
                (GameFactory) _allServices.Single<IGameFactory>()));
        }
    }
}