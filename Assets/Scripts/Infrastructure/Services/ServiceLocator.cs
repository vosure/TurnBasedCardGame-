using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Services
{
    public class ServiceLocator
    {
        public static ServiceLocator Container;

        private readonly Dictionary<string, IService> _services = new();

        public static void CreateContainer() => 
            Container = new ServiceLocator();

        public T Get<T>() where T : IService
        {
            string key = typeof(T).Name;
            if (!_services.ContainsKey(key))
            {
                Debug.LogError($"{key} not registered with {GetType().Name}");
                throw new InvalidOperationException();
            }

            return (T)_services[key];
        }
        
        public void Register<T>(T service) where T : IService
        {
            string key = typeof(T).Name;
            if (_services.ContainsKey(key))
                return;

            _services.Add(key, service);
        }
        
        public void Unregister<T>() where T : IService
        {
            string key = typeof(T).Name;
            if (!_services.ContainsKey(key))
            {
                Debug.LogError($"Attempted to unregister service of type {key} which is not registered with the {GetType().Name}.");
                return;
            }

            _services.Remove(key);
        }
    }
}
