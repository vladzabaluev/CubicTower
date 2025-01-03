using System.Collections.Generic;
using _Scripts.GameLogic.DropZoneLogic;
using _Scripts.GameLogic.Rectangle;
using _Scripts.Infrastructure.AssetManager;
using _Scripts.Infrastructure.Services.PersistantProgress;
using _Scripts.Infrastructure.Services.StaticData;
using _Scripts.StaticData;
using UnityEngine;

namespace _Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        private const string RectanglePath = "Prefabs/Rectangle";
        private const string RectangleButtonPath = "Prefabs/RectangleButton";
        public List<ISavedProgressReader> ProgressReaders { get; } = new();

        public List<ISavedProgress> ProgressWriters { get; } = new();
        public List<IGameStateSender> GameStateChangers { get; } = new();

        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticDataService)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
        }

        public GameObject CreateRectangle(Vector3 at, Transform parent)
        {
            GameObject rectangle = _assetProvider.InstantiateAt(RectanglePath, at);

            Vector3 originalScale = rectangle.transform.localScale;
            rectangle.transform.SetParent(parent);
            rectangle.transform.localScale = originalScale;

            return rectangle;
        }

        public List<GameObject> CreateRectangleButtons(Transform parent)
        {
            List<GameObject> rectangleButtons = new();
            ButtonsConfiguration buttonsConfiguration = _staticDataService.Buttons;

            for (int i = 0; i < buttonsConfiguration.Buttons.Count; i++)
            {
                GameObject rectangleButton = Object.Instantiate(buttonsConfiguration.ButtonPrefab, parent);

                rectangleButton.GetComponent<RectangleView>().SetColor(buttonsConfiguration.Buttons[i].Color);
                rectangleButtons.Add(rectangleButton);
            }

            return rectangleButtons;
        }

        public void RegisterProgressWatchers(GameObject registeredWatcher)
        {
            foreach (var progressReader in registeredWatcher.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }

        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
            {
                ProgressWriters.Add(progressWriter);
            }

            ProgressReaders.Add(progressReader);
        }

        public void RegisterGameChanger(IGameStateSender gameChanger)
        {
            GameStateChangers.Add(gameChanger);
        }

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }
    }
}