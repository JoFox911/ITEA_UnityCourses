using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class ServiceLocator
{
    private static Dictionary<Type, object> _services = new Dictionary<Type, object>();

    public static void Register<T>(object service)
    {
        if (_services.ContainsKey(typeof(T)))
        {
            Debug.LogError($"[ServiceLocator][{MethodBase.GetCurrentMethod().Name}] This type of service ({typeof(T).Name}) already registered");
        }
        else
        {
            _services[typeof(T)] = service;
        }
    }

    public static void Unregister<T>()
    {
        if (!_services.ContainsKey(typeof(T)))
        {
            Debug.LogError($"[ServiceLocator][{MethodBase.GetCurrentMethod().Name}] This type of service ({typeof(T).Name}) already registered");
        }
        else
        {
            _services.Remove(typeof(T));
        }

    }

    public static T Resolved<T>() where T : class
    {
        if (_services.ContainsKey(typeof(T)))
        {
            return (T)_services[typeof(T)];
        }
        else
        {
            Debug.LogError($"[ServiceLocator][{MethodBase.GetCurrentMethod().Name}] This type of service ({typeof(T).Name}) not registered");
        }

        return null;
    }
}
