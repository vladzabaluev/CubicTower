using System;
using System.Collections.Generic;
using _Scripts.GameLogic;
using _Scripts.Infrastructure.Factory;
using _Scripts.Infrastructure.Services;
using _Scripts.Infrastructure.Services.Localization;
using _Scripts.Infrastructure.Services.PersistantProgress;
using _Scripts.Infrastructure.Services.SaveLoad;
using _Scripts.Infrastructure.Services.StaticData;
using CodeBase.Infrastructure;

namespace _Scripts.Infrastructure.States
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;
        private readonly AllServices _allServices;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, AllServices allServices)
        {
            _allServices = allServices;

            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, _allServices),
                [typeof(LoadProgressState)] =
                    new LoadProgressState(this,
                        (IPersistantProgressService) _allServices.Single<IPersistantProgressService>(),
                        (ISaveLoadService) _allServices.Single<ISaveLoadService>(), sceneLoader, _allServices),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingCurtain,
                    (IGameFactory) _allServices.Single<IGameFactory>(),
                    (IPersistantProgressService) _allServices.Single<IPersistantProgressService>(),
                    (IStaticDataService) _allServices.Single<IStaticDataService>(),
                    (ILocalizationService) _allServices.Single<ILocalizationService>()),
                [typeof(GameLoopState)] = new GameLoopState(this), };
        }

        public void Enter<TState>() where TState : class, IState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            var state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}