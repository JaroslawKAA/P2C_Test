using System;
using UnityEngine;

namespace Core.Patterns
{
    public interface IPoolObject
    {
        Guid GUID { get; }
        void OnInstantiated();
        void OnGetFromPool();
        void OnReleaseToPool();
        void SetParent(Transform parent);
        void SetTransform(Vector3 position, Quaternion rotation);
    }
}
