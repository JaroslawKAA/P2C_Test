using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.GameEvents
{
    public class EventManager : MonoBehaviour
    {
        static EventManager instance;
        public static EventManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<EventManager>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject("EventManager");
                        instance = obj.AddComponent<EventManager>();
                    }
                }
                return instance;
            }
        }
    
        Dictionary<Type, List<EventListener>> eventListeners = new();

        public void RegisterListener<T>(EventListener listener) where T : EventBase
        {
            Type eventType = typeof(T);
            if (!eventListeners.ContainsKey(eventType))
            {
                eventListeners[eventType] = new List<EventListener>();
            }
            eventListeners[eventType].Add(listener);
        }

        public void UnregisterListener<T>(EventListener listener) where T : EventBase
        {
            Type eventType = typeof(T);
            if (eventListeners.ContainsKey(eventType))
            {
                eventListeners[eventType].Remove(listener);
            }
        }

        public void TriggerEvent<T>(T eventInstance) where T : EventBase
        {
            Type eventType = typeof(T);
            if (eventListeners.ContainsKey(eventType))
            {
                foreach (EventListener listener in eventListeners[eventType])
                {
                    listener.OnEvent(eventInstance);
                }
            }
        }
    }
}