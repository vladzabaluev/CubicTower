using System;
using _Scripts.GameLogic;
using _Scripts.Infrastructure.States;
using CodeBase.Infrastructure;
using UnityEngine;

namespace _Scripts.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private Game _game;
        private SceneLoader _sceneLoader;
        [SerializeField] private LoadingCurtain _loadingCurtainPrefab;

        private void Awake()
        {
            _game = new Game(this, Instantiate(_loadingCurtainPrefab));
            _game.StateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}