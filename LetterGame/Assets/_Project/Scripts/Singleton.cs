
using UnityEngine;

namespace LetterQuest
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (ReferenceEquals(_instance, null) == false) return _instance;

                _instance = FindObjectOfType<T>();
                if (ReferenceEquals(_instance, null) == false) return _instance;

                var instance = new GameObject(typeof(T).Name);
                _instance = instance.AddComponent<T>();
                DontDestroyOnLoad(instance);
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (ReferenceEquals(_instance, null))
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else if (ReferenceEquals(_instance, null) == false)
            {
                Destroy(gameObject);
            }
        }
    }
}
