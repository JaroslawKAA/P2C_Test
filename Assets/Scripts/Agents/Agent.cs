using System;
using UnityEngine;

public class Agent : MonoBehaviour
{
    Transform cachedTransform;
    GameObject cachedGameObject;
    
    public Guid GUID { get; private set; }

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

    public void OnInstantiated()
    {
        GUID = Guid.NewGuid();
        cachedGameObject.SetActive(false);
    }

    public void OnGet()
    {
        cachedGameObject.SetActive(true);
    }

    public void OnRelease()
    {
        cachedGameObject.SetActive(false);
    }

    public void SetTransform(Vector3 position, Quaternion rotation, Transform parent = null)
    {
        cachedTransform.position = position;
        cachedTransform.rotation = rotation;
        if(parent) SetParent(parent);
    }

    public void SetParent(Transform parent)
    {
        cachedTransform.SetParent(parent, worldPositionStays: true);
    }
}
