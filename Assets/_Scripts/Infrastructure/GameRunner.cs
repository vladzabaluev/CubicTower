using System;
using UnityEngine;

namespace _Scripts.Infrastructure
{
    public class GameRunner: MonoBehaviour
    {
        [SerializeField] private GameBootstrapper _gameBootstrapper;
        private void Awake()
        {
            var bootstrapper = FindObjectOfType<GameBootstrapper>();

            if (bootstrapper == null)
            {
                Instantiate(_gameBootstrapper);
            }
        }
    }
}