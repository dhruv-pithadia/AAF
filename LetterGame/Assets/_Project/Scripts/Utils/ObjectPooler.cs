
using UnityEngine;

namespace LetterQuest.Utils
{
    public abstract class ObjectPooler<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private T prefab;
        [SerializeField] protected int maxSize;
        [SerializeField] protected int initialSize;
        [SerializeField] protected bool collectionCheck;
        [SerializeField] private Transform objParent;
        protected UnityEngine.Pool.ObjectPool<T> ObjectPool;

        protected void Initialize()
        {
            Debug.Log("[ObjectPooler]: Initialize");
            ObjectPool = new UnityEngine.Pool.ObjectPool<T>(Create, OnGetCallback, OnReleaseCallback,
                OnDestroyCallback, collectionCheck, initialSize, maxSize);
        }

        private T Create() => Instantiate(prefab, Vector3.zero, Quaternion.identity, objParent);
        private static void OnGetCallback(T obj) => obj.gameObject.SetActive(true);
        private static void OnReleaseCallback(T obj) => obj.gameObject.SetActive(false);

        private static void OnDestroyCallback(T obj)
        {
            if (ReferenceEquals(obj, null)) return;
            Debug.Log("[ObjectPooler]: On Destroy Callback");
            Destroy(obj.gameObject);
        }

        protected void Dispose()
        {
            if (ReferenceEquals(ObjectPool, null)) return;
            Debug.Log("[ObjectPooler]: Dispose");
            ObjectPool.Dispose();
            ObjectPool = null;
        }
    }
}
