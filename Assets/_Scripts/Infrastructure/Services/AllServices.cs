using System;
using System.Collections.Generic;
using _Scripts.Infrastructure.AssetManager;

namespace _Scripts.Infrastructure.Services
{
    public class AllServices
    {
        public static AllServices Container { get; } = new();

        private static readonly Dictionary<Type, IService> _services = new Dictionary<Type, IService>();

        public void RegisterSingle<TService>(TService implementation) where TService : IService
        {
            _services.TryAdd(typeof(TService), implementation);
        }

        public IService Single<TService>() where TService : IService
        {
            return _services[typeof(TService)];
        }
    }
}