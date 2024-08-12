using UnityEngine;

namespace Core.Patterns
{
    public class SimpleSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance;

        void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError($"Duplicated singleton - {gameObject.name} removed");
                Destroy(gameObject);
            }
            else
            {
                Instance = this as T;
            }
        }
    }
}
