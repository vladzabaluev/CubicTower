using _Scripts.Infrastructure.Services;
using UnityEngine;

namespace _Scripts.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateRectangle(Vector3 at, Transform parent);
        GameObject CreateRectangleButton(GameObject prefab, Transform buttonContainer);
    }
}