using System.Collections.Generic;
using _Scripts.Infrastructure.Services;
using _Scripts.Infrastructure.Services.PersistantProgress;
using UnityEngine;

namespace _Scripts.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateRectangle(Vector3 at, Transform parent);
        GameObject CreateRectangleButton(Transform buttonContainer);
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        void RegisterProgressWatchers(GameObject registeredWatcher);
        void Register(ISavedProgressReader progressReader);
        void CleanUp();
    }
}