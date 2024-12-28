using System.Collections.Generic;
using _Scripts.Infrastructure.AssetManager;
using _Scripts.Infrastructure.Services.PersistantProgress;
using UnityEngine;

namespace _Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private const string RectanglePath = "Prefabs/Rectangle";
        public List<ISavedProgressReader> ProgressReaders { get; } = new();

        public List<ISavedProgress> ProgressWriters { get; } = new();

        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public GameObject CreateRectangle(Vector3 at, Transform parent)
        {
            GameObject rectangle = _assetProvider.InstantiateAt(RectanglePath, at);
            rectangle.transform.SetParent(parent);

            return rectangle;
        }

        public GameObject CreateRectangleButton(GameObject prefab, Transform buttonContainer)
        {
            GameObject rectangleButton = _assetProvider.Instantiate(RectanglePath);
            rectangleButton.transform.SetParent(buttonContainer);
            return rectangleButton;
        }
        private void RegisterProgressWatchers(GameObject registeredWatcher)
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
                Debug.Log("Writers registered");

                ProgressWriters.Add(progressWriter);
            }

            Debug.Log("Watchers registered");

            ProgressReaders.Add(progressReader);
        }
        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }
    }
}