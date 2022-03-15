using UnityEngine;
namespace Picker
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }
        protected virtual void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            //DontDestroyOnLoad(gameObject);
            Instance = this as T;
        }
    }
}
