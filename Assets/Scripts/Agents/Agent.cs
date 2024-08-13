using System;
using Core.Patterns;
using UnityEngine;

namespace Agents
{
    public class Agent : MonoBehaviour, IPoolObject
    {
        // PRIVATE
        Transform cachedTransform;
        GameObject cachedGameObject;
    
        // PROPERTIES
        public Guid GUID { get; private set; }

        // UNITY EVENTS
        void Awake()
        {
            cachedTransform = transform;
            cachedGameObject = gameObject;
        }

        void OnDestroy()
        {
            cachedTransform = null;
            cachedGameObject = null;
        }

        // METHODS
        public void OnInstantiated()
        {
            GUID = Guid.NewGuid();
            cachedGameObject.SetActive(false);
        }

        public void OnGetFromPool()
        {
            cachedGameObject.SetActive(true);
        }

        public void OnReleaseToPool()
        {
            cachedGameObject.SetActive(false);
        }

        public void SetParent(Transform parent)
        {
            cachedTransform.SetParent(parent, worldPositionStays: true);
        }

        public void SetTransform(Vector3 position, Quaternion rotation)
        {
            cachedTransform.position = position;
            cachedTransform.rotation = rotation;
        }
    }
}
