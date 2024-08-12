using System;
using System.Collections.Generic;

namespace Core.GameEvents
{
    public static class EventManager
    {
        static Dictionary<Type, List<EventListener>> eventListeners = new();

        public static void RegisterListener<T>(EventListener listener) where T : EventBase
        {
            Type eventType = typeof(T);
            if (!eventListeners.ContainsKey(eventType))
            {
                eventListeners[eventType] = new List<EventListener>();
            }
            eventListeners[eventType].Add(listener);
        }

        public static void UnregisterListener<T>(EventListener listener) where T : EventBase
        {
            Type eventType = typeof(T);
            if (eventListeners.ContainsKey(eventType))
            {
                eventListeners[eventType].Remove(listener);
            }
        }

        public static void TriggerEvent<T>(T eventInstance) where T : EventBase
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