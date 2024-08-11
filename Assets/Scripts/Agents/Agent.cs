using System;
using UnityEngine;

public class Agent : MonoBehaviour
{
    Transform cachedTransform;
    
    public Guid GUID { get; private set; }

    void Awake()
    {
        cachedTransform = transform;
    }

    void OnDestroy()
    {
        cachedTransform = null;
    }

    public void OnInstantiated()
    {
        GUID = Guid.NewGuid();
    }

    public void SetTransform(Vector3 position, Quaternion rotation, Transform parent = null)
    {
        cachedTransform.position = position;
        cachedTransform.rotation = rotation;
        cachedTransform.parent = null;
    }
}
