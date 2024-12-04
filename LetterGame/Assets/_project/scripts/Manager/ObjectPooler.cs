
using UnityEngine;

namespace LetterQuest
{
    public abstract class ObjectPooler<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private T prefab;
        [SerializeField] protected int maxSize;
        [SerializeField] protected int initialSize;
        [SerializeField] protected bool collectionCheck;
        protected UnityEngine.Pool.ObjectPool<T> ObjectPool;


        protected void Initialize()
        {
            ObjectPool = new UnityEngine.Pool.ObjectPool<T>(Create, OnGetCallback, OnReleaseCallback,
                OnDestroyCallback, collectionCheck, initialSize, maxSize);
        }

        private T Create()
        {
            return Instantiate(prefab, Vector3.zero, Quaternion.identity);
        }

        protected virtual void OnGetCallback(T obj)
        {
            obj.gameObject.SetActive(true);
        }

        protected virtual void OnReleaseCallback(T obj)
        {
            obj.gameObject.SetActive(false);
        }

        private static void OnDestroyCallback(T obj)
        {
            if (ReferenceEquals(obj, null)) return;
            Destroy(obj.gameObject);
        }

        protected void Dispose()
        {
            if (ReferenceEquals(ObjectPool, null)) return;
            ObjectPool.Dispose();
            ObjectPool = null;
        }
    }
}
