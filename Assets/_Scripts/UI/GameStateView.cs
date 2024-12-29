using System;
using _Scripts.GameLogic.DropZoneLogic;
using _Scripts.Infrastructure.Factory;
using _Scripts.Infrastructure.Services;
using UnityEngine;
using TMPro;

namespace _Scripts.UI
{
    public class GameStateView : MonoBehaviour
    {
        [SerializeField] private TMP_Text titleText;
        private IGameFactory _gameFactory;

        private void Awake()
        {
            _gameFactory = (IGameFactory) AllServices.Container.Single<IGameFactory>();
        }

        private void Start()
        {
            foreach (IGameStateSender gameStateSender in _gameFactory.GameStateChangers)
            {
                gameStateSender.OnGameStateChange.OnValueChanged += SetTitle;
            }
        }

        public void SetTitle(string title)
        {
            titleText.text = title;
        }
    }
}