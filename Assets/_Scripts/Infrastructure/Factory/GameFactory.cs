using _Scripts.Infrastructure.AssetManager;
using UnityEngine;

namespace _Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private const string RectanglePath = "Prefabs/Rectangle";

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
    }
}