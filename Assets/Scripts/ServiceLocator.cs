using System;
using System.Collections.Generic;
using UnityEngine;

public static class ServiceLocator
{
    private static Dictionary<Type, object> services = new Dictionary<Type, object>();

    public static void Register<T>(T service) where T : class
    {
        services[typeof(T)] = service;
    }

    public static T Get<T>() where T : class
    {
        if (services.TryGetValue(typeof(T), out object service))
        {
            return service as T;
        }

        if (typeof(T) == typeof(IAudioSystem))
        {
            var audioService = new AudioService();
            Register<IAudioSystem>(audioService);
            return audioService as T;
        }

        throw new Exception($"Service {typeof(T).Name} not found");
    }

    public static void Unregister<T>() where T : class
    {
        services.Remove(typeof(T));
    }
}