using GameUnity.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;

namespace GameUnity.Infrastructure.Services
{
    public class ServiceLocator : IServiceLocator
    {
        private readonly Dictionary<Type, object> services = new();

        public void Register<T>(T service) where T : class
        {
            services[typeof(T)] = service;
        }

        public T Get<T>() where T : class
        {
            return services.TryGetValue(typeof(T), out var service)
                ? (T)service
                : throw new Exception($"Service {typeof(T).Name} not registered!");
        }
    }
}